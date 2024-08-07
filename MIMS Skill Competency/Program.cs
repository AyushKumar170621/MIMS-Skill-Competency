
using MIMS_Skill_Competency.Data;
using MIMS_Skill_Competency.Interfaces;
using MIMS_Skill_Competency.Repository;

namespace MIMS_Skill_Competency
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddSingleton<ApplicationDbContext>();
            builder.Services.AddScoped<ISkillRepository,SkillRepository>();
            builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
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
        }
    }
}
