namespace BitcoinWalletManagementSystem
{
    public class Wallet
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public long Balance { get; set; }

        public override bool Equals(object obj)
        {
            return Id == (obj as Wallet).Id;
        }
    }
}