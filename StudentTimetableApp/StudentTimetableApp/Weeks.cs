using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTimetableApp
{
    public class Weeks
    {
        public Weeks(string name)
        {
            this.Name = name;
        }

        public string Name { private set; get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
