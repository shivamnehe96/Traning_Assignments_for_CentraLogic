using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Interfaces;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class EmployeeBasicDetailsController : ControllerBase
    {

        public IEmployeeBasicDetailsInterface employeeBasicDetailsInterface;

        public EmployeeBasicDetailsController(IEmployeeBasicDetailsInterface employeeBasicDetails)
        {
            
            employeeBasicDetailsInterface = employeeBasicDetails;

        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeBasicDetails(EmployeeBasicDetailsDTO employeeDTO)
        {
            EmployeeBasicDetailsEntity employeeEntity = new EmployeeBasicDetailsEntity
            {
                
                Salutory = employeeDTO.Salutory,
                FirstName = employeeDTO.FirstName,
                MiddleName = employeeDTO.MiddleName,
                LastName = employeeDTO.LastName,
                NickName = employeeDTO.NickName,
                Email = employeeDTO.Email,
                Mobile = employeeDTO.Mobile,
                EmployeeID = employeeDTO.EmployeeID,
                Role = employeeDTO.Role,
                ReportingManagerUId = employeeDTO.ReportingManagerUId,
                ReportingManagerName = employeeDTO.ReportingManagerName,
                Address = employeeDTO.Address
            };

            var response = await employeeBasicDetailsInterface.AddEmployeeBasicDetails(employeeEntity);

            employeeDTO.Id = response.Id;

            return Ok(employeeDTO);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var employee = await employeeBasicDetailsInterface.GetEmployeeById(id);
            
            return Ok(employee);
        }




        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await employeeBasicDetailsInterface.GetAllEmployees();
            return Ok(employees);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(string id, [FromBody] EmployeeBasicDetailsEntity employee)
        {
            var updatedEmployee = await employeeBasicDetailsInterface.UpdateEmployeeBasicDetails(id, employee);
            return Ok(updatedEmployee);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            var isDeleted = await employeeBasicDetailsInterface.DeleteEmployeeBasicDetails(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
