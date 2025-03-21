# LinqToSQL

# Description
This application demonstrates the use of LINQ to SQL in a WPF application for managing universities, students, and lectures. It provides various operations (Create, Read, Update, Delete) to manipulate data in an SQL database.

# Requirements
- .NET Framework or .NET Core with WPF support
- Visual Studio (recommended)
- SQL Server or a compatible database

# Installation and Setup
- Clone or download the repository.
- Open the project file (.sln) in Visual Studio.
- Ensure that the connection string in App.config is correct:
    <connectionStrings>
        <add name="LinqToSQL.Properties.Settings.CSharpTutorialDBConnectionString" 
             connectionString="Your-Connection-String" 
             providerName="System.Data.SqlClient" />
    </connectionStrings>
- Ensure that the CSharpTutorialDB database exists and the necessary tables are created.

# Features
**1. Inserting Data**
- InsertUniversities(): Adds universities.
- InsertStudents(): Adds students and associates them with universities.
- InsertLectures(): Adds lectures.
- InsertStudentLectureAssociations(): Creates associations between students and lectures.

**2. Querying Data**
- GetUniversityOfTony(): Retrieves Tony’s university.
- GetLecturesFromTony(): Displays Tony’s lectures.
- GetAllStudentsFromYale(): Shows all students from Yale University.
- GetAllUniversitiesWithTransgenders(): Displays all universities with transgender students.
- GetAllLecturesFromBeijingTech(): Shows all lectures from Beijing Tech University.

**3. Updating Data**
- UpdateTony(): Changes Tony’s name to Antonio.

**4. Deleting Data**
- DeleteJames(): Removes the student James from the database.

# Usage
- Run the application in Visual Studio.
- Call the desired methods by activating the corresponding buttons or via code.
- The results will be displayed in a DataGrid in the user interface.
