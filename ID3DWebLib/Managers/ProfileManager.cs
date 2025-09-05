namespace ID3DWebLib.Managers
{
	public class ProfileManager
	{
		public Profile Login(string username, string password)
		{ 
			using(MyDbContext db = new MyDbContext())
			{
				var profile = db.Profiles
					.Where(p => p.Username == username && p.Password == password && p.Enabled)
					.FirstOrDefault();
				
				if(profile != null)
				{
					profile.LoginCount = (profile.LoginCount ?? 0) + 1;
					profile.LastLoginDate = DateTime.UtcNow;
					db.SaveChanges();
				}

				if (profile.IdNwo.HasValue)
				{
					profile.Nwo = this.GetNwo(profile.IdNwo.Value);
				}

				if (profile.IdPackage.HasValue)
				{
					profile.Package = this.GetPackage(profile.IdPackage.Value);
				}

				if(profile.IdWallet.HasValue)
				{
					profile.Wallet = this.GetWallet(profile.IdWallet.Value);
				}

				return profile;
			}
		}

		public void Logout(int profileId)
		{
			using(MyDbContext db = new MyDbContext())
			{
				var profile = db.Profiles.Find(profileId);
				if(profile != null)
				{
					profile.LastLoginDate = DateTime.UtcNow;
					db.SaveChanges();
				}
			}
		}

		public Profile GetProfile(int profileId)
		{
			using(MyDbContext db = new MyDbContext())
			{
				return db.Profiles.Find(profileId);
			}
		}

		public Package GetPackage(int packageId)
		{
			using(MyDbContext db = new MyDbContext())
			{
				return db.Packages.Find(packageId);
			}
		}

		public Nwo GetNwo(int nwoId)
		{
			using(MyDbContext db = new MyDbContext())
			{
				Nwo result = db.Nwoes.Find(nwoId);

				result.Status = db.Status.Find(result.IdStatus);
				result.School = db.Schools.Find(result.IdSchool);
				result.Project = db.Projects.Find(result.IdProject);
				result.House = db.Houses.Find(result.IdHome);

				result.House.Location = db.Locations.Find(result.House.IdLocation);

				//result.Company = db.Companies.FirstOrDefault(i => i.IdProject == result.IdProject);
				
				return result;
			}
		}

		private Wallet GetWallet(int walletId)
		{
			using (MyDbContext db = new MyDbContext())
			{
				return db.Wallets.Find(walletId);
			}
		}

		public void UpdateProfile(Profile profile)
		{
			using(MyDbContext db = new MyDbContext())
			{
				db.Profiles.Update(profile);
				db.SaveChanges();
			}
		}

		public Profile CreateProfile(Profile profile)
		{
			using(MyDbContext db = new MyDbContext())
			{
				db.Profiles.Add(profile);
				db.SaveChanges();
			}

			return profile;
		}

		public void ChangePassword(int profileId, string newPassword)
		{
			using(MyDbContext db = new MyDbContext())
			{
				var profile = db.Profiles.Find(profileId);
				if(profile != null)
				{
					profile.Password = newPassword;
					db.SaveChanges();
				}
			}
		}
	}
}
