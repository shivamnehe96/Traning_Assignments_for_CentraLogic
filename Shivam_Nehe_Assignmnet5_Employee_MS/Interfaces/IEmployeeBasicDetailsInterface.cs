using EmployeeManagementSystem.Comman;
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


    }
}
