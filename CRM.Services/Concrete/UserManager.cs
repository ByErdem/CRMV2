using AutoMapper;
using CRM.Services.Abstract;
using CRM.Data.Abstract;
using CRM.Entities.Concrete;
using CRM.Entities.Dtos;
using CRM.Shared.Utilities.ComplexTypes;
using Newtonsoft.Json;

namespace CRM.Services.Concrete
{
	public class UserManager : IUserService
	{
		private readonly IUnitOfWork _uow;
		private readonly IMapper _mapper;
		private readonly IEncryptionService _encryptionService;
		private readonly ITokenService _tokenService;
		private readonly ISessionService _sessionService;
		private readonly IRoleService _roleService;

		public UserManager(IUnitOfWork uow, IMapper mapper, ITokenService tokenService, IEncryptionService encryptionService, ISessionService sessionService, IRoleService roleService)
		{
			_mapper = mapper;
			_encryptionService = encryptionService;
			_uow = uow;
			_tokenService = tokenService;
			_sessionService = sessionService;
			_roleService = roleService;
		}

		public async Task<ResponseDto> ResetPassword(ResetPasswordDto dto)
		{
			var rsp = new ResponseDto();
			var usr = await _uow.Users.GetAsync(x => x.Email == dto.Email);
			if (usr != null)
			{
				//Mail gönderme kodları burada yazılacak.
				//..
				//..
				//..
				//..

				rsp.ResultStatus = ResultStatus.Success;
				rsp.SuccessMessage = "Mail adresinize şifre değişikliği için e-posta gönderildi.";
				return rsp;
			}

			rsp.ResultStatus = ResultStatus.Error;
			rsp.ErrorMessage = "Sistemde böyle bir mail adresi bulunamadı.";
			return rsp;
		}

		public UserParameterDto? GetUserParameters(string guidKey)
		{
			var sessionValue = _sessionService.GetSessionValue(guidKey);
			if (sessionValue != "" && sessionValue != null)
			{
				var decryptedValue = _encryptionService.AESDecrypt(sessionValue);
				var userParameter = JsonConvert.DeserializeObject<UserParameterDto>(decryptedValue);

				return userParameter; // userParameter zaten null olabilir, bu yüzden ekstra kontrol gerekmez.
			}
			return null;
		}

		public async Task<ResponseDto<string>> Register(UserRegisterDto userDto)
		{
			var rsp = new ResponseDto<string>();
			var usr = await _uow.Users.GetAsync(x => x.Email == userDto.Email);
			if (usr != null)
			{
				rsp.ResultStatus = ResultStatus.Error;
				rsp.ErrorMessage = "Böyle bir kullanıcı bulunmaktadır, lütfen başka bir kullanıcı adı deneyiniz.";
				return rsp;
			}

			var newUser = new User
			{
				PasswordHash = _encryptionService.AESEncrypt(userDto.Password + userDto.SaltPassword),
				SaltPassword = userDto.SaltPassword,
				AccessFailedCount = 0,
				Email = userDto.Email,
				EmailConfirmed = true,
				LockoutEnabled = false,
				PhoneNumber = userDto.PhoneNumber,
				PhoneNumberConfirmed = false,
				TwoFactorEnabled = false,
				NormalizedEmail = userDto.Email,
			};

			int i = 0;

			try
			{
				await _uow.Users.AddAsync(newUser);
				i = await _uow.SaveAsync();
			}
			catch (Exception ex)
			{

			}


			if (i > 0)
			{
				await _uow.UserDetails.AddAsync(new UserDetail
				{
					UserId = newUser.Id,
					Name = userDto.Name,
					Surname = userDto.Surname,
					TCKN = userDto.TCKN,
				});

				await _uow.Accounts.AddAsync(new Account
				{
					UserId = newUser.Id,
					Phone = newUser.PhoneNumber
				});
			}

			try
			{
				await _uow.SaveAsync();
			}
			catch (Exception ex)
			{

			}


			var guidKey = Guid.NewGuid().ToString();
			var tokenParameters = _tokenService.GenerateToken(newUser.Email);
			var sessionDto = new UserParameterDto
			{
				UserId = newUser.Id,
				GuidKey = guidKey,
				Password = newUser.PasswordHash,
				Token = tokenParameters.Item1,
				SecretKey = tokenParameters.Item2,
				EMail = newUser.Email
			};

			/*
				Gerekli tüm değerlerimizi Session'a hem guidKey olarak hem de UserParameters adı altında kaydediyoruz.
				Örneğin bir istek gönderirken eğer elimizde token parametresi varsa bunu elimizdeki guidkey ile doğrulayabiliriz.
				Eğer menülere tıklayarak istek gönderiyorsak bu durumda token parametresi göndermemiz mümkün olmayacak. 
				Bu durumda sadece UserParameters'ı Session'daki değerlerden okuyarak AuthorizeFilter'ı true olarak geçirmeyi sağlayabiliriz.
			 */

			try
			{
				var encryptedValue = _encryptionService.AESEncrypt(JsonConvert.SerializeObject(sessionDto));
				_sessionService.SetSessionValue(guidKey, encryptedValue);
				_sessionService.SetSessionValue("UserParameters", encryptedValue);
			}
			catch (Exception ex)
			{

			}



			rsp.ResultStatus = ResultStatus.Success;
			rsp.Data = guidKey;
			rsp.SuccessMessage = "Kullanıcı oluşturuldu";

			return rsp;
		}

		public async Task<ResponseDto<string>> SignIn(UserLoginDto userDto)
		{
			var rsp = new ResponseDto<string>();

			try
			{
				var usr = await _uow.Users.GetAsync(x => x.Email == userDto.Email);
				if (usr != null)
				{
					var encrypted = _encryptionService.AESEncrypt(userDto.Password + usr.SaltPassword);
					var usrpass = await _uow.Users.GetAsync(x => x.Email == userDto.Email && x.PasswordHash == encrypted);
					if (usrpass != null)
					{
						var tokenParameters = _tokenService.GenerateToken(userDto.Email);
						var guidKey = Guid.NewGuid().ToString();
						var sessionDto = new UserParameterDto
						{
							UserId = usrpass.Id,
							Token = tokenParameters.Item1,
							SecretKey = tokenParameters.Item2,
							GuidKey = guidKey,
							EMail = userDto.Email,
							Password = userDto.Password
						};

						var encryptedValue = _encryptionService.AESEncrypt(JsonConvert.SerializeObject(sessionDto));
						_sessionService.SetSessionValue("UserParameters", encryptedValue);

						rsp.ResultStatus = ResultStatus.Success;
						rsp.Data = guidKey;
						rsp.SuccessMessage = "Giriş başarılı";
					}
				}
				else
				{
					rsp.ResultStatus = ResultStatus.Error;
					rsp.ErrorMessage = "Kullanıcı adı veya şifre yanlış!";
				}
			}
			catch (Exception ex)
			{

			}

			return rsp;
		}

		public async Task<ResponseDto<List<User>>> GetAllUsersByNonDeleted()
		{
			var rsp = new ResponseDto<List<User>>();
			var users = await _uow.Users.GetAllAsync(x => x.IsDeleted == false);
			if (users.Count > 0)
			{
				rsp.ResultStatus = ResultStatus.Success;
				rsp.Data = users.ToList();
			}
			else
			{
				rsp.ResultStatus = ResultStatus.Error;
				rsp.ErrorMessage = "Sistemde kayıtlı kullanıcı bulunamadı";
			}

			return rsp;
		}

		public async Task<ResponseDto<bool>> DeleteUserById(UserDto userDto)
		{
			var rsp = new ResponseDto<bool>();

			var userParameters = GetUserParameters("UserParameters");
			var user = await _uow.Users.GetAsync(x => x.Id == userDto.Id);
			if (user != null)
			{
				if (userParameters.UserId == user.Id)
				{
					rsp.ErrorMessage = "Login olduğunuz kullanıcıyı silemezsiniz";
					rsp.ResultStatus = ResultStatus.Error;
					rsp.Data = false;
				}
				else
				{
					user.IsDeleted = true;
					await _uow.Users.UpdateAsync(user);
					await _uow.SaveAsync();

					rsp.ResultStatus = ResultStatus.Success;
					rsp.SuccessMessage = "Kullanıcı başarılı bir şekilde silinmiştir.";
					rsp.Data = true;
				}
			}

			return rsp;
		}

		public async Task<ResponseDto<User>> AddUser(UserDto userDto)
		{
			var rsp = new ResponseDto<User>();
			var user = await _uow.Users.GetAsync(x => x.Email == userDto.Email);
			if (user == null)
			{
				string saltPassword = Guid.NewGuid().ToString();

				var newUser = new User
				{
					Email = userDto.Email,
					SaltPassword = saltPassword,
					PasswordHash = _encryptionService.AESEncrypt(userDto.PasswordHash + saltPassword),
					PhoneNumber = userDto.PhoneNumber,
					LockoutEnabled = false,
					EmailConfirmed = true,
					AccessFailedCount = 0,
					PhoneNumberConfirmed = false,
					TwoFactorEnabled = false,
					NormalizedEmail = userDto.Email,
				};

				await _uow.Users.AddAsync(newUser);
				await _uow.SaveAsync();

				var newUserDetail = new UserDetail
				{
					UserId = newUser.Id,
					Name = userDto.Name,
					Surname = userDto.Surname,
					DateOfBirth = userDto.DateOfBirth,
					TCKN = userDto.TCKN				
				};

				var newAccount = new Account
				{
					UserId = newUser.Id,
				};

				var roleDto = new RoleDto
				{
					UserId = newUser.Id,
					RoleName = userDto.RoleName
				};

				await _roleService.AssignRoleToUser(roleDto);
				await _uow.UserDetails.AddAsync(newUserDetail);
				await _uow.Accounts.AddAsync(newAccount);
				await _uow.SaveAsync();

				rsp.ResultStatus = ResultStatus.Success;
				rsp.Data = newUser;
				rsp.SuccessMessage = "Yeni kullanıcı başarıyla oluşturuldu";
			}
			else
			{
				rsp.ResultStatus = ResultStatus.Error;
				rsp.Data = null;
				rsp.SuccessMessage = "Sistemde bu mail adresine ait kullanıcı tanımlı.";
			}

			return rsp;

		}
	}
}
