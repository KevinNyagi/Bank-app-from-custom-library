using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 using System.Net.Http;

namespace FirebaseNet.Database
{
   
    public class FirebaseRequest
    {
        private const string JSON_SUFFIX = ".json";

        /// <summary>  
        /// Initializes a new instance of the <see cref="FirebaseRequest"/> class  
        /// </summary>  
        /// <param name="method">HTTP Method</param>  
        /// <param name="uri">URI of resource</param>  
        /// <param name="jsonString">JSON string</param>  
        public FirebaseRequest(HttpMethod method, string uri, string jsonString = null)
        {
            this.Method = method;
            this.JSON = jsonString;
            if (uri.Replace("/", string.Empty).EndsWith("firebaseio.com"))
            {
                this.Uri = uri + '/' + JSON_SUFFIX;
            }
            else
            {
                this.Uri = uri + JSON_SUFFIX;
            }
        }

      
        /// Gets or sets HTTP Method  
       
        private HttpMethod Method { get; set; }

      
        /// Gets or sets JSON string  
        
        private string JSON { get; set; }

        
        /// Gets or sets URI  
    
        private string Uri { get; set; }

     
        /// Executes a HTTP requests  
     
        /// <returns>Firebase Response</returns>  
        public FirebaseResponse Execute()
        {
            Uri requestURI;
            if (UtilityHelper.ValidateURI(this.Uri))
            {
                requestURI = new Uri(this.Uri);
            }
            else
            {
                return new FirebaseResponse(false, "Proided Firebase path is not a valid HTTP/S URL");
            }

            string json = null;
            if (this.JSON != null)
            {
                if (!UtilityHelper.TryParseJSON(this.JSON, out json))
                {
                    return new FirebaseResponse(false, string.Format("Invalid JSON : {0}", json));
                }
            }

            var response = UtilityHelper.RequestHelper(this.Method, requestURI, json);
            response.Wait();
            var result = response.Result;

            var firebaseResponse = new FirebaseResponse()
            {
                HttpResponse = result,
                ErrorMessage = result.StatusCode.ToString() + " : " + result.ReasonPhrase,
                Success = response.Result.IsSuccessStatusCode
            };

            if (this.Method.Equals(HttpMethod.Get))
            {
                var content = result.Content.ReadAsStringAsync();
                content.Wait();
                firebaseResponse.JSONContent = content.Result;
            }

            return firebaseResponse;
        }
    }
}
