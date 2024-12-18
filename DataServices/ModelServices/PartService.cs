using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices
{
  public class PartService
  {
    private readonly PartRepository _partRepository;
    private readonly PartServisRepository _partServisRepository;
    private readonly ServisRepository _servisRepository;
    private readonly Dictionary<int, Part> _partIdentityMap;

    public PartService(PartRepository partRepository, PartServisRepository partServisRepository, ServisRepository servisRepository)
    {
      _partRepository = partRepository;
      _partServisRepository = partServisRepository;
      _servisRepository = servisRepository;
      _partIdentityMap = [];
    }

    private Part? AddPart(Part part)
    {
      var id = _partRepository.Add(part);
      var existingPart = _partRepository.Get(id);
      if (existingPart != null)
      {
        foreach (var servis in _servisRepository.GetAll())
        {
          _partServisRepository.Add(existingPart.Id, servis.Id, 0);
        }
        existingPart.Stock = _partServisRepository.GetAll(existingPart.Id);
        _partIdentityMap[existingPart.Id] = existingPart;
      }

      return existingPart;
    }

    private Part UpdatePart(Part part)
    {
      var existingPart = _partIdentityMap[part.Id];
      existingPart.Brand = part.Brand;
      existingPart.Model = part.Model;
      existingPart.Description = part.Description;

      _partRepository.Update(existingPart);

      return existingPart;
    }

    public void DeletePart(Part part)
    {
      if (_partIdentityMap.ContainsKey(part.Id))
      {
        _partRepository.Delete(part.Id);
        _partServisRepository.DeleteAll(part.Id);
        _partIdentityMap.Remove(part.Id);
      }
    }

    public Part? GetPart(int id)
    {
      if (_partIdentityMap.ContainsKey(id))
      {
        return _partIdentityMap[id];
      }

      var part = _partRepository.Get(id);
      if (part != null)
      {
        part.Stock = _partServisRepository.GetAll(id);
        _partIdentityMap[id] = part;
      }

      return part;
    }

    public List<Part> GetAllParts()
    {
      var parts = new List<Part>(_partIdentityMap.Values);

      if (parts.Count == 0)
      {
        parts = _partRepository.GetAll();
        foreach (var part in parts)
        {
          part.Stock = _partServisRepository.GetAll(part.Id);
          _partIdentityMap[part.Id] = part;
        }
      }

      return parts;
    }

    public Part? UpdateOrCreatePart(Part part)
    {
      var existingPart = GetPart(part.Id);
      if (existingPart != null)
      {
        return UpdatePart(part);
      }
      else
      {
        return AddPart(part);
      }
    }
  }
}
