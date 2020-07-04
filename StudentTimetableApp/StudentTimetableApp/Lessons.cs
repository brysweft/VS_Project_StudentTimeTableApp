using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace StudentTimetableApp
{
    public class Lessons : INotifyPropertyChanged
    {
        private int last_id;
        public int Last_id { get { return last_id; } set { last_id = value; } }
        private ObservableCollection<Lesson> listLessons;
        public ObservableCollection<Lesson> ListLessons { get { return listLessons; } set { listLessons = value; } }
        private int maxNumber;
        public int MaxNumber { get { return maxNumber; } set { maxNumber = value; } }
        private int maxWeek;
        public int MaxWeek { get { return maxWeek; } set { maxWeek = value; } }

        public Lessons(int maxNumber = 8, int maxWeek = 1)
        {
            Last_id = 0;
            ListLessons = new ObservableCollection<Lesson>();
            MaxNumber = maxNumber;
            MaxWeek = maxWeek;
        }

        public Lesson AddNew(string subject = "", string place = "", string teacher = "", string detail = "", string day = "", int number = 0, int week = 0)
        {
            Last_id = Last_id + 1;
            Lesson new_lesson = new Lesson(Last_id, subject, place, teacher, detail, day, number, week);
            ListLessons.Add(new_lesson);
            return new_lesson;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public void Add(Lesson new_lesson)
        {
            Last_id = Last_id + 1;
            new_lesson.ID = Last_id;
            ListLessons.Add(new_lesson);
        }

    }
}
