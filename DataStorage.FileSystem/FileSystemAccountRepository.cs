using DataStorage.Abstractions;
using DataStorage.Abstractions.Exceptions;
using Domain;
using StaticProxy.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DataStorage.FileSystem
{
    internal struct LineContent
    {
        public string Identifier { get; set; }
        public string Value { get; set; }

        public LineContent(string key, string value)
        {
            Identifier = key;
            Value = value;
        }

        public string AsString()
        {
            return $"{Identifier} = {Value}";
        }
    }

    public class FileSystemAccountRepository : IAccountRepository
    {
        public string DefaultFileExtension = "account";
        private readonly IFileSystem _fileSystem;
        private Account _account;
        private string _filename;

        public int AccountCount { get; set; }

        public FileSystemAccountRepository(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
        }

        public void Create(Account account)
        {
            _account = account;

            EnsureAccountIsAssignedToUserAndHasNoBalance();
            GenerateNewIdForTheAccount();
            ConvertAccountIntoStringArrayAndSaveItToSpecificFile();
        }


        private void EnsureAccountIsAssignedToUserAndHasNoBalance()
        {
            if (_account.UserId <= 0)
            {
                throw new AccountIdMustNotBeZeroException();
            }

            var currentBalance = _account.GetBalance();
            if (currentBalance != 0)
            {
                throw new BalanceMustBeZeroDuringCreationException();
            }
        }

        private void GenerateNewIdForTheAccount()
        {
            _account.Id = ++AccountCount;
        }

        private void ConvertAccountIntoStringArrayAndSaveItToSpecificFile()
        {
            var fileContent = new string[]
            {
                $"[account]",
                $"Id = {_account.Id}",
                $"UserId = {_account.UserId}",
                $"UserName = {_account.Username}",
                $"Description = {_account.Description}",
                $"Balance = 0",
                $"IsFrozen = {_account.IsFrozen()}",
            };

            var fileNameForCurrentAccount = GetFileNameByAccountId(_account.Id);
            _fileSystem.WriteAllLines(fileNameForCurrentAccount, fileContent);
        }

        private string GetFileNameByAccountId(int id)
        {
            return $"{id}.{DefaultFileExtension}";
        }

        public Account GetAccountById(int id)
        {
            var accountFileName = GetFileNameByAccountId(id);

            EnsureAccountFileExists(accountFileName);

            return ReadWithinFile(accountFileName);
        }

        private void EnsureAccountFileExists(string accountFileName)
        {
            if (!_fileSystem.Exists(accountFileName))
            {
                throw new AccountIdNotFoundException();
            }
        }
        LineContent[] fileContent;
        private Account ReadWithinFile(string filename)
        {
            _filename = filename;

            fileContent = ReadAccountDocumentAndProvideRawData();

            var userId = GetNumericValueForSpecificIdentifier("UserId");
            var accountId = GetNumericValueForSpecificIdentifier("Id");

            var username = GetStringValueForSpecificIdentifier("UserName");
            var description = GetStringValueForSpecificIdentifier("Description");
            var balance = GetDecimalValueForSpecificIdentifier("Balance");
            var isFrozen = GetBooleanValueForSpecificIdentifier("IsFrozen");

            var account = new Account(userId, username, description);
            account.Id = accountId;
            account.Deposit(balance);

            if (isFrozen)
            {
                account.Freeze();
            }

            return account;
        }

        private LineContent[] ReadAccountDocumentAndProvideRawData()
        {
            var fileContent = _fileSystem.ReadAllLines(_filename);
            var output = new List<LineContent>();

            foreach (var line in fileContent)
            {
                var lineTuple = line.Split('=');

                if (lineTuple.Length != 2)
                {
                    continue;
                }

                var key = lineTuple[0].Trim();
                var value = lineTuple[1].Trim();
                var lineContent = new LineContent(key, value);

                output.Add(lineContent);
            }

            return output.ToArray();
        }

        private LineContent FindEntryForRequestedIdentifierWithinTheSourceFile(string key)
        {
            try
            {
                return fileContent.Single(x => x.Identifier.Equals(key));
            }
            catch (InvalidOperationException ex)
            {
                throw new InjuredAccountException($"Could not read '{key}' because it was not found within file '{_filename}'.", ex);
            }
        }

        private int GetNumericValueForSpecificIdentifier(string key)
        {
            var line = FindEntryForRequestedIdentifierWithinTheSourceFile(key);

            try
            {
                return Convert.ToInt32(line.Value);
            }
            catch (FormatException ex)
            {
                throw GetInjuredAccountExceptionForInvalidFormats(line, ex);
            }
        }

        private decimal GetDecimalValueForSpecificIdentifier(string key)
        {
            var line = FindEntryForRequestedIdentifierWithinTheSourceFile(key);

            try
            {
                return Convert.ToDecimal(line.Value.Replace(',', '.'), CultureInfo.InvariantCulture);
            }
            catch (FormatException ex)
            {
                throw GetInjuredAccountExceptionForInvalidFormats(line, ex);
            }
        }

        private bool GetBooleanValueForSpecificIdentifier(string key)
        {
            var line = FindEntryForRequestedIdentifierWithinTheSourceFile(key);

            try
            {
                return Convert.ToBoolean(line.Value);
            }
            catch (FormatException ex)
            {
                throw GetInjuredAccountExceptionForInvalidFormats(line, ex);
            }
        }

        private string GetStringValueForSpecificIdentifier(string key)
        {
            var line = FindEntryForRequestedIdentifierWithinTheSourceFile(key);

            return line.Value;
        }


        private Exception GetInjuredAccountExceptionForInvalidFormats(LineContent line, FormatException ex)
        {
            throw new InjuredAccountException($"Could not read '{line.Identifier}' with value '{line.Value}' within file '{_filename}'.", ex);
        }

        public IEnumerable<Account> GetAccountsByUserId(int userId)
        {
            return GetAll()
                .Where(acc => acc.UserId == userId);
        }

        public IEnumerable<Account> GetAccountsByUserName(string username)
        {
            return GetAll()
                .Where(acc => acc.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        private IEnumerable<Account> GetAll()
        {
            var files = _fileSystem.GetFiles(".\\");
            var output = new List<Account>();

            foreach (var file in files)
            {
                if (!file.EndsWith(DefaultFileExtension))
                {
                    continue;
                }

                var account = ReadWithinFile(file);
                output.Add(account);
            }

            return output;
        }
    }
}