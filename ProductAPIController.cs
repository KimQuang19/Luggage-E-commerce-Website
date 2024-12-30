using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebVali.Models;
using WebVali.Models.ProductModels;

namespace WebVali.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAPIController : ControllerBase
    {
        QlbanValiContext db=new QlbanValiContext();
        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            var sanPham = (from p in db.TDanhMucSps select new Product
            {
                MaSp = p.MaSp,
                TenSp = p.TenSp,
                MaLoai = p.MaLoai,
                AnhDaiDien = p.AnhDaiDien,
                GiaLonNhat = p.GiaLonNhat
            }).ToList();
            return sanPham;
        }
        [HttpGet("{maloai}")]
        public IEnumerable<Product> GetProductsByCategory(string maloai)
        {
            var sanPham = (from p in db.TDanhMucSps
                           where p.MaLoai == maloai
                           select new Product
                           {
                               MaSp = p.MaSp,
                               TenSp = p.TenSp,
                               MaLoai = p.MaLoai,
                               AnhDaiDien = p.AnhDaiDien,
                               GiaLonNhat = p.GiaLonNhat
                           }).ToList();
            return sanPham;
        }
    }
}
