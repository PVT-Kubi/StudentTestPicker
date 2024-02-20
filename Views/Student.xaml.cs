using StudentTestPicker.Models;

namespace StudentTestPicker.Views;

public partial class Student : ContentView
{
    public static readonly BindableProperty StudentNumberProperty = BindableProperty.Create(nameof(StudentNumber), typeof(int), typeof(Student), 0);
    public static readonly BindableProperty StudentNameProperty = BindableProperty.Create(nameof(StudentName), typeof(string), typeof(Student), string.Empty);
    public static readonly BindableProperty StudentSurnameProperty = BindableProperty.Create(nameof(StudentSurname), typeof(string), typeof(Student), string.Empty);
    public static readonly BindableProperty StudentPresenceProperty = BindableProperty.Create(nameof(StudentPresence), typeof(bool), typeof(Student), true);
    public static readonly BindableProperty StudentAskedCountProperty = BindableProperty.Create(nameof(StudentAskedCount), typeof(int), typeof(Student), 0);
    public static readonly BindableProperty StudentClassProperty = BindableProperty.Create(nameof(StudentClass), typeof(string), typeof(Student), String.Empty);

    public string StudentClass
    {
        get => (string)GetValue(StudentClassProperty);
        set => SetValue(StudentClassProperty, value);
    }

    public int StudentNumber
    {
        get => (int)GetValue(Student.StudentNumberProperty);
        set => SetValue(Student.StudentNumberProperty, value);
    }

    public string StudentName
    {
        get => (string)GetValue(Student.StudentNameProperty);
        set => SetValue(Student.StudentNameProperty, value);
    }

    public string StudentSurname
    {
        get => (string)GetValue(Student.StudentSurnameProperty);
        set => SetValue(Student.StudentSurnameProperty, value);
    }

    public bool StudentPresence
    {
        get => (bool)GetValue(Student.StudentPresenceProperty);
        set => SetValue(Student.StudentPresenceProperty, value);
    }

    public int StudentAskedCount
    {
        get => (int)GetValue(Student.StudentAskedCountProperty);
        set => SetValue(Student.StudentAskedCountProperty, value);
    }


    public Student()
	{
		InitializeComponent();
	}

    private void SwitchPresence(object sender, EventArgs e)
    {
        AllStudents all = new AllStudents(StudentClass);
        all.ChangePresence(StudentClass, StudentName, StudentSurname);
        StudentPresence = !StudentPresence;
    }
}