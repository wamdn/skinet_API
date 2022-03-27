using API.Extensions;
using API.Helpers;
using API.Middlewares;
using Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Add repositories & Custom invalid ModelState error response
builder.Services.AddApplicationServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
if (builder.Environment.IsDevelopment()) builder.Services.AddSwaggerDocumentation();
// Add DbContext
builder.Services.AddDbContext<StoreContext>();
// Add Automapper
builder.Services.AddAutoMapper(typeof(MappingProfiles));


var app = builder.Build();

// Configure the HTTP request pipeline.

// Custom exception handler
app.UseMiddleware<ExceptionMiddleware>();
// Custom error handler in case no route is match
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("developmentPolicy");

app.UseAuthorization();

if (app.Environment.IsDevelopment()) app.UseSwaggerDocumentation();

app.MapControllers();

// Database creating and seeding at startup
await app.UseDatabaseSeeding();


app.Run();