using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Vzory._5_Dedicnost
{
	public abstract class Order3
	{
		public int OrderId { get; set; }
		public int CustomerId { get; set; }
		public DateTime CreatedAt { get; set; }
		public abstract void CreateTable();
		public abstract void GetOrder();
	}

	public class OnlineOrder3 : Order3
	{
		public string PaymentMethod { get; set; }
		public override void CreateTable()
		{
			//	CREATE TABLE OnlineOrders(
			//		OrderId INT PRIMARY KEY,
			//		CustomerId INT,
			//		CreatedAt DATETIME
			//		PaymentMethod VARCHAR(100),
			//		FOREIGN KEY(OrderId) REFERENCES Orders(OrderId)
			//	);
		}
		public override void GetOrder()
		{
			//	SELECT * FROM OnlineOrders WHERE OrderId = @OrderId;
		}
	}

	public class InStoreOrder3 : Order3
	{
		public string StoreLocation { get; set; }
		public override void CreateTable()
		{
			//	CREATE TABLE StoreOrders(
			//		OrderId INT PRIMARY KEY,
			//		CustomerId INT,
			//		CreatedAt DATETIME
			//		StoreLocation VARCHAR(100),
			//		FOREIGN KEY(OrderId) REFERENCES Orders(OrderId)
			//	);
		}
		public override void GetOrder()
		{
			//	SELECT * FROM StoreOrders WHERE OrderId = @OrderId;
		}
	}
}
