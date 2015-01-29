using System.Collections.Generic;

namespace WebTorrent.Domain.Services.User
{
    public interface IUserService
    {
        UserDto Login(string name, string password);
        UserDto GetByUserName(string userName);
        void Add(UserDto userDto);
        void Update(UserDto userDto);
        void Delete(int id);
        List<UserDto> All();
        UserDto GetUserById(int id);
    }
}