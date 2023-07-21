using System.Threading.Tasks;
using FluentValidation.Results;
using Ifood.Core.Data;
using MediatR;

namespace Ifood.Core.Messages
{
    public abstract class CommandHandler<TCommand, TResult> :
        IRequestHandler<TCommand, TResult> where TCommand : ICommand<TResult>
    {
        protected ValidationResult ValidationResult;

        protected CommandHandler()
        {
            ValidationResult = new ValidationResult();
        }

        public abstract Task<TResult> Handle(TCommand request, CancellationToken cancellationToken);

        protected void AddError(string mensagem)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, mensagem));
        }

        protected async Task<ValidationResult> PersistData(IUnitOfWork uow)
        {
            if (!await uow.Commit()) AddError("Houve um erro ao persistir os dados");

            return ValidationResult;
        }
    }
}