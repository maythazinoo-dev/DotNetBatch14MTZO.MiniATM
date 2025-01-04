using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetBatch14MTZO.MiniATM.ConsoleApp
{
    public class MiniATMHttpClientService
    {
        private readonly string endpoint = "https://localhost:7223/";
        private readonly HttpClient _httpClient;
    }
}
