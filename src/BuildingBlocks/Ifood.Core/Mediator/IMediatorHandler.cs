using System.Threading.Tasks;
using FluentValidation.Results;
using Ifood.Core.Messages;
using Microsoft.EntityFrameworkCore;

namespace Ifood.Core.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T e) where T : Event;
        Task PublishEventDbContext(DbContext dbcontext);
        Task<CommandHandlerOutput<R>> SendCommand<T,R>(T comando)
            where T : Command<CommandHandlerOutput<R>>
            where R : class;
    }
}