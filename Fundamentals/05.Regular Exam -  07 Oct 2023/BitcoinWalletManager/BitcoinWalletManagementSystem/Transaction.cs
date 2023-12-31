using System;

namespace BitcoinWalletManagementSystem
{
    public class Transaction
    {
        public string Id { get; set; }

        public string SenderWalletId { get; set; }

        public string ReceiverWalletId { get; set; }

        public long Amount { get; set; }

        public DateTime Timestamp { get; set; }

        public override bool Equals(object obj)
        {
            return Id == (obj as Transaction).Id && Timestamp.Equals((obj as Transaction).Timestamp);
        }
    }
}

