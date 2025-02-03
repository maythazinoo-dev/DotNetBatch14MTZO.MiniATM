using DotNetBatch14MTZO.DB;
using DotNetBatch14MTZO.DB.Model;
using DotNetBatch14MTZO.MiniATM.Domain.MiniATMServices.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using AcountResponseModel = DotNetBatch14MTZO.MiniATM.Domain.MiniATMServices.Model.AcountResponseModel;

namespace DotNetBatch14MTZO.MiniATM.Domain.MiniATMServices
{
    public class MiniATMEFCoreService
    {
        private readonly AppDbContext _db;

        public MiniATMEFCoreService()
        {
            _db = new AppDbContext();
        }

        public AcountResponseModel Login(string cardNumber, int pin)
        {
            var userModel = _db.UserAcounts.FirstOrDefault(u => u.CardNumber == cardNumber && u.Pin == pin);
            
            if (userModel == null)
            {
                return new AcountResponseModel()
                {
                    IsSuccess = false,
                    Message = "Acount Not Found",

                };
            }
            return new AcountResponseModel()
            {
                IsSuccess = true,
                Message = "Login Successful",

            };

        }

        public AcountResponseModel RegisterAcount(UserAcountModel requestModel)
        {
          
            _db.UserAcounts.Add(requestModel);
            var result = _db.SaveChanges();

            String message = result > 0 ? "Create Acount Successful " : "Create Acount Failed";
            AcountResponseModel model = new AcountResponseModel();
            model.IsSuccess = result > 0;
            model.Message = message;
            return model;

        }

        public AcountResponseModel WithdrawCash(string cardnumber, decimal balance)
        {

            var user = _db.UserAcounts.FirstOrDefault(u => u.CardNumber == cardnumber);
            if (user == null || user.Balance < balance)
                return new AcountResponseModel()
                {
                    IsSuccess = false,
                    Message = " Insufficient balance or account not found."
                };


            user.Balance -= balance;
            _db.SaveChangesAsync();
            return new AcountResponseModel()
            {
                IsSuccess = true,
                Message = "Withdraw Successful"
            };
        }

        public AcountResponseModel Deposit(string cardnumber, decimal balance)
        {
            var user = _db.UserAcounts.FirstOrDefault(u => u.CardNumber == cardnumber);
            if (user == null)
                return new AcountResponseModel
                {
                    IsSuccess = false,
                    Message = "Deposit failed"
                };

            user.Balance += balance;
            _db.SaveChangesAsync();
            return new AcountResponseModel()
            {
                IsSuccess = true,
                Message = "Deposit Successful"
            };
        }

        public decimal? GetBalance(string cardNumber)
        {
            var user = _db.UserAcounts.FirstOrDefault(u => u.CardNumber == cardNumber);
            if (user == null) return null;
            return user.Balance;

        }


    }
}
