using step_buy_server.models.Personal;

namespace step_buy_server.DTO;

public class UserDTO
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserType Role { get; set; } = UserType.User;
}