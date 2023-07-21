
using System.ComponentModel.DataAnnotations;
using Ifood.Core.Messages;
using System.Linq;

namespace Catalog.Application.Commands.AddProduct
{
    public class AddProductCommand : Command<CommandHandlerOutput<AddProductCommandOutput>>
    {
        public AddProductCommand(string name, decimal price, int quantity)
        {
            this.Name = name;
            this.Price = price;
            this.Quantity = quantity;

        }
        [Required]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public AddProductCommand()
        {

        }

        public override bool IsValid()
        {
            var context = new ValidationContext(this, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(this, context, results, true);
            if (!isValid)
            {
                var validationResult = new FluentValidation.Results.ValidationResult
                {
                    Errors = results
                                .ConvertAll(x => new FluentValidation.Results.ValidationFailure( string.Empty,  x.ErrorMessage))
                };
            }
            return isValid;
        }

        public override CommandHandlerOutput<AddProductCommandOutput> ConvertToCommandOutput()
        {
            return ConvertToCommandOutput(Guid.Empty);
        }

        public override CommandHandlerOutput<AddProductCommandOutput> ConvertToCommandOutput(Guid id)
        {
            if (this.GetValidationResult().IsValid)
            {
                return new CommandHandlerOutput<AddProductCommandOutput>
                {
                    ValidationResult = this.GetValidationResult(),
                    Data = new AddProductCommandOutput
                    {
                        Id = id ,
                        Price = this.Price,
                        Quantity = this.Quantity,
                        Name = this.Name,
                    }
                };
            }
            else
            {
                return new CommandHandlerOutput<AddProductCommandOutput>
                {
                    ValidationResult = this.GetValidationResult(),
                };
            }
        }
    }
}
