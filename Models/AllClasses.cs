using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentTestPicker.Models
{
    class AllClasses
    {
        public ObservableCollection<Klasa> Classes { get; set; } = new ObservableCollection<Klasa>();

        public AllClasses()
        {
            LoadClasses();
        }

        public void LoadClasses()
        {
            Classes.Clear();

            string appDataPath = FileSystem.AppDataDirectory;

            IEnumerable<Klasa> classes = Directory
                .EnumerateFiles(appDataPath, "*.cls.txt")
                .Select(filename => new Klasa()
                {
                    Filename = filename,
                    Text = File.ReadAllText(filename)
                });

            foreach (Klasa klasa in classes)
            {
                Classes.Add(klasa);
            }
        }

        public int AddClass(string name)
        {
            string appDataPath = FileSystem.AppDataDirectory;
            string className = $"{name}.cls.json";

            if(File.Exists(Path.Combine(appDataPath, className)))
            {
                return 1; 
            }

            File.WriteAllText(Path.Combine(appDataPath, className), "");
            return 0;
        }

        public int DeleteClass(string klasa)
        {
            string path = Path.Combine(FileSystem.AppDataDirectory, $"/{klasa}.cls.txt");

            if (!File.Exists(path)) return 1;

            File.Delete(path);

            return 0;
        }
    }
}
