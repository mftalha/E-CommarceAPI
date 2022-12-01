//bu sayfa ýos'i container wep appýdaki = mvc nin kendi yapýsýnda mevcut
using ETicaretAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

//Apý katmanýndan Persistence katmanýna eriþim için yazdýðýmýz methodu depending enjectiona enjecte ediyoruz. = tabi bunun için API katmanýna Persistence katmanýný referans olarak veriyoruz.
builder.Services.AddPersistenceService();

//

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
