using SecurityClearanceSystem.Entity;

namespace SecurityClearanceSystem.Interface
{
    public interface IManagerInterface
    {
        Task<ManagerEntity> ManagerRegister(ManagerEntity manager);
    }
}
