using Amazon.Kinesis;
using Amazon.Kinesis.Model;
using System.Text;
using System.Text.Json;

namespace YandexCloud.DataStreams
{
    public static class DataStreamsClientExtension
    {
    
        public static async Task<List<PutRecordsResponse>> PutRecords<T>(this DataStreamsClient client, IEnumerable<T> entries)
        {
            List<PutRecordsResponse> AllResponces = new List<PutRecordsResponse>();
            var kinesisClient = new AmazonKinesisClient(client.dataStreamConfiguration.KeyId, client.dataStreamConfiguration.Secret, client.config);
            var butches = client.CreateButch(entries);
            for (var i = 0; i < butches.Count; i++)
            {
                var requestRecords = new PutRecordsRequest
                {
                    StreamName = client.dataStreamConfiguration.StreamPath,
                    Records = butches[i]
                };
                var responces = await kinesisClient.PutRecordsAsync(requestRecords);
                AllResponces.Add(responces);               
            }
            return AllResponces;
        }
        private static List<List<PutRecordsRequestEntry>> CreateButch<T>(this DataStreamsClient client, IEnumerable<T> entries)
        {
            var count = entries.Count() / 500 + 1;
            var butches = new List<List<PutRecordsRequestEntry>>();
            int i = 0;
            while (i < count)
            {
                List<PutRecordsRequestEntry> list = new(500);
                var take = entries.Take(new Range(i * 500, i * 500 + 500)).ToArray();              
                i++;
                for(var j = 0; j < take.Length; j++)
                {
                    var data = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(take[j]));                    
                    var record = new PutRecordsRequestEntry()
                    {
                        Data = new MemoryStream(data),
                        PartitionKey = client.dataStreamConfiguration.PartitionKey
                    };
                    RecordSizeException.Ensure(record.Data.Capacity, j);
                    list.Add(record);
                }
                RecordsSizeException.Ensure(list.Capacity);
                butches.Add(list);
            }
            return butches;
        }
    }
}