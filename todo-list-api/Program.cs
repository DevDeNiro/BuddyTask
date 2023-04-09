using TodoApi.Data;
using TodoApi.Service.ITodoService;
using TodoApi.Service.ICategoryService;

var builder = WebApplication.CreateBuilder(args);

// Add CORS Middleware
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

builder.Host.ConfigureLogging(logging =>
{
    logging.ClearProviders();
    logging.AddConsole();
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services 
builder.Services.AddScoped<MongoDbContext>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    return new MongoDbContext(config);
});

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

// builder.WebHost.ConfigureKestrel(options =>
// {
//     options.ListenAnyIP(7174);
// });


// If needed, add : dotnet dev-certs https --trust
// app.UseHttpsRedirection();

// Really usefull for 415 error ?
app.UseCors("AllowAllOrigins");

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
