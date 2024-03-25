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
    public class SubjectController : MainController
    {
        private readonly ApplicationDbContext _schoolContext;
        public SubjectController(IConfiguration configuration, ApplicationDbContext dbContext, ILogger<MainController> logger)
            : base(configuration, dbContext, logger)
        {
            _schoolContext = dbContext;
        }

        /// <summary>
        /// -- Insert a new Subject --
        /// </summary>
        // Post : /api/SubjectsAPI

        [HttpPost]
        public async Task<IActionResult> InsertSubject(Subject subject)
        {
            _schoolContext.Subject.Add(subject);
            await _schoolContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetSubject), new { id = subject.Id }, subject);
        }

        /// <summary>
        /// -- Retrieves all subjects from the database -- 
        /// </summary>
        // Get : /api/SubjectsAPI

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {
            try
            {
                var subjects = await _schoolContext.Subject.ToListAsync();
                if (subjects == null || subjects.Count == 0)
                {
                    return NotFound("No subject found.");
                }
                return Ok(subjects);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving subjects: {ex.Message}");
            }
        }

        /// <summary>
        /// -- Retrieves a specific Subject from the database --
        /// </summary>
        // Get : /api/SubjectsAPI/{Id}

        [HttpGet("{Id}")]
        public async Task<ActionResult<Subject>> GetSubject(int Id)
        {
            if (_schoolContext.Subject is null)
            {
                return NotFound();
            }

            var subject = await _schoolContext.Subject.FindAsync(Id);
            if (subject is null)
            {
                return NotFound();
            }
            return subject;
        }

        /// <summary>
        /// -- Delete all Subjects from the database -- 
        /// </summary>
        // Delete : /api/SubjectsAPI

        [HttpDelete]
        public async Task<IActionResult> DeleteSubjects()
        {
            try
            {
                var subjects = await _schoolContext.Subject.ToListAsync();
                _schoolContext.Subject.RemoveRange(subjects);
                await _schoolContext.SaveChangesAsync();
                return Ok("All subjects have been deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting all subjects: {ex.Message}");
            }
        }

        /// <summary>
        /// -- Delete a specific subject from the database -- 
        /// </summary>
        // Delete : /api/SubjectsAPI/{id}

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubjects(int id)
        {
            try
            {
                var subject = await _schoolContext.Subject.FindAsync(id);
                if (subject == null)
                {
                    return NotFound($"Subject with ID {id} not found.");
                }

                _schoolContext.Subject.Remove(subject);
                await _schoolContext.SaveChangesAsync();

                return Ok($"Subject with ID {id} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the subject: {ex.Message}");
            }
        }

        /// <summary>
        /// -- Update a specific Subject from the database -- 
        /// </summary>
        // Put : /api/SubjectsAPI/{id}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(int id, Subject updatedSubject)
        {
            try
            {
                var ExistingSubject = await _schoolContext.Subject.FindAsync(id);
                if (ExistingSubject == null)
                {
                    return NotFound($"Subject with ID {id} not found.");
                }

                ExistingSubject.Name = updatedSubject.Name;
                await _schoolContext.SaveChangesAsync();

                return Ok(ExistingSubject);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating the subject: {ex.Message}");
            }
        }
    }
}
