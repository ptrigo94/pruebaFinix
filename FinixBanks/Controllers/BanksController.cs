using FinixBanks.BL.Banks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinixBanks.Controllers
{
    public class BanksController : BaseController
    {

        private readonly IMediator _mediator;
        public BanksController(IMediator mediator) => _mediator = mediator;


        [HttpGet("[action]")]
        public async Task <ActionResult> GetBanks()
        {
            return HandleResult(await _mediator.Send(new GetBanks.GetBanksQuery()));
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> CreateBank(CreateBank.CreateBankCommand request)
        {
            return HandleResult(await _mediator.Send(request));
        }
        [HttpGet("[action]")]
        public async Task<ActionResult> GetBankByUid( string uid)
        {
            return HandleResult(await _mediator.Send(new GetBankByUid.GetBankByUidQuery { Uid = uid }));
        }
        [HttpDelete("[action]")]
        public async Task<ActionResult> DeleteBank (DeleteBank.DeleteBankQuery query)
        {
            return HandleResult(await _mediator.Send(query));
        }
        [HttpPut("[action]")]
        public async Task<ActionResult> EditBankName(EditBank.EditBankQuery query)
        {
            return HandleResult(await _mediator.Send(query));   
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
