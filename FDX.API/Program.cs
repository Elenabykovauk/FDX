using FDX.Services;
using FDX.DataAccess.Context;
using FDX.DataAccess.Context.Interfaces;
using FDX.DataAccess.Repositories;
using FDX.DataAccess.Repositories.Interfaces;
using FDX.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var SpecificOriginsCors = "_SpecificOriginsCors";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: SpecificOriginsCors,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:44426");
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMappingSetup();
builder.Services.AddDbContext<MessagesDbContext>(options => options.UseInMemoryDatabase(databaseName: "Messages"));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IMessagesDbContext, MessagesDbContext>();
builder.Services.AddScoped<ISmsRepository, SmsRepository>();

builder.Services.AddScoped<ISmsService, SmsService>();
builder.Services.AddScoped<ISmsSendService, SmsSendService>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FDX.Api", Version = "v1" });
});

builder.Services.Configure<MessageQueueOptions>(builder.Configuration.GetSection("MessageQueue"));


var app = builder.Build();
app.UseCors(SpecificOriginsCors);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
