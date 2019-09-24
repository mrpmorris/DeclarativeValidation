using System;
using Microsoft.Extensions.DependencyInjection;

namespace PeterLeslieMorris.DeclarativeValidation.DependencyInjection
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class InjectableAttribute : Attribute
	{
		public readonly InjectableScope Scope;

		public InjectableAttribute(InjectableScope scope)
		{
			Scope = scope;
		}

		public void Register(IServiceCollection services, Type classType)
		{
			switch (Scope)
			{
				case InjectableScope.Transient:
					services.AddTransient(classType);
					break;
				case InjectableScope.Scoped:
					services.AddScoped(classType);
					break;
				case InjectableScope.Singleton:
					services.AddSingleton(classType);
					break;
				default:
					throw new NotImplementedException(Scope.ToString());
			}
		}
	}

	public enum InjectableScope
	{
		Transient,
		Scoped,
		Singleton
	}
}
