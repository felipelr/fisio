
using System.ComponentModel.DataAnnotations.Schema;

namespace fisio.domain.Entities
{
    public class RefreshToken : Base
    {
        public RefreshToken() { }

        public RefreshToken(string tokenKey, string refreshToken)
        {
            TokenKey = tokenKey;
            Token = refreshToken;
        }

        [Column("token_key")]
        public string TokenKey { get; private set; }

        [Column("refresh_token")]
        public string Token { get; private set; }
    }
}
