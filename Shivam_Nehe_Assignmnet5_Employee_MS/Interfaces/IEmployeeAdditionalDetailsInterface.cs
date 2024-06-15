using EmployeeManagementSystem.Entity;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IEmployeeAdditionalDetailsInterface
    {
        Task<EmployeeAdditionalDetailsEntity> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity);

        Task<EmployeeAdditionalDetailsEntity> GetAdditionalDetailsOfEmployeeById(string id);

        Task<IEnumerable<EmployeeAdditionalDetailsEntity>> GetAllEmployeesWithAdditioanlDetails();

        Task<EmployeeAdditionalDetailsEntity> UpdateEmployeeForAdditionalDetails(string id, EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity);


        Task<bool> DeleteEmployeeForAdditionalDetails(string id);

    }
}
