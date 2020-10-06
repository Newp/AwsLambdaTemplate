
using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

namespace AwsLambdaTemplate.Services
{

    public class DynamoDbService
    {
        public AmazonDynamoDBClient Client { get; protected set; }
        public DynamoDBContext Context { get; protected set; }
        public DynamoDbService(RegionEndpoint region)
        {

            AmazonDynamoDBConfig config = new AmazonDynamoDBConfig()
            {
                ServiceURL = "http://localhost:8000/",
                Timeout = TimeSpan.FromSeconds(3),
                
            };

            Client = new AmazonDynamoDBClient("anonymous", "anonymous", config);

            Context = new DynamoDBContext(Client);
            RegisterConverter();
        }

        protected void RegisterConverter()
        {
            // Context.ConverterCache[typeof(DateTimeOffset)] = new DateTimeOffsetAsSConverter();
        }

        Table GetTable(string name)
        {
            var table = Table.LoadTable(Client, name);

            return table;
        }

        public async Task<bool> PutItem(string tableName, object obj)
        {

            var table = this.GetTable(tableName);
            Document doc = ToDocument(obj);

            var result = await table.PutItemAsync(doc);

            return true;
        }


        Document ToDocument(object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var result = Document.FromJson(json);

            return result;
        }
    }

    public class DateTimeOffsetAsSConverter : IPropertyConverter
    {
        public DynamoDBEntry ToEntry(object value)
        {
            var bookDimensions = value as DateTimeOffset?;
            if (bookDimensions == null) return new Primitive();

            var data = bookDimensions.Value.ToString("o");

            DynamoDBEntry entry = new Primitive
            {
                Value = data
            };
            return entry;
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            var primitive = entry as Primitive;
            if (primitive == null || !(primitive.Value is String) || string.IsNullOrEmpty((string)primitive.Value))
                throw new ArgumentOutOfRangeException();

            var value = primitive.Value as string;

            return new DateTimeOffset(DateTime.Parse(value));
        }
    }
}
