using CRM.Data.Abstract;
using CRM.Data.Concrete.EntityFramework.Contexts;
using CRM.Data.Concrete.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CRMContext _context;
        private EfUserRepository _userRepository;
        private EfUserDetailRepository _userDetailRepository;
        private EfAccountRepository _accountRepository;
        private EfRoleRepository _roleRepository;
        private EfUserRoleRepository _userRoleRepository;
        private EfMenuItemRepository _menuItemRepository;
        private EfRoleMenuItemRepository _roleMenuItemRepository;

        public UnitOfWork(CRMContext context)
        {
            _context = context;
        }

        public IUserRepository Users => _userRepository ?? new EfUserRepository(_context);

        public IUserDetailRepository UserDetails => _userDetailRepository ?? new EfUserDetailRepository(_context);

        public IAccountRepository Accounts => _accountRepository ?? new EfAccountRepository(_context);

        public IRoleRepository Roles => _roleRepository ?? new EfRoleRepository(_context);

        public IUserRoleRepository UserRoles => _userRoleRepository ?? new EfUserRoleRepository(_context);

        public IMenuItemRepository MenuItems => _menuItemRepository ?? new EfMenuItemRepository(_context);

        public IRoleMenuItemRepository RoleMenuItems => _roleMenuItemRepository ?? new EfRoleMenuItemRepository(_context);


        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
