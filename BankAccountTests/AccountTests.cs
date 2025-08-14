using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankAccount;

namespace BankAccountTests;

[TestClass]
public class AccountTests
{
    // Test that the Account class initializes
    [TestMethod]
    public void Account_InitialBalance_ShouldBeZero()
    {
        // Arrange & Act
        var account = new Account { AccountNumber = "1234-ABCDE" };

        // Assert
        Assert.AreEqual(0, account.Balance);
    }

    // Test that the AccountNumber property can be set and retains its value
    [TestMethod]
    public void AccountNumber_SetValue_ShouldRetainValue()
    {
        // Arrange
        string expectedAccountNumber = "5678-FGHIJ";

        // Act
        var account = new Account { AccountNumber = expectedAccountNumber };

        // Assert
        Assert.AreEqual(expectedAccountNumber, account.AccountNumber);
    }

    // Test deposit
    [TestMethod]
    [DataRow(0.01)]
    [DataRow(1.00)]
    [DataRow(50.25)]
    [DataRow(100.50)]
    [DataRow(999.99)]
    [DataRow(1000000.00)]
    public void Deposit_ValidAmounts_ShouldIncreaseBalance(double amount)
    {
        // Arrange
        var account = new Account { AccountNumber = "1234-ABCDE" };
        decimal depositAmount = (decimal)amount;

        // Act
        decimal newBalance = account.Deposit(depositAmount);

        // Assert
        Assert.AreEqual(depositAmount, account.Balance);
        Assert.AreEqual(depositAmount, newBalance);
    }

    // Test depositing twice and check the balance
    [TestMethod]
    public void Deposit_MultipleDeposits_ShouldAccumulateBalance()
    {
        // Arrange
        var account = new Account { AccountNumber = "1234-ABCDE" };

        // Act
        account.Deposit(50.00m);
        decimal finalBalance = account.Deposit(75.25m);

        // Assert
        Assert.AreEqual(125.25m, account.Balance);
        Assert.AreEqual(125.25m, finalBalance);
    }

    // Test deposit with invalid amounts (negative and zero)
    [TestMethod]
    [DataRow(-10.00)]
    [DataRow(-0.01)]
    [DataRow(0)]
    public void Deposit_InvalidAmounts_ShouldThrowArgumentOutOfRangeException(double amount)
    {
        // Arrange
        var account = new Account { AccountNumber = "1234-ABCDE" };

        // Act & Assert
        Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Deposit((decimal)amount));
    }

    // Test withdraw with valid amounts and expected balances
    [TestMethod]
    [DataRow(30.50, 100.00, 69.50)]
    [DataRow(25.00, 100.00, 75.00)]
    [DataRow(100.00, 100.00, 0.00)]
    [DataRow(0.01, 1.00, 0.99)]
    public void Withdraw_ValidAmounts_ShouldDecreaseBalance(double withdrawAmount, double initialBalance, double expectedBalance)
    {
        // Arrange
        var account = new Account { AccountNumber = "1234-ABCDE" };
        account.Deposit((decimal)initialBalance);

        // Act
        decimal newBalance = account.Withdraw((decimal)withdrawAmount);

        // Assert
        Assert.AreEqual((decimal)expectedBalance, account.Balance);
        Assert.AreEqual((decimal)expectedBalance, newBalance);
    }

    // Test withdraw with amounts that exceed balance or from empty account
    [TestMethod]
    // DataRows: first is for amount exceeding balance, second is for withdrawing from empty account
    [DataRow(75.00, 50.00)]  // Amount exceeds balance
    [DataRow(1.00, 0.00)]    // Withdraw from empty account
    public void Withdraw_AmountExceedsBalance_ShouldThrowArgumentOutOfRangeException(double withdrawAmount, double initialBalance)
    {
        // Arrange
        var account = new Account { AccountNumber = "1234-ABCDE" };
        if (initialBalance > 0)
        {
            account.Deposit((decimal)initialBalance);
        }

        // Act & Assert
        var exception = Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Withdraw((decimal)withdrawAmount));
        Assert.AreEqual("amount", exception.ParamName);
        Assert.IsTrue(exception.Message.Contains("Amount needs to be positive and not more than the account balance."));
    }

    // Test withdraw with invalid amounts (negative and zero)
    [TestMethod]
    [DataRow(-10.00, 100.00)]
    [DataRow(0, 100.00)]
    public void Withdraw_InvalidAmounts_ShouldThrowArgumentOutOfRangeException(double withdrawAmount, double initialBalance)
    {
        // Arrange
        var account = new Account { AccountNumber = "1234-ABCDE" };
        account.Deposit((decimal)initialBalance);

        // Act & Assert
        var exception = Assert.ThrowsException<ArgumentOutOfRangeException>(() => account.Withdraw((decimal)withdrawAmount));
        Assert.AreEqual("amount", exception.ParamName);
    }

    [TestMethod]
    public void Account_MultipleTransactions_ShouldMaintainCorrectBalance()
    {
        // Arrange
        var account = new Account { AccountNumber = "1234-ABCDE" };

        // Act
        account.Deposit(100.00m);  // Balance: 100.00
        account.Withdraw(25.50m);  // Balance: 74.50
        account.Deposit(50.00m);   // Balance: 124.50
        decimal finalBalance = account.Withdraw(24.50m); // Balance: 100.00

        // Assert
        Assert.AreEqual(100.00m, account.Balance);
        Assert.AreEqual(100.00m, finalBalance);
    }

    [TestMethod]
    public void Account_PrecisionTest_ShouldHandleDecimalPrecision()
    {
        // Arrange
        var account = new Account { AccountNumber = "1234-ABCDE" };

        // Act
        account.Deposit(0.01m);
        account.Deposit(0.02m);
        decimal balance = account.Withdraw(0.01m);

        // Assert
        Assert.AreEqual(0.02m, account.Balance);
        Assert.AreEqual(0.02m, balance);
    }
}
