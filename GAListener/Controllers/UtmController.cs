using System.Configuration;
using System.Linq.Dynamic;

namespace GAListener.Controllers
{
	using System.Web.Mvc;
	using GAListener.Models;
	using GAListener.Repository;

    public class UtmController : Controller
    {
	    private readonly HitRepository _hitRepository;
		private readonly int Max;

	    public UtmController()
		{
			_hitRepository = new HitRepository();
		    Max = int.Parse(ConfigurationManager.AppSettings["MaxResults"]);
		}

		public ActionResult RecordHit(Hit hit)
		{
			hit.ClientIP = Request.UserHostAddress;
			_hitRepository.Record(hit);
			return new EmptyResult();
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
				catch (ParseException e) {
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
