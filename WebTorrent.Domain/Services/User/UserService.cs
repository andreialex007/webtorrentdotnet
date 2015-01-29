using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using WebTorrent.Domain.Exceptions;
using WebTorrent.Domain.Extensions;
using WebTorrent.Domain.Services._Common;
using WebTorrent.Domain.Utils;

namespace WebTorrent.Domain.Services.User
{
    public class UserService : ServiceBase, IUserService
    {
        #region Public methods

        public List<UserDto> GetAllUsers()
        {
            using (var session = OpenSession())
            {
                var users = session.Query<UserRecord>()
                    .OrderBy(x => x.Id)
                    .Select(x => new UserDto
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     Email = x.Email,
                                     Role = x.Role,
                                     Password = x.Password
                                 })
                    .ToList();

                return users;
            }
        }

        public void Add(UserDto userDto)
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userRecord = new UserRecord
                                     {
                                         Id = userDto.Id,
                                         Name = userDto.Name,
                                         Email = userDto.Email,
                                         Role = userDto.Role,
                                         Password = userDto.Password
                                     };

                    session.Save(userRecord);
                    transaction.Commit();
                }
            }
        }

        public void Update(UserDto userDto)
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userRecord = new UserRecord
                                     {
                                         Id = userDto.Id,
                                         Name = userDto.Name,
                                         Email = userDto.Email,
                                         Role = userDto.Role,
                                         Password = userDto.Password
                                     };

                    session.Update(userRecord);
                    transaction.Commit();
                }
            }
        }

        public UserDto GetUserById(int id)
        {
            using (var session = OpenSession())
            {
                var userDto = session.Query<UserRecord>()
                    .Where(x => x.Id == id)
                    .OrderBy(x => x.Id)
                    .Select(x => new UserDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Email = x.Email,
                        Role = x.Role,
                        Password = x.Password
                    })
                    .SingleOrDefault();

                return userDto;
            }
        } 

        public List<UserDto> All()
        {
            using (var session = OpenSession())
            {
                var users = session.Query<UserRecord>()
                    .OrderBy(x => x.Id)
                    .Select(x => new UserDto
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     Email = x.Email,
                                     Role = x.Role
                                 })
                                 .ToList();
                return users;
            }
        }

        public UserDto Login(string name, string password)
        {
            using (var session = OpenSession())
            {
                ValidateNameAndPassword(name, password);
                var hash = PasswordHasher.HashPassword(password);
                var user = session
                    .Query<UserRecord>()
                    .SingleOrDefault(x => x.Name.ToLower() == name.ToLower() && x.Password.ToLower() == hash.ToLower());
                if (user == null)
                    throw new ValidationException("Login or password incorrect.");
                return new UserDto
                       {
                           Email = user.Email,
                           Name = user.Name,
                           Id = user.Id,
                           Password = user.Password,
                           Role = user.Role,
                       };
            }
        }

        public UserDto GetByUserName(string userName)
        {
            using (var session = OpenSession())
            {
                var userDto = session.Query<UserRecord>()
                    .Where(x => x.Name.ToLower() == userName)
                    .OrderBy(x => x.Id)
                    .Select(x => new UserDto
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     Email = x.Email,
                                     Role = x.Role,
                                     Password = x.Password
                                 })
                    .SingleOrDefault();

                return userDto;
            }
        }

        public void Delete(int id)
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.DeleteById<UserRecord>(id);
                    transaction.Commit();
                }
            }
        }

        #endregion

        private void ValidateNameAndPassword(string name, string password)
        {
            var user = new UserRecord { Name = name, Password = password };
            var errors = user.GetValidationErrors(x => x.Name, x => x.Password);
            errors.ThrowIfHasErrors();
        }
    }
}
