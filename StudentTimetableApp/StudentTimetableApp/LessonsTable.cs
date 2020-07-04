using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentTimetableApp
{
    [Table("LessonsTable")]
    public class LessonsTable
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int ID { get; set; }
        //[Column("_subject")]
        public string Subject { get; set; }
       // [Column("_place")]
        public string Place { get; set; }
       //[Column("_teacher")]
        public string Teacher { get; set; }
       // [Column("_detail")]
        public string Detail { get; set; }
       // [Column("_day")]
        public string Day { get; set; }
      //  [Column("_number")]
        public int Number { get; set; }
      //  [Column("_week")]
        public int Week { get; set; }

    }
}
