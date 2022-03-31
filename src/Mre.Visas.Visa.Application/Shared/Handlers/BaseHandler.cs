using Mre.Visas.Visa.Application.Shared.Interfaces;

namespace Mre.Visas.Visa.Application.Shared.Handlers
{
    public abstract class BaseHandler
    {
        protected BaseHandler()
        {
        }

        protected BaseHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected IUnitOfWork UnitOfWork;
    }
}