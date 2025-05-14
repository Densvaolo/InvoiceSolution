using InvoiceService.Data;
using Microsoft.EntityFrameworkCore;
using InvoiceService.Repositories;
using InvoiceService.Services;




var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowFrontend",
         policy =>
         {
             policy.WithOrigins("https://localhost:7080")
                 .AllowAnyMethod()
                 .AllowAnyHeader();
         });
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<InvoiceService.Services.IInvoiceService, InvoiceService.Services.InvoiceService>();



builder.Services.AddDbContext<InvoiceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InvoiceConnection")));


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
