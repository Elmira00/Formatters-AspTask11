using Formatters_AspTask11.DataAccess;
using Formatters_AspTask11.Formatters;
using Formatters_AspTask11.Repositories.Abstract;
using Formatters_AspTask11.Repositories.Concrete;
using Formatters_AspTask11.Services.Abstract;
using Formatters_AspTask11.Services.Concrete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.OutputFormatters.Insert(0, new TextCsvOutputFormatter());
    options.InputFormatters.Insert(1, new TextCsvInputFormatter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IStudentService, StudentService>();


var conn = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<StudentDbContext>(opt =>
{
    opt.UseSqlServer(conn);
});
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
