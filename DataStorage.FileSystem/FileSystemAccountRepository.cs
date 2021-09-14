using DataStorage.Abstractions;
using DataStorage.Abstractions.Exceptions;
using Domain;
using StaticProxy.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DataStorage.FileSystem
{
    public class FileSystemAccountRepository : IAccountRepository
    {
        private readonly IFileSystem _fileSystem;

        public int AccountCount { get; set; }

        public FileSystemAccountRepository(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Create(Account account)
        {
            if (account.UserId <= 0)
                throw new AccountIdMustNotBeZeroException();

            var currentBalance = account.GetBalance();
            if (currentBalance != 0)
                throw new BalanceMustBeZeroDuringCreationException();

            account.Id = ++AccountCount;

            var fileContent = new string[]
            {
                $"[account]",
                $"Id = {account.Id}",
                $"UserId = {account.UserId}",
                $"UserName = {account.Username}",
                $"Description = {account.Description}",
                $"Balance = 0",
                $"IsFrozen = {account.IsFrozen()}",
            };

            _fileSystem.WriteAllLines($"{account.Id}.account", fileContent);
        }

        public Account GetAccountById(int id)
        {
            var accountFileName = $"{id}.account";
            if (!_fileSystem.Exists(accountFileName))
            {
                throw new AccountIdNotFoundException();
            }

            return ReadWithinFile(accountFileName);
        }

        private Account ReadWithinFile(string filename)
        {
            var fileContent = _fileSystem.ReadAllLines(filename);
            var userId = 0;
            var accountId = 0;
            var username = string.Empty;
            var description = string.Empty;
            var balance = 0m;
            var isFrozen = false;

            foreach (var line in fileContent)
            {
                var lineTuple = line.Split('=');
                var key = lineTuple[0].Trim();
                var value = lineTuple.Length > 1 ? lineTuple[1].Trim() : "";

                switch (key)
                {
                    case "UserId":
                        try
                        {
                            userId = Convert.ToInt32(value);
                            break;
                        }
                        catch (FormatException ex)
                        {
                            throw new InjuredAccountException($"Could not read 'UserId' with value '{value}' within file '{filename}'.",ex);
                        }
                        
                    case "Id":
                        try
                        {
                            accountId = Convert.ToInt32(value);
                            break;
                        }
                        catch (FormatException ex)
                        {

                            throw new InjuredAccountException($"Could not read 'Id' with value '{value}' within file '{filename}'.", ex);
                        }
                        
                    case "UserName": username = Convert.ToString(value); break;
                    case "Description": description = Convert.ToString(value); break;
                    case "Balance":
                        try
                        {
                            balance = Convert.ToDecimal(value.Replace(',','.'), CultureInfo.InvariantCulture);
                            break;
                        }
                        catch (FormatException ex)
                        {
                            throw new InjuredAccountException($"Could not read 'Balance' with value '{value}' within file '{filename}'.", ex);
                        }
                    case "IsFrozen":
                        try
                        {
                            isFrozen = Convert.ToBoolean(value); break;
                        }
                        catch (FormatException ex)
                        {
                            throw new InjuredAccountException($"Could not read 'IsFrozen' with value '{value}' within file '{filename}'.", ex);
                        }
                        
                    default:
                        break;
                }
            }

            var account = new Account(userId, username, description);
            account.Id = accountId;
            account.Deposit(balance);

            if (isFrozen) account.Freeze();

            return account;
        }

        public IEnumerable<Account> GetAccountsByUserId(int userId)
        {
            var files = _fileSystem.GetFiles(".\\");
            var output = new List<Account>();

            foreach (var file in files)
            {
                if (!file.EndsWith("account"))
                {
                    continue;
                }

                var account = ReadWithinFile(file);

                if (account.UserId == userId)

                    output.Add(account);
            }

            return output;
        }

        public IEnumerable<Account> GetAccountsByUserName(string username)
        {
            var files = _fileSystem.GetFiles(".\\");
            var output = new List<Account>();

            foreach (var file in files)
            {
                if (!file.EndsWith("account"))
                {
                    continue;
                }

                var account = ReadWithinFile(file);

                if (account.Username.Equals(username, StringComparison.OrdinalIgnoreCase))

                    output.Add(account);
            }

            return output;
        }
    }
}
