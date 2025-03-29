using MinimalAPI_2;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Leo's Library API",
        Version = "v1",
        Description = "API for managing books in Leo's Library."
    });
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSingleton<IBookService, BookService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.MapGet("/books", (IBookService bookService) =>
    TypedResults.Ok(bookService.GetBooks()))
    .WithName("GetBooks");

app.MapGet("/books/{id}", (int id, IBookService bookService) =>
{
    var book = bookService.GetBook(id);
    return book is not null ? Results.Ok(book) : Results.NotFound();

}).WithName("GetBookById");

app.MapPost("/books", (Book book, IBookService bookService) =>
{
    bookService.AddBook(book);
    return Results.Created($"/books/{book.Id}", book);
}).WithName("CreateBook");

app.MapPut("/books/{id}", (int id, Book updatedBook, IBookService bookService) =>
{
    var book = bookService.UpdateBook(id, updatedBook);
    return book is not null ? Results.Ok(book) : Results.NotFound();
}).WithName("UpdateBook");

app.MapDelete("/books/{id}", (int id, IBookService bookService) =>
{
    var deleted = bookService.DeleteBook(id);
    return deleted ? Results.NoContent() : Results.NotFound();
}).WithName("DeleteBook");
app.Run();
