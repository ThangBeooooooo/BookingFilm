using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BookingFilm.Controllers
{
    public class AdminAddFoodController : Controller
    {
		private readonly BookingFilmTicketsEntities1 _context;
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]

		public ActionResult Create(FormCollection form, HttpPostedFileBase HinhDA)
		{
			using (var context = new BookingFilmTicketsEntities1())
			{
				var doAn = new DoAn
				{
					MaDA = form["MaDA"],
					TenDA = form["TenDA"],
					GiaDA = decimal.Parse(form["GiaDA"]),
				};

				var fileName = Path.GetFileName(HinhDA.FileName);
				var path = Path.Combine(Server.MapPath("~/Images/"), fileName);

				// Create the directory if it doesn't exist
				var directory = Path.GetDirectoryName(path);
				if (!Directory.Exists(directory))
				{
					Directory.CreateDirectory(directory);
				}

				HinhDA.SaveAs(path);
				doAn.HinhDA = fileName;

				context.DoAns.Add(doAn);
				context.SaveChanges();

				return RedirectToAction("Index");
			}
		}
	}
}