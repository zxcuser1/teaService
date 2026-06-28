using DatabaseToAccess;
using DatabaseToAccess.Repository;
using Microsoft.EntityFrameworkCore;
using Service.Application.Interfaces;
using Service.Application.TeaMatchService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BaseDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ITeaRepository, TeaRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<TeaMatchService>();
builder.Services.AddCors(options =>
    options.AddPolicy(
        "AllowAll",
        policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        }
    )
);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});


app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAll");

app.MapControllers();
app.UseStaticFiles();

app.Run();
