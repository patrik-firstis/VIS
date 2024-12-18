using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vistest.DataServices
{
  public interface IModelService<T>
  {
    public T? Get(int id);
    public List<T> GetAll();
    public void Delete(T entity);
    public T? UpdateOrCreate(T entity);
  }
}
