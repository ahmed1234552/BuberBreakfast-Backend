using BuberBreakfast.Services.Breakfasts;
var builder = WebApplication.CreateBuilder(args);
{
// Add services to the container.
builder.Services.AddControllers();
//to tell the framework how to create the breakfast service interface
builder.Services.AddScoped<IBreakfastService, BreakfastService>();
//every time the interface is requested, the same instance of the class will be returned
//!AddSingleton
// every time we Instantiates an object that object requests the I 
//breakfast service in the constructor then use the breakfast service
// object  as implementation of this interface 
// options for the lifetime of this breakfast service object 
// Singleton tells the framework the first time that someone
//  requests that IBreakfastService interface then create new breakfast
//   service object but from now on throughout the lifetime of 
//   the application every time someone requests this interface 
//   always use the same breakfast service object that you just
//    created
//other option over here is to say AddScoped<>.
//!AddScoped<>
// Scope says within the lifetime of a single request 
// the first time someone request this interface  then 
// create a new object but from now until we finish
//  processing this request every time we create an object 
//  and request this interface then return the same breakfast
//   service object that you create before.
//!AddTransient<>
//every time someone requests this
//interface create a new breakfast service object
}

var app = builder.Build();//manage request pipeline
{
    app.UseExceptionHandler("/error");//middleware
    //if there is error change the route to /error
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}