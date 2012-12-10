using GAListener.Models;
using GAListener.Repository;

namespace GAListener.Tests
{
	using NUnit.Framework;

	[TestFixture]
	public class HitRepositoryTests
	{
		[Test]
		public void TestAdd()
		{
			var repo = new HitRepository();
			repo.Record(new Hit { utmt = "event" });
			Assert.AreEqual(repo.Get(10, 0).Count, 1);
		}

		[Test]
		public void TestClear()
		{
			var repo = new HitRepository();
			repo.Record(new Hit { utmt = "event" });
			repo.Clear();
			Assert.AreEqual(repo.Get(10, 0).Count, 0);
		}
	}
}
