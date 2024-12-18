using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.Classes
{
  public static class AppState
  {
    public static Employee CurrentEmployee { get; set; } = new Employee();
    public static Servis CurrentServis { get; set; } = new Servis();
    public static Order CurrentOrder { get; set; } = new Order();
    public static Customer CurrentCustomer { get; set; } = new Customer();
    public static Part CurrentPart { get; set; } = new Part();
    public static Car CurrentCar { get; set; } = new Car();
  }
}
