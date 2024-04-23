using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingFilm.Controllers
{
    public class LoginController : Controller
    {
		// GET: Login

		private readonly BookingFilmTicketsEntities1 _context;

		public LoginController()
		{
			_context = new BookingFilmTicketsEntities1();
		}

		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Index(string Email, string MatKhauKH)
		{
			var user = _context.KhachHangs.FirstOrDefault(u => u.Email == Email && u.MatKhauKH == MatKhauKH);

			if (user != null)
			{
				Session["User"] = user; // Lưu user vào Session
				TempData["Message"] = "Logged in successfully!";
				return RedirectToAction("Index", "Home");
			}
			else
			{
				TempData["Message"] = "User account or password incorrect!";
				return RedirectToAction("Index");
			}
		}

	}
}