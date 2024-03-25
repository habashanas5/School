using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using School.Data;
using School.Models;

namespace School.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : MainController
    {
        private readonly ApplicationDbContext _schoolContext;
        public StudentController(IConfiguration configuration, ApplicationDbContext dbContext, ILogger<MainController> logger)
           : base(configuration, dbContext, logger)
        {
            _schoolContext = dbContext;
        }

        /// <summary>
        /// -- Insert a new student --
        /// </summary>
        // Post : /api/StudentsAPI

        [HttpPost]
        public async Task<IActionResult> InsertStudent(Student student)
        {
            _schoolContext.Student.Add(student);
            await _schoolContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudent), new { id = student.Id }, student);
        }

        /// <summary>
        /// -- Retrieves all students from the database -- 
        /// </summary>
        // Get : /api/StudentsAPI

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            try
            {
                var students = await _schoolContext.Student.ToListAsync();
                if (students == null || students.Count == 0)
                {
                    return NotFound("No student found.");
                }
                return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving students: {ex.Message}");
            }
        }

        /// <summary>
        /// -- Retrieves a specific student from the database --
        /// </summary>
        // Get : /api/StudentsAPI/{Id}

        [HttpGet("{Id}")]
        public async Task<ActionResult<Student>> GetStudent(int Id)
        {
            if (_schoolContext.Student is null)
            {
                return NotFound();
            }

            var student = await _schoolContext.Student.FindAsync(Id);
            if (student is null)
            {
                return NotFound();
            }
            return student;
        }

        /// <summary>
        /// -- Delete all students from the database -- 
        /// </summary>
        // Delete : /api/StudentsAPI

        [HttpDelete]
        public async Task<IActionResult> DeleteStudents()
        {
            try
            {
                var students = await _schoolContext.Student.ToListAsync();
                _schoolContext.Student.RemoveRange(students);
                await _schoolContext.SaveChangesAsync();
                return Ok("All students have been deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting all students: {ex.Message}");
            }
        }

        /// <summary>
        /// -- Delete a specific student from the database -- 
        /// </summary>
        // Delete : /api/StudentsAPI/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            try
            {
                var student = await _schoolContext.Student.FindAsync(id);
                if (student == null)
                {
                    return NotFound($"Student with ID {id} not found.");
                }

                _schoolContext.Student.Remove(student);
                await _schoolContext.SaveChangesAsync();

                return Ok($"Student with ID {id} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the student: {ex.Message}");
            }
        }

        /// <summary>
        /// -- Update a specific student from the database -- 
        /// </summary>
        // Put : /api/StudentsAPI/{id}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id, Student updatedStudent)
        {
            try
            {
                var ExistingStudent = await _schoolContext.Student.FindAsync(id);
                if (ExistingStudent == null)
                {
                    return NotFound($"Student with ID {id} not found.");
                }

                ExistingStudent.Name = updatedStudent.Name;
                ExistingStudent.Email = updatedStudent.Email;
                ExistingStudent.PhoneNumber = updatedStudent.PhoneNumber;
                await _schoolContext.SaveChangesAsync();

                return Ok(ExistingStudent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the student: {ex.Message}");
            }
        }
    }
}
