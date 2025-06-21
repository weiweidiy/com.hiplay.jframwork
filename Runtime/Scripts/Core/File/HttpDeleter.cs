using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JFramework.Common.Interface;

namespace JFramework.Common
{
    public class HttpDeleter : IDelete, IDeleteAsync
    {

        private IHttpRequest _webRequest;

        public HttpDeleter(IHttpRequest request)
        {
            _webRequest = request;
        }

        public void Delete(string location)
        {
            _webRequest.Delete(location);
        }

        public async Task DeleteAsync(string location)
        {
            try
            {
                await _webRequest.DeleteAsync(location);
            }
            catch(Exception e)
            {
                throw e;
            }
            
        }

    }
}
