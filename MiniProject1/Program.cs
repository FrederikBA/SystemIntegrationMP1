using MiniProject1.Services;

var policyName = "AllowOrigin";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: policyName,
        builder =>
        {
            builder
                .WithOrigins("http://localhost:5183")
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


builder.Services.AddScoped<SoapService>();
builder.Services.AddScoped<EmailService>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(policyName);

app.UseAuthorization();

app.MapControllers();

app.Run();