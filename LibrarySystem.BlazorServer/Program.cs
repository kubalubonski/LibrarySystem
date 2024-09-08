using FluentValidation;
using FluentValidation.AspNetCore;
using LibrarySystem.Application.Mapper;
using LibrarySystem.Application.Services.Implementation;
using LibrarySystem.Application.Services.Interfaces;
using LibrarySystem.Application.Validators;
using LibrarySystem.BlazorServer.Data;
using LibrarySystem.Domain;
using LibrarySystem.Domain.Concrats.Implementation;
using LibrarySystem.Domain.Concrats.Interfaces;
using LibrarySystem.Server.Application.Implementation;
using LibrarySystem.SharedKernel.DTOs;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Configure NLog
LogManager.LoadConfiguration("nlog.config");

builder.Logging.ClearProviders(); // Opcjonalnie usuñ domyœlne dostawców logów
builder.Logging.AddNLog();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

var sqliteConnectionString = "Data Source=../LibrarySystem.WebAPI/librarymanagement.db";
builder.Services.AddDbContext<LibraryDbContext>(options =>
 options.UseSqlite(sqliteConnectionString, b => b.MigrationsAssembly("LibrarySystem.Infrastructure")));
// Dodane z WebApi
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddTransient<IValidator<CreateLoanDTO>, CreateLoanDTOValidator>();
builder.Services.AddTransient<IValidator<UpdateLoanDTO>, UpdateLoanDTOValidator>();
builder.Services.AddTransient<IValidator<BookDTO>, BookValidator>();
builder.Services.AddTransient<IValidator<AuthorDTO>, AuthorValidator>();
builder.Services.AddTransient<IValidator<ReaderDTO>, ReaderValidator>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddScoped<IReaderRepository, ReaderRepository>();
builder.Services.AddScoped<IReaderService, ReaderService>();

builder.Services.AddScoped<ILoanRepository, LoanRepository>();
builder.Services.AddScoped<ILoanService, LoanService>();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
