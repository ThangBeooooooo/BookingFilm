using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookingFilm.Controllers
{
    public class HomeController : Controller
    {
		private readonly BookingFilmTicketsEntities1 _context;
		public HomeController()
		{
			_context = new BookingFilmTicketsEntities1();
		}
		// GET: Home
		public ActionResult Index()
		{
			return View();
		}
		public ActionResult GetMovies()
		{
			var cards = _context.Phims.Select(c => new
			{
				background = c.HinhPhim,
				display_background = c.HinhPhim,
				title = c.TenPhim,
				description = c.MoTa
			}).ToList();

			return Json(cards, JsonRequestBehavior.AllowGet);
		}
	}
}