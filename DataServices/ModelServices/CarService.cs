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
    private readonly CustomerRepository _customerRepository;
    private readonly Dictionary<int, Car> _carIdentityMap;

    public CarService(CarRepository carRepository, CustomerRepository customerRepository)
    {
      _carRepository = carRepository;
      _customerRepository = customerRepository;
      _carIdentityMap = [];
    }

    private Car? Add(Car car)
    {
      int id = _carRepository.Add(car);
      var existingCar = _carRepository.Get(id);
      if (existingCar != null)
      {
				existingCar.Customer = _customerRepository.Get(existingCar.IdCustomer)!;
				_carIdentityMap[existingCar.Id] = existingCar;
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
      existingCar.Customer = car.Customer;

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
        car.Customer = _customerRepository.Get(car.IdCustomer)!;
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
          car.Customer = _customerRepository.Get(car.IdCustomer)!;
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
