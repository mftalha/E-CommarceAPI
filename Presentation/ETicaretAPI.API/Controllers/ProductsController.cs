using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        readonly private IProductWriteRepository _productWriteRepository;
        readonly private IProductReadRepository _productReadRepository;

        readonly private IOrderReadRepository _orderReadRepository;
        readonly private IOrderWriteRepository _orderWriteRepository;

        readonly private ICustomerReadRepository _customerReadRepository;
        readonly private ICustomerWriteRepository _customerWriteRepository;
        public ProductsController(IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IOrderWriteRepository orderWriteRepository,
            ICustomerWriteRepository customerWriteRepository,
            IOrderReadRepository orderReadRepository,
            ICustomerReadRepository customerReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _orderWriteRepository = orderWriteRepository;
            _customerWriteRepository = customerWriteRepository;
            _orderReadRepository = orderReadRepository;
            _customerReadRepository = customerReadRepository;
        }

        [HttpGet]
        public async Task Get()  // async void Get() == task kullanma sebebimiz == asennkron çalışmada işlem bittikten sonra diğer işteme geçmesi için : biz burda Task kullanmadığımız için : AddRangeAsync methodu bitmeden  SaveAsync geçtiğinden AddRangeAsync requestinin dependingi bitmeden yok edildiği için hata alıyorduk. services.AddSingleton da hata almamamızın nedeni ise AddRangeAsync den SaveAsync geçerken == AddRangeAsync tamamlamasa bile dispose/imha yapmadığından hata almıyorduk. == çünkü depending services.AddSingleton'da uygulamay mahsuz o nesneler 1 defa kalacak bidaha dispose edilmeyecek : dispose edilmediğinden hatayı engelliyordu.  == ama doğru olan çalışma şekli AddScope ile devam etmek
        {
            //await _productWriteRepository.AddRangeAsync(new()
            //{
            // new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, CreateDate = DateTime.UtcNow ,Stock=10},
            // new() { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, CreateDate = DateTime.UtcNow ,Stock=20},
            // new() { Id = Guid.NewGuid(), Name = "Product 3", Price = 300, CreateDate = DateTime.UtcNow ,Stock=130},
            //});
            //var count = await _productWriteRepository.SaveAsync();
            /*
           Product p = await _productReadRepository.GetByIdAsync("dba02bd2-54a5-49e2-993e-3c87a0e765bb",false);*/ //tracking ile ilgili hiçbir data vermez isem tracking true olacak. == yani takip edecek tracking , false verirsem trackingi takip etmiyecek böylece apide çalıştırdığımda status 200 başarılı sonucu alsamda veritabanında fiziksel bi değişim olmıyacaktır.
            /*
            p.Name = "Mehmet";
            _productWriteRepository.SaveAsync();*/ // _productReadRepository ile okuduğum veriyi nasıl _productWriteRepository ile yazdırıyorum = depending enjectionda aynı scope kullanıyorlar = bu yüzden _productReadRepository'i kullandığı dbcontext ne ise bunu talep eden _productWriteRepository'de aynı instance'yi elde edecektir. o yüzden biz datayı elde ettiğimiz dbcontext üzerinden SaveAsync() fonksiyonunu çağırmış olacaz.
                                                   // yapılan değişikliği kaydediyoruz veritabanına.

            /*
            await  _productWriteRepository.AddAsync(new() { Name = "D Product", Price = 1.600F, Stock = 20, CreatedDate = DateTime.UtcNow }); // Id verirsem alır ; vermezsem kendisi verir arka planda.
            await _productWriteRepository.SaveAsync();
            */
            /*
            var customerId = Guid.NewGuid();
            await _customerWriteRepository.AddAsync(new() { Id = customerId ,Name="Muhiddin"});

            await _orderWriteRepository.AddAsync(new() { Description = "bla bla bla", Address = "Ankara, Çankaya", CustomerId = customerId });
            await _orderWriteRepository.AddAsync(new() { Description = "bla bla bla 2", Address = "Ankara, Pursaklar", CustomerId = customerId });
            await _orderWriteRepository.SaveAsync(); //burda biz _orderWriteRepository ine SaveAsync() yapsakda arkada scope olarak aynı dbconteci kullandıklarından hepsine işleyip  _customerWriteRepository içinde ekleme işlemi gerçekleşecektir.
            */

            Order order = await _orderReadRepository.GetByIdAsync("bcb19f78-62fa-405d-a5ba-94c7b09552e0");
            order.Address = "İstanbul";
            await _orderWriteRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
          Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
