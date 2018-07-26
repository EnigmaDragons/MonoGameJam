using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MonoDragons.Core.Errors
{
    public class ReportErrorHandler : IErrorHandler
    {
        private readonly MetaAppDetails _appDetails;

        public ReportErrorHandler(MetaAppDetails appDetails)
        {
            _appDetails = appDetails;
        }

        public async Task ResolveError(Game game, Exception ex)
        {
            using (var client = new HttpClient())
                await client.PostAsync("https://hk86vytqs1.execute-api.us-west-2.amazonaws.com/GameMetrics/ReportCrashDetail", 
                    new StringContent(JsonConvert.SerializeObject(new CrashDetail
                    {
                        ApplicationName = _appDetails.Name,
                        ApplicationVersion = _appDetails.Version,
                        ContextJson = JsonConvert.SerializeObject(new Context
                        {
                            OS = _appDetails.OS,
                            ErrorMessage = ex.Message
                        }),
                        StackTrace = ex.StackTrace
                    }), Encoding.UTF8, "application/json"));
            game.Exit();
        }

        public async Task ResolveError(Exception ex)
        {
            using (var client = new HttpClient())
            {
                await client.PostAsync(
                    "https://hk86vytqs1.execute-api.us-west-2.amazonaws.com/GameMetrics/ReportCrashDetail",
                    new StringContent(JsonConvert.SerializeObject(new CrashDetail
                    {
                        ApplicationName = _appDetails.Name,
                        ApplicationVersion = _appDetails.Version,
                        ContextJson = JsonConvert.SerializeObject(new Context
                        {
                            OS = _appDetails.OS,
                            ErrorMessage = ex.Message
                        }),
                        StackTrace = ex.StackTrace
                    }), Encoding.UTF8, "application/json"));
            }
            throw ex;
        }

        private class CrashDetail
        {
            public string ApplicationName { get; set; }
            public string ApplicationVersion { get; set; }
            public string ContextJson { get; set; }
            public string StackTrace { get; set; }
        }

        private class Context
        {
            public string OS { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
