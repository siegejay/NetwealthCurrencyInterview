using System.Globalization;

namespace CurrencyExchange.Model
{
    /// <summary>
    /// Money Data Type
    /// </summary>
    public class Money : IMoney
    {
        protected decimal _value = 0.00M;

        /// <summary>
        /// Constructor: Fully construct 
        /// </summary>
        /// <param name="value">Quantity of currency</param>
        /// <param name="isoCurrencyCode">Currency Code to use to select currency type</param>
        public Money(decimal value, string isoCurrencyCode = "GBP")
        {
            _value = value;
            CurrencyCode = isoCurrencyCode;
        }

        /// <summary>
        /// Constructor: Fully construct 
        /// </summary>
        /// <param name="value">Quantity of currency</param>
        /// <param name="culture">Culture to use to specify currency type</param>
        public Money(decimal value, CultureInfo culture) : this(value, culture.Name) { }

        public string CurrencyCode { get; }

        public decimal Value { get { return _value; } }

        public int CompareTo(IMoney? other)
        {
            if (other == null) return 1;
            if (!string.Equals(other.CurrencyCode, CurrencyCode, StringComparison.InvariantCultureIgnoreCase))
                throw new ArgumentException("Incompatible Currency Type", nameof(other));
            return Value.CompareTo(other.Value);
        }

        public bool Equals(IMoney? other)
        {
            if (other == null) return false;
            if (!string.Equals(other.CurrencyCode, CurrencyCode, StringComparison.InvariantCultureIgnoreCase)) return false;
            return Value.Equals(other.Value);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;

            var moneyObj = (IMoney)obj;
            if (moneyObj == null) return false;
            return Equals(moneyObj);
        }

        public string FormattedValue(CultureInfo cultureForDisplay) 
        {
            if (cultureForDisplay == null) throw new ArgumentException("You must supply a Culture to determine the display format");
            return string.Format(cultureForDisplay, "{0:C}", Value); 
        }

        public static bool operator >(Money operand1, Money operand2)
        {
            return operand1.CompareTo(operand2) > 0;
        }

        public static bool operator <(Money operand1, Money operand2)
        {
            return operand1.CompareTo(operand2) < 0;
        }

        public static bool operator >=(Money operand1, Money operand2)
        {
            return operand1.CompareTo(operand2) >= 0;
        }

        public static bool operator <=(Money operand1, Money operand2)
        {
            return operand1.CompareTo(operand2) <= 0;
        }

        public override int GetHashCode()
        {
            return CurrencyCode.ToUpper().GetHashCode() ^ Value.GetHashCode();
        }

        public static bool operator ==(Money operand1, Money operand2)
        {
            if (((object)operand1) == null || ((object)operand2) == null)
                return Equals(operand1, operand2);

            return operand1.Equals(operand2);
        }

        public static bool operator !=(Money operand1, Money operand2)
        {
            if (((object)operand1) == null || ((object)operand2) == null)
                return !Equals(operand1, operand2);

            return !(operand1.Equals(operand2));
        }        
    }
}
