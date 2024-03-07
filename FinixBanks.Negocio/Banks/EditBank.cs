using FinixBanks.Core.General;
using FinixBanks.Core.Models;
using FinixBanks.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FinixBanks.BL.Banks
{
    public class EditBank
    {
        public class EditBankQuery : IRequest<Result<Bank>>
        {
            public string Uid { get; set; }
            public string BankName { get; set; }
        }
        public class EditBankHandler : IRequestHandler <EditBankQuery, Result<Bank>>{
            private readonly FinixDbContext _context;

            public EditBankHandler (FinixDbContext context)
            {
                _context = context;
            }
            public async Task<Result<Bank>> Handle(EditBankQuery query, CancellationToken cancellationToken)
            {

                if (query.Uid == null || query.Uid == "")
                    return Result<Bank>.Failure("The Uid field is required");
                if (query.BankName == null || query.BankName == "")
                    return Result<Bank>.Failure("The BankName field is required");
                var bank = await _context.Banks.Where(x => x.Uid == query.Uid).FirstOrDefaultAsync(cancellationToken);
                if (bank == null)
                    return Result<Bank>.Failure($"The bank with the uid {query.Uid} does not exist");
                bank.BankName = query.BankName;
                bank.LastUpdatedAt = DateTime.UtcNow;
                _context.Banks.Update(bank);
                return await _context.SaveChangesAsync(cancellationToken) > 0
                    ? Result<Bank>.Success(bank)
                    : Result<Bank>.Failure("An error has ocurred while updating the bank.");

            }
        }
    }
}
