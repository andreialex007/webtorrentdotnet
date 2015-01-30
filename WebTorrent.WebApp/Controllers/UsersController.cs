﻿using System.Collections.Generic;
using System.Web.Http;
using Common.Utils;
using WebTorrent.Domain.Services.User;
using WebTorrent.WebApp.Controllers.Common;

namespace WebTorrent.WebApp.Controllers
{
    [RoutePrefix("api/users")]
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