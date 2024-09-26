using AdCreative.Business;
using AdCreative.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// appsettings.json'dan ba�lant� dizisini alacak �ekilde DbContext eklendi
builder.Services.AddDbContext<WordContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Konsola yazd�rmak i�in bir TraceListener eklendi
Trace.Listeners.Add(new ConsoleTraceListener());

// Dosyaya yazd�rmak i�in bir TextWriterTraceListener eklendi
var textListener = new TextWriterTraceListener("trace.log");
Trace.Listeners.Add(textListener);

// Otomatik olarak buffer'� temizler
Trace.AutoFlush = true;

// Loglama seviyeleri
Trace.TraceError("Bu bir hata mesaj�d�r.");
Trace.TraceWarning("Bu bir uyar� mesaj�d�r.");
Trace.TraceInformation("Bu bir bilgilendirme mesaj�d�r.");
// AutoMapper'� ve WordService'i DI konteynerine ekle
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<WordService>();

var app = builder.Build();

// Veritaban� migration i�lemi
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WordContext>();
    try
    {
        dbContext.Database.Migrate(); // Migrations'u otomatik olarak �al��t�r�r
    }
    catch (Exception ex)
    {
        Trace.TraceError($"Migration s�ras�nda bir hata olu�tu: {ex.Message}");
    }
}

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
