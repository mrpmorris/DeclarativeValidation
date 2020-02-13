namespace PeterLeslieMorris.DeclarativeValidation
{
	public class MemberIdentifier
	{
		public object Owner { get; set; }
		public string MemberName { get; set; }

		public MemberIdentifier() { }

		public MemberIdentifier(object owner, string memberName)
		{
			Owner = owner;
			MemberName = memberName;
		}
	}
}
