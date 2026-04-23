
using Api.Repositories;
using Api.Repositories.Members;
using Api.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddCors(op => op.AddPolicy("DatingAppClient", url => url.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseSqlite("Data Source=datingApp.db").UseLazyLoadingProxies());
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IMembersService, MembersService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IUserClaimsService, UserClaimsService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IMemberMatchingService,MemberMatchingService>();
builder.Services.AddSwaggerGen(
    op => op.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Dating App",
        Version = "1.0.0",
        Description = "API Endpoint for Dating App"
    })
);
WebApplication app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseCors("DatingAppClient");
app.UseMiddleware<JWTAuthenticatorMiddleware>();
app.UseHttpsRedirection();
app.UseSwagger();
app.UseDeveloperExceptionPage();
app.UseSwaggerUI();
app.MapControllers();
app.Run();

