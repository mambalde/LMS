namespace POSDesktopUI.Library.Models
{
    public interface ILoggedInUserModel
    {
        string Email { get; set; }
        string Id { get; set; }
        string UserName { get; set; }
        string Token { get; set; }
        string UserRole { get; set; }
        string UserId { get; set; }

        void LogOffUser();
    }
}