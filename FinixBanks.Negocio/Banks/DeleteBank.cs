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
    public class DeleteBank
    {
        public class DeleteBankQuery : IRequest<Result<string>>
        {
            public string Uid { get; set; } 
        }
        public class DeleteBankHandler: IRequestHandler <DeleteBankQuery, Result<string>>
        {

            private readonly FinixDbContext _context;
            public DeleteBankHandler(FinixDbContext context)
            {
                _context = context;
            }
            public async Task<Result<string>> Handle(DeleteBankQuery query, CancellationToken cancellationToken)
            {
                if (query.Uid == null || query.Uid == "")
                    return Result<string>.Failure("The Uid field is required");
                var uid = query.Uid;
                var bank = await _context.Banks.Where(x => x.Uid == uid).FirstOrDefaultAsync(cancellationToken);
                if (bank == null)
                    return Result<string>.Failure($"The bank with the uid {uid} does not exist");
                bank.IsDeleted = true;
                bank.LastUpdatedAt = DateTime.UtcNow;
                _context.Banks.Update(bank);
                return await _context.SaveChangesAsync(cancellationToken) > 0
                    ? Result<string>.Success($"Bank with Uid : {uid} has been deleted.")
                    : Result<string>.Failure("An error has ocurred while deleting the bank.");
            }
        }
    }
}
