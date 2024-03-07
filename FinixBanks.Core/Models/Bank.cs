using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinixBanks.Core.Models
{
    public class Bank
    {
        public int Id { get; set; }
        public string Uid { get; set; }
        public long AccountNumber { get; set; }
        public string Iban { get; set; }
        public string BankName { get; set; }
        public long RoutingNumber { get; set; }
        public string SwiftBic { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdatedAt { get; set; } 
        public bool IsDeleted { get; set; }

    }
}
