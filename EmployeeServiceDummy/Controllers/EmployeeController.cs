using EmployeeServiceDummy.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private static List<Employee> employees = new List<Employee>
    {
        new Employee { Id = 1, FirstName = "John", LastName = "Doe", Position = "Developer" },
        new Employee { Id = 2, FirstName = "Jane", LastName = "Smith", Position = "Manager" }
    };

    [HttpGet]
    public ActionResult<IEnumerable<Employee>> Get()
    {
        return Ok(employees);
    }

    [HttpGet("{id}")]
    public ActionResult<Employee> GetById(int id)
    {
        var employee = employees.Find(e => e.Id == id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    [HttpPost]
    public ActionResult<Employee> Create(Employee employee)
    {
        employee.Id = employees.Count + 1;
        employees.Add(employee);
        return CreatedAtAction(nameof(GetById), new { id = employee.Id }, employee);
    }

    [HttpPut("{id}")]
    public ActionResult<Employee> Update(int id, Employee updatedEmployee)
    {
        var employee = employees.Find(e => e.Id == id);
        if (employee == null)
        {
            return NotFound();
        }

        employee.FirstName = updatedEmployee.FirstName;
        employee.LastName = updatedEmployee.LastName;
        employee.Position = updatedEmployee.Position;

        return Ok(employee);
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var employee = employees.Find(e => e.Id == id);
        if (employee == null)
        {
            return NotFound();
        }

        employees.Remove(employee);
        return NoContent();
    }
}
