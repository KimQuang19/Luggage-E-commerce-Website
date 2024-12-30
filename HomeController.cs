using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebVali.Models;
using WebVali.Models.Authenticaion;
using X.PagedList;

namespace WebVali.Controllers
{
    public class HomeController : Controller
    {
        QlbanValiContext db= new QlbanValiContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        //[Authentication]

        public IActionResult Index(int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page <0?1:page.Value;
            var lstsanpham = db.TDanhMucSps.AsNoTracking().OrderBy(x=>x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp> (lstsanpham, pageNumber, pageSize);
            return View(lst);
        }
        public IActionResult SanPhamTheoLoai(String maloai, int? page)
        {
            int pageSize = 8;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstsanpham = db.TDanhMucSps.AsNoTracking().Where(x=>x.MaLoai==maloai).OrderBy(x => x.TenSp);
            PagedList<TDanhMucSp> lst = new PagedList<TDanhMucSp>(lstsanpham, pageNumber, pageSize);
            ViewBag.maloai = maloai;
            return View(lst);
        }
        public IActionResult ChiTietSanPham(string maSp)
        {
            if (string.IsNullOrEmpty(maSp))
            {
                return RedirectToAction("Error");
            }

            var sanPham = db.TDanhMucSps.SingleOrDefault(x => x.MaSp == maSp);
            if (sanPham == null)
            {
                return RedirectToAction("Error");
            }

            var anhSanPham = db.TAnhSps.Where(x => x.MaSp == maSp).ToList();
            ViewBag.anhSanPham = anhSanPham;

            return View(sanPham);

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
