using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace BitcoinWalletManagementSystem
{
    public class BitcoinWalletManager : IBitcoinWalletManager
    {
        private Dictionary<string, User> usersById = new Dictionary<string, User>();
        private Dictionary<string, Wallet> walletsById = new Dictionary<string, Wallet>();
        //private HashSet<Transaction> transactions = new HashSet<Transaction>();

        public void CreateUser(User user) => usersById.Add(user.Id, user);

        public void CreateWallet(Wallet wallet)
        {
            if (usersById.ContainsKey(wallet.UserId))
            {
                var user = usersById[wallet.UserId];
                walletsById.Add(wallet.Id, wallet);
                wallet.User = user;
                user.Wallets.Add(wallet);
            }
        }

        public bool ContainsUser(User user) => usersById.ContainsKey(user.Id);

        public bool ContainsWallet(Wallet wallet) => walletsById.ContainsKey(wallet.Id);

        public IEnumerable<Wallet> GetWalletsByUser(string userId)
        {
            if (!usersById.ContainsKey(userId))
            {
                return new List<Wallet>();
            }

            return usersById[userId].Wallets;
        }

        public void PerformTransaction(Transaction transaction)
        {
            if (!walletsById.ContainsKey(transaction.SenderWalletId) 
                || !walletsById.ContainsKey(transaction.ReceiverWalletId)
                || walletsById[transaction.SenderWalletId].Balance < transaction.Amount)
            {
                throw new ArgumentException();
            }

            walletsById[transaction.SenderWalletId].User.Transactions.Add(transaction);
            walletsById[transaction.SenderWalletId].Balance -= transaction.Amount;
            walletsById[transaction.ReceiverWalletId].User.Transactions.Add(transaction);
            walletsById[transaction.ReceiverWalletId].Balance += transaction.Amount;
        }

        public IEnumerable<Transaction> GetTransactionsByUser(string userId)
        {
            if (!usersById.ContainsKey(userId))
            {
                return new List<Transaction>();
            }

            return usersById[userId].Transactions;
        }

        public IEnumerable<Wallet> GetWalletsSortedByBalanceDescending() 
            => walletsById.Values.OrderByDescending(x => x.Balance);

        public IEnumerable<User> GetUsersSortedByBalanceDescending()
            =>usersById.Values.OrderByDescending(u => u.Wallets.Sum(x => x.Balance));

        public IEnumerable<User> GetUsersByTransactionCount()
            => usersById.Values.OrderByDescending(u => u.Transactions.Count);
    }
}