using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Vzory._5_Dedicnost
{
	public abstract class Order
	{
		public int OrderId { get; set; }
		public int CustomerId { get; set; }
		public DateTime CreatedAt { get; set; }

		public void CreateTable()
		{
			//			CREATE TABLE Orders(
			//				OrderId INT PRIMARY KEY,
			//				CustomerId INT,
			//				CreatedAt DATETIME,
			//				PaymentMethod VARCHAR(100), //OnlineOrder
			//				StoreLocation VARCHAR(100), //InStoreOrder
			//				OrderType VARCHAR(50)				//OnlineOrder || InStoreOrder
			//			);
		}
	}

	public class OnlineOrder : Order
	{
		public string PaymentMethod { get; set; }
		public void GetOrder()
		{
			//SELECT* FROM Orders WHERE OrderType = 'OnlineOrder';
		}
	}

	public class InStoreOrder : Order
	{
		public string StoreLocation { get; set; }
		public void GetOrder()
		{
			//SELECT* FROM Orders WHERE OrderType = 'InStoreOrder';
		}
	}

}
