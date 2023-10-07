namespace BitcoinWalletManagementSystem
{
    using System.Collections.Generic;

    public class User
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public HashSet<Wallet> Wallets { get; set; } = new HashSet<Wallet>();

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        public override bool Equals(object obj)
        {
            return Id == (obj as User).Id;
        }
    }
}