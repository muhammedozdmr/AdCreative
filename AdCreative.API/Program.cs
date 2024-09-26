using AdCreative.Business;
using AdCreative.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Uygulama kapan�rken TextWriterTraceListener'� kapat
app.Lifetime.ApplicationStopped.Register(() =>
{
    try
    {
        textListener.Dispose();
    }
    catch (Exception ex)
    {
        Trace.TraceError($"TraceListener kapat�l�rken bir hata olu�tu: {ex.Message}");
    }
});
