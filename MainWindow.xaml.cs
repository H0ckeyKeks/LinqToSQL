using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using LinqToSQL.CSharpTutorialDBDataSetTableAdapters;
using System.Xml.Serialization;

namespace LinqToSQL
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Creating an object of the LinqTOSQLDataClasses Class
        LinqToSQLDataClassesDataContext dataContext;

        public MainWindow()
        {
            InitializeComponent();

            // Creating the connection to the data source
            string connectionString = ConfigurationManager.ConnectionStrings["LinqToSQL.Properties.Settings.CSharpTutorialDBConnectionString"].ConnectionString;
            dataContext = new LinqToSQLDataClassesDataContext(connectionString);

            // InsertUniversities();
            // InsertStudents();
            // InsertLectures();
            // InsertStudentLectureAssociations();
            // GetUniversityOfTony();
            // GetLecturesFromTony();
            // GetAllStudentsFromYale();
            // GetAllUniversitiesWithTransgenders();
            // GetAllLecturesFromBeijingTech();
            // UpdateTony();
            // DeleteJames();
        }

        public void InsertUniversities()
        {
            // Deleting all entries in the database
            dataContext.ExecuteCommand("delete from University");
            
            // Creating new Universities
            University yale = new University();
            yale.Name = "Yale";

            University beijingTech = new University();
            beijingTech.Name = "Beijing Tech";

            // Inserting "Yale" into datatable
            dataContext.University.InsertOnSubmit(yale);
            dataContext.University.InsertOnSubmit(beijingTech);

            // Committing changes
            dataContext.SubmitChanges();

            // Making the MainWindow show all the data of University
            MainDataGrid.ItemsSource = dataContext.University;
        }

        public void InsertStudents()
        {
            // Lambda-Expression does this:
            // "from University in dataContext.University where university == "Yale" select university"
            // It checks for the first entry in Yale and stores that entry in Yale 
            // Returns an actual object of yale that can be used later on
            University yale = dataContext.University.First(un => un.Name.Equals("Yale"));
            University beijingTech = dataContext.University.First(un => un.Name.Equals("Beijing Tech"));

            List<Student> students = new List<Student>();

            // Adding a student to students list
            students.Add(new Student { Name = "Carla", Gender = "female", UniversityId = yale.Id });
            students.Add(new Student { Name = "Tony", Gender = "male", University = yale });
            students.Add(new Student { Name = "Leyla", Gender = "female", University = beijingTech });
            students.Add(new Student { Name = "James", Gender = "trans-gender", University = beijingTech });

            dataContext.Student.InsertAllOnSubmit(students);

            dataContext.SubmitChanges();

            MainDataGrid.ItemsSource = dataContext.Student;
        }

        public void InsertLectures()
        {
            dataContext.Lecture.InsertOnSubmit(new Lecture { Name = "Math" });
            dataContext.Lecture.InsertOnSubmit(new Lecture { Name = "History" });

            dataContext.SubmitChanges();
            MainDataGrid.ItemsSource = dataContext.Lecture;
        }

        public void InsertStudentLectureAssociations()
        {
            Student Carla = dataContext.Student.First(st => st.Name.Equals("Carla"));
            Student Tony = dataContext.Student.First(st => st.Name.Equals("Tony"));
            Student Leyla = dataContext.Student.First(st => st.Name.Equals("Leyla"));
            Student James = dataContext.Student.First(st => st.Name.Equals("James"));

            Lecture Math = dataContext.Lecture.First(lc => lc.Name.Equals("Math"));
            Lecture History = dataContext.Lecture.First(lc => lc.Name.Equals("History"));

            dataContext.StudentLecture.InsertOnSubmit(new StudentLecture { Student = Carla, Lecture = Math });
            dataContext.StudentLecture.InsertOnSubmit(new StudentLecture { Student = Tony, Lecture = Math });
            dataContext.StudentLecture.InsertOnSubmit(new StudentLecture { Student = Leyla, Lecture = History });
            // Alternative
            StudentLecture slTony = new StudentLecture();
            slTony.Student = Tony;
            slTony.Lecture = History;
            dataContext.StudentLecture.InsertOnSubmit(slTony);

            dataContext.SubmitChanges();

            MainDataGrid.ItemsSource = dataContext.StudentLecture;
        }

        public void GetUniversityOfTony()
        {
            // Using Linq-objects
            Student Tony = dataContext.Student.First(st => st.Name.Equals("Tony"));
            University TonysUniversity = Tony.University;

            // Needing to create a list because ItemsSource expects an IEnumerable and TonysUniversity is just one object
            List<University> universities = new List<University>();
            universities.Add(TonysUniversity);

            MainDataGrid.ItemsSource = universities;
        }

        public void GetLecturesFromTony()
        {
            Student Tony = dataContext.Student.First(st => st.Name.Equals("Tony"));

            var toniesLectures = from sl in Tony.StudentLecture select sl.Lecture;

            MainDataGrid.ItemsSource = toniesLectures;
        }

        public void GetAllStudentsFromYale()
        {
            var studentsFromYale = from student in dataContext.Student
                                   where student.University.Name == "Yale"
                                   select student;

            MainDataGrid.ItemsSource = studentsFromYale;
        }

        public void GetAllUniversitiesWithTransgenders()
        {
            var universitiesWithTransgenders = from student in dataContext.Student
                                               join university in dataContext.University
                                               on student.University equals university
                                               where student.Gender == "trans-gender"
                                               select university;

            MainDataGrid.ItemsSource = universitiesWithTransgenders;
        }

        public void GetAllLecturesFromBeijingTech()
        {
            var lecturesFromBeijingTech = from sl in dataContext.StudentLecture
                                          join student in dataContext.Student on sl.StudentId equals student.Id
                                          where student.University.Name == "Beijing Tech"
                                          select sl.Lecture;

            MainDataGrid.ItemsSource = lecturesFromBeijingTech;
        }

        // Using Linq to update data
        public void UpdateTony()
        {
            Student Tony = dataContext.Student.FirstOrDefault(st => st.Name == "Tony");

            Tony.Name = "Antonio";

            dataContext.SubmitChanges();

            MainDataGrid.ItemsSource = dataContext.Student;
        }

        public void DeleteJames()
        {
            Student James = dataContext.Student.FirstOrDefault(st => st.Name == "James");

            dataContext.Student.DeleteOnSubmit(James);
            dataContext.SubmitChanges();

            MainDataGrid.ItemsSource = dataContext.Student;

        }
    }
}
