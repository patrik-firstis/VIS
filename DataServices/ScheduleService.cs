using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using vistest.Models;

namespace vistest.DataServices
{
  public class ScheduleService
  {
    private const string ScheduleFilePath = "schedule.json";
    private static readonly TimeSpan WorkDayStart = TimeSpan.FromHours(8);
    private static readonly TimeSpan WorkDayEnd = TimeSpan.FromHours(20);

    public static void AddToSchedule(int orderId)
    {
      var schedule = LoadSchedule();

      var appointment = new Appointment
      {
        OrderId = orderId
      };
      appointment.StartTime = FindNextAvailableTime(schedule);
      appointment.EndTime = appointment.StartTime.AddHours(1);
      schedule.Add(appointment);
      SaveSchedule(schedule);
    }

    private static List<Appointment> LoadSchedule()
    {
      if (!File.Exists(ScheduleFilePath))
        return new List<Appointment>();

      string jsonString = File.ReadAllText(ScheduleFilePath);
      return JsonSerializer.Deserialize<List<Appointment>>(jsonString) ?? new List<Appointment>();
    }

    private static void SaveSchedule(List<Appointment> schedule)
    {
      string jsonString = JsonSerializer.Serialize(schedule, new JsonSerializerOptions { WriteIndented = true });
      File.WriteAllText(ScheduleFilePath, jsonString);
    }

    private static DateTime FindNextAvailableTime(List<Appointment> schedule)
    {
      DateTime now = DateTime.Now;
      if (schedule.Count == 0)
        return AlignToWorkHours(RoundToFullHour(now));

      var sortedSchedule = schedule.OrderBy(a => a.StartTime).ToList();
      DateTime lastEndTime = sortedSchedule.Last().EndTime;

      if (lastEndTime.TimeOfDay >= WorkDayEnd)
      {
        return AlignToNextWorkDay(lastEndTime.Date.AddDays(1));
      }

      return AlignToWorkHours(RoundToFullHour(lastEndTime));
    }

    private static DateTime AlignToWorkHours(DateTime time)
    {
      if (time.TimeOfDay < WorkDayStart)
        return time.Date.Add(WorkDayStart);
      if (time.TimeOfDay >= WorkDayEnd)
        return AlignToNextWorkDay(time.Date);

      return time;
    }

    private static DateTime AlignToNextWorkDay(DateTime date)
    {
      return date.AddDays(1).Add(WorkDayStart);
    }

    private static DateTime RoundToFullHour(DateTime time)
    {
      return time.Minute == 0 && time.Second == 0
          ? time
          : time.AddMinutes(60 - time.Minute).AddSeconds(-time.Second);
    }
  }
}
