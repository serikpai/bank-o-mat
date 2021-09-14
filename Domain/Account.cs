using Domain.Exceptions;
using System;

namespace Domain
{
    public class Account
    {
        private decimal _balance;
        private bool _isFrozen;

        public Account(int userId, string username, string description)
        {
            UserId = userId;
            Username = username;
            Description = description;
        }

        public int Id { get; set; }
        public int UserId { get; private set; }
        public string Username { get; private set; }
        public string Description { get; private set; }

        public decimal GetBalance() => _balance;
        public bool IsFrozen() => _isFrozen;

        public void Freeze() => _isFrozen = true;


        public void Deposit(decimal amount)
        {
            if (amount < 0)
            {
                throw new CannotDepositNegativeAmountException($"You are trying to deposit {amount}€ on account 'id:{Id}'. This ain't working!");
            }

            _balance += amount;
        }
        
        public void Withdraw(decimal amount)
        {
            if (amount < 0)
            {
                throw new CannotWithdrawNegativeAmountException($"You are trying to withdraw {amount}€ on account 'id:{Id}'. This ain't working!");
            }

            if (_balance <= amount)
            {
                throw new InsufficientBalanceException($"Not enough money on your account 'id:{Id}'. You are trying to get {amount}€ but you have only {_balance}€");
            }

            _balance -= amount;
        }

        public void Transfer(decimal amount, Account receiver)
        {
            if (amount < 0)
            {
                throw new CannotTransferNegativeAmountException($"You are trying to transfer {amount}€ from account 'id:{Id}'. This ain't working!");
            }

            if (_balance <= amount)
            {
                throw new InsufficientBalanceException($"Not enough money on your account 'id:{Id}'. You are trying to transfer {amount}€ but you have only {_balance}€");
            }

            _balance -= amount;
            receiver._balance += amount;
        }
    }
}
