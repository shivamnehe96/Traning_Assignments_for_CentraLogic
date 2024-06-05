using SecurityClearanceSystem.DTO;
using SecurityClearanceSystem.Entity;

namespace SecurityClearanceSystem.Interface
{
    public interface IVisitorInterface
    {
        Task<VisitorEntity> VisitorRegister(VisitorEntity visitor);

        Task<VisitorEntity> VisitorById(string visitorId);

        Task<VisitorEntity> UpdateVisitor(VisitorEntity visitor);

    }
}
