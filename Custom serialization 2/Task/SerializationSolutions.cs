using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Task.DB;
using Task.TestHelpers;
using System.Runtime.Serialization;
using System.Linq;
using System.Collections.Generic;

namespace Task
{
	[TestClass]
	public class SerializationSolutions
	{
		private Northwind _dbContext;

		[TestInitialize]
		public void Initialize()
		{
			_dbContext = new Northwind();
		}

		[TestMethod]
		public void SerializationCallbacks()
		{
			_dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Category>>(
                new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, _dbContext)), true);

			var categories = _dbContext.Categories.ToList();

			var c = categories.First();

			tester.SerializeAndDeserialize(categories);
		}

		[TestMethod]
		public void ISerializable()
		{
			_dbContext.Configuration.ProxyCreationEnabled = false;

			var tester = new XmlDataContractSerializerTester<IEnumerable<Product>>(
                new NetDataContractSerializer(new StreamingContext(StreamingContextStates.All, _dbContext)), true);
			var products = _dbContext.Products.ToList();

			tester.SerializeAndDeserialize(products);
		}


		[TestMethod]
		public void ISerializationSurrogate()
		{
			_dbContext.Configuration.ProxyCreationEnabled = false;

            SurrogateSelector surrogateSelector = new SurrogateSelector();
            surrogateSelector.AddSurrogate(
                typeof(Order_Detail), 
                new StreamingContext(StreamingContextStates.All, _dbContext), 
                new OrderDetailSerializationSurrogate());

			var tester = new XmlDataContractSerializerTester<IEnumerable<Order_Detail>>(
                new NetDataContractSerializer()
                {
                    Context = new StreamingContext(StreamingContextStates.All, _dbContext),
                    SurrogateSelector = surrogateSelector
                }, 
                true);

			var orderDetails = _dbContext.Order_Details.ToList();

			tester.SerializeAndDeserialize(orderDetails);
		}

		[TestMethod]
		public void IDataContractSurrogate()
		{
			_dbContext.Configuration.ProxyCreationEnabled = true;
			_dbContext.Configuration.LazyLoadingEnabled = true;

            DataContractSerializerSettings settings = new DataContractSerializerSettings()
            {
                DataContractSurrogate = new OrderDataContractSurrogate()
            };

            DataContractSerializer serializer = new DataContractSerializer(typeof(IEnumerable<Order>), settings);

            var tester = new XmlDataContractSerializerTester<IEnumerable<Order>>(serializer, true);

			var orders = _dbContext.Orders.ToList();

			tester.SerializeAndDeserialize(orders);
		}
	}
}
