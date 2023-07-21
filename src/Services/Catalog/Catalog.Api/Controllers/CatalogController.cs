using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Catalog.Api.ViewModel;
using Catalog.Application.Commands;
using Catalog.Application.Commands.AddProduct;
using Catalog.Application.DTO;
using Catalog.Application.Queries;
using FluentValidation.Results;
using Ifood.Core.Mediator;
using Ifood.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CatalogController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        private readonly IProductQueries _productQueries;
        public CatalogController(IMediatorHandler mediatorHandler, IProductQueries productQueries)
        {
            _mediatorHandler = mediatorHandler;
            _productQueries = productQueries;
        }

        [HttpPost]
        [ProducesResponseType(
            typeof(AddProductCommandOutput),
            (int)HttpStatusCode.Created)]
        [ProducesResponseType(
            typeof(ValidationResult),
            (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddProductAsync(AddProductCommand product)
        {
            var command = new AddProductCommand(product.Name, product.Price, product.Quantity);
            var commandHandlerOutput = await _mediatorHandler.SendCommand<AddProductCommand,AddProductCommandOutput>(command);
            return CustomResponseStatusCodeCreated(commandHandlerOutput,$"Catalog/{commandHandlerOutput.Data?.Id}" );
        }

        [HttpGet("{productId}")]
        [ProducesResponseType(
            typeof(ProductDTO),
            (int)HttpStatusCode.OK)]
        [ProducesResponseType(
            typeof(ValidationResult),
            (int)HttpStatusCode.BadRequest)]
         [ProducesResponseType(
            typeof(ValidationResult),
            (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetByProductId(Guid productId)
        {
            {
                var product = await _productQueries.GetByProductId(productId);

                return product == null ? NotFound() : CustomResponse(product);
            }
        }
    }
}