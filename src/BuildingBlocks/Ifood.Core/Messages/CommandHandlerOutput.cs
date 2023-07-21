using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Ifood.Core.Messages
{
    public class CommandHandlerOutput<T> where T : class
    {   
        public ValidationResult ValidationResult {get;set;}
        public T? Data {get;set;}
    }
}