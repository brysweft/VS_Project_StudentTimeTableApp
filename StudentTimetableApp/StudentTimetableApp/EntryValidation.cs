using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StudentTimetableApp
{
    public class EntryValidation: TriggerAction<Entry>
    {
        protected override void Invoke(Entry sender)
        {
            if (Check(sender.Text))
            {
                sender.BackgroundColor = Color.Red;
                
            }
            else
            {
                sender.BackgroundColor = Color.Default;
            }
        }

        public static bool Check(string toCheck)
        {
            if (string.IsNullOrEmpty(toCheck))
            {
                return false;
            }
            return (System.Text.RegularExpressions.Regex.IsMatch(toCheck, @"^[A-Za-zА-Яа-я]+$")) ? false : true;
        }

    }
}
