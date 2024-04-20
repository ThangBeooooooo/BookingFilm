using System;
using System.Collections.Generic;
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
	}
}