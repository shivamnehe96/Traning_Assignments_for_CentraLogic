using AutoMapper;
using EmployeeManagementSystem.Comman;
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
        private readonly IMapper _mapper;

        public EmployeeBasicDetailsController(IEmployeeBasicDetailsInterface employeeBasicDetails , IMapper mapper)
        {
            
            employeeBasicDetailsInterface = employeeBasicDetails;
            _mapper = mapper;

        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeBasicDetails(EmployeeBasicDetailsDTO employeeDTO)
        {
            var employeeEntity = _mapper.Map<EmployeeBasicDetailsEntity>(employeeDTO);

            var response = await employeeBasicDetailsInterface.AddEmployeeBasicDetails(employeeEntity);

            var resultDTO = _mapper.Map<EmployeeBasicDetailsDTO>(response);
            return Ok(resultDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(string id)
        {
            var employee = await employeeBasicDetailsInterface.GetEmployeeById(id);

            var resultDTO = _mapper.Map<EmployeeBasicDetailsDTO>(employee);
            return Ok(resultDTO);
        }




        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await employeeBasicDetailsInterface.GetAllEmployees();
            var resultDTOs = _mapper.Map<IEnumerable<EmployeeBasicDetailsDTO>>(employees);
            return Ok(resultDTOs);
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

        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeByRole(string role)
        {
            var response = await employeeBasicDetailsInterface.GetAllEmployeeByRole(role);
            return Ok(response);
        }

        [HttpPost]
        public async Task<EmployeeFilterCriteria> GetAllEmployeeByPagination(EmployeeFilterCriteria employeeFilterCriteria)
        {
            var response = await employeeBasicDetailsInterface.GetAllEmployeeByPagination(employeeFilterCriteria);
            return response;
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeByMakePostRequest( EmployeeBasicDetailsDTO employeeBasicDetailsDTO)
        {
            var response = await employeeBasicDetailsInterface.AddEmployeeByMakePostRequest(employeeBasicDetailsDTO);
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeByMakeGetRequest(string employeeId)
        {
            var response = await employeeBasicDetailsInterface.GetAllEmployeeByMakeGetRequest(employeeId);
            return Ok(response);
        }
    }
}
