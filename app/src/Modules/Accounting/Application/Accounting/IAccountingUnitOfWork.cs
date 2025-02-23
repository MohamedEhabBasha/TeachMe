using Application;

namespace Accounting.Application.Accounting;

public interface IAccountingUnitOfWork : IUnitOfWork
{
    IUserRepository UserRepository { get; }
}
