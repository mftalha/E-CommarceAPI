using ETicaretAPI.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase //API yapılarında controllar genel bi kanı olarak çoğul isim alır.
    {
        private readonly IProductService _productService; // ben IProductService çağırdığımda Application katmanından çağırdı bunu yapma sebebi : biz Dependency enjection ile IProductService IOC Container'a atmıştık ve : Persistence katmanını API Katmanından referans almıştık. Persistence katmanıda Application katmanını referans aldığından biz dolaylı yoldan IProductService erişebiliyoruz Application katmanıdaki.

        public ProductsController(IProductService productService) //_productService = ctrl+. yapıyorum ve constractdan referansı(_productService) enjecte ediyoruz. == bu şekilde enjecte oluyor. == bu interface referansına karşı Persistence katmanındaki Concretes sınıfı nesnesi gelecektir. == Service Registration'da ayarlamıştık : IProductService(bu çağrıldığında) , ProductService(bunu gönder) == diye ayarlamıştık.
        {
            _productService = productService;
        }
        [HttpGet] //API deki methodların türünü belirtmezsek swigger = proje çalıştırıldığında patlıyor.
        public IActionResult GetProducts()
        {
             var products = _productService.GetProducts(); //Persistence katmanındaki tüm verileri döndüren methodu çağırıyoruz : tüm verileri apı katmanına çekmiş olduk şuan.
            return Ok(products); //debugda çalıştırken gördümki  producttaki entityler domainden çekiliyor :mimarinin amacıda bu zaten.
        }
    }
}
