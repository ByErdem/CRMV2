using CRM.Data.Abstract;
using CRM.Entities.Concrete;
using CRM.Entities.Dtos;
using CRM.Services.Abstract;
using CRM.Shared.Utilities.ComplexTypes;
using System.Text;

namespace CRM.Services.Concrete
{
    public class HomeManager : IHomeService
    {
        private readonly IUserService _userService;
        private readonly IMenuService _menuService;
        private readonly IUnitOfWork _uow;

        public HomeManager(IUserService userService, IMenuService menuService, IUnitOfWork uow)
        {
            _userService = userService;
            _menuService = menuService;
            _uow = uow;
        }

        public async Task<ResponseDto<HomeDto>> GetDataForHomePage()
        {
            var rsp = new ResponseDto<HomeDto>();
            var parameters = _userService.GetUserParameters("UserParameters");
            var user = await _uow.Users.GetAsync(x => x.Id == parameters.UserId);
            var menuItems = await _menuService.GetMenusForUser(user.Id);

            rsp.ResultStatus = ResultStatus.Success;
            rsp.Data = new HomeDto
            {
                User = user,
                MenuItems = BuildMenuHtml(menuItems.Data)
            };

            return rsp;
        }

        public string BuildMenuHtml(List<MenuItem> menuItems)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<div class=\"sidebar\" id=\"sidebar\">");
            sb.AppendLine("<div class=\"slimScrollDiv\" style=\"position: relative; overflow: hidden; width: 100%; height: 895px;\">");
            sb.AppendLine("<div class=\"sidebar-inner slimscroll\" style=\"overflow: hidden; width: 100%; height: 895px;\">");
            sb.AppendLine("<div id=\"sidebar-menu\" class=\"sidebar-menu\">");
            sb.AppendLine("<ul>");

            foreach (var menu in menuItems.Where(m => m.ParentId == null))
            {
                sb.Append("<li");

                if (menu.Children.Any())
                {
                    sb.Append(" class=\"submenu\"");
                }

                sb.AppendLine(">");
                // Eğer alt menü varsa, 'href' özniteliğini 'javascript:void(0);' yap
                sb.AppendLine($"<a href=\"{(menu.Children.Any() ? "javascript:void(0);" : menu.Link)}\"><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"feather feather-box\"><path d=\"M21 16V8a2 2 0 0 0-1-1.73l-7-4a2 2 0 0 0-2 0l-7 4A2 2 0 0 0 3 8v8a2 2 0 0 0 1 1.73l7 4a2 2 0 0 0 2 0l7-4A2 2 0 0 0 21 16z\"></path><polyline points=\"3.27 6.96 12 12.01 20.73 6.96\"></polyline><line x1=\"12\" y1=\"22.08\" x2=\"12\" y2=\"12\"></line></svg><span>{menu.Title}</span>");

                if (menu.Children.Any())
                {
                    sb.AppendLine("<span class=\"menu-arrow\"></span>");
                }

                sb.AppendLine("</a>");

                if (menu.Children.Any())
                {
                    sb.AppendLine("<ul>");
                    foreach (var subMenu in menu.Children)
                    {
                        sb.AppendLine($"<li><a href=\"{subMenu.Link}\">{subMenu.Title}</a></li>");
                    }
                    sb.AppendLine("</ul>");
                }

                sb.AppendLine("</li>");
            }

            sb.AppendLine("</ul>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            sb.AppendLine("<div class=\"slimScrollBar\" style=\"background: rgb(204, 204, 204); width: 7px; position: absolute; top: 0px; opacity: 0.4; display: none; border-radius: 7px; z-index: 99; right: 1px; height: 325.355px;\"></div>");
            sb.AppendLine("<div class=\"slimScrollRail\" style=\"width: 7px; height: 100%; position: absolute; top: 0px; display: none; border-radius: 7px; background: rgb(51, 51, 51); opacity: 0.2; z-index: 90; right: 1px;\"></div>");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");

            return sb.ToString();
        }
    }
}
