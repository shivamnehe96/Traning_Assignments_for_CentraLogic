using EmployeeManagementSystem.Comman;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entity;

namespace EmployeeManagementSystem.CosmosDB
{
    public interface ICosmosDBInterface
    {
        Task<EmployeeBasicDetailsEntity> AddEmployeeBasicDetails(EmployeeBasicDetailsEntity employeeBasicDetailsEntity);

        Task<EmployeeBasicDetailsEntity> GetEmployeeById(string id);

        Task<List<EmployeeBasicDetailsEntity>> GetAllEmployees();

        Task<EmployeeBasicDetailsEntity> UpdateEmployeeBasicDetails(string id, EmployeeBasicDetailsEntity employeeBasicDetailsEntity);

        Task<bool> DeleteEmployeeBasicDetails(string id);

        Task<EmployeeAdditionalDetailsEntity> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity);

        Task<EmployeeAdditionalDetailsEntity> GetAdditionalDetailsOfEmployeeById(string id);

        Task<List<EmployeeAdditionalDetailsEntity>> GetAllEmployeesWithAdditioanlDetails();


        Task<EmployeeAdditionalDetailsEntity> UpdateEmployeeForAdditionalDetails(string id, EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity);

        Task<bool> DeleteEmployeeForAdditionalDetails(string id);

        Task<List<EmployeeBasicDetailsEntity> >GetAllEmployeeByRole(string role);

        Task<List<EmployeeAdditionalDetailsEntity>> GetEmployeeByDesignation(string designationName);

        Task<EmployeeFilterCriteria> GetAllEmployeeByPagination(EmployeeFilterCriteria employeeFilterCriteria);

        Task<EmployeeAdditionalDetailsEntity> GetAdditionalDetailsByBasicDetailsUId(string employeeBasicDetailsUid);

        Task<EmployeeBasicDetailsDTO> AddEmployeeByMakePostRequest(EmployeeBasicDetailsDTO employeeBasicDetailsDTO);

        Task<EmployeeBasicDetailsEntity>GetAllEmployeeByMakeGetRequest(string employeeId);



    }

}

