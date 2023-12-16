using Microsoft.EntityFrameworkCore;
using CSProject.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<CSProjectContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("CSProjectContext") ??
        throw new InvalidOperationException("Connection string 'CSProjectContext' not found."));
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
