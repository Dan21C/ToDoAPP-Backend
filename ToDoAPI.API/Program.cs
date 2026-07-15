using ToDoAPI.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Registra todo Infrastructure (DbContext, repositorios, servicios)
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();

// Permite peticiones desde cualquier origen (necesario para Swagger y el frontend)
builder.Services.AddCors();

// Agrega Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Permite cualquier origen, método y header
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();