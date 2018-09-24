using System;
using Newtonsoft.Json;
using RestSharp;
using _1oT.Models;
using MetroLog;
using MetroLog.Targets;

namespace _1oT.Services
{
    public class DataService
    {
        private ILogger logger = LogManagerFactory.DefaultLogManager.GetLogger<DataService>();
        Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
        Windows.Storage.StorageFolder localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;

        public void Authorize()
        {
            string localApiUserName = string.Empty;
            string localApiPasword = string.Empty;
            string refreshToken = string.Empty;
            localApiUserName = localSettings.Values["apiUsername"].ToString();
            localApiPasword = localSettings.Values["apiPassword"].ToString();
            refreshToken = localSettings.Values["refreshToken"].ToString();

            if (localApiUserName != string.Empty && localApiPasword != string.Empty)
            {
                var client = new RestClient("https://api.terminal.1ot.mobi/v1/");
                var request = new RestRequest("oauth/token", Method.POST);
                request.AddParameter("grant_type", "password");
                request.AddParameter("username", localApiUserName);
                request.AddParameter("client_id", localApiUserName);
                request.AddParameter("password", localApiPasword);
                request.AddParameter("refresh_token", refreshToken);
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
                    logger.Info("Did we have IP Access? Authorize Call");
                    logger.Error(ex.Message);
                    logger.Debug(ex.StackTrace);


                    logger.Info("Rerfreshing token just in case");
                    localSettings.Values["refreshToken"] = "1";
                }
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
                    model = JsonConvert.DeserializeObject<ManageSims>(response.Content);
                    if (model == null)
                    {
                        localSettings.Values["refreshToken"] = "1";
                    }
                    return model;
                }
                catch (Exception ex)
                {
                    logger.Info("Did we have IP Access? GetSimCards");
                    logger.Error(ex.Message);
                    logger.Debug(ex.StackTrace);
                    return model;
                }
            }
            catch (Exception ex)
            {
                logger.Info("Mostlikely bad credentials GetSimCards");
                logger.Error(ex.Message);
                logger.Debug(ex.StackTrace);
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
                    model = JsonConvert.DeserializeObject<SimCard>(response.Content);
                    if (model == null)
                    {
                        localSettings.Values["refreshToken"] = "1";
                    }
                    return model;
                }
                catch (Exception ex)
                {
                    logger.Info("Did we have IP Access? ActivateSimCard");
                    logger.Error(ex.Message);
                    logger.Debug(ex.StackTrace);
                    return model;
                }
            }
            catch (Exception ex)
            {
                logger.Info("Mostlikely bad credentials ActivateSimCard");
                logger.Error(ex.Message);
                logger.Debug(ex.StackTrace);
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
                    model = JsonConvert.DeserializeObject<SimCard>(response.Content);
                    if (model == null)
                    {
                        localSettings.Values["refreshToken"] = "1";
                    }
                    return model;
                }
                catch (Exception ex)
                {
                    logger.Info("Did we have IP Access? DeActivateSimCard");
                    logger.Error(ex.Message);
                    logger.Debug(ex.StackTrace);
                    return model;
                }
            }
            catch (Exception ex)
            {
                logger.Info("Mostlikely bad credentials DeActivateSimCard");
                logger.Error(ex.Message);
                logger.Debug(ex.StackTrace);
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
                    model = JsonConvert.DeserializeObject<SimCard>(response.Content);
                    if (model == null)
                    {
                        localSettings.Values["refreshToken"] = "1";
                    }
                    return model;
                }
                catch (Exception ex)
                {
                    logger.Info("Did we have IP Access? SetDataLimit");
                    logger.Error(ex.Message);
                    logger.Debug(ex.StackTrace);
                    return model;
                }
            }
            catch (Exception ex)
            {
                logger.Info("Mostlikely bad credentials SetDataLimit");
                logger.Error(ex.Message);
                logger.Debug(ex.StackTrace);
                return model;
            }
        }
    }
}
