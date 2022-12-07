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
        public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
        }

        [HttpGet]
        public async Task Get()  // async void Get() == task kullanma sebebimiz == asennkron çalışmada işlem bittikten sonra diğer işteme geçmesi için : biz burda Task kullanmadığımız için : AddRangeAsync methodu bitmeden  SaveAsync geçtiğinden AddRangeAsync requestinin dependingi bitmeden yok edildiği için hata alıyorduk. services.AddSingleton da hata almamamızın nedeni ise AddRangeAsync den SaveAsync geçerken == AddRangeAsync tamamlamasa bile dispose/imha yapmadığından hata almıyorduk. == çünkü depending services.AddSingleton'da uygulamay mahsuz o nesneler 1 defa kalacak bidaha dispose edilmeyecek : dispose edilmediğinden hatayı engelliyordu.  == ama doğru olan çalışma şekli AddScope ile devam etmek
        {
            await _productWriteRepository.AddRangeAsync(new()
            {
                new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, CreateDate = DateTime.UtcNow ,Stock=10},
                new() { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, CreateDate = DateTime.UtcNow ,Stock=20},
                new() { Id = Guid.NewGuid(), Name = "Product 3", Price = 300, CreateDate = DateTime.UtcNow ,Stock=130},
            });
            var count = await _productWriteRepository.SaveAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
          Product product = await _productReadRepository.GetByIdAsync(id);
            return Ok(product);
        }
    }
}
