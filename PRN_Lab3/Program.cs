using PRN_Lab3.Hubs;
using PRN_Lab3.Logic;
using PRN_Lab3.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<NorthwindContext>();
//config for session
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession();
//config for realtime
builder.Services.AddSignalR();
//access to db should be transient
builder.Services.AddTransient<ProductService>();
builder.Services.AddTransient<OrderService>();
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

app.UseAuthorization();

app.MapRazorPages();
//session
app.UseSession();
//config hub for realtime
app.MapHub<CartHub>("/CartHub");
app.Run();
