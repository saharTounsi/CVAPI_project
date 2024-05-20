using CVAPI.Models;

namespace CVAPI.Interfaces {
    public interface ITokenService {
        string createToken(User user);
    }
}
