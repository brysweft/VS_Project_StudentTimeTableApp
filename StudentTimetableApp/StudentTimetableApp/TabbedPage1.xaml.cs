using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.StyleSheets;
using Xamarin.Forms.Xaml;

namespace StudentTimetableApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedPage1 : TabbedPage
    {
        public TabbedPage1()
        {
            InitializeComponent();

            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly, "StudentTimetableApp.MyStyles.css"));

            Title = "Расписание студента";
            this.ItemsSource =  new Weeks[] {
                new Weeks("Неделя 1"),
                new Weeks("Неделя 2"),
            };

            WeeksPages WeeksPagesObj = new WeeksPages();
            this.ItemTemplate = new DataTemplate(() => {
                return new MainPage2(WeeksPagesObj);
            });
        }
    }
}