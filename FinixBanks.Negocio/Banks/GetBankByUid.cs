using FinixBanks.Core.General;
using FinixBanks.Core.Models;
using FinixBanks.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinixBanks.BL.Banks
{
    public class GetBankByUid
    {
        public class GetBankByUidQuery : IRequest <Result<Bank>>
        {
            public string Uid { get; set; }
        }
        public class GetBankByUidHandler : IRequestHandler <GetBankByUidQuery, Result<Bank>>
        {
            private readonly FinixDbContext _context;
            public GetBankByUidHandler (FinixDbContext context)
            {
                _context = context;
            }
            public async Task<Result<Bank>> Handle(GetBankByUidQuery query, CancellationToken cancellationToken)
            {
                if (query.Uid == null || query.Uid == "")
                    return Result<Bank>.Failure("The Uid field is required");
                var uid = query.Uid;
                var bank = await _context.Banks.Where(x => x.Uid == uid).FirstOrDefaultAsync(cancellationToken);
                return bank is null  || bank.IsDeleted == true
                    ? Result<Bank>.Failure("The bank does not exist.")
                    : Result<Bank>.Success(bank);
            }

        }
    }
}
