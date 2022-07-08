using StudentAPI.Model;

namespace StudentAPI.Data
{
    public class Login
    {
        ConnectionDbContext connectionContext;
        public Login(ConnectionDbContext _connectionContext)
        {
            connectionContext = _connectionContext;
        }

        public bool LoginUser(string email, string passsword)
        {
            SignupProperties signupProperties = new SignupProperties();
            signupProperties = connectionContext.signupDetails.FirstOrDefault(op => op.Email == email);
            if (signupProperties.Password == passsword)
                return true;
            else
                return false;
        }
    }
}
