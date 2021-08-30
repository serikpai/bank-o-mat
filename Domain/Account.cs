using Domain.Exceptions;
using System;

namespace Domain
{
    public class Account
    {
        private decimal _balance;

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
        
        public void Deposit(decimal money)
        {
            if (money < 0)
            {
                throw new CannotDepositNegativeAmountException($"You are trying to deposit {money}€ on account 'id:{Id}'. This ain't working!");
            }

            _balance += money;
        }

        public decimal GetBalance() => _balance;

        public void Withdraw(decimal money)
        {
            if (money < 0)
            {
                throw new CannotWithdrawNegativeAmountException($"You are trying to withdraw {money}€ on account 'id:{Id}'. This ain't working!");
            }

            if (_balance <= money)
            {
                throw new InsufficientBalanceException($"Not enough money on your account 'id:{Id}'. You are trying to get {money}€ but you have only {_balance}€");
            }

            _balance -= money;
        }
    }
}
