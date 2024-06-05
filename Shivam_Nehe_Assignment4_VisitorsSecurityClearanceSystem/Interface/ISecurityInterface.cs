using SecurityClearanceSystem.DTO;
using SecurityClearanceSystem.Entity;

namespace SecurityClearanceSystem.Interface
{
    public interface ISecurityInterface
    {
        Task<SecurityEntity> SecurityRegister(SecurityEntity security);

        List<VisitorDTO> GetPendingVisitorRequests();
    }
}
