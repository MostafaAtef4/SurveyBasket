using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SurveyBasket.Api.Authentication;
using SurveyBasket.Api.Errors;
using SurveyBasket.Api.Persistence;
using System.Reflection;
using System.Text;

namespace SurveyBasket.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencies(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddControllers();

        
        services.AddCors(option =>
        option.AddDefaultPolicy(builder=>
        builder.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>()! )
        )
            
        );
        services.AddAuthConfig(configuration);

        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
            throw new InvalidOperationException("Conection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(connectionString)
        );
        services
            .AddSwaggerServices()
            .AddMapesterConf()
            .AddFluentValidation();

      

        services.AddScoped<IPollService, PollService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IQuestionService, QuestionService>();
        services.AddScoped<IVoteService, VoteService>();
        services.AddScoped<IResultService, ResultService>();

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
      
        return services;
    }

    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
       
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
       
        return services;
    }

    public static IServiceCollection AddMapesterConf(this IServiceCollection services)
    {

        //Add Mapester
        var mappingConfig = TypeAdapterConfig.GlobalSettings;
        mappingConfig.Scan(Assembly.GetExecutingAssembly());
        services
            .AddSingleton<IMapper>(new Mapper(mappingConfig));

        return services;
    }
    public static IServiceCollection AddFluentValidationConf(this IServiceCollection services)
    {
        services
                  .AddFluentValidationAutoValidation()
                  .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        return services;
    }
    private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
           .AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddSingleton<IJwtProvider, JwtProvider>();

        //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var jwtSetting = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
       

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(o =>
            {
                o.SaveToken=true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSetting?.Key!)),
                    ValidIssuer= jwtSetting?.Issuer,
                    ValidAudience=jwtSetting?.Audience

                };

            });

        return services;
    }
}
