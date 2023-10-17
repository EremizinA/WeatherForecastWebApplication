using WeatherForecast.Api.Logging;
using WeatherForecast.Api.Repositories;
using WeatherForecast.Api.Services;
using WeatherForecast.Api.Data;
using System.Reflection;
using WeatherForecast.Api.Options;

namespace WeatherForecast.Api
{
    public class Startup
    {
        public IConfigurationRoot? Configuration { get; set; }

        public Startup(IHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddUserSecrets(Assembly.GetExecutingAssembly());

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.Configure<DbConnectionOptions>(Configuration?.GetSection(nameof(DbConnectionOptions)));

            services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();
            services.AddSingleton<ISqlServerDbConnectionFactory, SqlServerDbConnectionFactory>();

            services.AddSingleton<ICountryRepository, CountryRepository>();
            services.AddSingleton<ICountryService, CountryService>();
            services.AddSingleton<ICityRepository, CityRepository>();
            services.AddSingleton<ICityService, CityService>();
            services.AddSingleton<IWeatherTodayRepository, WeatherTodayRepository>();
            services.AddSingleton<IWeatherTodayService, WeatherTodayService>();
            services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
        }

        public void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseHttpLogging();
        }
    }
}
