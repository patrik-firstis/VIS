using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices
{
  public class ServisService : IModelService<Servis>
  {
    private readonly ServisRepository _servisRepository;
    private readonly Dictionary<int, Servis> _servisIdentityMap;

    public ServisService(ServisRepository servisRepository)
    {
      _servisRepository = servisRepository;
      _servisIdentityMap = new Dictionary<int, Servis>();
    }

    private Servis? Add(Servis servis)
    {
      var id = _servisRepository.Add(servis);
      var existingServis = _servisRepository.Get(id);
      if (existingServis != null)
      {
        _servisIdentityMap[existingServis.Id] = existingServis;
      }
      return existingServis;
    }

    private Servis Update(Servis servis)
    {
      var existingServis = _servisIdentityMap[servis.Id];
      existingServis.Name = servis.Name;
      existingServis.Address = servis.Address;
      existingServis.OpenedAt = servis.OpenedAt;
      existingServis.ClosedAt = servis.ClosedAt;

      _servisRepository.Update(existingServis);

      return existingServis;
    }

    public void Delete(Servis servis)
    {
      if (_servisIdentityMap.ContainsKey(servis.Id))
      {
        _servisRepository.Delete(servis.Id);
        _servisIdentityMap.Remove(servis.Id);
      }
    }

    public Servis? Get(int id)
    {
      if (_servisIdentityMap.ContainsKey(id))
      {
        return _servisIdentityMap[id];
      }

      var servis = _servisRepository.Get(id);
      if (servis != null)
      {
        _servisIdentityMap[id] = servis;
        return servis;
      }

      return null;
    }

    public List<Servis> GetAll()
    {
      var servisList = new List<Servis>(_servisIdentityMap.Values);

      if (servisList.Count == 0)
      {
        servisList = _servisRepository.GetAll();
        foreach (var servis in servisList)
        {
          _servisIdentityMap[servis.Id] = servis;
        }
      }

      return servisList;
    }

    public Servis? UpdateOrCreate(Servis servis)
    {
      var existingServis = Get(servis.Id);
      if (existingServis != null)
      {
        return Update(servis);
      }
      else
      {
        return Add(servis);
      }
    }
  }
}
