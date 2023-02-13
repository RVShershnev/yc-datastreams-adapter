using System.Text.Json;
using RVShershnev.YandexCloud.DataStreams;

namespace YandexCloud.DataStreams.Demo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = JsonSerializer.Deserialize<DataStreamsConfiguration>(File.ReadAllText("Configuration.json"));
            if (configuration == null)
            {
                return;
            }
            using (var client = new DataStreamsClient(configuration))
            {
                var orders = new List<DifferentType>()
                {
                    new DifferentType
                    {
                        Int = 1,
                        Long = 1,
                        Bool = true,
                        Byte = 1,
                        DateTime = DateTime.Now,
                        Decimal = 1,
                        Double = 1.99,
                        Float = 1.99f,
                        String = "string",
                        Char = '+'                        
                    },              
                };
                await client.PutRecords(orders);
            }

            using (var client = new DataStreamsClient(configuration))
            {
                var orders = new List<Order>()
                {
                    new Order
                    {
                        Id = 1,
                        Title = "Book"
                    },
                    new Order
                    {
                        Id = 2,
                        Title = "Phone"
                    },
                    new Order
                    {
                        Id = 3,
                        Title = "Game"
                    },
                };
                await client.PutRecords(orders);
            }
        }
    }
}