using System;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace StudentTimetableApp
{

    public partial class App : Application
    {
        public Lessons LessonsObj;
        public App()
        {
            InitializeComponent();
            OnStart();
            //MainPage = new MainPage();
            //MainPage = new NavigationPage(new MainPage(LessonsObj));
            //MainPage = new NavigationPage(new MainPage2());
            MainPage = new NavigationPage(new TabbedPage1());
        }

        public const string DATABASE_NAME = "TimeTableStud.db";
        public static LessonsRepository database;
        public static LessonsRepository Database
        {
            get
            {
                if (database == null)
                {
                    database = new LessonsRepository(DATABASE_NAME);
                }
                return database;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            //списки занятия
            LessonsObj = new Lessons(8, 2);
            LessonsObj.AddNew("Математика", "каб 1", "Иванова Н.В.", "читать лецию 3, учиить", "Понедельник", 1, 1);
            LessonsObj.AddNew("Физика", "каб 2", "Гарбулаева Р.И.", "задачи № 13, 15", "Понедельник", 2, 1);

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
