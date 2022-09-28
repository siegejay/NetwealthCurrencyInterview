﻿namespace CurrencyExchange.Model
{
    internal interface IExchangeRateProvider
    {
        IExchangeRate Lookup(ICurrency from, ICurrency to);
        bool Exists(ICurrency from, ICurrency to);
    }
}
