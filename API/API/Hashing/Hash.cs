using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Hashing
{
    public class Hash
    {
        public string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public string HashingPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());
        }
        public bool ValidatePassword(string password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, correctHash);
        }
    }
}
