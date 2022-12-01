using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Concretes // Concrete = Somut  
{
    public class ProductService : IProductService // Application katmanında soyut bir şekilde oluşturduğumuz iskeleti ; Persistence(veritabanı ilişki) katmanında somut bir hale getiriyoruz : mimari bu şeklilde.
    { //burda yapmamız gereken veritabanına gerekli isteklerde bulunup : gelen verileri yollamak.

        /*
        //bormal interfaci implement ettiğimden oluşturmam gereken method.
        public List<Product> GetProducts()
        {
            throw new NotImplementedException();
        }
        */
        public List<Product> GetProducts()
            => new() //bu yapıyı kullanma sebebimiz : new Product diyip productun objesinden yeni nesne oluşturmak yerine direk kendisi method'da da Product olduğundan Productun nesnesi olduğunu algılıyor benim belirtmeme gerek kalmıyor. == target type özelliği : ismide == referansı belli olan nesne oluştururken kullanıyoruz. =) new nesne tipi demeye gerek kalmıyor kısaca = return işlemi yapacaksak direk ona göre algılıyor kendisi ve bizim şu obje diye belirtmemize gerek kalmıyor.
            { // Guid.NewGuid() == yeni bi guid oluşturabilmek için = her seferinde farklı oluşturur kendisi
                new() {Id = Guid.NewGuid(), Name = "Product 1", Price = 100, Stock =10},
                new() {Id = Guid.NewGuid(), Name = "Product 2", Price = 200, Stock =10},
                new() {Id = Guid.NewGuid(), Name = "Product 3", Price = 300, Stock =10},
                new() {Id = Guid.NewGuid(), Name = "Product 4", Price = 400, Stock =10},
                new() {Id = Guid.NewGuid(), Name = "Product 5", Price = 500, Stock =10}
            };
    }
}
