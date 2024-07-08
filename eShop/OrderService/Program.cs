using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Services;
using OrderService.Services.Interfaces;
using OrderService.Models; // ������� ��� ������

var builder = WebApplication.CreateBuilder(args);

// ��������� ������ ��� ������������ SmtpSettings
builder.Configuration.Bind("SmtpSettings", new SmtpSettings());
builder.Services.AddSingleton(builder.Configuration.Get<SmtpSettings>());

// ��������� ������ ��� �������� ���������� �����
builder.Services.AddTransient<IEmailService, EmailService>();

// ��������� ���������� ���������
builder.Services.AddControllers();

// ��������� Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ��������� �������� ����������
app.UseRouting();
app.UseAuthorization();

app.MapControllers(); // ������ app.MapPost, ������������� MapControllers()

app.Run();