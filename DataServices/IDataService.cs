using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.DataServices
{
	public interface IDataService<T> where T : class
	{
		bool Add(T item);
		bool Updat(T item);
		bool Delete(T item);
		T Get(int id);
		List<T> GetAll();
	}
}
