using FluentValidation;
using FluentValidation.AspNetCore;
using LibrarySystem.SharedKernel.DTOs;
using LibrarySystem.WebAPI.Middleware;
using LibrarySystem.Application.Services.Implementation;
using LibrarySystem.Application.Services.Interfaces;
using LibrarySystem.Application.Validators;
using LibrarySystem.Domain;
using LibrarySystem.Domain.Concrats.Implementation;
using LibrarySystem.Domain.Concrats.Interfaces;
using LibrarySystem.Server.Application.Implementation;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;



var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");


try
{

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    // NLog: Setup NLog for Dependency injection
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    builder.Services.AddTransient<ExceptionMiddleware>();

    builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
    builder.Services.AddScoped<IBookRepository, BookRepository>();

    builder.Services.AddScoped<IAuthorService, AuthorService>();
    builder.Services.AddScoped<IBookService, BookService>();

    builder.Services.AddScoped<IReaderRepository, ReaderRepository>();
    builder.Services.AddScoped<IReaderService, ReaderService>();

    builder.Services.AddScoped<ILoanRepository, LoanRepository>();
    builder.Services.AddScoped<ILoanService, LoanService>();

    builder.Services.AddControllers()
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

    builder.Services.AddTransient<IValidator<CreateLoanDTO>, CreateLoanDTOValidator>();
    builder.Services.AddTransient<IValidator<UpdateLoanDTO>, UpdateLoanDTOValidator>();
    builder.Services.AddTransient<IValidator<BookDTO>, BookValidator>();
    builder.Services.AddTransient<IValidator<AuthorDTO>, AuthorValidator>();
    builder.Services.AddTransient<IValidator<ReaderDTO>, ReaderValidator>();
    builder.Services.AddControllers();


    //builder.Services.AddFluentValidationAutoValidation();

    builder.Services.AddAutoMapper(typeof(Program));
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Konfiguracja DbContext dla SQLite
    builder.Services.AddDbContext<LibraryDbContext>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
            b => b.MigrationsAssembly("LibrarySystem.Infrastructure")));

    builder.Services.AddCors(o => o.AddPolicy("LibrarySystem", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    }));

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseCors("LibrarySystem");
    app.UseMiddleware<ExceptionMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}

catch (Exception exception)
{
    logger.Error(exception, "Stopped program because of exception");
    throw;
}

finally
{
    NLog.LogManager.Shutdown();
}
