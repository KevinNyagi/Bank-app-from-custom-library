using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace FirebaseNet.Database
{
    using System.Net.Http;
    public class FirebaseResponse
    {
        public FirebaseResponse()
        {
        }

        
        /// Initializes a new instance of the <see cref="FirebaseResponse"/> class  
      
        /// <param name="success">If Success</param>  
        /// <param name="errorMessage">Error Message</param>  
        /// <param name="httpResponse">HTTP Response</param>  
        /// <param name="jsonContent">JSON Content</param>  
        public FirebaseResponse(bool success, string errorMessage, HttpResponseMessage httpResponse = null, string jsonContent = null)
        {
            this.Success = success;
            this.JSONContent = jsonContent;
            this.ErrorMessage = errorMessage;
            this.HttpResponse = httpResponse;
        }

  
        /// Gets or sets Boolean status of Success  

        public bool Success { get; set; }

        
        /// Gets or sets JSON content returned by the Request  
     
        public string JSONContent { get; set; }

        
        /// Gets or sets Error Message if Any  
      
        public string ErrorMessage { get; set; }

     
        /// Gets or sets full Http Response  
     
        public HttpResponseMessage HttpResponse { get; set; }
    }
}
