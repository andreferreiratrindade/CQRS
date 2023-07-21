using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Text;
using System.Threading.Tasks;
using EasyNetQ;
using Ifood.Core.Messages.Integration;
using Polly;
using RabbitMQ.Client.Exceptions;
using Ifood.Message.Bus;
using Ifood.Message.Bus.ExangesAttributes;
using EasyNetQ.DI;

namespace Ifood.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private IBus _bus;
        private IAdvancedBus _advancedBus;
        const string BROKER_NAME = "eshop_event_bus";


        private const string INTEGRATION_EVENT_SUFFIX = "IntegrationEvent";


        private readonly string _connectionString;

        public MessageBus(string connectionString)
        {
            _connectionString = connectionString;
            TryConnect();
        }


        public bool IsConnected(){
            var isConnected = false;
            if(_bus?.Advanced != null)
            {
                isConnected = _bus.Advanced.IsConnected;
            }

            return isConnected;
        }

        public IAdvancedBus AdvancedBus => _bus?.Advanced;

        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();

            _bus.PubSub.Publish(message);
        }

        public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
        {
            TryConnect();
            var eventName = @event.GetType().Name;
            // _logger.LogTrace("Creating RabbitMQ channel to publish event: {EventId} ({EventName})", @event.Id, eventName);

            var jsonMessage = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(jsonMessage);
            await _bus.PubSub.PublishAsync(@event);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            TryConnect();
            _bus.PubSub.Subscribe(subscriptionId, onMessage);
        }

        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            _bus.PubSub.SubscribeAsync(subscriptionId, onMessage);
        }

        public TResponse Request<TRequest, TResponse>(TRequest request) where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            // return _bus.Advanced.Request<TRequest, TResponse>(request);
            return null;
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            TryConnect();
            // return await _bus.RequestAsync<TRequest, TResponse>(request);
            return null;

        }

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            TryConnect();
            // return _bus.Respond(responder);
            return null;

        }

        public IDisposable RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent where TResponse : ResponseMessage
        {
            TryConnect();
            // return _bus.RespondAsync(responder);
            return null;

        }

        private void TryConnect()
        {
            if( IsConnected()) return;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(3, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() =>
            {
                _bus = RabbitHutch.CreateBus(_connectionString,x => x.Register<IConventions, AttributeBasedConventions>());
                // _bus = RabbitHutch.CreateBus(_connectionString);
                _advancedBus = _bus.Advanced;
                _advancedBus.Disconnected += OnDisconnect;
            });
        }

        private void OnDisconnect(object s, EventArgs e)
        {
            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();

            policy.Execute(TryConnect);
        }

        public void Dispose()
        {
            _bus.Dispose();
        }
    }
}