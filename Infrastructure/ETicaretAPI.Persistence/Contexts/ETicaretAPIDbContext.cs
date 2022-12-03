using ETicaretAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Contexts //bu classın amacı sql deki tabloların proğram içindeki karşılığı ?
{
    public class ETicaretAPIDbContext : DbContext // mirası Microsoft.EntityFrameworkCore'dan alıyoruz : ad önemli değil miras almamız önemli 
    {
        public ETicaretAPIDbContext(DbContextOptions options) : base(options) //ctrl+. diyerek bu contractırı oluşturuyorum IOC Containerda doldurulacak : bu contstractırı oluşturmaz sam süreçte hata alırım.
        {}

        // burada bizim oluşturduğumuz entitylere karşı : Product,Order,Customer karşılık = Products, Orders,Customers tablolarını oluştur diyorum : entity propertylerinide tablo özelliği yap. bunu DbSet ile belirtiyorum : tabloları oluştur diye
        public DbSet<Product> Products { get; set; } //tablo isimleri defaul olarak burda belirttiğim Products oluyor mesela biz bunu sonradan müdahale ile değiştirebiliyoruz isteğe göre.
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
