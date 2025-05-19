

using FluentValidation;
using FluentValidation.AspNetCore;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using SurveyBasket.Api;
using SurveyBasket.Api.Contracts.Polls;
using SurveyBasket.Api.Persistence;
using SurveyBasket.Api.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDependencies(builder.Configuration );


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();
