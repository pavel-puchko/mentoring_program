using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Task
{
	[TestClass]
	public class SerializationSolutions
	{
		private Northwind dbContext;

	    private const string filePath = "D://EPAM/mentoring/Serialization";

        private Book dickBook = new Book()
        {
            Author = "John",
            Title = "C#",
            Genre = Genres.Fantasy,
            Publisher = "John",
            PublishDateWrapper = "2010-03-15",
            Description = "C# everywhere",
            RegistraionDateWrapper = "2000-12-01"
        };


        [TestInitialize]
		public void Initialize()
		{
			dbContext = new Northwind();
		}

		[TestMethod]
		public void SerializationCallbacks()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(new NetDataContractSerializer(), true);
			var categories = dbContext.Categories.ToList();

			var c = categories.First();

			tester.SerializeAndDeserialize(categories);
		}

		[TestMethod]
		public void ISerializable()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(new NetDataContractSerializer(), true);
			var products = dbContext.Products.ToList();

			tester.SerializeAndDeserialize(products);
		}


		[TestMethod]
		public void ISerializationSurrogate()
		{
			dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(new NetDataContractSerializer(), true);
			var orderDetails = dbContext.Order_Details.ToList();

			tester.SerializeAndDeserialize(orderDetails);
		}

		[TestMethod]
		public void IDataContractSurrogate()
		{
			dbContext.Configuration.ProxyCreationEnabled = true;
			dbContext.Configuration.LazyLoadingEnabled = true;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(new DataContractSerializer(typeof(IEnumerable<Order>)), true);
			var orders = dbContext.Orders.ToList();

			tester.SerializeAndDeserialize(orders);
		}

        [TestMethod]
        public void CheckIXmlSerializable_DoesSerialization()
        {
            var serializer = new XmlSerializer(typeof(Book));
            var stream = new FileStream(filePath, FileMode.Create);
            serializer.Serialize(stream, dickBook);
            stream.Close();
        }

        [TestMethod]
        public void CheckIXmlSerializable_DoesDeSerialization()
        {
            var serializer = new XmlSerializer(typeof(Book));
            Book book = serializer.Deserialize(new FileStream(filePath, FileMode.Open)) as Book;
        }
    }
}
