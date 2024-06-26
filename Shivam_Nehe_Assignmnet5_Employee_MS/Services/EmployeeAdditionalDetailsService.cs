﻿using AutoMapper;
using EmployeeManagementSystem.CosmosDB;
using EmployeeManagementSystem.DTO;
using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Interfaces;

namespace EmployeeManagementSystem.Services
{
    public class EmployeeAdditionalDetailsService:IEmployeeAdditionalDetailsInterface
    {

        public ICosmosDBInterface _cosmosService;
        public readonly IMapper _mapper;
        public EmployeeAdditionalDetailsService(ICosmosDBInterface cosmosService,IMapper mapper)
        {
            _cosmosService = cosmosService;
            _mapper = mapper;
        }

        public async Task<EmployeeAdditionalDetailsEntity> AddEmployeeAdditionalDetails(EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity)
        {
            employeeAdditionalDetailsEntity.Id = Guid.NewGuid().ToString();

            employeeAdditionalDetailsEntity.DocumnetType = "EmployeeAdditionalDetails";
            employeeAdditionalDetailsEntity.CreatedBy = "shivam";
            employeeAdditionalDetailsEntity.CreatedOn = DateTime.Now;
            employeeAdditionalDetailsEntity.UpdatedBy = "shivam";
            employeeAdditionalDetailsEntity.UpdatedOn = DateTime.Now;
            employeeAdditionalDetailsEntity.Version = 1;
            employeeAdditionalDetailsEntity.Active = true;
            employeeAdditionalDetailsEntity.Archived = false;

            EmployeeAdditionalDetailsEntity respons = await _cosmosService.AddEmployeeAdditionalDetails(employeeAdditionalDetailsEntity);
            return respons;
        }

        public async Task<EmployeeAdditionalDetailsEntity> GetAdditionalDetailsOfEmployeeById(string id)
        {
            return await _cosmosService.GetAdditionalDetailsOfEmployeeById(id);
        }

        public async Task<List<EmployeeAdditionalDetailsEntity>> GetAllEmployeesWithAdditioanlDetails()
        {
            return await _cosmosService.GetAllEmployeesWithAdditioanlDetails();
        }

        public async Task<EmployeeAdditionalDetailsEntity> UpdateEmployeeForAdditionalDetails(string id, EmployeeAdditionalDetailsEntity employeeAdditionalDetailsEntity)
        {
            return await _cosmosService.UpdateEmployeeForAdditionalDetails(id, employeeAdditionalDetailsEntity);
        }

        public async Task<bool> DeleteEmployeeForAdditionalDetails(string id)
        {
            return await _cosmosService.DeleteEmployeeForAdditionalDetails(id);
        }
        public async Task<List<EmployeeAdditionalDetailsEntity>> GetEmployeeByDesignation(string designationName)
        {
            return await _cosmosService.GetEmployeeByDesignation(designationName);
        }

        
    }
}
