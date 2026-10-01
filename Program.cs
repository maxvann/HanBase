using HanBase.Data;
using HanBase.Interfaces;
using HanBase.Lib;
using HanBase.Mediators;
using HanBase.Repositories;
using HanBase.WorkerService;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<HanBaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("HanBaseContext") ??
        throw new InvalidOperationException("Connection string 'HanBaseContext' not found.")));

builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<IViewModelMediator, ViewModelMediator>();

builder.Services.AddScoped<ICharacterDetails, CharacterDetails>();

builder.Services.AddScoped<IReadingRepository, ReadingRepository>();

builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();

builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddWindowsService();

builder.Services.AddHostedService<Worker>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(12);
});

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");

    // Note: This Windows service will exist behind the Firewall and Reverse Proxy as an Intranet website.
    // app.UseHsts();
}
else
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
