namespace GigaChat.Backend.Application.Auth;

public interface IOtpGenerator
{
    string GenerateCode(int length = 6);
}