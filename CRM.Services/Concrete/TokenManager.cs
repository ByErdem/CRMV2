using CRM.Services.Abstract;
using CRM.Entities.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;

namespace CRM.Services.Concrete
{
    public class TokenManager : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly ISessionService _sessionService;
        private readonly IEncryptionService _encryptionService;


        public TokenManager(IConfiguration config, ISessionService sessionService, IEncryptionService encryptionService)
        {
            _config = config;
            _sessionService = sessionService;
            _encryptionService = encryptionService;
        }

        //public string CreateToken(string emailAddress)
        //{
        //	var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
        //	var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
        //	var token = new JwtSecurityToken(
        //		_config["Jwt:Issuer"],
        //		_config["Jwt:Audience"],
        //		new Claim[]
        //		{
        //			new Claim(ClaimTypes.Email, emailAddress),
        //		},
        //		expires: DateTime.Now.AddMinutes(int.Parse(_config["Jwt:Time"])),
        //		signingCredentials: credentials);
        //	return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        public Tuple<string, string> GenerateToken(string username)
        {
            var secretKey = GenerateSecureKey(32);

            // Header
            var header = new { alg = "HS256", typ = "JWT" };
            string headerJson = JsonConvert.SerializeObject(header);
            string headerBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(headerJson));

            // Payload
            var payload = new { name = username };
            string payloadJson = JsonConvert.SerializeObject(payload);
            string payloadBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(payloadJson));

            // Signature
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(headerBase64 + "." + payloadBase64));
            string signatureBase64 = Convert.ToBase64String(signatureBytes);

            // Token
            string jwtToken = headerBase64 + "." + payloadBase64 + "." + signatureBase64;
            return new Tuple<string, string>(jwtToken, secretKey);
        }

        public static string GenerateSecureKey(int length = 32)
        {
            var randomBytes = new byte[length];
            RandomNumberGenerator.Fill(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public bool TokenValidateViaSession(string guidKey)
        {
            var userParameters = JsonConvert.DeserializeObject<UserParameterDto>(_encryptionService.AESDecrypt(_sessionService.GetSessionValue(guidKey)));

            if (userParameters != null)
            {
                return userParameters.GuidKey == guidKey;
            }
            return false;
        }

        public bool ValidateToken(string token, string secretKey)
        {
            var parts = token.Split('.');
            if (parts.Length != 3) // Bir JWT token 3 bölümden oluşmalıdır: header, payload ve signature
            {
                return false;
            }

            string header = parts[0];
            string payload = parts[1];
            string tokenSignature = parts[2];

            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            var computedSignatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(header + "." + payload));
            string computedSignature = Convert.ToBase64String(computedSignatureBytes);

            return computedSignature == tokenSignature; // Eğer hesaplanan imza, token'daki imza ile aynıysa doğrulama başarılıdır
        }
    }
}
