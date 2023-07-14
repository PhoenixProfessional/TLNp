namespace TLNp
{
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;

    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly List<Student> _students;

        public StudentsController()
        {
            // In a real-world scenario, you would retrieve data from a database
            // For simplicity, we'll use an in-memory list
            _students = new List<Student>
        {
            new Student { StudentId = 1, FirstName = "John", LastName = "Doe" },
            new Student { StudentId = 2, FirstName = "Jane", LastName = "Smith" }
        };
        }

        // GET: api/students
        [HttpGet]
        public ActionResult<IEnumerable<Student>> Get()
        {
            return Ok(_students);
        }

        // GET: api/students/{id}
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

        // POST: api/students
        [HttpPost]
        public ActionResult<Student> Create(Student student)
        {
            // In a real-world scenario, you would validate the input data
            student.StudentId = _students.Count + 1;
            _students.Add(student);
            return CreatedAtAction(nameof(GetById), new { id = student.StudentId }, student);
        }

        // PUT: api/students/{id}
        [HttpPut("{id}")]
        public ActionResult<Student> Update(int id, Student updatedStudent)
        {
            var student = _students.Find(s => s.StudentId == id);
            if (student == null)
            {
                return NotFound();
            }

            // In a real-world scenario, you would validate the input data
            student.FirstName = updatedStudent.FirstName;
            student.LastName = updatedStudent.LastName;

            return Ok(student);
        }

        // DELETE: api/students/{id}
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
    }

}
