using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ifood.Core.DomainObjects
{
    public interface IBusinessRule
    {
        bool IsBroken();

        string Message { get; }
    }
}