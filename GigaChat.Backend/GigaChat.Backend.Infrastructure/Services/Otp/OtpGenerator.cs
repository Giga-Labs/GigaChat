using System.Security.Cryptography;
using GigaChat.Backend.Application.Auth;

namespace GigaChat.Backend.Infrastructure.Services.Otp;

public class OtpGenerator : IOtpGenerator
{
    private static readonly RandomNumberGenerator _rng = RandomNumberGenerator.Create();

    public string GenerateCode(int length = 6)
    {
        if (length <= 0) throw new ArgumentOutOfRangeException(nameof(length));

        var digits = new char[length];
        var buffer = new byte[length];

        _rng.GetBytes(buffer);

        for (int i = 0; i < length; i++)
            digits[i] = (char)('0' + (buffer[i] % 10));

        return new string(digits);
    }
}