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
	public class FoodController : Controller
	{
		private readonly BookingFilmTicketsEntities1 _context;


		public FoodController()
		{
			_context = new BookingFilmTicketsEntities1();
		}

		public ActionResult Index()
		{
			var doAns = _context.DoAns.ToList(); // Truy vấn dữ liệu từ database
			return View(doAns); // Truyền dữ liệu sang View
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
		[Obsolete]
		public ActionResult Create(FormCollection form, HttpPostedFileBase HinhDA)
		{
			var doAn = new DoAn
			{
				MaDA = form["MaDA"],
				TenDA = form["TenDA"],
				GiaDA = decimal.Parse(form["GiaDA"]),
				HinhDA = UrlImageAfterUpload(HinhDA)
			};

			_context.DoAns.Add(doAn);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
		// GET: Food/Delete/BAPPHOMAI
		public ActionResult Delete(string id)
		{
			var doAn = _context.DoAns.Find(id);
			if (doAn == null)
			{
				return HttpNotFound();
			}

			_context.DoAns.Remove(doAn);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

        public ActionResult Edit(string id)
		{
            DoAn doAn = _context.DoAns.Find(id);
            //DoAn doAn= _context.DoAns.Where(row => row.MaDA == id).FirstOrDefault();
            return View(doAn);

        }
        [HttpPost]
        [Obsolete]
        public ActionResult Edit(DoAn da, HttpPostedFileBase HinhDA)
        {
            DoAn doAn = _context.DoAns.Find(da.MaDA);
            //DoAn doAn = _context.DoAns.Where(row => row.MaDA == da.MaDA).FirstOrDefault();

            doAn.TenDA = da.TenDA;
			doAn.GiaDA = da.GiaDA;

			doAn.HinhDA = UrlImageAfterUpload(HinhDA);

            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        //// GET: Food/Delete/BAPPHOMAI
        //public ActionResult Delete(string id)
        //{
        //	var doAn = _context.DoAns.Find(id);
        //	if (doAn == null)
        //	{
        //		return HttpNotFound();
        //	}
        //	return View(doAn);
        //}

        //// POST: Food/Delete/BAPPHOMAI
        //[HttpPost]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //	var doAn = _context.DoAns.Find(id);
        //	_context.DoAns.Remove(doAn);
        //	_context.SaveChanges();
        //	return RedirectToAction("Index");
        //}
    }
}