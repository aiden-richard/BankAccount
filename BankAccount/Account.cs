using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount;

internal class Account
{
    /// <summary>
    /// AccountNumbers must start with 4 digits followed bny a dash and then 5 characters (A - Z) not case sensitive.
    /// </summary>
    public required string AccountNumber { get; private set; }

    /// <summary>
    /// The current balance of the account.
    /// </summary>
    public decimal Balance { get; private set; }

    /// <summary>
    /// Deposits money into the account and returns the new balance.
    /// </summary>
    /// <param name="amount">The amount to deposit. Must be a positive value</param>
    /// <returns></returns>
    public decimal Deposit(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(amount);

        Balance += amount;
        return Balance;
    }
    
    public decimal Withdraw(decimal amount)
    {
        if (amount <= 0 || amount > Balance)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount needs to be positive and not more than the account balance.");
        }

        Balance -= amount;
        return Balance;
    }

    public Account()
    {

    }
}
