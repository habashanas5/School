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
    public class AssignedMaterialController : MainController
    {
        private readonly ApplicationDbContext _schoolContext;
        public AssignedMaterialController(IConfiguration configuration, ApplicationDbContext dbContext, ILogger<MainController> logger)
            : base(configuration, dbContext, logger)
        {
            _schoolContext = dbContext;
        }

        /// <summary>
        /// -- Insert a new AssignedMaterials -- 
        /// </summary>
        // Post : /api/AssignedMaterialsAPI

        [HttpPost]
        public async Task<IActionResult> InsertAssignedMaterial(int studentId, int subjectId)
        {
            try
            {
                // Retrieve the student, teacher, and subject from the database
                var student = await _schoolContext.Student.FindAsync(studentId);
                var subject = await _schoolContext.Subject.FindAsync(subjectId);

                // Check if any of them is null
                if (student == null || subject == null)
                {
                    return BadRequest("One or more entities not found.");
                }

                // Create a new AssignedMaterial instance
                var assignedMaterial = new AssignedMaterial
                {
                    Student = student,
                    Subject = subject
                };

                // Add the assigned material to the context
                _schoolContext.AssignedMaterial.Add(assignedMaterial);
                await _schoolContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAssignedMaterial), new { id = assignedMaterial.AssignedMaterialID }, assignedMaterial);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while adding assigned material: {ex.Message}");
            }
        }

        /// <summary>
        /// -- Retrieves all AssignedMaterials from the database -- 
        /// </summary>
        // Get : /api/AssignedMaterialsAPI

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssignedMaterial>>> GetAssignedMaterials()
        {
            try
            {
                var assignedMaterials = await _schoolContext.AssignedMaterial.ToListAsync();
                if (assignedMaterials == null || assignedMaterials.Count == 0)
                {
                    return NotFound("No assigned Materials found.");
                }
                return Ok(assignedMaterials);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving assigned Materials : {ex.Message}");
            }
        }


        /// <summary>
        /// -- Retrieves a specific AssignedMaterial from the database --
        /// </summary>
        // Get : /api/AssignedMaterialsAPI/{Id}

        [HttpGet("{Id}")]
        public async Task<ActionResult<AssignedMaterial>> GetAssignedMaterial(int Id)
        {
            if (_schoolContext.AssignedMaterial is null)
            {
                return NotFound();
            }

            var assignedMaterial = await _schoolContext.AssignedMaterial.FindAsync(Id);
            if (assignedMaterial is null)
            {
                return NotFound();
            }
            return assignedMaterial;
        }

        /// <summary>
        /// -- Delete all AssignedMaterial from the database -- 
        /// </summary>
        // Delete : /api/AssignedMaterialsAPI

        [HttpDelete]
        public async Task<IActionResult> DeleteAllAssignedMaterial()
        {
            try
            {
                var assignedMaterials = await _schoolContext.AssignedMaterial.ToListAsync();
                _schoolContext.AssignedMaterial.RemoveRange(assignedMaterials);
                await _schoolContext.SaveChangesAsync();
                return Ok("All Assigned Materials have been deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting all Assigned Materials: {ex.Message}");
            }
        }


        /// <summary>
        /// -- Delete a specific AssignedMaterials from the database -- 
        /// </summary>
        // Delete : /api/AssignedMaterialsAPI/{id} 

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssignedMaterial(int id)
        {
            try
            {
                var assignedMaterial = await _schoolContext.AssignedMaterial.FindAsync(id);
                if (assignedMaterial == null)
                {
                    return NotFound($"Assigned Material with ID {id} not found.");
                }

                _schoolContext.AssignedMaterial.Remove(assignedMaterial);
                await _schoolContext.SaveChangesAsync();

                return Ok($"Assigned Material with ID {id} has been deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while deleting the Assigned Material: {ex.Message}");
            }
        }

        /// <summary>
        /// -- Update a specific AssignedMaterial from the database -- 
        /// </summary>
        // Put : /api/AssignedMaterialsAPI/{id}

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignedMaterial(int id, int studentId, int subjectId)
        {
            try
            {
                var ExistingMaterial = await _schoolContext.AssignedMaterial.FindAsync(id);
                if (ExistingMaterial == null)
                {
                    return NotFound($"Assigned material with ID {id} not found.");
                }

                var student = await _schoolContext.Student.FindAsync(studentId);
                var subject = await _schoolContext.Subject.FindAsync(subjectId);

                if (student == null || subject == null)
                {
                    return BadRequest("One or more entities not found.");
                }

                ExistingMaterial.Student = student;
                ExistingMaterial.Subject = subject;

                await _schoolContext.SaveChangesAsync();

                return Ok($"Assigned material with ID {id} has been updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while updating assigned material: {ex.Message}");
            }
        }
    }
}
