using System;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers.API
{
    [Route("api/console")]
    [ApiController]
    public class ConsoleApiController : ControllerBase
    {
        [HttpGet("clear")]
        public void Clear() => Console.Clear();

        [HttpGet("writeline/{str}")]
        public void WriteLine(string str) => Console.WriteLine(str);
    }
}
