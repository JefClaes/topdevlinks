using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Elmah;
using System.Configuration;
using System.Collections;

namespace TopDevLinks.Infrastructure
{
    public class AppHbMongoErrorLog : MongoErrorLog
    {
        public AppHbMongoErrorLog(IDictionary config) : base(config) { } 

        public override string  GetConnectionString(IDictionary config)
        {
 	        return ConfigurationManager.AppSettings["MONGOLAB_URI"] ?? base.GetConnectionString(config);
        }      
    }
}