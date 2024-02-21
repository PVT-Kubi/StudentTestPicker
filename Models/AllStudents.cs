using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace StudentTestPicker.Models
{
    class AllStudents
    {
        public ObservableCollection<Student> Students { get; set; } = new ObservableCollection<Student>();
        public string classNumber;
        public int luckyNumber { get; set; } = 0;

        public AllStudents(string klasa)
        {
            classNumber = klasa;
            LoadStudents(klasa);
            rollLuckyNumber();
        }

        public void rollLuckyNumber()
        {
            Random rnd = new Random();
            luckyNumber = rnd.Next(Students.Count) + 1;
        }

        public string getClassNumber()
        {
            return classNumber;
        }

        public void LoadStudents(string klasa)
        {
            Students.Clear();

            string path= FileSystem.AppDataDirectory;
            List<Student> students = new List<Student>();

            if(File.Exists(Path.Combine(path, $"{klasa}.cls.txt"))) 
            {
                string text = File.ReadAllText(Path.Combine(path, $"{klasa}.cls.txt"));
                string[] studentsData = text.Split('\n');
                foreach (string s in studentsData)
                {
                    string[] studentData = s.Split("\t");
                    if (studentData.Length > 2)
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

        public Student DrawStudent(string classNumber)
        {
            List<Student> classStudents = new List<Student>();
            Random rnd = new Random();
            int counterOfUnavaiable = 0;
            string path = Path.Combine(FileSystem.AppDataDirectory, $"{classNumber}.cls.txt");
            foreach (Student student in Students)
            {
                if(student.ClassNumber == classNumber)
                {
                    classStudents.Add(student);
                    if(student.AskedCount != 0 || student.Number == luckyNumber)
                    {
                        counterOfUnavaiable++;
                    }
                }
            }

            if (counterOfUnavaiable >= classStudents.Count)
            {
                foreach (Student s in classStudents)
                {
                    if (s.AskedCount == 3)
                        s.AskedCount = 0;
                    else
                        s.AskedCount++;

                    string text = File.ReadAllText(path);
                    string[] studentData = text.Split("\n");
                    for (int i = 0; i < studentData.Length; i++)
                    {
                        string[] stD = studentData[i].Split("\t");
                        if (stD.Length > 1 && stD[1] == s.Name && stD[2] == s.Surname)
                        {
                            studentData[i] = $"{s.Number}\t{s.Name}\t{s.Surname}\t{s.Presence}\t{s.AskedCount}\t{s.ClassNumber}";
                            File.WriteAllText(path, string.Join("\n", studentData));
                        }
                    }

                }
                rollLuckyNumber();
                return new Student();
            }

            while(true)
            {
                int random = rnd.Next(classStudents.Count);  
                Student target = classStudents[random];
                if(target.AskedCount == 0 && target.Presence) 
                {
                    foreach(Student s in classStudents)
                    {
                        if (s.AskedCount == 3)
                            s.AskedCount = 0;
                        else if (s.Name != target.Name && s.Surname != target.Surname && s.AskedCount == 0)
                            continue;
                        else if (s.Number == luckyNumber)
                            continue;
                        else
                            s.AskedCount++;

                        string text = File.ReadAllText(path);
                        string[] studentData = text.Split("\n");
                        for (int i = 0; i < studentData.Length; i++)
                        {
                            string[] stD = studentData[i].Split("\t");
                            if (stD.Length > 1 && stD[1] == s.Name && stD[2] == s.Surname)
                            {
                                studentData[i] = $"{s.Number}\t{s.Name}\t{s.Surname}\t{s.Presence}\t{s.AskedCount}\t{s.ClassNumber}";
                                File.WriteAllText(path, string.Join("\n", studentData));
                            }
                        }
                    }
                    rollLuckyNumber();
                    return target;
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
            string text = File.ReadAllText(Path.Combine(path, $"{klasa}.cls.txt"));
            string studentData = text + $"\n{student.Number}\t{student.Name}\t{student.Surname}\t{student.Presence}\t{student.AskedCount}\t{student.ClassNumber}";

            File.WriteAllText(Path.Combine(path, $"{klasa}.cls.txt"), studentData);
            Students.Add(student);

            return 0;
        }

        public int ChangePresence(string klasa, int number, string name, string surname, bool presence, int askedCount)
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, $"{klasa}.cls.txt");
            for(int j = 0; j < Students.Count; j++)
            {
                if (Students[j].Name == name && Students[j].Surname == surname)
                {
                    Students[j].Presence = presence;
                    string text = File.ReadAllText(path);
                    string[] studentData = text.Split("\n");
                    for(int i = 0; i < studentData.Length; i++)
                    {
                        string[] stD = studentData[i].Split("\t");
                        if (stD.Length > 1 && stD[1] == name && stD[2] == surname)
                        {
                            studentData[i] = $"{number}\t{name}\t{surname}\t{presence}\t{askedCount}\t{klasa}";
                            File.WriteAllText(path, string.Join("\n", studentData));
                            
                            return 0;
                        }
                    }
                }
            }

            return 1;
        }
        public int DeleteStudent(string klasa, string name, string surname)
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, $"{klasa}.cls.txt");
            string new_text = "";
            string text = File.ReadAllText(path);
            string[] studentData = text.Split("\n");
            for(int i = 0; i < studentData.Length; i++)
            {
                string[] stD = studentData[i].Split("\t");
                if (stD.Length == 1)
                {
                    new_text += $"{stD[0]}";
                }else if (stD.Length > 2 && stD[1] != name && stD[2] != surname)
                {
                    new_text += $"\n{stD[0]}\t{stD[1]}\t{stD[2]}\t{stD[3]}\t{stD[4]}\t{stD[5]}";
                }
            }

            File.WriteAllText(path, new_text);

            return 1;
        }
    }
}
