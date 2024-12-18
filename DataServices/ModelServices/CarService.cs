using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices
{
  public class CarService : IModelService<Car>
  {
    private readonly CarRepository _carRepository;
    private readonly Dictionary<int, Car> _carIdentityMap;

    public CarService(CarRepository carRepository)
    {
      _carRepository = carRepository;
      _carIdentityMap = [];
    }

    private Car? Add(Car car)
    {
      _carRepository.Add(car);
      var existingCar = _carRepository.Get(car.Id);
      if (existingCar != null)
      {
        _carIdentityMap[car.Id] = existingCar;
      }
      return existingCar;
    }

    private Car Update(Car car)
    {
      var existingCar = _carIdentityMap[car.Id];
      existingCar.IdCustomer = car.IdCustomer;
      existingCar.Brand = car.Brand;
      existingCar.Model = car.Model;
      existingCar.LicencePlate = car.LicencePlate;
      existingCar.LastMileage = car.LastMileage;

      _carRepository.Update(existingCar);

      return existingCar;
    }

    public void Delete(Car car)
    {
      if (_carIdentityMap.ContainsKey(car.Id))
      {
        _carRepository.Delete(car.Id);
        _carIdentityMap.Remove(car.Id);
      }
    }

    public Car? Get(int id)
    {
      if (_carIdentityMap.ContainsKey(id))
      {
        return _carIdentityMap[id];
      }

      var car = _carRepository.Get(id);
      if (car != null)
      {
        _carIdentityMap[id] = car;
        return car;
      }

      return null;
    }

    public List<Car> GetAll()
    {
      var cars = new List<Car>(_carIdentityMap.Values);

      if (cars.Count == 0)
      {
        cars = _carRepository.GetAll();
        foreach (var car in cars)
        {
          _carIdentityMap[car.Id] = car;
        }
      }

      return cars;
    }

    public Car? UpdateOrCreate(Car car)
    {
      var existingCar = Get(car.Id);
      if (existingCar != null)
      {
        return Update(car);
      }
      else
      {
        return Add(car);
      }
    }
  }
}
