using System.IdentityModel.Tokens.Jwt;

namespace Common;

public static class JwtParser
{
    public static int GetUserId(this string token)
    {
        return int.Parse(token.ParserToken("Id"));
    }

    private static string ParserToken(this string token, string role)
    {
        var removeBearer = token.Split(' ')[1];
        var handler = new JwtSecurityTokenHandler();
        var tokenData = handler.ReadJwtToken(removeBearer);
        var s = tokenData.Payload;
        var t = s.Claims.FirstOrDefault(c => c.Type.Split('/').Last() == role).Value;
        return t;
    }
}
