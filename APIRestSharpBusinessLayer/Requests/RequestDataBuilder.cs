using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIRestSharpBusinessLayer.Requests
{
    public class RequestDataBuilder
    {
        private RequestData requestData;
        public RequestDataBuilder() 
        {
            requestData = new RequestData();
        }
        public RequestDataBuilder AddUserID(int userId)
        {
            requestData.userId = userId;
            return this;
        }
        public RequestDataBuilder AddID(int id)
        {
            requestData.id = id;
            return this;
        }
        public RequestDataBuilder AddTitle(string title)
        {
            requestData.title = title;
            return this;
        }
        public RequestDataBuilder AddBody(string body)
        {
            requestData.body = body;
            return this;
        }

        public RequestData Build()
        {
            return requestData;
        }
    }
}
