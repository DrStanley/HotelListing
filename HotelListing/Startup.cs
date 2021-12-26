using HotelListing.Configurations;
using HotelListing.Data;
using HotelListing.Services.IRepository;
using HotelListing.Services.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace HotelListing
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

			services.InjectClasses();
			services.AddDbContext<DatabaseContext>(opt =>
				opt.UseSqlServer(Configuration.GetConnectionString("SqlConnection")
				));

			services.AddAuthentication();
			services.ConfigureIdentity();
			services.ConfigJWT(Configuration);

			services.AddCors(o =>
			{
				o.AddPolicy("AllowAll", builder =>
					 builder.AllowAnyOrigin()
						 .AllowAnyMethod()
						 .AllowAnyHeader());
			});

			services.AddAutoMapper(typeof(MapperInitialzer));

			services.AddSwaggerGen(options =>
			{
				options.SwaggerDoc("v1", new OpenApiInfo { Title = "HotelListing", Version = "v1" });
			});
			services.AddControllers().AddNewtonsoftJson(
				opt => opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();

			}
			app.UseSwagger();
			app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "HotelListing v1"));

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
