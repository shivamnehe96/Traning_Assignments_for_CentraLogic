using EmployeeManagementSystem.Comman;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entity;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IEmployeeBasicDetailsInterface
    {
        Task<EmployeeBasicDetailsEntity> AddEmployeeBasicDetails(EmployeeBasicDetailsEntity employeeBasicDetailsEntity);

        Task<EmployeeBasicDetailsEntity>GetEmployeeById(string id);

        Task<List<EmployeeBasicDetailsEntity>> GetAllEmployees();

        Task<EmployeeBasicDetailsEntity> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsEntity employeeBasicDetailsEntity);

        Task<bool> DeleteEmployeeBasicDetails(string id);

        Task<List<EmployeeBasicDetailsEntity> >GetAllEmployeeByRole(string role);

        Task<EmployeeFilterCriteria> GetAllEmployeeByPagination(EmployeeFilterCriteria employeeFilterCriteria);

        Task<EmployeeBasicDetailsDTO> AddEmployeeByMakePostRequest(EmployeeBasicDetailsDTO employeeBasicDetailsDTO);

        Task<EmployeeBasicDetailsEntity>GetAllEmployeeByMakeGetRequest(string employeeId);


    }
}
