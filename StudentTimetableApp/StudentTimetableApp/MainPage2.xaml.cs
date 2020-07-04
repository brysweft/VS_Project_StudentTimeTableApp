using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class MainPage2 : ContentPage
    {
        public Lessons LessonsObj;
        public string[] day;
        public Label selected;

        public ListView listLessons;

        public MainPage2(WeeksPages WeeksPagesObj = null)
        {
            InitializeComponent();

            //стили
            this.Resources.Add(StyleSheet.FromAssemblyResource(IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly, "StudentTimetableApp.MyStyles.css"));

            //Title = "Расписание студента";
            this.SetBinding (ContentPage.TitleProperty, "Name");
             if (WeeksPagesObj != null){
                WeeksPagesObj.Add(this);
                AutomationId = WeeksPagesObj.Last_id.ToString();
             } 

            StackLayout stackLayout = new StackLayout();

            //списки занятия
            listLessons = new ListView
            {
                HasUnevenRows = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                // определяем источник данных
                //ItemsSource = App.Database.GetItemsLessons(),
                ItemsSource = App.Database.GetItemsLessons().Where(p => p.Week == Int16.Parse(AutomationId)).OrderBy(p => p.Number).GroupBy(p => p.Day).Select(g => new Grouping<string, LessonsTable>(g.Key, g)),
                IsGroupingEnabled = true,
                GroupDisplayBinding = new Binding("Name"),
            ItemTemplate = new DataTemplate(() =>
                {

                    //Label lanelDay = new Label { FontSize = 14 };
                    //lanelDay.SetBinding(Label.TextProperty, "Day");
                    Label labelNumber = new Label { FontSize = 14 };
                    labelNumber.SetBinding(Label.TextProperty, "Number");
                    //labelNumber.SetBinding(Label.TextProperty, "ID");
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
                            new ColumnDefinition {Width = new GridLength (5, GridUnitType.Star)}
                        },

                        Padding = new Thickness(0, 1),
                        //Orientation = StackOrientation.Vertical,
                        //Children = { labelNumber, labelSubject, labelPlace, labelTeacher }
                    };
                    gridView.Children.Add(labelNumber, 0, 0);
                    //gridView.Children.Add(lanelDay, 0, 1);
                    gridView.Children.Add(labelSubject, 1, 0);
                    gridView.Children.Add(labelPlace, 1, 1);
                    //gridView.Children.Add(labelTeacher, 1, 2);

                    return new ViewCell
                    {
                        View = gridView
                    };
                })
            };
            //listLessons.BindingContext = App.Database.GetItemsLessons();

            listLessons.ItemTapped += ListLessons_ItemTapped;
            listLessons.ItemSelected += ListLessons_ItemSelected;
            selected = new Label { Text = "NULL", HorizontalOptions = LayoutOptions.Center, FontSize = 12 };

            Button buttonAdd = new Button
            {
                Text = "+",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                BorderWidth = 1,
                //HorizontalOptions = LayoutOptions.Center,
                //VerticalOptions = LayoutOptions.Center,
            };
            buttonAdd.Clicked += OnButtonClicked;

            Button buttonClearLessons = new Button
            {
                Text = "Очистить",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                BorderWidth = 1,
                //HorizontalOptions = LayoutOptions.Center,
                //VerticalOptions = LayoutOptions.Center,
            };
            buttonClearLessons.Clicked += OnButtonClicked_buttonClearLessons;

            Button buttonCreateLessons = new Button
            {
                Text = "Заполнить",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                BorderWidth = 1,
                //HorizontalOptions = LayoutOptions.Center,
               //VerticalOptions = LayoutOptions.Center,
            };
            buttonCreateLessons.Clicked += OnButtonClicked_buttonCreateLessons;


            FlexLayout flex_buttons = new FlexLayout()
            {
                Children = { buttonClearLessons, buttonCreateLessons, buttonAdd },
                //Direction = FlexDirection.Row,
                Margin = new Thickness(15, 0),
                //VerticalOptions = LayoutOptions.Center,
                //HorizontalOptions = LayoutOptions.Center,
            };

            //посик
            Entry searchEntry = new Entry
            {
                Placeholder = "<поиск>",
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            searchEntry.Completed += Completed_searchEntry;

            //тригеры
            var triggerFocusEntry = new Trigger(typeof(Entry))
            {
                Property = Entry.IsFocusedProperty,
                Value = true,
                Setters = {
                    new Setter{
                        Property = Entry.BackgroundColorProperty,
                        Value = Color.LightYellow
                    },
                    new Setter{
                        Property = Entry.TextColorProperty,
                        Value = Color.DarkGreen
                    }
                }
            };
            var triggerCheckName = new EventTrigger()
            {
                Event = "TextChanged",
                Actions = {
                    new EntryValidation()
                }
            };
            searchEntry.Triggers.Add(triggerFocusEntry);
            searchEntry.Triggers.Add(triggerCheckName);

            stackLayout.Children.Add(searchEntry);
            stackLayout.Children.Add(listLessons);
            stackLayout.Children.Add(selected);
            stackLayout.Children.Add(flex_buttons);
            Content = stackLayout;

        }

        private  void Completed_searchEntry(object sender, EventArgs e){
            Entry searchEntry = sender as Entry;
            string text = searchEntry.Text;
            if (!string.IsNullOrEmpty(text))
                {
                    //listLessons.ItemsSource = App.Database.GetItemsLessons().Where(u => u.Subject.Contains(text));
                     var groupsLessons = App.Database.GetItemsLessons().Where(p => p.Week == Int16.Parse(AutomationId)).OrderBy(p => p.Number).Where(u => u.Subject.Contains(text)).GroupBy(p => p.Day).Select(g => new Grouping<string, LessonsTable>(g.Key, g));
                     listLessons.ItemsSource = groupsLessons;
            }
                else
                {
                    //listLessons.ItemsSource = App.Database.GetItemsLessons();
                    var groupsLessons = App.Database.GetItemsLessons().Where(p => p.Week == Int16.Parse(AutomationId)).OrderBy(p => p.Number).GroupBy(p => p.Day).Select(g => new Grouping<string, LessonsTable>(g.Key, g));
                    listLessons.ItemsSource = groupsLessons;
                };
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            LessonsTable lesson = new LessonsTable();
            //PageLesson2 page2 = new PageLesson2();
            //page2.BindingContext = lesson;//не работает
            await Navigation.PushAsync(new PageLesson2(lesson));
        }
        private async void OnButtonClicked_buttonClearLessons(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Подтвердить действие", "Вы хотите удалить все записи?", " Да", "Нет");
            if (result)
            {
                App.Database.DeleteLessonsAll();
                // обновляем источник данных
                //listLessons.ItemsSource = App.Database.GetItemsLessons();
                var groupsLessons = App.Database.GetItemsLessons().Where(p => p.Week == Int16.Parse(AutomationId)).OrderBy(p => p.Number).GroupBy(p => p.Day).Select(g => new Grouping<string, LessonsTable>(g.Key, g));
                listLessons.ItemsSource = groupsLessons;
            }
        }
        private async void OnButtonClicked_buttonCreateLessons(object sender, EventArgs e)
        {
            bool result = await DisplayAlert("Подтвердить действие", "Вы действительно хотите заполнить данными по умолчанию? Созданные ранее записи будут удалены.", " Да", "Нет");
            if (result)
            {
                App.Database.DeleteLessonsAll();


                string actionDays = await DisplayActionSheet("Сколько отображать дней в неделе?", null, null, "5", "6", "7");
                string actionNumbers = await DisplayActionSheet("Сколько отображать занятий (пар) в день?", null, null, "4", "6", "8");

                //база данных таблица lessons
                LessonsTable lessonnew;

                string[] days = new string[] { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница", "Суббота", "Воскресенье" };
                for (int i = 1; i <= 2; i++)
                {
                    for (int k = 0; k <= Int16.Parse(actionDays); k++)
                    {
                        for (int j = 1; j <= Int16.Parse(actionNumbers); j++)
                        {
                            lessonnew = new LessonsTable
                            {
                                Day = days[k],
                                Number = j,
                                Week = i,
                                Subject = "",
                            };
                            App.Database.SaveItemLessons(lessonnew);
                        }
                    }
                }


                // обновляем источник данных
                //listLessons.ItemsSource = App.Database.GetItemsLessons();
                var groupsLessons = App.Database.GetItemsLessons().Where(p => p.Week == Int16.Parse(AutomationId)).OrderBy(p => p.Number).GroupBy(p => p.Day).Select(g => new Grouping<string, LessonsTable>(g.Key, g));
                listLessons.ItemsSource = groupsLessons;
            }
        }

        private async void ListLessons_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            LessonsTable selectedLesson = e.Item as LessonsTable;

            var action = await DisplayActionSheet("Действия", "Отмена", "Удалить", "Открыть");
            if (action == "Открыть")
            {
                //PageLesson2 page2 = new PageLesson2();
                //page2.BindingContext = selectedLesson;//не работает
                await Navigation.PushAsync(new PageLesson2(selectedLesson));
            };
            if (action == "Удалить")
            {
                if (selectedLesson != null)
                {
                    bool result = await DisplayAlert("Подтвердить действие", "Вы хотите удалить элемент?", " Да", "Нет");
                    // await DisplayAlert("Уведомление", "Вы выбрали: " + (result ? " Удалить" : " Отменить"), "ОК");
                    if (result)
                    {
                        App.Database.DeleteItemLessons(selectedLesson.ID);
                        // обновляем источник данных
                        //listLessons.ItemsSource = App.Database.GetItemsLessons();
                        var groupsLessons = App.Database.GetItemsLessons().Where(p => p.Week == Int16.Parse(AutomationId)).OrderBy(p => p.Number).GroupBy(p => p.Day).Select(g => new Grouping<string, LessonsTable>(g.Key, g));
                        listLessons.ItemsSource = groupsLessons;
                    }
                }
            };
        }

        private void ListLessons_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                Binding Binding1 = new Binding { Path = "Detail", StringFormat = "Детали: {0}", Mode = BindingMode.OneWay };
                selected.BindingContext = e.SelectedItem;
                selected.SetBinding(Label.TextProperty, Binding1);
            }
        }
        protected override void OnAppearing()
        {
            // обновляем источник данных
            //listLessons.ItemsSource = App.Database.GetItemsLessons();
            var groupsLessons = App.Database.GetItemsLessons().Where(p => p.Week == Int16.Parse(AutomationId)).OrderBy(p => p.Number).GroupBy(p => p.Day).Select(g => new Grouping<string, LessonsTable>(g.Key, g));
            listLessons.ItemsSource = groupsLessons;

            base.OnAppearing();
        }
        protected internal void AddLesson(LessonsTable lesson)
        {
            App.Database.SaveItemLessons(lesson);
        }
    }
}