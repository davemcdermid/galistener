namespace GAListener.Models
{
	using System;

	public class Hit
	{
		public Hit()
		{
			Recorded = DateTime.UtcNow;
		}

		public DateTime Recorded { get; set; }
		public string ClientIP { get; set; }

		public string utmwv { get; set; }
		public int utms { get; set; }
		public long utmn { get; set; }
		public string utmhn { get; set; }
		public string utmt { get; set; }
		public string utme { get; set; }
		public string utmcs { get; set; }
		public string utmsr { get; set; }
		public string utmvp { get; set; }
		public string utmsc { get; set; }
		public string utmul { get; set; }
		public int utmje { get; set; }
		public string utmfl { get; set; }
		public string utmdt { get; set; }
		public long utmhid { get; set; }
		public string utmr { get; set; }
		public string utmp { get; set; }
		public string utmac { get; set; }
		public string utmcc { get; set; }
		public string utmu { get; set; }
	}
}