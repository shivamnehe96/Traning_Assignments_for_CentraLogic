using EmployeeManagementSystem.Entity;
using EmployeeManagementSystem.Interfaces;
using EmployeeManagementSystem.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.Drawing;

namespace EmployeeManagementSystem.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ImportExportController : ControllerBase
    {
        private readonly IEmployeeBasicDetailsInterface employeeBasicDetailsInterface;
        private readonly IEmployeeAdditionalDetailsInterface employeeAdditionalDetailsInterface;

        public ImportExportController(IEmployeeBasicDetailsInterface employeeBasicDetails, IEmployeeAdditionalDetailsInterface employeeAdditionalDetails)
        {
            employeeBasicDetailsInterface = employeeBasicDetails;
            employeeAdditionalDetailsInterface = employeeAdditionalDetails;
        }

        private string GetStringFromCell(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            return cellValue?.ToString()?.Trim();
        }

        private DateTime GetDateTimeFromCell(ExcelWorksheet worksheet, int row, int column)
        {
            var cellValue = worksheet.Cells[row, column].Value;
            return DateTime.TryParse(cellValue?.ToString(), out var date) ? date : default(DateTime);
        }

        [HttpPost]
        public async Task<ActionResult> ImportExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File is empty or null");

            var employeeBasicDetails = new List<EmployeeBasicDetailsEntity>();
            var employeeAdditionalDetails = new List<EmployeeAdditionalDetailsEntity>();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets[0];
                    var rowCount = worksheet.Dimension.Rows;

                    for (int row = 2; row <= rowCount; row++)
                    {
                        var basicDetails = new EmployeeBasicDetailsEntity
                        {
                            FirstName = GetStringFromCell(worksheet, row, 2),
                            LastName = GetStringFromCell(worksheet, row, 3),
                            Email = GetStringFromCell(worksheet, row, 4),
                            Mobile = GetStringFromCell(worksheet, row, 5),
                            ReportingManagerName = GetStringFromCell(worksheet, row, 6)
                        };

                        var additionalDetails = new EmployeeAdditionalDetailsEntity
                        {
                            DateOfBirth = GetDateTimeFromCell(worksheet, row, 7),
                            DateOfJoining = GetDateTimeFromCell(worksheet, row, 8)
                        };

                        var addedBasicDetails = await employeeBasicDetailsInterface.AddEmployeeBasicDetails(basicDetails);
                        additionalDetails.Id = addedBasicDetails.Id;
                        await employeeAdditionalDetailsInterface.AddEmployeeAdditionalDetails(additionalDetails);

                        employeeBasicDetails.Add(basicDetails);
                        employeeAdditionalDetails.Add(additionalDetails);
                    }

                }
            }
            return Ok(new { employeeBasicDetails, employeeAdditionalDetails });

        }

        [HttpGet]
        public async Task<IActionResult> Export()
        {
            var basicDetails = await employeeBasicDetailsInterface.GetAllEmployees();
            var additionalDetails = await employeeAdditionalDetailsInterface.GetAllEmployeesWithAdditioanlDetails();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employees");

                // Add header
                worksheet.Cells[1, 1].Value = "SrNo";
                worksheet.Cells[1, 2].Value = "FirstName";
                worksheet.Cells[1, 3].Value = "LastName";
                worksheet.Cells[1, 4].Value = "Email";
                worksheet.Cells[1, 5].Value = "PhoneNumber";
                worksheet.Cells[1, 6].Value = "ReportingManagerName";
                worksheet.Cells[1, 7].Value = "DateOfBirth";
                worksheet.Cells[1, 8].Value = "DateOfJoining";

                // Set header style
                using (var range = worksheet.Cells[1, 1, 1, 8])
                {
                    range.Style.Font.Bold = true;
                    range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGreen);
                }

                // Add employee data
                int srNo = 1;
                foreach (var basic in basicDetails)
                {
                    var additional = additionalDetails.FirstOrDefault(a => a.Id == basic.Id);
                    if (additional != null)
                    {
                        int row = srNo + 1;
                        worksheet.Cells[row, 1].Value = srNo++;
                        worksheet.Cells[row, 2].Value = basic.FirstName;
                        worksheet.Cells[row, 3].Value = basic.LastName;
                        worksheet.Cells[row, 4].Value = basic.Email;
                        worksheet.Cells[row, 5].Value = basic.Mobile;
                        worksheet.Cells[row, 6].Value = basic.ReportingManagerName;
                        worksheet.Cells[row, 7].Value = additional.DateOfBirth.ToString("yyyy-MM-dd");
                        worksheet.Cells[row, 8].Value = additional.DateOfJoining.ToString("yyyy-MM-dd");
                    }
                }

                var stream = new MemoryStream();
                package.SaveAs(stream);
                stream.Position = 0;
                var fileName = "Employees.xlsx";
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}
