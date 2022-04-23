using CinemaProject.Data.JsonData;

namespace CinemaProject.Data
{

    public class CinemaShedule
    {
        public List<string> _datas_minutes = new List<string>()
        {
            "00",
            "05",
            "10",
            "15",
            "20",
            "25",
            "30",
            "35",
            "40",
            "45",
            "50",
            "55"
        };

        public List<string> _datas_hours = new List<string>()
        {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
        };

        public static void RoundFive(ref double d)
        {
            if (d % 5 > 2.5)
                d = (int)(d / 5) * 5 + 5;
            else
                d = (int)(d / 5) * 5;
        }


        public Cinema cinema { get; set; } 

        public List<SheduleDay> sheduleDays { get; set; }

        public CinemaShedule(Cinema cinema)
        {
            this.cinema = cinema;

            var rnd = new Random();
            sheduleDays = new List<SheduleDay>();

            var countDays = 3;

            for(int i = 0; i < countDays; i++)
            {
                var countSeans = rnd.Next(5, 10);

                var sheduleDaysIte = new SheduleDay();

                sheduleDaysIte.sheduleItems = new List<SheduleItem>();

                for (int z = 0; z < countSeans; z++)
                {
                    var SheduleItem = new SheduleItem();

                    SheduleItem.Date = _datas_hours[rnd.Next(0, _datas_hours.Count)] + ":" + _datas_minutes[rnd.Next(0, _datas_minutes.Count)];

                    double price = rnd.Next(100, 500);
                    RoundFive(ref price);

                    SheduleItem.Price = Convert.ToString(price);

                    SheduleItem.is3D = Convert.ToBoolean(rnd.Next(0, 1));

                    sheduleDaysIte.sheduleItems.Add(SheduleItem);
                }

                sheduleDays.Add(sheduleDaysIte);
            }
        }
    }

    public class SheduleDay
    {
        public List<SheduleItem> sheduleItems { get; set; }
    }

    public class SheduleItem
    {
        public string Date { get; set; }
        public string Price { get; set; }
        public bool is3D { get; set; }
    }
}
