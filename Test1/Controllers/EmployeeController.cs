using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test1.Models;

namespace Test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public static List<Employee> _employees = new List<Employee>() {
            new Employee(){Id=1, Name="Alice", BirthDay=new DateOnly(2002,09,28)},
            new Employee(){Id=2, Name="Bob", BirthDay=new DateOnly(1985,05,12)},
            new Employee(){Id=3, Name="Charlie", BirthDay=new DateOnly(2002,09,28)}
        };
        [HttpGet("GetAll")]
        public IActionResult GetAllEmployee(long? id) {
            var allEmployess = _employees.Where(x=>x.Id==id || id==null).Select(x=> new EmployeeDTO { 
                Id = x.Id,
                Name = x.Name,
                BirthDay = x.BirthDay


            });
            return Ok(allEmployess);


        }

        [HttpGet("GetById")]
        public IActionResult GetById(long ?id) {
            var employee = _employees.FirstOrDefault(e=>e.Id==id);
            if (employee == null) { 
            
            return NotFound("Employee not found");
            }
            else
            {
                var employeeDTO = new EmployeeDTO
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    BirthDay = employee.BirthDay
                };
                return Ok(employeeDTO);
            }




        }
        [HttpPost("Add")]
        public IActionResult Add(EmployeeDTO employeeDTO) {
            Employee employee = new Employee();
            employee.Id = (_employees.LastOrDefault()?.Id??0)+1;
            employee.Name = employeeDTO.Name;
            employee.BirthDay = employeeDTO.BirthDay;
            _employees.Add(employee);
            if (_employees.Contains(employee))
            {

                return Ok("Adding   success");
            }
            else {
                return NotFound("Failed To add");
            
            
            }
        
        
        }
        [HttpPut("Update")]

        public IActionResult Update(EmployeeDTO employeeDTO) {
            var findEmployee= _employees.FirstOrDefault(e => e.Id == employeeDTO.Id);
            if (findEmployee == null)
            {
                return NotFound("Employee not found");
            }
            else
            {
                findEmployee.Name = employeeDTO.Name;
                findEmployee.BirthDay = employeeDTO.BirthDay;
                return Ok("Employee updated successfully");
            }




        }
        [HttpDelete("Delete")]
        public IActionResult Delete(long id)
        {
            var FindEmployee = _employees.FirstOrDefault(e => e.Id == id);
            if (FindEmployee == null)
            {
                return NotFound("Employee not found");
            }
            else
            {
                _employees.Remove(FindEmployee);
                if (!(_employees.Contains(FindEmployee)))
                {
                    return Ok("Employee deleted successfully");
                }
                else
                {
                    return NotFound("Failed to delete employee");
                }


            }
        }

    }
}
