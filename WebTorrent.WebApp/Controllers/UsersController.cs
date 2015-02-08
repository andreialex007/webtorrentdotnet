using System.Collections.Generic;
using System.Web.Http;
using Common.Utils;
using Microsoft.Ajax.Utilities;
using WebTorrent.Domain.Services.User;
using WebTorrent.WebApp.Controllers.Common;

namespace WebTorrent.WebApp.Controllers
{
    [RoutePrefix("api/users")]
    [CustomAuthorize(Roles = "Administrator")]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UsersController()
        {
            _userService = IoC.Resolve<IUserService>();
        }

        [Route(@"")]
        [HttpGet]
        public List<UserDto> List()
        {
            return _userService.All();
        }

        [Route(@"{id:int}")]
        [HttpGet]
        public UserDto Item(int id)
        {
            return _userService.GetUserById(id);
        }

        [Route(@"update")]
        [HttpPost]
        public void Update(UserDto userDto)
        {
            if (userDto.Id == 0)
            {
                _userService.Add(userDto);
            }
            else
            {
                _userService.Update(userDto);
            }
        }

        [Route(@"{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            _userService.Delete(id);
        }
    }
}