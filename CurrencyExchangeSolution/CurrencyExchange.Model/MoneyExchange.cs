using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyExchange.Model
{
    internal class MoneyExchange : IMoneyExchange
    {
        public MoneyExchange()
        {

        }

        public IMoney Exchange(IMoney value, ICurrency to)
        {
            throw new NotImplementedException();
        }
    }
}
