using System.Collections.Generic;
using System.Web.Http;
using WebTorrent.Domain.Services.User;
using WebTorrent.WebApp.Controllers.Common;

namespace WebTorrent.WebApp.Controllers
{
    [RoutePrefix("api/users")]
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [Route(@"")]
        [HttpGet]
        public List<UserDto> List()
        {
            return _userService.All();
        }

        [Route(@"{id:int}")]
        [HttpGet]
        public UserDto List(int id)
        {
            return _userService.GetUserById(id);
        }

        [Route(@"add")]
        [HttpPost]
        public void Add()
        {

        }

        [Route(@"update")]
        [HttpPost]
        public void Update()
        {

        }

        [Route(@"{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            _userService.Delete(id);
        }
    }
}