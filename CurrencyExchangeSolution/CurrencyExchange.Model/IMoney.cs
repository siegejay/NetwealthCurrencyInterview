using System.Globalization;

namespace CurrencyExchange.Model
{
    /// <summary>
    /// Money Data Type - Definition
    /// </summary>
    public interface IMoney : IComparable<IMoney>, IEquatable<IMoney>
    {
        string CurrencyCode { get; }
        decimal Value { get; }
        string FormattedValue(CultureInfo cultureForDisplay);
    }
}