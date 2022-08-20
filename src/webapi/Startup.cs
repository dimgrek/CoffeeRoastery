using System;
using System.Text.Json.Serialization;
using CoffeeRoastery.DAL.PostgreSQL.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using webapi.Configs;

namespace webapi;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCorsConfiguration(Configuration);
        services.AddSwaggerConfiguration();
        services.AddAutoMapper(typeof(Startup));
        services.AddMvc();
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            options.SerializerSettings.Converters.Add(new StringEnumConverter());
        }).AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        //add db context
        Action<DbContextOptionsBuilder> dbContextOptions = options =>
            options.UseNpgsql(Configuration.GetConnectionString("PostgreSQL.CoffeeRoastery"));
        services.AddDbContext<CoffeeRoasteryContext>(dbContextOptions);
        
        //configure JWT
        //todo: add JWT config

        // services.AddAuthentication(options =>
        // {
        //     options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //     options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        // }).AddJwtBearer(options =>
        // {
        //     options.MetadataAddress = authOptions.MetadataAddress;
        //     options.SaveToken = authOptions.SaveToken;
        //
        //     options.IncludeErrorDetails = authOptions.IncludeErrorDetails;
        //     options.TokenValidationParameters = new TokenValidationParameters
        //     {
        //         ValidateAudience = authOptions.TokenValidationParameters.ValidateAudience
        //     };
        // });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration config)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UsePathBase(Configuration["BasePath"]);

        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        if (bool.Parse(config["Swagger.IsEnabled"]))
        {
            SwaggerConfig(app);
        }
    }

    private void SwaggerConfig(IApplicationBuilder app)
    {
        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint($"{Configuration.GetValue<string>("BasePath").TrimEnd('/')}/swagger/v1/swagger.json",
                "CoffeeRoastery api/v1");
        });
    }
}