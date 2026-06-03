
using Api.Helpers;
using Api.Repositories;
using Api.Repositories.Members;
using Api.Services.Health;
using Api.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.SignalR;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddOpenApi();
builder.Services.AddCors(op => op.AddPolicy("DatingAppClient", url => url.AllowAnyHeader().AllowAnyMethod().AllowCredentials().WithOrigins("http://localhost:4200")));
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddSignalR();
builder.Services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateProfileValidator>();
builder.Services.AddDbContext<ApplicationDbContext>(op => op.UseSqlite("Data Source=datingApp.db").UseLazyLoadingProxies());
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IMembersService, MembersService>();
builder.Services.AddScoped<IAttachmentRepository, AttachmentRepository>();
builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddScoped<IChatMessageService, ChatMessageService>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IMatchRepository, MatchRepository>();
builder.Services.AddScoped<IMatchService, MatchService>();
builder.Services.AddScoped<IUserClaimsService, UserClaimsService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IMemberMatchingService, MemberMatchingService>();
builder.Services.AddSingleton<IHealthPerformanceMetrics, HealthPerformanceMetrics>();
builder.Services.AddSingleton<IFileService, FileService>();
builder.Services.AddSingleton<INotificationService, NotificationService>();
builder.Services.AddSingleton<IUserIdProvider, HubConnectionIdProvider>();
builder.Services.AddSwaggerGen(
    op => op.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo
    {
        Title = "Dating App",
        Version = "1.0.0",
        Description = "API Endpoint for Dating App"
    })
);
builder.Services.Configure<FileStorageConfiguration>(builder.Configuration.GetSection("FileStorageInfo"));
WebApplication app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Database.Migrate();
}
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseCors("DatingAppClient");
app.UseMiddleware<JWTAuthenticatorMiddleware>();
app.MapHub<ChatHub>("/hub");
app.UseHttpsRedirection();
app.UseSwagger();
app.UseSwaggerUI();
app.MapControllers();
app.Run();

