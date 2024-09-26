using AdCreative.Business;
using AdCreative.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Uygulama kapanýrken TextWriterTraceListener'ý kapat
app.Lifetime.ApplicationStopped.Register(() =>
{
    try
    {
        textListener.Dispose();
    }
    catch (Exception ex)
    {
        Trace.TraceError($"TraceListener kapatýlýrken bir hata oluþtu: {ex.Message}");
    }
});
