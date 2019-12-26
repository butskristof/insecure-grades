using System.Linq;
using System.Text;
using GradesApi.Models;
using GradesApi.Services;
using GradesDomain;
using GradesRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

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
			
			services.AddOptions();

			// Add our Config object so it can be injected
			services.Configure<Secrets>(Configuration.GetSection("Secrets"));
			
			// set up jwt authentication
			var sec = Configuration.GetSection("Secrets").Get<Secrets>();
			var key = Encoding.ASCII.GetBytes(sec.Jwt);
			services
				.AddAuthentication(x =>
				{
					x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer(x =>
				{
					x.RequireHttpsMetadata = false;
					x.SaveToken = true;
					x.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuerSigningKey = true,
						IssuerSigningKey = new SymmetricSecurityKey(key),
						ValidateIssuer = false,
						ValidateAudience = false,
						ValidateLifetime = true,
					};
				});

			services.AddSingleton<IPersonRepository, PersonRepository>();
			services.AddSingleton<ITestRepository, TestRepository>();

			services.AddScoped<IUserService, UserService>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			// app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();
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