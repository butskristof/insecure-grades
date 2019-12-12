using System.Linq;
using GradesDomain;
using GradesRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GradesApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			services.AddSingleton<IPersonRepository, PersonRepository>();
			services.AddSingleton<ITestRepository, TestRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
			
			Seed(app);
		}

		private void Seed(IApplicationBuilder app)
		{
			var personRepo = app.ApplicationServices.GetRequiredService<IPersonRepository>();
			var testRepo = app.ApplicationServices.GetRequiredService<ITestRepository>();
			var people = personRepo.ReadPersons();
			var student = people.FirstOrDefault(p => p is Student) as Student;
			var t = new Test()
			{
				Student = student,
				MaxScore = 10,
				Score = 5
			};
			
			testRepo.CreateTest(t);
		}
	}
}