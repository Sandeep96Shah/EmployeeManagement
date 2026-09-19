using EmployeeManagement.Entity;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DepartmentsController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentsController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<ActionResult<List<DepartmentResponse>>> GetDepartments()
    {
        var departments = await _departmentService.GetDepartments();
        return Ok(departments);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Department>> GetDepartmentById(Guid id)
    {
        var department = await _departmentService.GetDepartmentById(id);
        if (department == null)
        {
            return NotFound();
        }
        return Ok(department);
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateDepartment(CreateDepartment department)
    {
        if (await _departmentService.CreateDepartment(department))
        {
            return StatusCode(201, new { Message = $"{department.Name} created successfully." });
        }
        return BadRequest("Department with the same name already exists.");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> UpdateDepartment(Guid id, Department updatedDepartment)
    {
        updatedDepartment.Id = id; // Ensure the ID is set correctly
        if (await _departmentService.UpdateDepartment(updatedDepartment))
        {
            return StatusCode(204, new { Message = $"{updatedDepartment.Name} updated successfully." });
        }
        return NotFound(new { Message = "Department not found." });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteDepartment(Guid id)
    {
        if (await _departmentService.DeleteDepartment(id))
        {
            return StatusCode(204, new { Message = $"Department with ID {id} deleted successfully." });
        }
        return NotFound(new { Message = "Department not found." });
    }

}