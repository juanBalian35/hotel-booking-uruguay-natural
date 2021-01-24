using System;

namespace Model.Out
{
    public class TokenModel
    {
        public Guid Token { get; set; }

        public TokenModel(Guid token)
        {
            Token = token;
        }
    }
}
