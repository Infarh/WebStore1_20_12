using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebStore.Infrastructure.Middleware
{
    public class TestMiddleware
    {
        private readonly RequestDelegate _Next;

        public TestMiddleware(RequestDelegate Next) => _Next = Next;

        public async Task InvokeAsync(HttpContext context)
        {
            // Выполнить действия в свою очередь перед следующим звеном конвейера

            //await context.Response.WriteAsync("Hello World");

            await _Next(context);

            //context.Response.Clear();

            // Выполнить действия после того, как оставшаяся часть конвейера отработает
        }
    }
}
