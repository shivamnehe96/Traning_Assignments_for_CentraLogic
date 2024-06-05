using SecurityClearanceSystem.Interface;
using SecurityClearanceSystem.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IVisitorInterface, VisitorService>();
builder.Services.AddSingleton<ISecurityInterface, SecurityService>();
builder.Services.AddSingleton<IManagerInterface, ManagerService>();
builder.Services.AddSingleton<IOfficeInterface, OfficeService>();
builder.Services.AddSingleton<IUserInterface, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
