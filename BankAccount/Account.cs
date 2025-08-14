using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankAccount;

public class Account
{
    /// <summary>
    /// AccountNumbers must start with 4 digits followed by a dash and then 5 characters (A - Z) not case sensitive.
    /// </summary>
    public required string AccountNumber { get; init; }

    /// <summary>
    /// The current balance of the account.
    /// </summary>
    public decimal Balance { get; private set; }

    /// <summary>
    /// Deposits money into the account and returns the new balance.
    /// </summary>
    /// <param name="amount">The amount to deposit. Must be a positive value</param>
    /// <returns>The new account balance after the deposit.</returns>
    public decimal Deposit(decimal amount)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(amount);

        Balance += amount;
        return Balance;
    }
    
    /// <summary>
    /// Withdraws money from the account and returns the new balance.
    /// </summary>
    /// <param name="amount">The amount to withdraw. Must be a positive value and not more than the account balance.</param>
    /// <returns>The new balance after withdrawal.</returns>
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
