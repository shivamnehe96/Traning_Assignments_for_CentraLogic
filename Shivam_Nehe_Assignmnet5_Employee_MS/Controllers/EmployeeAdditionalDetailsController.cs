using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class EmployeeAdditionalDetailsController : ControllerBase
    {
        public IEmployeeAdditionalDetailsInterface employeeAdditionalDetailsInterface;

        public EmployeeAdditionalDetailsController(IEmployeeAdditionalDetailsInterface employeeAdditionalDetails)
        {

            employeeAdditionalDetailsInterface = employeeAdditionalDetails;

        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeAdditionalDetails(EmployeeAdditionDetailsDTO employeeDTO)
        {
            EmployeeAdditionalDetailsEntity employeeEntity = new EmployeeAdditionalDetailsEntity
            {

                EmployeeBasicDetailsUId = employeeDTO.EmployeeBasicDetailsUId,
                AlternateEmail = employeeDTO.AlternateEmail,
                AlternateMobile = employeeDTO.AlternateMobile,
                WorkInformation = employeeDTO.WorkInformation,
                PersonalDetails = employeeDTO.PersonalDetails,
                IdentityInformation = employeeDTO.IdentityInformation,
                
            };

            var response = await employeeAdditionalDetailsInterface.AddEmployeeAdditionalDetails(employeeEntity);

            employeeDTO.EmployeeBasicDetailsUId = response.Id;

            return Ok(employeeDTO);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdditionalDetailsOfEmployeeById(string id)
        {
            var employee = await employeeAdditionalDetailsInterface.GetAdditionalDetailsOfEmployeeById(id);

            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeesWithAdditioanlDetails()
        {
            var employees = await employeeAdditionalDetailsInterface.GetAllEmployeesWithAdditioanlDetails();
            return Ok(employees);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployeeForAdditionalDetails(string id, [FromBody] EmployeeAdditionalDetailsEntity employee)
        {
            var updatedEmployee = await employeeAdditionalDetailsInterface.UpdateEmployeeForAdditionalDetails(id, employee);
            return Ok(updatedEmployee);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployeeForAdditionalDetails(string id)
        {
            var isDeleted = await employeeAdditionalDetailsInterface.DeleteEmployeeForAdditionalDetails(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
