using FluentValidation.Results;
using MediatR;

namespace Ifood.Core.Messages
{

    public abstract class CommandBase : Message
    {
        public DateTime Timestamp { get; private set; }

        private  ValidationResult _validationResult;

        public void AddValidationResult(ValidationResult validationResult){
            this._validationResult = validationResult;
        }

        public ValidationResult GetValidationResult(){
            return _validationResult;
        }
        protected CommandBase()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }

    }

    public abstract class Command : CommandBase, ICommand
    {

    }

    public abstract class Command<TResult> : CommandBase, ICommand<TResult>
    {
        public abstract TResult ConvertToCommandOutput(Guid Id );
        public abstract TResult ConvertToCommandOutput();
    }
}