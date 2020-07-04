using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StudentTimetableApp
{


    public partial class MainPage : ContentPage
    {
        public Lessons LessonsObj;
        public string[] day;

        public Label selected;
        public MainPage(Lessons lessonsObj = null)
        {

            InitializeComponent();

            if (lessonsObj != null)
            {
                LessonsObj = lessonsObj;
            }

            Title = "Расписание студента";

            StackLayout stackLayout = new StackLayout();
            Button buttonAdd = new Button
            {
                Text = "+",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            buttonAdd.Clicked += OnButtonClicked;



            //списки занятия
            day = new string[] { " Понедельник", " Вторник", " Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
            ListView listView1 = new ListView
            {
                HasUnevenRows = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                // определяем источник данных
                //listView1.ItemsSource = LessonsObj.ListLessons.OrderBy(p => p.Day).ThenBy(p => p.Number);
                ItemsSource = LessonsObj.ListLessons,

                ItemTemplate = new DataTemplate(() =>
                {

                    Label lanelDay = new Label { FontSize = 14 };
                    lanelDay.SetBinding(Label.TextProperty, "Day");
                    Label labelNumber = new Label { FontSize = 14 };
                    labelNumber.SetBinding(Label.TextProperty, "Number");
                    Label labelSubject = new Label { FontSize = 14 };
                    Binding BindingSubject = new Binding { Path = "Subject", StringFormat = "Предмет: {0}", Mode = BindingMode.OneWay };
                    labelSubject.SetBinding(Label.TextProperty, BindingSubject);
                    Label labelPlace = new Label { FontSize = 12 };
                    Binding BindingPlace = new Binding { Path = "Place", StringFormat = "Место: {0}", Mode = BindingMode.OneWay };
                    labelPlace.SetBinding(Label.TextProperty, BindingPlace);
                    Label labelTeacher = new Label { FontSize = 10 };
                    Binding BindingTeacher = new Binding { Path = "Teacher", StringFormat = "Преподаватель: {0}", Mode = BindingMode.OneWay };
                    labelTeacher.SetBinding(Label.TextProperty, BindingTeacher);

                    Grid gridView = new Grid
                    {
                        RowDefinitions = {
                            new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
                            //new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
                            new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }},
                        ColumnDefinitions = {
                            new ColumnDefinition {Width = new GridLength (1, GridUnitType.Star)},
                            new ColumnDefinition {Width = new GridLength (2, GridUnitType.Star)}
                        },

                        Padding = new Thickness(0, 1),
                        //Orientation = StackOrientation.Vertical,
                        //Children = { labelNumber, labelSubject, labelPlace, labelTeacher }
                    };
                    gridView.Children.Add(lanelDay, 0, 0);
                    gridView.Children.Add(labelNumber, 0, 1);
                    gridView.Children.Add(labelSubject, 1, 0);
                    gridView.Children.Add(labelPlace, 1, 1);
                    //gridView.Children.Add(labelTeacher, 1, 2);

                    return new ViewCell
                    {
                        View = gridView
                    };
                }),
                BindingContext = LessonsObj.ListLessons
            };

            listView1.ItemTapped += ListView1_ItemTapped;
            listView1.ItemSelected += ListView1_ItemSelected;
            selected = new Label { Text = "NULL", HorizontalOptions = LayoutOptions.Center, FontSize = 12 };

            stackLayout.Children.Add(listView1);
            stackLayout.Children.Add(selected);
            stackLayout.Children.Add(buttonAdd);
            Content = stackLayout;

            this.Appearing += Page_Appearing;
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageLesson(null));
        }

        private async void ListView1_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Lesson selectedLesson = e.Item as Lesson;

            var action = await DisplayActionSheet("Действия", "Отмена", "Удалить", "Открыть");
            Label actionLabel = new Label
            {
                Text = action
            };
            if (action == "Открыть")
            {
                await Navigation.PushAsync(new PageLesson(selectedLesson));
            };
            if (action == "Удалить")
            {
                if (selectedLesson != null)
                {
                    bool result = await DisplayAlert("Подтвердить действие", "Вы хотите удалить элемент?", " Да", "Нет");
                    // await DisplayAlert("Уведомление", "Вы выбрали: " + (result ? " Удалить" : " Отменить"), "ОК");
                    if (result)
                    {
                        LessonsObj.ListLessons.Remove(selectedLesson);
                    }
                }
            };



        }
        private void ListView1_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Binding Binding1 = new Binding { Path = "Detail", StringFormat = "Детали: {0}", Mode = BindingMode.OneWay };
                selected.BindingContext = e.SelectedItem;
                selected.SetBinding(Label.TextProperty, Binding1);
            }
        }
        private void Page_Appearing(object sender, EventArgs e)
        {

        }
        protected internal void AddLesson(Lesson lesson)
        {
            LessonsObj.Add(lesson);
        }
    }

}


