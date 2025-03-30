using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("http://localhost:3000")  // Your React app URL
              .AllowAnyMethod()
              .AllowAnyHeader()
              .SetPreflightMaxAge(TimeSpan.FromMinutes(10))
              .AllowCredentials();
    });
});

// 1. Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "JWT API", Version = "v1" });

    // Add JWT support in Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Enter JWT Bearer token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

// 2. JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "App",
            ValidAudience = "App",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_super_secret_key_which_is_long"))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();

// 3. Swagger Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "JWT API v1");
    });
}

// API Route
app.MapGet("/api/validate", [Authorize] () =>
{
    return Results.Json(new { message = "Token is Valid" });
});

//app.MapGet("/api/validate", [Authorize] (HttpContext context) =>
//{
//    var token = context.Request.Headers["Authorization"];
//    Console.WriteLine($"Received Token: {token}");

//    if (string.IsNullOrEmpty(token))
//    {
//        Console.WriteLine("No Token Received");
//    }

//    return Results.Json(new { message = "Token is Valid" });
//});

//app.UseCors(options =>
//    options.AllowAnyOrigin()   // Allow requests from any origin
//           .AllowAnyHeader()   // Allow any headers
//           .AllowAnyMethod());


//app.Lifetime.ApplicationStarted.Register(() =>
//{
//    Console.WriteLine($"App running on {app.Urls.FirstOrDefault()}");
//});

app.Run();

//Console.WriteLine($"App running on {app.Urls.FirstOrDefault()}");

