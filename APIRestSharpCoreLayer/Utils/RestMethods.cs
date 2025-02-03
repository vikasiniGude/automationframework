using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using static APIRestSharpCoreLayer.Utils.CustomException;

namespace APIRestSharpCoreLayer.Utils
{
    public class RestMethods
    {
        private string _url;
        private string _username;
        private string _password;
        private RestClient _client;
        private RestRequest _request;
        public RestMethods(string url, string user, string pwd)
        {
            _url = url;
            _username = user;
            _password = pwd;
        }
        public RestClient RestClient
        {
            get
            {
                if (!string.IsNullOrEmpty(_username))
                {
                    var options = new RestClientOptions(_url)
                    {
                        Authenticator = new RestSharp.Authenticators.HttpBasicAuthenticator(_username, _password)
                    };
                    _client = new RestClient(options);

                }
                else
                    _client = new RestClient(_url);
                return _client;
            }
        }
        public RestRequest CreateRequest(string resource, Method method, DataFormat format, string header = null, string token = null, string data = null)
        {
            try
            {
                _request = new RestRequest(resource, method);
                _request.RequestFormat = format;
                if (header != null)
                    _request.AddHeader(header, token);
                if (data != null)
                    _request.AddBody(data);
                _request.AddHeader("Content-type", "application/json");
                _request.AddHeader("accept", "/*/");
                return _request;
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Failed at CreateRequest - {ex.Message}");
                throw;
            }
        }
        public T? GETWithRestClient<T>(string resource, DataFormat format, HttpStatusCode expectedCode, string header = null, string token = null)
        {
            try
            {
                RestRequest restrequest = CreateRequest(resource, Method.Get, format, header, token);
                var response = RestClient.Execute(restrequest);
                ValidateStatusCode(expectedCode, response.StatusCode);
                if (response.Content == null)
                    throw new NullReferenceException($"response contentis null for {_url}/{resource}");
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Failed at GETWithRestClient - {ex.Message}");
                throw;
            }
        }
        public T? POSTWithRestClient<T>(string resource, DataFormat format, string data, HttpStatusCode expectedCode, string header = null, string token = null)
        {
            try
            {
                RestRequest restrequest = CreateRequest(resource, Method.Post, format, header, token, data);
                var response = RestClient.Execute(restrequest);
                ValidateStatusCode(expectedCode, response.StatusCode);
                if (response.Content == null)
                    throw new NullReferenceException($"response contentis null for {_url}/{resource}");
                return JsonConvert.DeserializeObject<T>(response.Content);

            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Failed at POSTWithRestClient - {ex.Message}");
                throw;
            }
        }
        public T? PUTWithRestClient<T>(string resource, DataFormat format, string data, HttpStatusCode expectedCode, string header = null, string token = null)
        {
            try
            {
                RestRequest restrequest = CreateRequest(resource, Method.Put, format, header, token, data);
                var response = RestClient.Execute(restrequest);
                ValidateStatusCode(expectedCode, response.StatusCode);
                if (response.Content == null)
                    throw new NullReferenceException($"response contentis null for {_url}/{resource}");
                return JsonConvert.DeserializeObject<T>(response.Content);
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Failed at PUTWithRestClient - {ex.Message}");
                throw;
            }
        }
        public bool DELETEWithRestClient(string resource, HttpStatusCode expectedCode, string header = null, string token = null)
        {
            try
            {
                RestRequest restrequest = CreateRequest(resource, Method.Delete, DataFormat.None, header, token);
                var response = RestClient.Execute(restrequest);
                ValidateStatusCode(expectedCode, response.StatusCode);
                return true;
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Failed at DELETEWithRestClient - {ex.Message}");
                throw;
            }
        }

        public void ValidateStatusCode(HttpStatusCode expectedStatus, HttpStatusCode actualStatus)
        {
            try
            {
                if (actualStatus != expectedStatus)
                    throw new StatusCodeMismatchException($"Status code mismatch: Expected - {expectedStatus} but actual- {actualStatus}");
            }
            catch (StatusCodeMismatchException ex)
            {
                Log4NetLogger.Error($"Status code mismatch: Expected - {expectedStatus} but actual- {actualStatus} - {ex.Message}");
                throw;
            }
        }
    }

}
