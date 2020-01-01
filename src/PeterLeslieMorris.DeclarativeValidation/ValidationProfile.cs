﻿using System;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public class ValidationProfile
	{
		public void ForClass<TClass>(Action<IClassRuleBuilder<TClass>> validation)
			where TClass : class
		=> validation(new ClassRuleBuilder<TClass>());
	}
}
