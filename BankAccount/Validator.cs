namespace BankAccount;

public class Validator
{
    /// <summary>
    /// Checks to see if a value is within a specified range (inclusive)
    /// </summary>
    /// <returns></returns>
    public bool IsWithinRange(double value, double min, double max)
    {
        if (min < value && value < max)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
