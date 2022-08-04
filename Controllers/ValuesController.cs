using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Runtime.Caching;

namespace forapi.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public string Get()
        {
            Students student = new Students("", 0, ""); //object to access studentlist method//
            List<Students> studentsList = student.StudentsList(); //studentsList stores the returned value//
            ObjectCache cache = MemoryCache.Default;

            if (cache["students"] == null)
            {
                var cacheItemPolicy = new CacheItemPolicy
                {
                    AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)
                };
                var cacheItem = new CacheItem("students", studentsList);
                cache.Add(cacheItem, cacheItemPolicy);
            }


            JavaScriptSerializer js = new JavaScriptSerializer();


            return js.Serialize((List<Students>)cache.Get("students")); //get returns all the data in json string format//
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
