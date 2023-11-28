using LeoCore.Code.Mappers.IBF;
using LeoCore.Code.Mappers.Trainings;
using LeoCore.Data;
using LeoCore.Models.Trainings;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(typeof(Program));

//

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddDbContext<LeoDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("QuestionsConnection")));
builder.Services.AddControllers();
//builder.Services.AddGridMvc();

builder.Services.AddScoped<IPersonQuestionnaireRepository, PersonQuestionnaireRepository>();
builder.Services.AddScoped<IPersonTrainingRepository, PersonTrainingRepository>();
builder.Services.AddScoped<IQuestionnaireRepository, QuestionnaireRepository>();
builder.Services.AddScoped<ITrainingRepository, TrainingRepository>();
builder.Services.AddScoped<IUserCodeRepository, UserCodeRepository>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Questionnaires}/{action=Maintenance}/{pID?}");

app.Run();
