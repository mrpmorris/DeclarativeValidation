﻿using System.Collections.Generic;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class CompositeRule : Rule
	{
		private readonly List<Rule> Rules = new List<Rule>();

		public void AddRule(Rule rule)
		{
			Rules.Add(rule);
		}

		public override string ToJson() => "";
	}
}
