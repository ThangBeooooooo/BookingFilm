﻿using CloudinaryDotNet.Actions;
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
			var maDA = form["MaDA"];

			// Kiểm tra xem ID đã tồn tại hay chưa
			var existingDoAn = _context.DoAns.FirstOrDefault(d => d.MaDA == maDA);
			if (existingDoAn != null)
			{
				ViewBag.AlertMessage = "This MaDA already exists.";
				// Trả về view Create với ModelState chứa thông báo lỗi
				return View("Create");
			}

			// Nếu ID chưa tồn tại, tiếp tục thêm đồ ăn mới
			var doAn = new DoAn
			{
				MaDA = maDA,
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