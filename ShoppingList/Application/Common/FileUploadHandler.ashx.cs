using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShoppingList.Application.Common
{
    /// <summary>
    /// Summary description for FileUploadHandler1
    /// </summary>
    public class FileUploadHandler1 : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}