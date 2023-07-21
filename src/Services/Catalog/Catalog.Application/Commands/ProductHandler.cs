using FluentValidation.Results;
using MediatR;
using Ifood.Core.Messages;
using Catalog.Application.Commands.AddProduct;
using Catalog.Domain.Models.Repositories;
using Catalog.Domain.Models.Entities;
using Catalog.Application.Events;

namespace Catalog.Application.Commands
{ 
    public class ProductHandler : CommandHandler<AddProductCommand,CommandHandlerOutput<AddProductCommandOutput>>
    {
        private readonly IProductRepository _productRepository;

        public ProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public override async Task<CommandHandlerOutput<AddProductCommandOutput>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {

            if (!request.IsValid()){

                return request.ConvertToCommandOutput();
            }

            var product = new Product(request.Name, request.Quantity, request.Price);
            _productRepository.Add(product);

            product.AddEvent(new ProductRegisteredEvent(product.Id, product.Name,  product.Quantity, product.Price));

            var resultPersistData =  await PersistData(_productRepository.UnitOfWork);
            request.AddValidationResult(resultPersistData);
            
            var commandHandlerOutput = request.ConvertToCommandOutput(product.Id);
            
            return commandHandlerOutput;
        }

    }
}