using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinixBanks.Core.Models
{
    public class BankDTO
    {
        public string Uid { get; set; }
        public int AccountNumber { get; set; }
        public string Iban { get; set; }
        public string BankName { get; set; }
        public int RoutingNumber { get; set; }
        public string SwiftBic { get; set; }
    }
}
