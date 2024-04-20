using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BookingFilm.Controllers
{
    public class AdminAddFoodController : Controller
    {
		public ActionResult Index()
		{
			return View();
		}
		[HttpPost] // Đánh dấu action này chỉ xử lý yêu cầu POST
		public ActionResult AddFood(DoAn doAn)
		{
			// Kiểm tra xem dữ liệu đã được nhập đầy đủ và hợp lệ không
			if (ModelState.IsValid)
			{
				// Thực hiện lưu dữ liệu vào cơ sở dữ liệu, ở đây là một ví dụ đơn giản
				// Code lưu vào cơ sở dữ liệu ở đây
				// Ví dụ:
				// DbContext.Add(doAn);
				// DbContext.SaveChanges();

				// Sau khi lưu thành công, bạn có thể chuyển hướng người dùng đến một trang thông báo thành công
				return RedirectToAction("Success");
			}
			else
			{
				// Nếu dữ liệu không hợp lệ, bạn có thể hiển thị lại form với thông báo lỗi
				return View("Index", doAn);
			}
		}

	}
}