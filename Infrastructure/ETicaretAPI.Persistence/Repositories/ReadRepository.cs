using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Repositories
{ //predicate == yüklem
    public class ReadRepository<T> : IReadRepository<T> where T :BaseEntity  //genel okuma işlemleri = sadece in tabloya göre değil. = generic
    {
        private readonly ETicaretAPIDbContext _context; // ETicaretAPIDbContext.cs yi  ıos containera vermiştim bu class'ıda Ios containera verecem o sayede bu değişken direk ordan miras almış olacak. : IOS'ı container kendi içinde miras işlemlerini kontrol ediyor. = böylece classının nesnesini oluşturmama gerke kalmıyor.
        public ReadRepository(ETicaretAPIDbContext context) // Constructerdan injecte edebiliyorum contextimi
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>(); // biz tablo olarak bi T generic = entitie vermemiz lazım = customer , product diye belirli bir entities vermek istmeiyoruz : genel yapı laızm = bu yüzden Entitiy framwork core da == Set<entity>() yapısı var bizde bunu kullanıyoruz : bizim entity'imizde = T
        //Repostiyorden geldi bu evrensel
        public IQueryable<T> GetAll() => Table; //veritabanında T ye uygun ne kadar veri varsa getir. (IQueryable = veritabanından filtreliyerek veri getirmek içindi.)

        public IQueryable<T> getWhere(Expression<Func<T, bool>> method) 
            => Table.Where(method); // şarta uygun verileri getir.
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method)
            => await Table.FirstOrDefaultAsync(method); //tek bir veri döndürmek için. == asenkron olduğu için await async mantıklarını uyguluyoruz.
        public async Task<T> GetByIdAsync(string id)
            //=> Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id)); // where T :class yerine where T :BaseEntity çevirme mantığını bu method için yaptık. id ye erişim için. Guid.Parse(id) ; id yi guid id ye çevirme.
            => await Table.FindAsync(Guid.Parse(id)); // yukarıdaki yönteme ek olarak FindAsync kullanılabilir : daha kolay.
    }
}
