using EmployeeManagementSystem.Entity;

namespace EmployeeManagementSystem.Interfaces
{
    public interface IEmployeeAdditionalDetailsInterface
    {
        Task<EmployeeAdditionalDetailsEntity> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity);

        Task<EmployeeAdditionalDetailsEntity> GetAdditionalDetailsOfEmployeeById(string id);

        Task<List<EmployeeAdditionalDetailsEntity>> GetAllEmployeesWithAdditioanlDetails();

        Task<EmployeeAdditionalDetailsEntity> UpdateEmployeeForAdditionalDetails(string id, EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity);


        Task<bool> DeleteEmployeeForAdditionalDetails(string id);

        Task<List<EmployeeAdditionalDetailsEntity>> GetEmployeeByDesignation(string role);

        Task<EmployeeAdditionalDetailsEntity> GetAdditionalDetailsByBasicDetailsUId(string employeeBasicDetailsUid);



    }
}
