using SecurityClearanceSystem.Entity;

namespace SecurityClearanceSystem.Interface
{
    public interface IUserInterface
    {
        Task<UserEntity> UserRegister(UserEntity user);

        Task<string> LoginUser(UserEntity user);

    }
}
