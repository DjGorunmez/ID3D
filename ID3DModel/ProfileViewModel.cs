public class ProfileViewModel
{
    public PersonalInfoSection PersonalInfo { get; set; }
    public ContactInfoSection ContactInfo { get; set; }
    public PreferencesSection Preferences { get; set; }
}

public class PersonalInfoSection
{
    public string Name { get; set; }
	public int? MaxDevices { get; set; }
}

public class ContactInfoSection
{
    public string Email { get; set; }
	public string Package { get; set; }
}

public class PreferencesSection
{
    public string FavoriteColor { get; set; }
    public bool ReceiveNewsletter { get; set; }
}