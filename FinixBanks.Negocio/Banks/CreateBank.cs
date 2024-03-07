using FinixBanks.Core.General;
using FinixBanks.Core.Models;
using FinixBanks.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinixBanks.BL.Banks
{
    public class CreateBank
    {
        public class CreateBankCommand : IRequest<Result<Bank>>
        {
            public string Uid { get; set; }
            public long AccountNumber { get; set; }
            public string Iban { get; set; }
            public string BankName { get; set; }
            public int RoutingNumber { get; set; }
            public string SwiftBic { get; set; }
        }
        public class Handler : IRequestHandler<CreateBankCommand, Result<Bank>>
        {
            private readonly FinixDbContext _finixDbContext;
            public Handler(FinixDbContext finixDbContext)
            {
                _finixDbContext = finixDbContext;
            }
            public async Task<Result<Bank>> Handle(CreateBankCommand request, CancellationToken cancellationToken)
            {
                if (request.Uid == "")
                    return Result<Bank>.Failure("Please check field Uid");
                else if (_finixDbContext.Banks.Where(x => x.Uid == request.Uid).Count() > 0)
                    return Result<Bank>.Failure("Uid is already registered");
                if (request.Iban == "")
                    return Result<Bank>.Failure("Please check field Iban");
                if (request.BankName == "")
                    return Result<Bank>.Failure("Please check field BankName");
                if (request.SwiftBic == "")
                    return Result<Bank>.Failure("Please check field SwiftBic");
                var newBank = new Bank()
                {
                    Uid = request.Uid,
                    BankName = request.BankName,
                    SwiftBic = request.SwiftBic,
                    AccountNumber = request.AccountNumber,
                    Iban = request.Iban,
                    RoutingNumber = request.RoutingNumber,
                    CreatedAt = DateTime.UtcNow,

                };
                await _finixDbContext.AddAsync(newBank, cancellationToken);
                return await _finixDbContext.SaveChangesAsync(cancellationToken) > 0 
                    ? Result<Bank>.Success(newBank)
                    : Result<Bank>.Failure("An error has ocurred while saving the data.");

            }
        }
    }
}
