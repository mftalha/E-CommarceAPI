//bu sayfa �os'i container wep app�daki = mvc nin kendi yap�s�nda mevcut
using ETicaretAPI.Persistence;

var builder = WebApplication.CreateBuilder(args);

//Ap� katman�ndan Persistence katman�na eri�im i�in yazd���m�z methodu depending enjectiona enjecte ediyoruz. = tabi bunun i�in API katman�na Persistence katman�n� referans olarak veriyoruz.
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
