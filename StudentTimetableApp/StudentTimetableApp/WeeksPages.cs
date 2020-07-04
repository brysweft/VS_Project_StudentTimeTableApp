using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace StudentTimetableApp
{
    public class WeeksPages
    {
        private int last_id;
        public int Last_id { get { return last_id; } set { last_id = value; } }
        private List<MainPage2> listWeeksPages;
        public List<MainPage2> ListWeeksPages { get { return listWeeksPages; } set { listWeeksPages = value; } }


        public WeeksPages()
        {
            Last_id = 0;
            ListWeeksPages = new List<MainPage2>();
        }

        public void Add(MainPage2 page)
        {
            Last_id = Last_id + 1;
            ListWeeksPages.Add(page);
        }
    }
}
