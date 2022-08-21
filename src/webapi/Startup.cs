using System;
using System.Text;
using System.Text.Json.Serialization;
using CoffeeRoastery.BLL.Interface.Common;
using CoffeeRoastery.BLL.Interface.Services;
using CoffeeRoastery.BLL.Services;
using CoffeeRoastery.DAL.Interface.Repositories;
using CoffeeRoastery.DAL.PostgreSQL.Context;
using CoffeeRoastery.DAL.PostgreSQL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using webapi.Configs;
using webapi.Extensions;
using webapi.Options;

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

        //add options
        services.Configure<JWTTokenOptions>(Configuration.GetSection(JWTTokenOptions.ConfigurationKey));
        services.Configure<AuthOptions>(Configuration.GetSection(AuthOptions.ConfigurationKey));

        //add repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        
        //add services
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();

        //configure JWT
        var jwtBearerOptions = Configuration.GetSection(JWTTokenOptions.ConfigurationKey).Get<JWTTokenOptions>();

        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtBearerOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtBearerOptions.Audience,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtBearerOptions.Secret)),
                    ValidateIssuerSigningKey = true,
                };
            });
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