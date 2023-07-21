using System.Net;
using FluentValidation.Results;
using Ifood.Core.Communication;
using Ifood.Core.Messages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Ifood.WebApi.Core.Controllers
{
    [ApiController]
    public abstract class MainController : Controller
    {
        protected ICollection<string> Erros = new List<string>();


        protected ActionResult CustomResponseStatusCodeCreated<T> (CommandHandlerOutput<T> commandHandlerOutput, string urlRedirect) where T : class
        {

            foreach (var erro in commandHandlerOutput.ValidationResult.Errors)
            {
                AddErrorProcessing(erro.ErrorMessage);
            }

            if(IsValidOperation()){
                var obj = new {data = commandHandlerOutput.Data, Link =urlRedirect};
                return Created(urlRedirect, obj);

            }
            
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponseStatusCodeOk(CommandHandlerOutput<object> commandHandlerOutput){

            foreach (var erro in commandHandlerOutput.ValidationResult.Errors)
            {
                AddErrorProcessing(erro.ErrorMessage);
            }

            if(IsValidOperation()){
                return  Ok( commandHandlerOutput.Data);
            }
            
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }


        protected ActionResult CustomResponse(object result = null)
        {
            if (IsValidOperation())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Mensagens", Erros.ToArray() }
            }));
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros)
            {
                AddErrorProcessing(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var erro in validationResult.Errors)
            {
                AddErrorProcessing(erro.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ResponseResult resposta)
        {
            IsResponseWithErros(resposta);

            return CustomResponse();
        }

        protected bool IsResponseWithErros(ResponseResult resposta)
        {
            if (resposta == null || !resposta.Errors.Mensagens.Any()) return false;

            foreach (var mensagem in resposta.Errors.Mensagens)
            {
                AddErrorProcessing(mensagem);
            }

            return true;
        }

        protected bool IsValidOperation()
        {
            return !Erros.Any();
        }

        protected void AddErrorProcessing(string erro)
        {
            Erros.Add(erro);
        }

        protected void ClearErrorProcessing()
        {
            Erros.Clear();
        }
    }
}