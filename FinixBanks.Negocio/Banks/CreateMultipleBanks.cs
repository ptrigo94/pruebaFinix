using FinixBanks.Core.General;
using FinixBanks.Core.Models;
using FinixBanks.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FinixBanks.BL.Banks.CreateBank;

namespace FinixBanks.BL.Banks
{
    public class CreateMultipleBanks
    {

        public class CreateMultipleBanksCommand : IRequest<Result<List<Bank>>>
        {
            public List<BankInputModel> Banks { get; set; }
            public class BankInputModel
            {
                public string Uid { get; set; }
                public long AccountNumber { get; set; }
                public string Iban { get; set; }
                public string BankName { get; set; }
                public int RoutingNumber { get; set; }
                public string SwiftBic { get; set; }
            }

        }
        public class Handler : IRequestHandler<CreateMultipleBanksCommand, Result<List<Bank>>>
        {
            private readonly FinixDbContext _dbContext;
            public Handler(FinixDbContext dbContext)
            {
                _dbContext = dbContext;
            }
            public async Task<Result<List<Bank>>> Handle(CreateMultipleBanksCommand request, CancellationToken cancellationToken)
            {
                List<Bank>newBanks = new List<Bank>();
                foreach (var newBank in request.Banks)
                {
                    if (string.IsNullOrWhiteSpace(newBank.Uid))
                        return Result<List<Bank>>.Failure($"Please check field Uid for bank {newBank.BankName}");

                    if (_dbContext.Banks.Any(x => x.Uid == newBank.Uid))
                        return Result<List<Bank>>.Failure($"Uid is already registered for bank {newBank.BankName}");

                    if (string.IsNullOrWhiteSpace(newBank.Iban))
                        return Result<List<Bank>>.Failure($"Please check field Iban for bank {newBank.BankName}");

                    if (string.IsNullOrWhiteSpace(newBank.BankName))
                        return Result<List<Bank>>.Failure($"Please check field BankName for bank {newBank.BankName}");

                    if (string.IsNullOrWhiteSpace(newBank.SwiftBic))
                        return Result<List<Bank>>.Failure($"Please check field SwiftBic for bank {newBank.BankName}");
                    var newBankToCreate = new Bank
                    {
                        Uid = newBank.Uid,
                        AccountNumber = newBank.AccountNumber,
                        BankName = newBank.BankName,
                        CreatedAt = DateTime.Now,
                        Iban = newBank.Iban,
                        SwiftBic = newBank.SwiftBic,
                        RoutingNumber = newBank.RoutingNumber

                    };
                    newBanks.Add(newBankToCreate);
                }
                await _dbContext.AddRangeAsync(newBanks, cancellationToken);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return Result<List<Bank>>.Success(newBanks);
            }
        }
    }
}
