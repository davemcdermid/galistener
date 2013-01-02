namespace GAListener.Controllers
{
	using System;
	using System.Configuration;
	using System.Web.Mvc;
	using Models;
	using Repository;

    public class UtmController : Controller
    {
	    private readonly IHitRepository _hitRepository;
		private readonly int Max;
	    private readonly byte[] tinyImage = Convert.FromBase64String(@"R0lGODlhAQABAID/AP///wAAACwAAAAAAQABAAACAkQBADs=");

	    public UtmController(IHitRepository hitRepository)
	    {
		    _hitRepository = hitRepository;
		    Max = int.Parse(ConfigurationManager.AppSettings["MaxResults"]);
		}

		public ActionResult RecordHit(Hit hit)
		{
			hit.ClientIP = Request.UserHostAddress;
			_hitRepository.Record(hit);
			return File(tinyImage, "image/gif");
		}

		public ActionResult GetHits(Hit hit, int? o, string s)
		{
			ViewBag.Search = s;
			ViewBag.Offset = o ?? 0;
			ViewBag.Max = Max;
			if (!string.IsNullOrEmpty(s)) {
				try {
					return View(_hitRepository.Get(s, Max, o ?? 0));
				}
				catch (Exception e) {
					ModelState.AddModelError("s", e.Message);
				}
			}
			return View(_hitRepository.Get(Max, o ?? 0));
		}

		[HttpPost]
		public ActionResult Clear()
		{
			_hitRepository.Clear();
			return RedirectToAction("gethits");
		}
    }
}
