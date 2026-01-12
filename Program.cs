using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Register the OpenAPI generator
builder.Services.AddOpenApi("enums");
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Enable the OpenAPI JSON endpoint
    app.MapOpenApi();
}

// Swapping query1 and query2 order of registration, adds the null value back into the generated OpenAPI spec.

// Adds null value to XYZ enum
app.MapGet("/query1", (ABC abc, XYZ xyz, XYZ? nullableXyz) => $"HELLO {abc} | {xyz} | {nullableXyz}");

// Does not add null value to XYZ enum, but also doesn't declare nullableXyz as oneOf with null value and reference.2
app.MapGet("/query2", (ABC abc, XYZ? nullableXyz, XYZ xyz) => $"HELLO {abc} | {xyz} | {nullableXyz}");

app.Run();

public enum ABC
{
    A,
    B,
    C
}

public enum XYZ
{
    X,
    Y,
    Z
}
