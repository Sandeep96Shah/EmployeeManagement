using EmployeeManagement.Entity;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<EmplyeeResponse>>> GetEmployees([FromQuery] EmployeeQuery query)
    {
        _logger.LogInformation("Get apis is reached.");
        var employees = await _employeeService.GetEmployees(query);
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Employee>> GetEmployeeById(Guid id)
    {
        var employee = await _employeeService.GetEmployeeById(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateEmployee(CreateEmployee employee)
    {
        _logger.LogInformation("Reached at create api.");
        if (await _employeeService.CreateEmployee(employee))
        {
            _logger.LogInformation("New Employee is created:{Name}", employee.Name);
            return StatusCode(201, new { Message = $"{employee.Name} created successfully." });
        }
        return BadRequest("Employee with the same email already exists.");
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<string>> UpdateEmployee(Guid id, Employee updatedEmployee)
    {
        updatedEmployee.Id = id; // Ensure the ID is set correctly
        if (await _employeeService.UpdateEmployee(updatedEmployee))
        {
            return StatusCode(204, new { Message = $"{updatedEmployee.Name} updated successfully." });
        }
        return NotFound(new { Message = "Employee not found." });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteEmployee(Guid id)
    {
        if (await _employeeService.DeleteEmployee(id))
        {
            return StatusCode(204, new { Message = $"Employee with ID {id} deleted successfully." });
        }
        return NotFound(new { Message = "Employee not found." });
    }

}