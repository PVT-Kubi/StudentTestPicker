

using StudentTestPicker.Models;

namespace StudentTestPicker.Views;

[QueryProperty(nameof(ItemId), nameof(ItemId))]

public partial class ClassStudents : ContentPage
{

    public string ItemId
    {
        set { LoadStudens(value); }
    }
    public ClassStudents()
	{
		InitializeComponent();
    }


    public void LoadStudens(string classNumber)
    {
        BindingContext = new Models.AllStudents(classNumber);
        StudentluckyNumber.Text = ((Models.AllStudents)BindingContext).luckyNumber.ToString();
    }

    private void Add_Student(object sender, EventArgs e)
    {
        ((Models.AllStudents)BindingContext).AddStudent(((Models.AllStudents)BindingContext).getClassNumber(), StudentName.Text, StudentSurname.Text);
        ((Models.AllStudents)BindingContext).LoadStudents(((Models.AllStudents)BindingContext).getClassNumber());
    }

    private async void ReturnButtonClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
    }

    private async void DrawStudent(object sender, EventArgs e)
    {
        ((Models.AllStudents)BindingContext).LoadStudents(((Models.AllStudents)BindingContext).getClassNumber());
        Models.Student s = ((Models.AllStudents)BindingContext).DrawStudent(((Models.AllStudents)BindingContext).getClassNumber());
        if(s.Name == null && s.ClassNumber == null)
        {
            await DisplayAlert("Uwaga", "Wszyscy uczniowie byli pytani w przeci¹gu 3 dni, nie mo¿na nikogo zapytaæ", "OK");
        }
        else
        {
            await DisplayAlert("Wylosowanu ucznia", $"Dzisiaj pytany bêdzie {s.Name} {s.Surname}", "OK");
        }
       
        ((Models.AllStudents)BindingContext).LoadStudents(((Models.AllStudents)BindingContext).getClassNumber());
        StudentluckyNumber.Text = ((Models.AllStudents)BindingContext).luckyNumber.ToString();
    }

    private void DeleteStudent(object sender, EventArgs e)
    {
       ((Models.AllStudents)BindingContext).DeleteStudent(((Models.AllStudents)BindingContext).getClassNumber(), StudentNameD.Text, StudentSurnameD.Text);
       ((Models.AllStudents)BindingContext).LoadStudents(((Models.AllStudents)BindingContext).getClassNumber());
    }


}