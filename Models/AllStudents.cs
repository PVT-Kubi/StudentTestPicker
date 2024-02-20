using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTestPicker.Models
{
    class AllStudents
    {
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();

        public AllStudents(string klasa)
        {
            LoadStudents(klasa);
        }

        public void LoadStudents(string klasa)
        {
            Students.Clear();

            string path= FileSystem.AppDataDirectory;
            List<Student> students = new List<Student>();

            if(File.Exists(Path.Combine(path, $"{klasa}.cls.txt"))) 
            {
                string text = File.ReadAllText(path);
                string[] studentsData = text.Split('\n');
                foreach (string s in studentsData)
                {
                    string[] studentData = s.Split("\t");
                    if (s.Length > 1)
                    {
                        students.Add(new Student()
                        {
                            Number = int.Parse((string)studentData[0]),
                            Name = studentData[1],
                            Surname = studentData[2],
                            Presence = bool.Parse(studentData[3]),
                            AskedCount = int.Parse((string)studentData[4]),
                            ClassNumber = studentData[5]
                        });
                    }
                }

                foreach (Student student in students) 
                {
                    Students.Add(student);
                }
            }
        }

        public int AddStudent(string klasa, string name, string surname)
        {
            string path = FileSystem.AppDataDirectory;
            Student student = new Student()
            {
                Number = Students.Count + 1,
                Name = name,
                Surname = surname,
                Presence = true,
                AskedCount = 0,
                ClassNumber = klasa
            };

            if (!File.Exists(Path.Combine(path, $"{klasa}.cls.txt")))
                return 1;

            string studentData = $"\n{student.Number}\t{student.Name}\t{student.Surname}\t{student.Presence}\t{student.AskedCount}\t{student.ClassNumber}";

            File.WriteAllText(Path.Combine(path, $"{klasa}.cls.txt"), studentData);

            return 0;
        }

        public int ChangePresence(string klasa, string name, string surname)
        {
            foreach(Student s in Students)
            {
                if(s.Name == name && s.Surname == surname)
                {
                    Student student = s;
                    s.Presence = !s.Presence;
                    return 0;
                }
            }
            return 1;
        }
        public int DeleteStudent(string klasa, string name, string surname)
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, $"{klasa}.cls.txt");

            if(!File.Exists(path)) return 1;

           foreach (Student student in Students)
            {
                if(student.Name == name && student.Surname == surname)
                {
                    File.Delete(path); return 0;
                }
            }

            return 2;
        }
    }
}
