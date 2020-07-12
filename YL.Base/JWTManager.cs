using YL.Base.dtos;
using Jose;
using System;
using System.Collections.Generic;
using System.Text;

namespace YL.Base
{
    public class JWTManager
    {
        public static string Key => "JWTManager";

        public static string Encode(APIUserInfo user)
        {
            return Encode<APIUserInfo>(user);
        }

        public static APIUserInfo Decode(string token)
        {
            try
            {
                return Decode<APIUserInfo>(token);
            }
            catch
            {
                return null;
            }
        }

        public static string Encode<T>(T t)
        {
            return JWT.Encode(t, Key, JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A256CBC_HS512);
        }
        public static T Decode<T>(string token)
        {
            var u2 = JWT.Decode<T>(token, Key, JweAlgorithm.PBES2_HS256_A128KW, JweEncryption.A256CBC_HS512);
            return u2;
        }
    }
}
