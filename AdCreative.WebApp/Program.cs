using AdCreative.Business;
using AdCreative.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// appsettings.json'dan baðlantý dizisini alacak þekilde DbContext eklendi
builder.Services.AddDbContext<WordContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Konsola yazdýrmak için bir TraceListener eklendi
Trace.Listeners.Add(new ConsoleTraceListener());

// Dosyaya yazdýrmak için bir TextWriterTraceListener eklendi
var textListener = new TextWriterTraceListener("trace.log");
Trace.Listeners.Add(textListener);

// Otomatik olarak buffer'ý temizler
Trace.AutoFlush = true;

// Loglama seviyeleri
Trace.TraceError("Bu bir hata mesajýdýr.");
Trace.TraceWarning("Bu bir uyarý mesajýdýr.");
Trace.TraceInformation("Bu bir bilgilendirme mesajýdýr.");
// AutoMapper'ý ve WordService'i DI konteynerine ekle
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<WordService>();

var app = builder.Build();

// Veritabaný migration iþlemi
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WordContext>();
    try
    {
        dbContext.Database.Migrate(); // Migrations'u otomatik olarak çalýþtýrýr
    }
    catch (Exception ex)
    {
        Trace.TraceError($"Migration sýrasýnda bir hata oluþtu: {ex.Message}");
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
