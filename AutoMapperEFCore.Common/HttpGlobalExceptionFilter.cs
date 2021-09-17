using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoMapperEFCore.Common
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        readonly IHostingEnvironment _env;

        JsonSerializerSettings _jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        };

        public HttpGlobalExceptionFilter(IHostingEnvironment env)
        {
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var requestParameters = JsonConvert.SerializeObject(context.HttpContext.Items.Values);
            LogService.Instance.Error("全局异常处理：服务器内部错误", context.Exception, requestParameters);

            ActionResult<string> actionResult = new ActionResult<string>("服务器内部错误：" + context.Exception.ToString());

            context.Result = new ContentResult
            {
                Content = JsonConvert.SerializeObject(actionResult, Formatting.Indented, _jsonSerializerSettings),
                StatusCode = StatusCodes.Status200OK
            };

            context.ExceptionHandled = true;//异常已处理
        }
    }
}
