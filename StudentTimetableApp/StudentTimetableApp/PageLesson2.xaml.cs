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
    public partial class PageLesson2 : ContentPage
    {
        public LessonsTable LessonObj;
        public PageLesson2(LessonsTable lesson)
        {

            InitializeComponent();

            //стили
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly, "StudentTimetableApp.MyStyles.css"));

            LessonObj = lesson;

            Title = "Запись расписания";
 
            //основные элементы формы
            TableView tableView1 = new TableView
            {
                Intent = TableIntent.Form
            };

            TableRoot root1 = new TableRoot("Ввод данных")
            {
                new TableSection ("Занятие"){
                    new EntryCell{
                        Label = "Предмет:",
                        Placeholder = "<введите предмет>",
                        Keyboard = Keyboard.Default,
                        BindingContext = LessonObj,
                    },
                    new EntryCell{
                        Label = "Место:",
                        Placeholder = "<введите место>",
                        Keyboard = Keyboard.Default,
                        BindingContext = LessonObj,
                    },
                    new EntryCell{
                        Label = "Преподаватель:",
                        Placeholder = "<введите преподавателя>",
                        Keyboard = Keyboard.Default,
                        BindingContext = LessonObj,
                    },
                    new EntryCell{
                        Label = "Описание:",
                        Placeholder = "<введите опсание>",
                        Keyboard = Keyboard.Default,
                        BindingContext = LessonObj,
                    }
                },
                //new TableSection ("Когда"){
                    //new EntryCell{
                    //    Label = "День:",
                    //    Placeholder = "<введите день>",
                    //    Keyboard = Keyboard.Default,
                    //    BindingContext = LessonObj,
                    //},
                    //new EntryCell{
                    //    Label = "Номер пары/урока:",
                    //    Placeholder = "<введите номер>",
                    //    Keyboard = Keyboard.Default,
                    //    BindingContext = LessonObj,
                    //}
                //},
                new TableSection ("Выделить"){
                    new SwitchCell{
                        Text = "",
                        On = false
                    }
                }
            };

            tableView1.Root = root1;
            root1.ElementAt(0).ElementAt(0).SetBinding(EntryCell.TextProperty, new Binding { Path = "Subject", Mode = BindingMode.TwoWay });
            root1.ElementAt(0).ElementAt(1).SetBinding(EntryCell.TextProperty, new Binding { Path = "Place", Mode = BindingMode.TwoWay });
            root1.ElementAt(0).ElementAt(2).SetBinding(EntryCell.TextProperty, new Binding { Path = "Teacher", Mode = BindingMode.TwoWay });
            root1.ElementAt(0).ElementAt(3).SetBinding(EntryCell.TextProperty, new Binding { Path = "Detail", Mode = BindingMode.TwoWay });
            //root1.ElementAt(1).ElementAt(0).SetBinding(EntryCell.TextProperty, new Binding { Path = "Day", Mode = BindingMode.TwoWay });
            //root1.ElementAt(1).ElementAt(1).SetBinding(EntryCell.TextProperty, new Binding { Path = "Number", Mode = BindingMode.TwoWay });
            //root1.ElementAt(0).ElementAt(2).SetBinding(EntryCell.TextProperty, new Binding { Path = "Week", Mode = BindingMode.TwoWay });

            Picker pickerDay = new Picker
            {
                Title = "День недели:",
            };
            string[] days = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
            pickerDay.ItemsSource = days;
            pickerDay.BindingContext = LessonObj;
            pickerDay.SetBinding(Picker.SelectedItemProperty, new Binding { Path = "Day", Mode = BindingMode.TwoWay });

            Picker pickerNumber = new Picker
            {
                Title = "Пара/урок (номер):",
            };
            int[] numbers = new int[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            pickerNumber.ItemsSource = numbers;
            pickerNumber.BindingContext = LessonObj;
            pickerNumber.SetBinding(Picker.SelectedItemProperty, new Binding { Path = "Number", Mode = BindingMode.TwoWay });

            Picker pickerWeek = new Picker
            {
                Title = "Неделя (номер):",
            };
            int[] weeks = new int[] { 1, 2 };
            pickerWeek.ItemsSource = weeks;
            pickerWeek.BindingContext = LessonObj;
            pickerWeek.SetBinding(Picker.SelectedItemProperty, new Binding { Path = "Week", Mode = BindingMode.TwoWay });

            Grid gridView = new Grid
            {
                RowDefinitions = {
                            new RowDefinition { Height = new GridLength (100, GridUnitType.Star) } },
                //new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
                //new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }},
                ColumnDefinitions = {
                            new ColumnDefinition {Width = new GridLength (3, GridUnitType.Star)},
                            new ColumnDefinition {Width = new GridLength (6, GridUnitType.Star)},
                            new ColumnDefinition {Width = new GridLength (3, GridUnitType.Star)}
                        },

                Padding = new Thickness(0, 1),
                //Orientation = StackOrientation.Vertical,
            };
            gridView.Children.Add(pickerWeek, 0, 0);
            gridView.Children.Add(pickerDay, 1, 0);
            gridView.Children.Add(pickerNumber, 2, 0);

            StackLayout stackLayout = new StackLayout();
            Button buttonOK = new Button
            {
                Text = "Ок",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                BorderWidth = 1,
                //HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            buttonOK.Clicked += OnButtonClicked;

            //контент
            stackLayout.Children.Add(gridView);
            stackLayout.Children.Add(tableView1);
            stackLayout.Children.Add(buttonOK);

            Content = stackLayout;
        }
        private async void OnButtonClicked(object sender, EventArgs e)
        {
            App.Database.SaveItemLessons(LessonObj);
            await Navigation.PopAsync();
        }
    }
}