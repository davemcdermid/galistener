﻿namespace GAListener.Repository
{
	using System.Linq.Dynamic;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using Models;

	public interface IHitRepository
	{
		void Record(Hit hit);
		List<Hit> Get(string expression, int take, int offset);
		List<Hit> Get(int take, int offset);
		void Clear();
	}

	public class HitRepository : IHitRepository
	{
		private ConcurrentBag<Hit> _hits = new ConcurrentBag<Hit>();

		public void Record(Hit hit)
		{
			_hits.Add(hit);
		}

		public List<Hit> Get(string expression, int take, int offset)
		{
			return _hits
				.AsQueryable()
				.Where(expression)
				.OrderByDescending(h => h.Recorded)
				.Skip(offset)
				.Take(take)
				.ToList();
		}

		public List<Hit> Get(int take, int offset)
		{
			return _hits
				.OrderByDescending(h => h.Recorded)
				.Skip(offset)
				.Take(take)
				.ToList();
		}

		public void Clear()
		{
			_hits = new ConcurrentBag<Hit>();
		}
	}
}