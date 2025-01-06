using DoNEtBatch14MTZO.MiniATM.ConsoleApp;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoNEtBatch14MTZO.MiniATM.ConsoleApp
{
    public class MiniATMRefitService
    {
        private readonly string endpoint = "https://localhost:7223";
        private readonly IMiniATMApi _api;

        public MiniATMRefitService()
        {
            _api = RestService.For<IMiniATMApi>(endpoint);
        }

        public async Task<AcountResponseModel> Login(string cardNumber, int pin)
        {
            var model = await _api.Login(cardNumber, pin);
            return model;
        }

        public async Task<AcountResponseModel> Register(UserAcountModel requestModel)
        {
            var model = await _api.Create(requestModel);
            return model;
        }
        public async Task<AcountResponseModel> Withdraw(string cardNumber, decimal amount)
        {
            var model = await _api.Withdraw(cardNumber, amount);
            return model;

        }
        public async Task<AcountResponseModel> Deposit(string cardNumber, decimal amount)
        {
            var model = await _api.Deposit(cardNumber, amount);
            return model;
        }

        public async Task<decimal> Balance(string cardNumber)
        {
            var model = await _api.Blance(cardNumber);
            return model;
        }
    }
}
    public interface IMiniATMApi
    {
        [Post("/api/miniatm/login")]
        Task<AcountResponseModel> Login(string cardNumber, int pin);

        [Post("/api/miniatm/register")]
        Task<AcountResponseModel> Create(UserAcountModel requestModel);

        [Post("/api/miniatm/withdraw")]
        Task<AcountResponseModel> Withdraw(string cardNumber, decimal amount);

        [Post("/api/miniatm/deposit")]
        Task<AcountResponseModel> Deposit(string cardNumber, decimal amount);

        [Get("/api/miniatm/balance")]
        Task<decimal> Blance(string cardNumber);
}



