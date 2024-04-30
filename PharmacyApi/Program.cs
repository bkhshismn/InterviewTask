
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PharmacyApi.DataContext;
using PharmacyApi.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PharmacyContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<ICityService, CityService>();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
        ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]!))
    };
});

builder.Services.AddControllers(
    options => options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

//using (IServiceScope scope = app.Services.CreateAsyncScope())
//{
//    var context = scope.ServiceProvider.GetService<PharmacyContext>();
//    await context.Database.MigrateAsync();
//}

app.Run();
