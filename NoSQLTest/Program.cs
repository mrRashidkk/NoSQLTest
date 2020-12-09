using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using NoSQLTest.Entities;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using MongoDB.Bson.Serialization.Conventions;
using System.Diagnostics;

namespace NoSQLTest
{
    class Program
    {
        static string connectionString = "mongodb://localhost:27017";
        static AppDbContext context;
        static IMongoDatabase database;

        static Guid employeesListId = new Guid("4E44C21B-CC47-4A4A-5B59-08D89C0C884C");

        static void Main(string[] args)
        {
            context = new AppDbContext();
            Console.WriteLine("Context created");

            MongoClient client = new MongoClient(connectionString);
            database = client.GetDatabase("nosqltest");

            long[] results = new long[100];
            for (int i = 0; i < results.Length; i ++)
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                var items = GetAllItems("Employees");
                sw.Stop();
                results[i] = sw.ElapsedMilliseconds;
            }
            var average = results.Average();
            Console.WriteLine($"Average = {average}");


            Console.WriteLine("Finish");
            Console.ReadLine();
        }

        static void ShowItemsSorted(Guid listId, string attrName)
        {
            var list = context.EntityTypes.FirstOrDefault(x => x.Id == listId);
            var collection = database.GetCollection<BsonDocument>(list.Label);

            var sorted = collection.Find(new BsonDocument()).Sort("{" + $"{attrName}" + ":1}").ToList();
            sorted.ForEach(s => Console.WriteLine(s));
        }

        static void ShowItemsSearched(string collectionName, SearchQuery searchQuery)
        {
            var items = FindItems(collectionName, searchQuery);
            items.ForEach(x => Console.WriteLine(x));
        }

        static List<BsonDocument> SortItems(string collecationName, string attrName)
        {
            var collection = database.GetCollection<BsonDocument>(collecationName);
            return collection.Find(new BsonDocument()).Sort("{" + $"{attrName}" + ":1}").ToList();
        }

        static void FillEmployeesList()
        {
            List<object> employees = new List<object>();
            Random rnd = new Random();
            DateTime now = DateTime.Now;
            string userName = "System";

            string[] names = new string[] { "Petya", "Vanya", "Max", "Anna" };
            int[] ages = new int[] { 23, 90, 7, 55 };
            DateTime[] bdays = new DateTime[]
            {
                new DateTime(2019, 9, 5),
                new DateTime(2016, 7, 5),
                new DateTime(1987, 12, 1),
                new DateTime(1945, 5, 9)
            };
            for(int i = 0; i < 10000; i++)
            {
                employees.Add(new
                {
                    Age = ages[rnd.Next(4)],
                    Name = names[rnd.Next(4)],
                    BirthDay = bdays[rnd.Next(4)],
                    Created = now,
                    Modified = now,
                    CreatedBy = userName,
                    ModifiedBy = userName,
                    EntityTypeId = employeesListId,
                    String1 = names[rnd.Next(4)],
                    String2 = names[rnd.Next(4)],
                    String3 = names[rnd.Next(4)],
                    String4 = names[rnd.Next(4)],
                    String5 = names[rnd.Next(4)],
                    String6 = names[rnd.Next(4)],
                    String7 = names[rnd.Next(4)],
                    String8 = names[rnd.Next(4)],
                    String9 = names[rnd.Next(4)],
                    String10 = names[rnd.Next(4)],
                    String11 = names[rnd.Next(4)],
                    String12 = names[rnd.Next(4)],
                    String13 = names[rnd.Next(4)],
                    String14 = names[rnd.Next(4)],
                    String15 = names[rnd.Next(4)],
                    String16 = names[rnd.Next(4)],
                    String17 = names[rnd.Next(4)],
                    String18 = names[rnd.Next(4)],
                    String19 = names[rnd.Next(4)],
                    String20 = names[rnd.Next(4)],
                    Int1 = ages[rnd.Next(4)],
                    Int2 = ages[rnd.Next(4)],
                    Int3 = ages[rnd.Next(4)],
                    Int4 = ages[rnd.Next(4)],
                    Int5 = ages[rnd.Next(4)],
                    Int6 = ages[rnd.Next(4)],
                    Int7 = ages[rnd.Next(4)],
                    Int8 = ages[rnd.Next(4)],
                    Int9 = ages[rnd.Next(4)],
                    Int10 = ages[rnd.Next(4)],
                    Int11 = ages[rnd.Next(4)],
                    Int12 = ages[rnd.Next(4)],
                    Int13 = ages[rnd.Next(4)],
                    Int14 = ages[rnd.Next(4)],
                    Int15 = ages[rnd.Next(4)],
                    DateTime1 = bdays[rnd.Next(4)],
                    DateTime2 = bdays[rnd.Next(4)],
                    DateTime3 = bdays[rnd.Next(4)],
                    DateTime4 = bdays[rnd.Next(4)],
                    DateTime5 = bdays[rnd.Next(4)],
                    DateTime6 = bdays[rnd.Next(4)],
                    DateTime7 = bdays[rnd.Next(4)],
                    DateTime8 = bdays[rnd.Next(4)],
                    DateTime9 = bdays[rnd.Next(4)],
                    DateTime10 = bdays[rnd.Next(4)],
                    DateTime11 = bdays[rnd.Next(4)],
                    DateTime12 = bdays[rnd.Next(4)],
                    DateTime13 = bdays[rnd.Next(4)],
                    DateTime14 = bdays[rnd.Next(4)],
                    DateTime15 = bdays[rnd.Next(4)],
                });
            }

            var employeesJson = JsonConvert.SerializeObject(employees);

            AddItems("Employees", employeesJson);
        }

        static List<BsonDocument> FindItems(string collecationName, SearchQuery searchQuery)
        {
            var collection = database.GetCollection<BsonDocument>(collecationName);

            var filter = Builders<BsonDocument>.Filter
                .Eq(searchQuery.AttributeInnerLabel, new BsonRegularExpression(searchQuery.Value));

            return collection.Find(filter).ToList();
        }

        static List<BsonDocument> GetAllItems(string collecationName)
        {
            var collection = database.GetCollection<BsonDocument>(collecationName);
            return collection.Find(_ => true).ToList();
        }

        static List<BsonDocument> GetItems(string collectionName)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);

            var filter = new BsonDocument();
            return collection.Find(filter).ToList();
        }

        static void UpdateItem<AttrType>(Guid listId, string id, string attrName, AttrType attValue)
        {
            var list = context.EntityTypes.FirstOrDefault(x => x.Id == listId);
            var collection = database.GetCollection<BsonDocument>(list.Label);

            var filter = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));

            var update = Builders<BsonDocument>.Update.Set(attrName, attValue);

            collection.UpdateOne(filter, update);
        }

        static string GetItemById(Guid listId, string id)
        {
            var list = context.EntityTypes.FirstOrDefault(x => x.Id == listId);
            var collection = database.GetCollection<BsonDocument>(list.Label);

            var filter_id = Builders<BsonDocument>.Filter.Eq("_id", ObjectId.Parse(id));
            var entity = collection.Find(filter_id).FirstOrDefault();
            return entity.ToString();
        }

        static void AddItems(string collectionName, string jsonItems)
        {
            var collection = database.GetCollection<BsonDocument>(collectionName);

            var arrayDocs = BsonSerializer.Deserialize<BsonArray>(jsonItems);
            var documents = arrayDocs.Select(val => val.AsBsonDocument);

            collection.InsertMany(documents);
        }

        static void AddItem(Guid listId, string jsonItem)
        {
            var list = context.EntityTypes.FirstOrDefault(x => x.Id == listId);

            var collection = database.GetCollection<BsonDocument>(list.Label);

            BsonDocument item = BsonDocument.Parse(jsonItem);
            item.Add(new BsonElement("Created", DateTime.Now));

            collection.InsertOne(item);
        }

        static void AddAttributes(Guid listId, IEnumerable<EntityAttribute> attributes)
        {
            List<EntityAttribute> entityAttributes = attributes
                .Select(x => new EntityAttribute
                {
                    Id = new Guid(),
                    Label = x.Label,
                    AttributeTypeId = x.AttributeTypeId,
                    EntityTypeId = listId,
                    InnerLabel = string.Join("", x.Label.Split(' ')).Trim()
                })
                .ToList();

            context.EntityAttributes.AddRange(entityAttributes);
            context.SaveChanges();
        }

        private static async Task GetCollectionsNames(MongoClient client)
        {
            using (var cursor = await client.ListDatabasesAsync())
            {
                var dbs = await cursor.ToListAsync();
                foreach (var db in dbs)
                {
                    Console.WriteLine("В базе данных {0} имеются следующие коллекции:", db["name"]);

                    IMongoDatabase database = client.GetDatabase(db["name"].ToString());

                    using (var collCursor = await database.ListCollectionsAsync())
                    {
                        var colls = await collCursor.ToListAsync();
                        foreach (var col in colls)
                        {
                            Console.WriteLine(col["name"]);
                        }
                    }
                    Console.WriteLine();
                }
            }
        }

        static void AddList(string title)
        {
            EntityType list = new EntityType
            {
                Id = new Guid(),
                Label = title
            };
            context.EntityTypes.Add(list);
            context.SaveChanges();

            List<EntityAttribute> systemAttributes = new List<EntityAttribute>()
            {
                new EntityAttribute
                {
                    Id = new Guid(),
                    Label = "ID",
                    AttributeTypeId = Constants.AttributeTypeInt,
                    EntityTypeId = list.Id
                },
                new EntityAttribute
                {
                    Id = new Guid(),
                    Label = "Title",
                    AttributeTypeId = Constants.AttributeTypeString,
                    EntityTypeId = list.Id
                },
                new EntityAttribute
                {
                    Id = new Guid(),
                    Label = "Created",
                    AttributeTypeId = Constants.AttributeTypeDateTime,
                    EntityTypeId = list.Id
                }
            };
            context.EntityAttributes.AddRange(systemAttributes);
            context.SaveChanges();

            database.CreateCollection(title);
        }
    }
}
