using SecurityClearanceSystem.Entity;

namespace SecurityClearanceSystem.Interface
{
    public interface IOfficeInterface
    {
        Task<OfficeEntity> OfficeRegister(OfficeEntity office);
    }

}
