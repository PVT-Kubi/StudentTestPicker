

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
    }

    public void Add_Student()
    {
        ((Models.AllStudents)BindingContext).AddStudent(((Models.AllStudents)BindingContext).getClassNumber(), StudentName.Text, StudentSurname.Text);
        ((Models.AllStudents)BindingContext).LoadStudents(((Models.AllStudents)BindingContext).getClassNumber());
    }
}