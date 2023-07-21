using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ifood.Message.Bus.ExangesAttributes
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExchangeNameAttribute : Attribute
    {
        public ExchangeNameAttribute(string exchangeName)
        {
            ExchangeName = exchangeName;
        }

        public string ExchangeName { get; set; }
    }
}