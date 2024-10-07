using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGet("/Greeting/{name}", ([FromRoute] string name) =>
{
    return $"Hello {name}";
})
.WithName("GreetingWithRoute")
.WithOpenApi();

app.MapGet("/GreetingFromQueryParam", ([FromQuery] string name) =>
{
    return $"Hello {name}";
})
.WithName("GreetingWithQuery")
.WithOpenApi();

app.MapGet("/Add/{a}/{b}", ([FromRoute] int a, [FromRoute] int b) =>
{
    return $"{a} + {b} = {a + b}";
})
.WithName("Addition")
.WithOpenApi();

app.MapGet("/{op}/{a}/{b}", ([FromRoute] string op, [FromRoute] int a, [FromRoute] int b) =>
{
    switch (op)
    {
        case "Add":
        case "+":
            return $"{a} + {b} = {a + b}";

        case "Mult":
        case "*":
            return $"{a} * {b} = {a * b}";

        case "Division":
        case "/":
            return $"{a} / {b} = {a / b}";

        case "Remainder":
        case "%":
            return $"{a} % {b} = {a % b}";

        case "Sub":
        case "-":
            return $"{a} - {b} = {a - b}";

        default:
            return "Error: Unsupported operation";
    }
})
.WithName("Calculator")
.WithOpenApi();

app.MapGet("/Fact/{a}", ([FromRoute] int a) =>
{
    if (a < 0)
    {
        return "Please enter a positive int";
    }
    return $"{a}! = {Factorial(a)}";

})


.WithName("Factorial")
.WithOpenApi();

int Factorial(int n)
{
    if (n == 0 || n == 1)
        return 1;

    int result = 1;
    for (int i = 2; i <= n; i++)
    {
        result *= i;
    }
    return result;
}

app.Run();

