using FluentValidation.Results;
using Ifood.Core.DomainObjects;
using Ifood.Core.Messages;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ifood.Core.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<CommandHandlerOutput<R>> SendCommand<T, R>(T comando)
                   where T : Command<CommandHandlerOutput<R>>
                   where R : class
        {
            return await _mediator.Send(comando);
        }

        public async Task PublishEvent<T>(T e) where T : Event
        {
            await _mediator.Publish(e);
        }

        public async Task PublishEventDbContext(DbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.Notificacoes != null && x.Entity.Notificacoes.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.Notificacoes)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearEvent());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await PublishEvent(domainEvent);
                });
            await Task.WhenAll(tasks);
        }
    }
}