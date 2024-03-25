using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using School.Controllers;
using School.Data;
using School.Models;
using System.Reflection.Metadata.Ecma335;

namespace school.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : MainController
    {
        private readonly ApplicationDbContext _schoolContext;
        public TeacherController(IConfiguration configuration, ApplicationDbContext dbContext, ILogger<MainController> logger)
            : base(configuration, dbContext, logger)
        {
            _schoolContext = dbContext;
        }

        /// <summary>
        /// -- Insert a new teacher --
        /// </summary>
        // POST: /api/TeachersAPI

        [HttpPost]
        public async Task<IActionResult> InsertTeacher(Teacher teacher)
        {
            _schoolContext.Teacher.Add(teacher);
            await _schoolContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTeatcher), new { id = teacher.Id }, teacher);
        }

        /// <summary>
        /// -- Retrieves all Teatchers from the database -- 
        /// </summary>
        // Get : /api/TeachersAPI

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeatchers()
        {
            try
            {
                var teachers = await _schoolContext.Teacher.ToListAsync();
                if (teachers == null || teachers.Count == 0)
                {
                    return NotFound("No teachers found.");
                }
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving teachers: {ex.Message}");
            }
        }

        /// <summary>
        /// -- Retrieves a specific teacher from the database --
        /// </summary>
        // Get: /api/TeachersAPI/{Id}

        [HttpGet("{Id}")]
        public async Task<ActionResult<Teacher>> GetTeatcher(int Id) 
        {
            if (_schoolContext.Teacher is null)
            {
                return NotFound();
            }

            var teacher = await _schoolContext.Teacher.FindAsync(Id); 
            if (teacher is null)
            {
                return NotFound();
            }
            return teacher;
        }

        /// <summary>
        /// -- Delete all Teachers from the database -- 
        /// </summary>
        // Delete : /api/TeachersAPI

        [HttpDelete]
        public async Task<IActionResult> DeleteAllTeachers()
        {
            try
            {
                var teachers = await _schoolContext.Teacher.ToListAsync();
                _schoolContext.Teacher.RemoveRange(teachers);
                await _schoolContext.SaveChangesAsync();
                return Ok("All teachers have been deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting all teachers: {ex.Message}");
            }
        }

        /// <summary>
        /// -- Delete a specific teacher from the database -- 
        /// </summary>
        // Delete : /api/TeachersAPI/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                var teacher = await _schoolContext.Teacher.FindAsync(id);
                if (teacher == null)
                {
                    return NotFound($"Teacher with ID {id} not found.");
                }

                _schoolContext.Teacher.Remove(teacher);
                await _schoolContext.SaveChangesAsync();

                return Ok($"Teacher with ID {id} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the teacher: {ex.Message}");
            }
        }

        /// <summary>
        /// -- Update a specific teacher from the database -- 
        /// </summary>
        // Put : api/TeachersAPI/{id}
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, Teacher updatedTeacher)
        {
            try
            {
                var existingTeacher = await _schoolContext.Teacher.FindAsync(id);
                if (existingTeacher == null)
                {
                    return NotFound($"Teacher with ID {id} not found.");
                }

                existingTeacher.Name = updatedTeacher.Name;
                existingTeacher.Email = updatedTeacher.Email;
                existingTeacher.PhoneNumber = updatedTeacher.PhoneNumber;
                await _schoolContext.SaveChangesAsync();

                return Ok(existingTeacher); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the teacher: {ex.Message}");
            }
        }
    }
}