var builder = WebApplication.CreateBuilder(args);
{
// Add services to the container.
builder.Services.AddControllers();
}

var app = builder.Build();//manage request pipeline
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}