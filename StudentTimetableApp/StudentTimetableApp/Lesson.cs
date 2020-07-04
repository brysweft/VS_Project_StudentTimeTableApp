using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace StudentTimetableApp
{
    public class Lesson : INotifyPropertyChanged
    {
        private int id;
        public int ID { get { return id; } set { id = value; } }

        private string subject;
        public string Subject { get { return subject; } set { subject = value; OnPropertyChanged("Subject"); } }

        private string place;
        public string Place { get { return place; } set { place = value; OnPropertyChanged("Place"); } }

        private string teacher;
        public string Teacher { get { return teacher; } set { teacher = value; OnPropertyChanged("Teacher"); } }

        private string detail;
        public string Detail { get { return detail; } set { detail = value; OnPropertyChanged("Detail"); } }

        private string day;
        public string Day { get { return day; } set { day = value; OnPropertyChanged("Day"); } }

        private int number;
        public int Number { get { return number; } set { number = value; OnPropertyChanged("Number"); } }

        private int week;
        public int Week { get { return week; } set { week = value; OnPropertyChanged("Week"); } }

        public Lesson(int id = 0, string subject = "", string place = "", string teacher = "", string detail = "", string day = "", int number = 0, int week = 0)
        {
            ID = id;
            Subject = subject;
            Place = place;
            Teacher = teacher;
            Detail = detail;
            Day = day;
            Number = number;
            Week = week;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
