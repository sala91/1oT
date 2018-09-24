using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using RestSharp;
using _1oT.Models;
using Microsoft.Extensions.Logging;

namespace _1oT.Services
{
    public class DataService
    {
        readonly ILogger<DataService> logger;
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public void Authorize()
        {
            string localApiUserName = string.Empty;
            string localApiPasword = string.Empty;
            try
            {
                localApiUserName = localSettings.Values["apiUsername"].ToString();
                localApiPasword = localSettings.Values["apiPassword"].ToString();
                var client = new RestClient("https://api.terminal.1ot.mobi/v1/");
                var request = new RestRequest("oauth/token", Method.POST);
                request.AddParameter("grant_type", "password");
                request.AddParameter("username", localApiUserName);
                request.AddParameter("client_id", localApiUserName);
                request.AddParameter("password", localApiPasword);
                request.AddParameter("refresh_token", "");
                IRestResponse response = client.Execute(request);
                try
                {
                    // Update apitoken, everything supergud now
                    Authorize model = new Authorize();
                    model = JsonConvert.DeserializeObject<Authorize>(response.Content);
                    localSettings.Values["apiToken"] = model.access_token;
                }
                catch (Exception ex)
                {
                    logger.LogInformation("Did we have IP Access?");
                    logger.LogError(ex.Message);
                    logger.LogDebug(ex.StackTrace);
                }
            }
            catch (Exception ex)
            {
                logger.LogInformation("Mostlikely bad credentials");
                logger.LogError(ex.Message);
                logger.LogDebug(ex.StackTrace);
            }
        }

        public ManageSims GetSimCards()
        {
            string localApiToken = string.Empty;
            ManageSims model = new ManageSims();
            try
            {
                localApiToken = localSettings.Values["apiToken"].ToString();

                var client = new RestClient("https://api.terminal.1ot.mobi/v1/");
                var request = new RestRequest("get_account_sims", Method.POST);
                request.AddParameter("access_token", localApiToken);
                request.AddParameter("offset", "");
                IRestResponse response = client.Execute(request);
                try
                {
                    // Update apitoken, everything supergud now
                    model = JsonConvert.DeserializeObject<ManageSims>(response.Content);
                    return model;
                }
                catch (Exception ex)
                {
                    logger.LogInformation("Did we have IP Access?");
                    logger.LogError(ex.Message);
                    logger.LogDebug(ex.StackTrace);
                    return model;
                }

            }
            catch (Exception ex)
            {
                logger.LogInformation("Mostlikely bad credentials");
                logger.LogError(ex.Message);
                logger.LogDebug(ex.StackTrace);
                return model;
            }
        }

        public SimCard ActivateSimCard(string iccid)
        {
            string localApiToken = string.Empty;
            SimCard model = new SimCard();
            try
            {
                localApiToken = localSettings.Values["apiToken"].ToString();

                var client = new RestClient("https://api.terminal.1ot.mobi/v1/");
                var request = new RestRequest("activate", Method.PUT);
                request.AddParameter("access_token", localApiToken);
                request.AddParameter("iccid", iccid);
                IRestResponse response = client.Execute(request);
                try
                {
                    // Update apitoken, everything supergud now
                    model = JsonConvert.DeserializeObject<SimCard>(response.Content);
                    return model;
                }
                catch (Exception ex)
                {
                    logger.LogInformation("Did we have IP Access?");
                    logger.LogError(ex.Message);
                    logger.LogDebug(ex.StackTrace);
                    return model;
                }

            }
            catch (Exception ex)
            {
                logger.LogInformation("Mostlikely bad credentials");
                logger.LogError(ex.Message);
                logger.LogDebug(ex.StackTrace);
                return model;
            }
        }

        public SimCard DeActivateSimCard(string iccid)
        {
            string localApiToken = string.Empty;
            SimCard model = new SimCard();
            try
            {
                localApiToken = localSettings.Values["apiToken"].ToString();

                var client = new RestClient("https://api.terminal.1ot.mobi/v1/");
                var request = new RestRequest("deactivate", Method.PUT);
                request.AddParameter("access_token", localApiToken);
                request.AddParameter("iccid", iccid);
                IRestResponse response = client.Execute(request);
                try
                {
                    // Update apitoken, everything supergud now
                    model = JsonConvert.DeserializeObject<SimCard>(response.Content);
                    return model;
                }
                catch (Exception ex)
                {
                    logger.LogInformation("Did we have IP Access?");
                    logger.LogError(ex.Message);
                    logger.LogDebug(ex.StackTrace);
                    return model;
                }

            }
            catch (Exception ex)
            {
                logger.LogInformation("Mostlikely bad credentials");
                logger.LogError(ex.Message);
                logger.LogDebug(ex.StackTrace);
                return model;
            }
        }

        public SimCard SetDataLimit(string iccid, int limit)
        {
            string localApiToken = string.Empty;
            SimCard model = new SimCard();
            try
            {
                localApiToken = localSettings.Values["apiToken"].ToString();

                var client = new RestClient("https://api.terminal.1ot.mobi/v1/");
                var request = new RestRequest("set_data_limit", Method.PUT);
                request.AddParameter("access_token", localApiToken);
                request.AddParameter("iccid", iccid);
                request.AddParameter("limit", limit.ToString());
                IRestResponse response = client.Execute(request);
                try
                {
                    // Update apitoken, everything supergud now
                    model = JsonConvert.DeserializeObject<SimCard>(response.Content);
                    return model;
                }
                catch (Exception ex)
                {
                    logger.LogInformation("Did we have IP Access?");
                    logger.LogError(ex.Message);
                    logger.LogDebug(ex.StackTrace);
                    return model;
                }

            }
            catch (Exception ex)
            {
                logger.LogInformation("Mostlikely bad credentials");
                logger.LogError(ex.Message);
                logger.LogDebug(ex.StackTrace);
                return model;
            }
        }
    }
}
