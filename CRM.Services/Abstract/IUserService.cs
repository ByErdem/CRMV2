using CRM.Entities.Concrete;
using CRM.Entities.Dtos;

namespace CRM.Services.Abstract
{
    public interface IUserService
    {
        Task<ResponseDto<string>> SignIn(UserLoginDto userDto);
        Task<ResponseDto<string>> Register(UserRegisterDto user);
        UserParameterDto? GetUserParameters(string guidKey);
        Task<ResponseDto> ResetPassword(ResetPasswordDto dto);
        Task<ResponseDto<List<User>>> GetAllUsersByNonDeleted();
        Task<ResponseDto<bool>> DeleteUserById(UserDto userDto);
        Task<ResponseDto<User>> AddUser(UserDto userDto);

	}
}
