using BundlerMinifier.TagHelpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using WLVSTools.Web.Core.Models;
using WLVSTools.Web.Infrastructure.Authentication;
using WLVSTools.Web.Infrastructure.Authentication.OpenId;
using WLVSTools.Web.Infrastructure.ExceptionsLogging;
using WLVSTools.Web.Infrastructure.Lotto;
using WLVSTools.Web.Infrastructure.PersonalTools;

var builder = WebApplication.CreateBuilder(args);

ConfigureServices(builder);

await using var app = builder.Build();

await ConfigureApp(app);

static async Task ConfigureApp(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseStaticFiles();

    app.UseHttpsRedirection();

    // add these before controllers and any
    // handlers that need to be authenticated
    app.UseAuthentication();
    app.UseAuthorization();

    app.MapDefaultControllerRoute();

    await app.RunAsync();
}

static void ConfigureServices(WebApplicationBuilder builder)
{
    ConfigureDbContext(builder);
    ConfigureIdentity(builder);
    ConfigureOpenId(builder);

    // OTHER CONFUGRATIONS
    builder.Services.AddAutoMapper(typeof(Program));

    builder.Services.AddControllersWithViews();
    builder.Services.AddControllers();

    builder.Services.AddApiVersioning(o =>
    {
        o.AssumeDefaultVersionWhenUnspecified = true;
        o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
        o.ReportApiVersions = true;
        o.ApiVersionReader = ApiVersionReader.Combine(
            new QueryStringApiVersionReader("api-version"),
            new HeaderApiVersionReader("X-Version"),
            new MediaTypeApiVersionReader("ver"));
    });

    //Set Session Timeout. Default is 20 minutes.
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(60);
    });

    builder.Services.AddBundles(options =>
    {
        options.AppendVersion = true;
    });
}

static void ConfigureDbContext(WebApplicationBuilder builder)
{
    //builder.Services.AddDbContext<DbContext>(options =>
    //{
    //    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
    //    options.UseOpenIddict();
    //});

    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        options.UseCustomOAuth();
    });

    builder.Services.AddDbContext<PersonalToolsDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("PersonalToolBoxConnection")));

    builder.Services.AddDbContext<ExceptionLoggingDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("ExceptionLoggingConnection")));

    builder.Services.AddDbContext<LottoDbContext>(options =>
        options.UseSqlite(builder.Configuration.GetConnectionString("LottoConnection")));
}

static void ConfigureIdentity(WebApplicationBuilder builder)
{
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        // Password settings
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;
        options.Password.RequiredUniqueChars = 4;
        // Other settings can be configured here
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
}

static void ConfgureAuthentication(WebApplicationBuilder builder)
{
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = AppConstant.DefaultScheme;
        options.DefaultChallengeScheme = AppConstant.DefaultScheme;
    })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
        {
            options.LoginPath = "/Account/Login";
            options.ExpireTimeSpan = TimeSpan.FromDays(1);
        })
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true
            };
        })
        // this is the key piece!
        .AddPolicyScheme(AppConstant.DefaultScheme, AppConstant.DefaultScheme, options =>
        {
            // runs on each request
            options.ForwardDefaultSelector = context =>
            {
                // filter by auth type
                string authorization = context.Request.Headers[HeaderNames.Authorization];
                if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith($"{JwtBearerDefaults.AuthenticationScheme} "))
                    return JwtBearerDefaults.AuthenticationScheme;

                // otherwise always check for cookie auth
                return CookieAuthenticationDefaults.AuthenticationScheme;
            };
        });
}

static void ConfigureJWT(WebApplicationBuilder builder)
{
    var jwtConfig = builder.Configuration.GetSection("Jwt");
    var secretKey = jwtConfig["Key"];

    builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultScheme = AppConstant.DefaultScheme;
        opt.DefaultChallengeScheme = AppConstant.DefaultScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
        };
    })
    .AddPolicyScheme(AppConstant.DefaultScheme, AppConstant.DefaultScheme, options =>
    {
        // runs on each request
        options.ForwardDefaultSelector = context =>
        {
            // filter by auth type
            string authorization = context.Request.Headers[HeaderNames.Authorization];
            if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith($"{JwtBearerDefaults.AuthenticationScheme} "))
                return JwtBearerDefaults.AuthenticationScheme;

            // otherwise always check for cookie auth
            return "Identity.Application";
        };
    });
}

static void ConfigureOpenId(WebApplicationBuilder builder)
{
    builder.Services
        .AddOpenIddict()
        .AddCore(configuration =>
        {
            configuration.UseEntityFrameworkCore()
                .ReplaceWithCustomOAuthEntities()
                .UseDbContext<ApplicationDbContext>();
        })
        .AddServer(openIddictServerBuilder =>
        {
            //enable client_credentials grant_tupe support on server level
            openIddictServerBuilder.AllowClientCredentialsFlow();
            //specify token endpoint uri
            openIddictServerBuilder.SetTokenEndpointUris("token");
            //secret registration
            openIddictServerBuilder.AddDevelopmentEncryptionCertificate()
                .AddDevelopmentSigningCertificate();
            openIddictServerBuilder.DisableAccessTokenEncryption();
            //the asp request handlers configuration itself
            openIddictServerBuilder.UseAspNetCore().EnableTokenEndpointPassthrough();
        });

    builder.Services.AddHostedService<ClientSeeder>();

    builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultScheme = AppConstant.DefaultScheme;
        opt.DefaultChallengeScheme = AppConstant.DefaultScheme;
    })
    .AddGoogle(options => {
        if (builder != null)
        {
            var clientId = builder.Configuration["Authentication:Google:ClientId"];
            if (!string.IsNullOrWhiteSpace(clientId))
                options.ClientId = clientId;

            var clientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
            if (!string.IsNullOrWhiteSpace(clientSecret))
                options.ClientSecret = clientSecret;
        }
    })
    .AddFacebook(options =>
    {
        var appId = builder.Configuration["Authentication:Facebook:AppId"];
        if (!string.IsNullOrWhiteSpace(appId))
            options.AppId = appId;

        var appSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
        if (!string.IsNullOrWhiteSpace(appSecret))
            options.AppSecret = appSecret;

        options.AccessDeniedPath = "/";
    })
    .AddPolicyScheme(AppConstant.DefaultScheme, AppConstant.DefaultScheme, options =>
    {
        options.ForwardDefaultSelector = context =>
        {
            if (context != null && context.Request != null)
            {
                var headerAuthrorization = context.Request.Headers[HeaderNames.Authorization];

                if (!string.IsNullOrWhiteSpace(headerAuthrorization))
                {
                    string authorization = headerAuthrorization.ToString();

                    if (!string.IsNullOrEmpty(authorization) && authorization.StartsWith("Bearer "))
                    {
                        var token = authorization.Substring("Bearer ".Length).Trim();
                        var jwtHandler = new JwtSecurityTokenHandler();

                        // it's a self contained access token and not encrypted
                        if (jwtHandler.CanReadToken(token))
                        {
                            var issuer = jwtHandler.ReadJwtToken(token).Issuer;
                            if (issuer == AppConstant.AuthenticationSchemes.OPEN_ID_DICT)
                            {
                                return AppConstant.AuthenticationSchemes.OPEN_ID_DICT;
                            }
                            /*
                            if (issuer == Consts.MY_AUTH0_ISS) // Auth0
                            {
                                return Consts.MY_AUTH0_SCHEME;
                            }

                            if (issuer == Consts.MY_AAD_ISS) // AAD
                            {
                                return Consts.MY_AAD_SCHEME;
                            }
                            */
                        }
                    }
                }
            }

            // We don't know what it is
            return AppConstant.AuthenticationSchemes.IDENTITY_APPLICATION;
        };
    });
}