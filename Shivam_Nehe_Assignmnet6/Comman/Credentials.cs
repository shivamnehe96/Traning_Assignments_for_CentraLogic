﻿namespace EmployeeManagementSystem.Comman
{
    public class Credentials
    {
        public static readonly string DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
        public static readonly string ContainerName = Environment.GetEnvironmentVariable("ContainerName");
        public static readonly string CosmosEndPointURI = Environment.GetEnvironmentVariable("URI");
        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");

        internal static readonly string BasicDetailsUrl = Environment.GetEnvironmentVariable("BasicDetailsUrl");
        internal static readonly string AddBasicDetailsEndPoint = "/api/EmployeeBasicDetailsController/AddEmployeeBasicDetails";
        internal static readonly string GetBasicDetailsEndPoint = "/api/EmployeeBasicDetailsController/GetAllEmployees";

    }
}
