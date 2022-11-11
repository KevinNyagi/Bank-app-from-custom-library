using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace FirebaseNet.Database
{
    public class FirebaseDB
    {
        /// Initializes a new instance of the <see cref="FirebaseDB"/> class with base url of Firebase Database  
        public FirebaseDB(string baseURL)
        {
            this.RootNode = baseURL;
        }
        /// Gets or sets Represents current full path of a Firebase Database resource  
      
        private string RootNode { get; set; }

        /// Adds more node to base URL 
        public FirebaseDB Node(string node)
        {
            if (node.Contains("/"))
            {
                throw new FormatException("Node must not contain '/', use NodePath instead.");
            }

            return new FirebaseDB(this.RootNode + '/' + node);
        }

        public FirebaseDB NodePath(string nodePath)
        {
            return new FirebaseDB(this.RootNode + '/' + nodePath);
        }
        public FirebaseResponse Get()
        {
            return new FirebaseRequest(HttpMethod.Get, this.RootNode).Execute();
        }
        public FirebaseResponse Get(int account)
        {
            return new FirebaseRequest(HttpMethod.Get, this.RootNode, account.ToString()).Execute();
        }
        public FirebaseResponse Put(string jsonData)
        {
            return new FirebaseRequest(HttpMethod.Put, this.RootNode, jsonData).Execute();
        }

        public FirebaseResponse Post(string jsonData)
        {
            return new FirebaseRequest(HttpMethod.Post, this.RootNode, jsonData).Execute();
        }
        public FirebaseResponse Patch(string jsonData)
        {
            return new FirebaseRequest(new HttpMethod("PATCH"), this.RootNode, jsonData).Execute();
        }
        public FirebaseResponse Delete()
        {
            return new FirebaseRequest(HttpMethod.Delete, this.RootNode).Execute();
        }
        public override string ToString()
        {
            return this.RootNode;
        }
    }
}
