using FinixBanks.Core.General;
using FinixBanks.Core.Models;
using FinixBanks.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinixBanks.BL.Banks
{
    public class GetBanks
    {
        public class GetBanksQuery : IRequest<Result<List<Bank>>> { }
        public class Handler: IRequestHandler<GetBanksQuery , Result<List<Bank>>> 
        {
            private readonly FinixDbContext _context;
            public Handler(FinixDbContext context)
            {
                _context = context;
            }
            public async Task<Result<List<Bank>>> Handle(GetBanksQuery query, CancellationToken cancellationToken)
            {
                return Result<List<Bank>>.Success(await _context.Banks.Where(x => x.IsDeleted == false).ToListAsync(cancellationToken));
            }
        }
    }
}
