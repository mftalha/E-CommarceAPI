using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Persistence.Concretes;
using Microsoft.Extensions.DependencyInjection; // IServiceCollection için nugetten kuruldu :  ctrl . diyerek üstündeki seçeneklerden kurdum == mvc nin dependin injection u olan ıoc' ye erişim sağlıyor aynı program.cs deki gibi
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceService(this IServiceCollection services)
        {
            services.AddSingleton<IProductService, ProductService>(); //IProductService çağrıldığında , ProductService gönder diyoruz burdan ama bu yeterli değil : API Katmanını ProductService almak için ve  bulunduğumuz AddPersistenceService methoduna = API katmanındaki Program.cs den erişmek için API katmanına , Persistebce katmanını referans etmemiz gerekiyor. sonrada Apı katmanında program.cs ye gidip : builder.Services.AddPersistenceService(); ekliyoruz = yani yazdığımız bu methodu ekliyoruz. : böylece apı katmanından Persistence katmanına eriişimi sağlamış oluyoruz. == Yatay ilişki.
        }
    }
}
