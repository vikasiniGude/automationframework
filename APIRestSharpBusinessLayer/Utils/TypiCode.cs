using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using APIRestSharpBusinessLayer.Requests;
using APIRestSharpCoreLayer.Utils;
using Newtonsoft.Json;

namespace APIRestSharpBusinessLayer.Utils
{
    public class TypiCode
    {
        public static RestMethods restMethods;
        public TypiCode() 
        {
            restMethods = new RestMethods(AppSettings.GetAppSettingData("url"), AppSettings.GetAppSettingData("username"), AppSettings.GetAppSettingData("password"));
        }
        //get   
        public static T GetUserDataForUserId<T>(int userId)
        {
            try
            {
                var responseData = restMethods.GETWithRestClient<T>($"/posts/{userId}", RestSharp.DataFormat.Json, HttpStatusCode.OK);
                return responseData;
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Failed at BL - GetUserDataForUserId - {ex.Message}");
                throw ex;
            }
        }

        //Create
        public static RequestData CreateUserData(int userId, int id, string title, string body)
        {
            try
            {
                var requestBody = new RequestDataBuilder()
                                    .AddUserID(userId)
                                    .AddID(id)
                                    .AddTitle(title)
                                    .AddBody(body).Build();
                string requestData = JsonConvert.SerializeObject(requestBody);
                var response = restMethods.POSTWithRestClient<RequestData>("/posts", RestSharp.DataFormat.Json, requestData, HttpStatusCode.Created);
                return response;
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Failed at BL - CreateUserData - {ex.Message}");
                throw;
            }
        }

        ////put
        public static string UpdateUserDataForUserId(int userId, string title)
        {
            try
            {
                RequestData data = GetUserDataForUserId<RequestData>(userId);
                data.title = title;
                string requestData = JsonConvert.SerializeObject(data);
                var response = restMethods.PUTWithRestClient<RequestData>($"/posts/{userId}", RestSharp.DataFormat.Json, requestData, HttpStatusCode.OK);
                return response.title;
            }
            catch (Exception ex)
            {
                Log4NetLogger.Error($"Failed at BL - CreateUserData - {ex.Message}");
                throw;
            }
        }

        //Delete
        public static bool DeletePost(string userId, HttpStatusCode httpStatusCode)
        {
            return restMethods.DELETEWithRestClient($"posts/{userId}", httpStatusCode);
        }


    }
}

