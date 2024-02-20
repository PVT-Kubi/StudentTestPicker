using StudentTestPicker.Models;
using StudentTestPicker.Views;

namespace StudentTestPicker
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new Models.AllClasses();
        }

        protected override void OnAppearing()
        {
            ((Models.AllClasses)BindingContext).LoadClasses();
        }

        private async void classCollection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.CurrentSelection.Count > 0)
            {
                Klasa klasa = (Models.Klasa)e.CurrentSelection[0];

                await Shell.Current.GoToAsync($"{nameof(ClassStudents)}?{nameof(ClassStudents.ItemId)}={klasa.ClassNumber}");

                classCollection.SelectedItem = null;
            }
        }
    }
}