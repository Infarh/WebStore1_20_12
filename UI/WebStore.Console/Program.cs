using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Products;

namespace WebStore.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var http = new HttpClient();
            var client = new swaggerClient("http://localhost:5001", http);

            var products = await client.ProductsAllAsync(new ProductFilter());

            foreach (var product in products)
                System.Console.WriteLine(product.Name);

            System.Console.WriteLine("------------");

            var configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();

            var products_client = new ProductsClient(configuration);

            var pp = products_client.GetProducts();

            foreach (var product in pp.Products)
                System.Console.WriteLine(product.Name);


        }
    }
}
