using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.UseCors(x => x
           .AllowAnyMethod()
           .AllowAnyHeader()
           //.AllowCredentials()
           .AllowAnyOrigin()
           //.WithOrigins("https://localhost:7106")); // Allow only this origin can also have multiple origins seperated with comma
           .SetIsOriginAllowed(origin => true));// Allow any origin  

app.Run();
