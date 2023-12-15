namespace CRM.Data.Abstract
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        IUserDetailRepository UserDetails { get; }
        IAccountRepository Accounts { get; }
        IRoleRepository Roles { get; }
        IMenuItemRepository MenuItems { get; }
        IRoleMenuItemRepository RoleMenuItems { get; }
        IUserRoleRepository UserRoles { get; }
        Task<int> SaveAsync();
    }
}
