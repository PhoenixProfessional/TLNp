// CoursesController.cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TLNp; // Assuming your models are in the TLNp.Models namespace
using Microsoft.EntityFrameworkCore; // Add this for DbContext

[Route("api/[controller]")]
[ApiController]
public class CoursesController : ControllerBase
{
    private readonly List<Course> _courses;

    public CoursesController()
    {
        _courses = new List<Course>
        {
            new Course { CourseId = 1, CourseName = "Mathematics" },
            new Course { CourseId = 2, CourseName = "Science" }
        };
    }

    [HttpGet]
    public ActionResult<IEnumerable<Course>> Get()
    {
        return Ok(_courses);
    }

    [HttpGet("{id}")]
    public ActionResult<Course> GetById(int id)
    {
        var course = _courses.Find(c => c.CourseId == id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }

    [HttpPost]
    public ActionResult<Course> Create(Course course)
    {
        course.CourseId = _courses.Count + 1;
        _courses.Add(course);
        return CreatedAtAction(nameof(GetById), new { id = course.CourseId }, course);
    }

    [HttpPut("{id}")]
    public ActionResult<Course> Update(int id, Course updatedCourse)
    {
        var course = _courses.Find(c => c.CourseId == id);
        if (course == null)
        {
            return NotFound();
        }

        course.CourseName = updatedCourse.CourseName;

        return Ok(course);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var course = _courses.Find(c => c.CourseId == id);
        if (course == null)
        {
            return NotFound();
        }

        _courses.Remove(course);
        return NoContent();
    }
}

// StudentsController.cs (add enrollment methods)


[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly List<Student> _students;
    private readonly List<Course> _courses;

    public StudentsController()
    {
        _students = new List<Student>
        {
            new Student { StudentId = 1, FirstName = "John", LastName = "Doe" },
            new Student { StudentId = 2, FirstName = "Jane", LastName = "Smith" }
        };

        _courses = new List<Course>
        {
            new Course { CourseId = 1, CourseName = "Mathematics" },
            new Course { CourseId = 2, CourseName = "Science" }
        };
    }

    [HttpGet]
    public ActionResult<IEnumerable<Student>> Get()
    {
        return Ok(_students);
    }

    [HttpGet("{id}")]
    public ActionResult<Student> GetById(int id)
    {
        var student = _students.Find(s => s.StudentId == id);
        if (student == null)
        {
            return NotFound();
        }
        return Ok(student);
    }

    [HttpPost]
    public ActionResult<Student> Create(Student student)
    {
        student.StudentId = _students.Count + 1;
        _students.Add(student);
        return CreatedAtAction(nameof(GetById), new { id = student.StudentId }, student);
    }

    [HttpPut("{id}")]
    public ActionResult<Student> Update(int id, Student updatedStudent)
    {
        var student = _students.Find(s => s.StudentId == id);
        if (student == null)
        {
            return NotFound();
        }

        student.FirstName = updatedStudent.FirstName;
        student.LastName = updatedStudent.LastName;

        return Ok(student);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var student = _students.Find(s => s.StudentId == id);
        if (student == null)
        {
            return NotFound();
        }

        _students.Remove(student);
        return NoContent();
    }

    [HttpPost("{studentId}/enroll/{courseId}")]
    public ActionResult Enroll(int studentId, int courseId)
    {
        var student = _students.Find(s => s.StudentId == studentId);
        var course = _courses.Find(c => c.CourseId == courseId);

        if (student == null || course == null)
        {
            return NotFound();
        }

        var enrollment = new Enrollment
        {
            StudentId = studentId,
            CourseId = courseId
        };

        student.Enrollments.Add(enrollment);

        return Ok(enrollment);
    }

    [HttpDelete("{studentId}/enroll/{courseId}")]
    public ActionResult Unenroll(int studentId, int courseId)
    {
        var student = _students.Find(s => s.StudentId == studentId);
        var course = _courses.Find(c => c.CourseId == courseId);

        if (student == null || course == null)
        {
            return NotFound();
        }

        var enrollment = student.Enrollments.FirstOrDefault(e => e.CourseId == courseId);
        if (enrollment != null)
        {
            student.Enrollments.Remove(enrollment);
        }

        return NoContent();
    }
}
