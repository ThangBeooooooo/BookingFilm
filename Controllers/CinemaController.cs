using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingFilm.Controllers
{
    public class CinemaController : Controller
    {
        // GET: Cinema
        private readonly BookingFilmTicketsEntities1 _context;


        public CinemaController()
        {
            _context = new BookingFilmTicketsEntities1();
        }

        public ActionResult Index()
        {
            var rapChieus = _context.RapChieux.ToList(); // Truy vấn dữ liệu từ database
            return View(rapChieus); // Truyền dữ liệu sang View
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Obsolete]
        public ActionResult Create(FormCollection form)
        {

            try
            {
                string tenRC = form["TenRC"];
                string diaChi = form["DiaChi"];

                if (string.IsNullOrEmpty(tenRC) || string.IsNullOrEmpty(diaChi))
                {
                    ViewBag.ErrorMessage = "Vui lòng nhập đủ thông tin cho tất cả các trường.";
                    return View();
                }

                var rapChieu = new RapChieu
                {
                    TenRC = tenRC,
                    DiaChi = diaChi,
                };

                _context.RapChieux.Add(rapChieu);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi tạo mới. Vui lòng thử lại sau.";
                return View();
            }
            //var rapChieu = new RapChieu
            //{
            //    TenRC = form["TenRC"],
            //    DiaChi = form["DiaChi"],
            //};

            //_context.RapChieux.Add(rapChieu);
            //_context.SaveChanges();
            //return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var rapChieu = _context.RapChieux.Find(id);
            if (rapChieu == null)
            {
                return HttpNotFound();
            }

            _context.RapChieux.Remove(rapChieu);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            RapChieu rapChieu = _context.RapChieux.Find(id);
            //DoAn doAn= _context.DoAns.Where(row => row.MaDA == id).FirstOrDefault();
            return View(rapChieu);

        }
        [HttpPost]
        [Obsolete]
        public ActionResult Edit(RapChieu ra)
        {
            //RapChieu rapChieu = _context.RapChieux.Find(ra.MaRC);
            ////DoAn doAn = _context.DoAns.Where(row => row.MaDA == da.MaDA).FirstOrDefault();

            //rapChieu.TenRC = ra.TenRC;
            //rapChieu.DiaChi = ra.DiaChi;

            //_context.SaveChanges();
            //return RedirectToAction("Index");
            try
            {
                if (string.IsNullOrEmpty(ra.TenRC) || string.IsNullOrEmpty(ra.DiaChi))
                {
                    ViewBag.ErrorMessage = "Vui lòng nhập đủ thông tin cho tất cả các trường.";
                    return View(ra);
                }

                RapChieu rapChieu = _context.RapChieux.Find(ra.MaRC);

                rapChieu.TenRC = ra.TenRC;
                rapChieu.DiaChi = ra.DiaChi;

                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = "Đã xảy ra lỗi khi chỉnh sửa. Vui lòng thử lại sau.";
                return View(ra);
            }

        }
    }
}