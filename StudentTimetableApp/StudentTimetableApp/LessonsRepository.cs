using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StudentTimetableApp
{

    public class LessonsRepository
    {
        public SQLiteConnection database;
        public LessonsRepository(string filename)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentsPath, filename);
            database = new SQLiteConnection(path);
            database.CreateTable<LessonsTable>();
        }

        public IEnumerable<LessonsTable> GetItemsLessons()
        {
            return (from i in database.Table<LessonsTable>() select i).ToList();
        }
        public LessonsTable GetItemLessons(int id)
        {
            return database.Get<LessonsTable>(id);
        }

        public int DeleteItemLessons(int id)
        {
            return
                  database.Delete<LessonsTable>(id);
        }
        public int DeleteLessonsAll()
        {
            return
                  database.DeleteAll<LessonsTable>();
        }
        public int SaveItemLessons(LessonsTable item)
        {
            if (item.ID != 0)
            {
                database.Update(item);
                return item.ID;
            }
            else
            {
                return database.Insert(item);
            }
        }

    }


}
