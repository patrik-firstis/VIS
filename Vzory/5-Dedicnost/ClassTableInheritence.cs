using Microsoft.Maui.ApplicationModel.DataTransfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Vzory._5_Dedicnost
{
	public abstract class Order2
	{
		public int OrderId { get; set; }
		public int CustomerId { get; set; }
		public DateTime CreatedAt { get; set; }

		public void CreateTable()
		{
			//			CREATE TABLE Orders (
			//				OrderId INT PRIMARY KEY,
			//				CustomerId INT,
			//				CreatedAt DATETIME
			//			);
		}
	}

	public class OnlineOrder2 : Order2
	{
		public string PaymentMethod { get; set; }
		public void CreateTable()
		{
			//	CREATE TABLE OnlineOrders(
			//		OrderId INT PRIMARY KEY,
			//		PaymentMethod VARCHAR(100),
			//		FOREIGN KEY(OrderId) REFERENCES Orders(OrderId)
			//	);
		}
		public void GetOrder() 
		{
			//	SELECT o.OrderId, o.CustomerId, o.CreatedAt, io.PaymentMethod
			//	FROM Orders o
			//	JOIN OnlineOrders io ON o.OrderId = io.OrderId
			//	WHERE o.OrderId = @OrderId;
		}
	}

	public class InStoreOrder2 : Order2
	{
		public string StoreLocation { get; set; }
		public void CreateTable()
		{
			//	CREATE TABLE StoreOrders(
			//		OrderId INT PRIMARY KEY,
			//		StoreLocation VARCHAR(100),
			//		FOREIGN KEY(OrderId) REFERENCES Orders(OrderId)
			//	);
		}
		public void GetOrder()
		{
			//	SELECT o.OrderId, o.CustomerId, o.CreatedAt, so.StoreLocation
			//	FROM Orders o
			//	JOIN StoreOrders so ON o.OrderId = io.OrderId
			//	WHERE o.OrderId = @OrderId;
		}
	}

}
