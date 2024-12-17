using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.Vzory._3_RelacneObjektoveChovani
{
	internal class LazyInitialization
	{
		[Obsolete]
		public class OrderWithLazyLoad
		{
			private readonly int _orderId;
			private Order2 _order;

			public OrderWithLazyLoad(int orderId)
			{
				_orderId = orderId;
			}
			public Order2 Order
			{
				get
				{
					if (_order == null)
					{
						// Načítanie objednávky z databázy
						_order = new Order2
						{
							OrderId = _orderId,
							CustomerId = 1,
							CreatedAt = DateTime.Now
						};
					}
					return _order;
				}
			}
		}
	}
}
