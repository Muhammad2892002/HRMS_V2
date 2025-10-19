using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test1.Models;

namespace Test1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private static List<Department> _departments=new List<Department>()
        {
            new Department(){Id=1, Name="IT",FloorNumber=-1,Description="Information Technology " +
                "it stand for web app and mobiles development "},
            new Department(){Id=2, Name="HR",FloorNumber=0,Description="Human Recourses "},
            new Department(){Id=3, Name="QA",FloorNumber=1,Description="Quality Assurance to " +
                "check the quality of program"},
        };

        [HttpGet("GetAllDepartments")]
        public IActionResult GetAllDepartments([FromBody]string? name) {
            var allDepartments = (from dept in _departments
                                  where  name==null|| dept.Name == name 
                                  select new DepartmentDTO
                                  {
                                       
                                        Name = dept.Name,
                                        Description = dept.Description,
                                        FloorNumber = dept.FloorNumber
                                  }).ToList();
            return Ok(allDepartments);


        }
        [HttpGet("GetById")]
        public IActionResult GetById(long id) {
            var department = _departments.FirstOrDefault(d => d.Id == id);
            if (department == null)
            {
                return NotFound("Department not found");
            }
            var departmentDTO = new DepartmentDTO
            {
                Name = department.Name,
                Description = department.Description,
                FloorNumber = department.FloorNumber
            };
            return Ok(departmentDTO);



        }
        [HttpPost("AddDepartment")]
        public IActionResult AddDepartment([FromQuery]DepartmentDTO departmentDTO) { 
            Department department=new Department()
            {
                Id = (_departments.LastOrDefault()?.Id??0)+1,
                Name = departmentDTO.Name,
                Description = departmentDTO.Description,
                FloorNumber = departmentDTO.FloorNumber
            };
            _departments.Add(department);
            return Ok("Department added successfully");



        }
        [HttpPut("EditDerpatment")]
        public IActionResult EditDepartment(DepartmentDTO departmentDTO) {
            var deprtment=_departments.FirstOrDefault(x=>x.Id==departmentDTO.Id);
            if (deprtment == null)
            {
                return NotFound("Department not found");
            }
            else {
              
              deprtment.Name = departmentDTO.Name;
                deprtment.Description = departmentDTO.Description;
                deprtment.FloorNumber = departmentDTO.FloorNumber;
                return Ok("Department updated successfully");


            }




        }

        [HttpDelete("DeleteDepartment")]
        public IActionResult DeleteDepartment(int id) { 
        var department=_departments.FirstOrDefault(x=>x.Id==id);
            if (department == null)
            {
                return NotFound("Department not found");
            }
            _departments.Remove(department);
            return Ok("Department deleted successfully");

        }






    }
}
