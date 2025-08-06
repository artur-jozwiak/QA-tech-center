using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using QA.BLL.Interfaces;
using QA.BLL.Services;
using QA.DataAccess;
using QA.DataAccess.Repositories;
using QA.DataAccess.Repositories.CoatingRepositories;
using QA.DataAccess.Repositories.ERPRepositories;
using QA.DataAccess.Repositories.Helicheck;
using QA.DataAccess.Repositories.Keyence;
using QA.DataAccess.Repositories.OldDbRepositories;
using QA.UI.Models;
using QA.UI.Services;
using Serilog;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddLocalization();
var supportedCultures = new[] { "pl-PL"};
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("pl-PL")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

var qaConnection = builder.Configuration.GetConnectionString("QAConnection");
var erpConnection = builder.Configuration.GetConnectionString("HermesConnection");
var oldDbConnection = builder.Configuration.GetConnectionString("OldDbConnection");
var helicheckConnection = builder.Configuration.GetConnectionString("HeliConnection");

Console.WriteLine(qaConnection);
Console.WriteLine(erpConnection);

//builder.Services.AddDbContextFactory<QAContext>(options => options.UseSqlServer(qaConnection).EnableSensitiveDataLogging());
builder.Services.AddDbContext<QAContext>(options => options.UseSqlServer(qaConnection).EnableSensitiveDataLogging());
builder.Services.AddDbContext<ERPContext>(options => options.UseSqlServer(erpConnection).EnableSensitiveDataLogging());
builder.Services.AddDbContext<OldDbContext>(options => options.UseSqlServer(oldDbConnection).EnableSensitiveDataLogging());
builder.Services.AddDbContext<HelicheckContext>(options => options.UseSqlServer(helicheckConnection).EnableSensitiveDataLogging());

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 5;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<QAContext>();

builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
{
    loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration);
});

builder.Services.AddScoped<AuthenticationStateProvider, IdentityValidationProvider<IdentityUser>>();
builder.Services.AddScoped<IErrorLogger, ErrorLogger>();
builder.Services.AddScoped<IKeyenceReader, KeyenceReader>();

builder.Services.AddScoped<IOrderRepository, OrderdRepository>();
builder.Services.AddScoped<IParameterRepository, ParameterRepository>();
builder.Services.AddScoped<ILaboratoryRepository, LaboratoryRepository>();
builder.Services.AddScoped<IChildParameterAssignementRepository, ChildParameterAssignementRepository>();
builder.Services.AddScoped<ISinteringOrdersRepository, SinteringOrdersRepository>();
builder.Services.AddScoped<IPowderRepository, PowderReposiotry>();
builder.Services.AddScoped<IOperationRepository, OperationRepository>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementsRepository>();
builder.Services.AddScoped<IMeasurementSeriesRepository, MeasurementSeriesRepository>();
builder.Services.AddScoped<IKeyenceRepository, KeyenceRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IErpOrderRepository, ErpOrderRepository>();
builder.Services.AddScoped<IErpOperationRepository, ErpOperationRepository>();
builder.Services.AddScoped<IMRBRepository, MRBRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();
builder.Services.AddScoped<IPressingRepository, PressingRepository>();
builder.Services.AddScoped<IPressingRepository, PressingRepository>();
builder.Services.AddScoped<ISinteringRepository, SinteringRepository>();
builder.Services.AddScoped<IPVDStatsRepository, PVDStatsRepository>();
builder.Services.AddScoped<ICoatingRepository, CoatingRepository>();
builder.Services.AddScoped<IHelicheckRepository, HelicheckRepository>();

//
builder.Services.AddScoped<IOldDbRepository, OldDbRepository>();
//

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IOperationService, OperationService>();
builder.Services.AddScoped<IParameterService, ParameterService>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<ISPCService, SPCService>();
builder.Services.AddScoped<IImgService, ImgService>();
builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddScoped<IHelperService, HelperService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddHostedService<DataCollectionService>();

builder.Services.AddScoped<DragAndDropService>();

builder.Services.AddMudServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

//odczyt raportu pvd - prawdopodobnie da siê tak czytac obrazki
//var appMainPath = builder.Configuration["AppSettings:QAPath"];
//if (!string.IsNullOrEmpty(appMainPath))
//{
//    app.UseStaticFiles(new StaticFileOptions
//    {
//        //FileProvider = new PhysicalFileProvider(pvdStatsPath),
//        FileProvider = new PhysicalFileProvider(Path.Combine(appMainPath, "PVD")), // dodaæ wiêcej lokalizacji albo cofn¹æ siê dalej
//        RequestPath = "/pvd-reports"
//    });
//}

//static Files
var coatingUnitPath = builder.Configuration["AppSettings:CoatingUnitPath"];
if (!string.IsNullOrEmpty(coatingUnitPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        //FileProvider = new PhysicalFileProvider(pvdStatsPath),
        FileProvider = new PhysicalFileProvider(coatingUnitPath), // dodaæ wiêcej lokalizacji albo cofn¹æ siê dalej
        RequestPath = "/coating-unit-report"
    });
}

var pvdMicroscopePath = builder.Configuration["AppSettings:PVDMicroscopePath"];
if (!string.IsNullOrEmpty(pvdMicroscopePath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        //FileProvider = new PhysicalFileProvider(pvdStatsPath),
        FileProvider = new PhysicalFileProvider(pvdMicroscopePath), // dodaæ wiêcej lokalizacji albo cofn¹æ siê dalej
        RequestPath = "/pvd-microscope"
    });
}

//Images - testowo
//Images - testowo

//Images - testowo
//var imagesPath = builder.Configuration["AppSettings:ImagesPath"];
//if (!string.IsNullOrEmpty(imagesPath))
//{
//    app.UseStaticFiles(new StaticFileOptions
//    {
//        //FileProvider = new PhysicalFileProvider(pvdStatsPath),
//        FileProvider = new PhysicalFileProvider(imagesPath), // dodaæ wiêcej lokalizacji albo cofn¹æ siê dalej
//        RequestPath = "/images"
//    });
//}

//static Files


app.UseRequestLocalization(localizationOptions);

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

//using (var scope = app.Services.CreateScope())
//{
//    await AdminAccountInitializer.Initialize(scope.ServiceProvider);
//}

//using (var scope = app.Services.CreateScope())
//{
//     VISCategoriesInitializer.SeedData(scope.ServiceProvider);
//}

app.Run();

