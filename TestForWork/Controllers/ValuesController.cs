using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using Microsoft.Rest.TransientFaultHandling;
using TestForWork.Classes;
using TestForWork.Func;

namespace TestForWork.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<Window> Get()
        {

            return OpenWindowGetter.GetListOfWindows();
        }

        // GET api/values/5
        [HttpGet("{name}")]
        public Window Get(string name)
        {
            var listofwind = OpenWindowGetter.GetListOfWindows();
            var findedname = listofwind.First(a => a.Name == name);
            if (findedname!=null)
            {
                return findedname;
            }
            throw new HttpRequestWithStatusException("Erorr 404  can't find name");
        }

        // POST api/values
      /*  [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
