namespace ID3DWebLib.Managers
{
	public class Managers
	{
		public Managers()
		{
			using(MyDbContext context = new MyDbContext())
			{
				var managers = context.Profiles.FirstOrDefault(i => i.Username == "Osman");
			}
		}
	}
}
