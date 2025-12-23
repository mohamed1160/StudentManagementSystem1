

// ================= Instructor =================
class Instructor
{
    public int InstructorId;
    public string Name;
    public string Specialization;

    public string PrintDetails()
    {
        return $"ID: {InstructorId}, Name: {Name}, Specialization: {Specialization}";
    }
}

// ================= Course =================
class Course
{
    public int CourseId;
    public string Title;
    public Instructor Instructor;

    public string PrintDetails()
    {
        return $"ID: {CourseId}, Title: {Title}, Instructor: {Instructor.Name}";
    }
}

// ================= Student =================
class Student
{
    public int StudentId;
    public string Name;
    public int Age;
    public List<Course> Courses = new List<Course>();

    public bool Enroll(Course course)
    {
        foreach (Course c in Courses)
        {
            if (c.CourseId == course.CourseId)
                return false;
        }
        Courses.Add(course);
        return true;
    }

    public bool IsEnrolledInCourse(string courseName)
    {
        foreach (Course c in Courses)
        {
            if (c.Title == courseName)
                return true;
        }
        return false;
    }

    public string PrintDetails()
    {
        string courseList = "None";
        if (Courses.Count > 0)
        {
            courseList = "";
            foreach (Course c in Courses)
                courseList += c.Title + ", ";
        }

        return $"ID: {StudentId}, Name: {Name}, Age: {Age}, Courses: {courseList}";
    }
}

// ================= SchoolStudentManager =================
class SchoolStudentManager
{
    public List<Student> Students = new List<Student>();
    public List<Course> Courses = new List<Course>();
    public List<Instructor> Instructors = new List<Instructor>();

    public void AddStudent(Student s) => Students.Add(s);
    public void AddInstructor(Instructor i) => Instructors.Add(i);
    public void AddCourse(Course c) => Courses.Add(c);

    public Student FindStudent(int id)
    {
        foreach (Student s in Students)
            if (s.StudentId == id)
                return s;
        return null;
    }

    public Course FindCourse(int id)
    {
        foreach (Course c in Courses)
            if (c.CourseId == id)
                return c;
        return null;
    }

    public bool EnrollStudentInCourse(int studentId, int courseId)
    {
        Student s = FindStudent(studentId);
        Course c = FindCourse(courseId);

        if (s == null || c == null)
            return false;

        return s.Enroll(c);
    }

    // ===== Bonus 12 =====
    public string GetInstructorNameByCourseName(string courseName)
    {
        foreach (Course c in Courses)
        {
            if (c.Title == courseName)
                return c.Instructor.Name;
        }
        return "Course Not Found";
    }
}

// ================= Program =================
class Program
{
    static void Main()
    {
        SchoolStudentManager manager = new SchoolStudentManager();
        int choice;

        do
        {
            Console.WriteLine("\n--- Student Management System ---");
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Add Instructor");
            Console.WriteLine("3. Add Course");
            Console.WriteLine("4. Enroll Student in Course");
            Console.WriteLine("5. Show All Students");
            Console.WriteLine("6. Show All Courses");
            Console.WriteLine("7. Show All Instructors");
            Console.WriteLine("8. Find Student by ID");
            Console.WriteLine("9. Find Course by ID");
            Console.WriteLine("10. Exit");
            Console.WriteLine("11. Check if student enrolled in course");
            Console.WriteLine("12. Get instructor name by course name");
            Console.Write("Choose: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Student s = new Student();
                    Console.Write("Student ID: ");
                    s.StudentId = int.Parse(Console.ReadLine());
                    Console.Write("Name: ");
                    s.Name = Console.ReadLine();
                    Console.Write("Age: ");
                    s.Age = int.Parse(Console.ReadLine());
                    manager.AddStudent(s);
                    break;

                case 2:
                    Instructor i = new Instructor();
                    Console.Write("Instructor ID: ");
                    i.InstructorId = int.Parse(Console.ReadLine());
                    Console.Write("Name: ");
                    i.Name = Console.ReadLine();
                    Console.Write("Specialization: ");
                    i.Specialization = Console.ReadLine();
                    manager.AddInstructor(i);
                    break;

                case 3:
                    Course c = new Course();
                    Console.Write("Course ID: ");
                    c.CourseId = int.Parse(Console.ReadLine());
                    Console.Write("Title: ");
                    c.Title = Console.ReadLine();
                    Console.Write("Instructor ID: ");
                    int instId = int.Parse(Console.ReadLine());
                    c.Instructor = manager.Instructors.Find(x => x.InstructorId == instId);
                    manager.AddCourse(c);
                    break;

                case 4:
                    Console.Write("Student ID: ");
                    int sid = int.Parse(Console.ReadLine());
                    Console.Write("Course ID: ");
                    int cid = int.Parse(Console.ReadLine());
                    manager.EnrollStudentInCourse(sid, cid);
                    break;

                case 5:
                    foreach (Student st in manager.Students)
                        Console.WriteLine(st.PrintDetails());
                    break;

                case 6:
                    foreach (Course co in manager.Courses)
                        Console.WriteLine(co.PrintDetails());
                    break;

                case 7:
                    foreach (Instructor ins in manager.Instructors)
                        Console.WriteLine(ins.PrintDetails());
                    break;

                case 8:
                    Console.Write("Student ID: ");
                    Student fs = manager.FindStudent(int.Parse(Console.ReadLine()));
                    Console.WriteLine(fs != null ? fs.PrintDetails() : "Not Found");
                    break;

                case 11:
                    Console.Write("Student ID: ");
                    int stId = int.Parse(Console.ReadLine());
                    Console.Write("Course Name: ");
                    string cn = Console.ReadLine();
                    Student st2 = manager.FindStudent(stId);
                    Console.WriteLine(st2 != null && st2.IsEnrolledInCourse(cn)
                        ? "Student is enrolled"
                        : "Student is NOT enrolled");
                    break;

                case 12:
                    Console.Write("Course Name: ");
                    string courseName = Console.ReadLine();
                    Console.WriteLine(manager.GetInstructorNameByCourseName(courseName));
                    break;
            }

        } while (choice != 10);
    }
}
