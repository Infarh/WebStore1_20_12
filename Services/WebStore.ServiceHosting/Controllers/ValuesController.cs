using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;

namespace WebStore.ServiceHosting.Controllers
{
    [Route("api/[controller]")] // http://localhost:5001/api/values
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private static readonly List<string> __Values = Enumerable
           .Range(1, 10)
           .Select(i => $"Value{i:00}")
           .ToList();

        [HttpGet] // http://localhost:5001/api/values
        public IEnumerable<string> Get() => __Values;

        [HttpGet("{id}")] // http://localhost:5001/api/values/5
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __Values.Count)
                return NotFound();

            return __Values[id];
        }

        [HttpPost]
        [HttpPost("add")] //http://localhost:5001/api/values/add
        public ActionResult Post([FromBody] string value)
        {
            __Values.Add(value);
            return Ok();
        }

        [HttpPut("{id}")]
        [HttpPut("edit/{id}")] //http://localhost:5001/api/values/edit/5
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __Values.Count)
                return NotFound();

            __Values[id] = value;

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __Values.Count)
                return NotFound();

            __Values.RemoveAt(id);

            return Ok();
        }
    }
}
