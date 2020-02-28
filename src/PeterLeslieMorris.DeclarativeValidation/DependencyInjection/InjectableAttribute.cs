using System;
using Microsoft.Extensions.DependencyInjection;

namespace PeterLeslieMorris.DeclarativeValidation.DependencyInjection
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class InjectableAttribute : Attribute
	{
		public readonly InjectableLifetime Lifetime;

		public InjectableAttribute(InjectableLifetime lifetime)
		{
			Lifetime = lifetime;
		}

		public void Register(IServiceCollection services, Type classType)
		{
			switch (Lifetime)
			{
				case InjectableLifetime.Transient:
					services.AddTransient(classType);
					break;
				case InjectableLifetime.Scoped:
					services.AddScoped(classType);
					break;
				case InjectableLifetime.Singleton:
					services.AddSingleton(classType);
					break;
				default:
					throw new NotImplementedException(Lifetime.ToString());
			}
		}
	}

	public enum InjectableLifetime
	{
		Transient,
		Scoped,
		Singleton
	}
}
