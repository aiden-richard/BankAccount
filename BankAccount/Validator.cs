namespace BankAccount;

public class Validator
{
    /// <summary>
    /// Checks to see if a value is within a specified range (inclusive)
    /// </summary>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Throws if min > max</exception>
    public bool IsWithinRange(double value, double min, double max)
    {
        if (min > max)
        {
            throw new ArgumentException("Min cannot be greater than max.");
        }


        if (min <= value && value <= max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
