using DotNetBatch14MTZO.DB.Model;
using DotNetBatch14MTZO.MiniATM.Domain.MiniATMServices;
using DotNetBatch14MTZO.MiniATM.Domain.MiniATMServices.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14MTZO.MiniATM.RestApi.Features
{
    [Route("api/[controller]")]
    [ApiController]
    public class MiniATMController : ControllerBase
    {
        private readonly MiniATMEFCoreService _miniATMService;

        public MiniATMController()
        {
            _miniATMService = new MiniATMEFCoreService();
        }

        [HttpPost("login")]
        public IActionResult Login(string cardNumber, int pin)
        {
            var user = _miniATMService.Login(cardNumber, pin);
            return user == null ? NotFound("Invalid card number or PIN.") : Ok(user);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create(UserAcountModel requestModel)
        {
            var model = _miniATMService.RegisterAcount(requestModel);
            if (!model.IsSuccess)
            {
                return BadRequest(model);
            }

            return Ok(model);
        }

        [HttpPost("withdraw")]
        public IActionResult Withdraw(string cardNumber, decimal amount)
        {
            var model = _miniATMService.WithdrawCash(cardNumber, amount);
            if (!model.IsSuccess)
            {
                return BadRequest(model);
            }
            return Ok(model);
        }

        [HttpPost("deposit")]
        public IActionResult Deposit(string cardNumber, decimal amount)
        {
            var model = _miniATMService.Deposit(cardNumber, amount);
            if (!model.IsSuccess)
            {
                return NotFound(model);
            }
            return Ok(model);
        }

        [HttpGet("balance")]
        public IActionResult GetBalance(string cardNumber)
        {
            var user = _miniATMService.GetBalance(cardNumber);
            if (user == null)
            {
                return NotFound("Acount Not Found");
            }

            return Ok(user);
        }

    }
}
