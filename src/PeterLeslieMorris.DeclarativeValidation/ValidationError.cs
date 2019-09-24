using System;

namespace PeterLeslieMorris.DeclarativeValidation
{
	public struct ValidationError
	{
		public object OwningObject { get; }
		public string MemberName { get; }
		public string Message { get; }

		public ValidationError(object owningObject, string memberName, string message)
		{
			OwningObject = owningObject ?? throw new ArgumentNullException(nameof(owningObject));
			MemberName = memberName;
			Message = message;
		}

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;
			if (!(obj is ValidationError))
				return false;

			var other = (ValidationError)obj;
			return 
				other.OwningObject == OwningObject
				&& other.MemberName == MemberName
				&& other.Message == Message;
		}

		public override int GetHashCode() =>
			OwningObject.GetHashCode() * 23 + string.Concat(MemberName, Message).GetHashCode();

		public static bool operator ==(ValidationError left, ValidationError right) =>
			left.Equals(right);

		public static bool operator !=(ValidationError left, ValidationError right) => 
			!(left == right);
	}
}
