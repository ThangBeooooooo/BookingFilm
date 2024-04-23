using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingFilm.Controllers
{
    public class MovieController : Controller
    {
		// GET: Movie
		private readonly BookingFilmTicketsEntities1 _context;

		public MovieController()
		{
			_context = new BookingFilmTicketsEntities1();
		}

		public ActionResult Index()
		{
			var khachHangs = _context.Phims.ToList(); // Truy vấn dữ liệu từ database
			return View(khachHangs); // Truyền dữ liệu sang View
		}

		public ActionResult Create()
		{
			return View();
		}

		[Obsolete]
		public string UrlImageAfterUpload(HttpPostedFileBase HinhAnh)
		{
			var account = new Account(
						"dzamheemx",  // Cloud name
						"279156174534789",  // API key
						"KFQQWMyliAcvwK7vYvX__qEYstM"   // API secret
					);

			var cloudinary = new Cloudinary(account);

			var uploadParams = new ImageUploadParams()
			{
				File = new FileDescription(Path.GetFileName(HinhAnh.FileName), HinhAnh.InputStream),
				UploadPreset = "ml_default"  // Upload preset name
			};

			var uploadResult = cloudinary.Upload(uploadParams);
			return uploadResult.SecureUri.AbsoluteUri;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create([Bind(Include = "MaPhim,TenPhim,TheLoai,ThoiLuong,DaoDien,NamSanXuat,HinhPhim,MoTa")] BookingFilm.Phim phim, HttpPostedFileBase HinhP)
		{
			if (ModelState.IsValid)
			{
				phim.HinhPhim = UrlImageAfterUpload(HinhP);
				_context.Phims.Add(phim);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(phim);
		}


		public ActionResult Delete(int id)
		{
			var phim = _context.Phims.Find(id);
			if (phim == null)
			{
				return HttpNotFound();
			}

			_context.Phims.Remove(phim);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}