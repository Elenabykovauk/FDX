var builder = WebApplication.CreateBuilder(args);

var SpecificOriginsCors = "_SpecificOriginsCors";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: SpecificOriginsCors,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7011", "http://localhost:5011");
                      });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(SpecificOriginsCors);

app.UseAuthorization();

app.MapControllers();

app.Run();
