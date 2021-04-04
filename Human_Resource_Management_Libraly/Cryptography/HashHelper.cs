using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Human_Resource_Management_Libraly.Cryptography
{
    public class HashHelper
    {
        public void CreatePasswordHash_Single()
        {
            int iterations = 100000; // The number of times to encrypt the password - change this
            int saltByteSize = 64; // the salt size - change this
            int hashByteSize = 512; // the final hash - change this

          

            var password = "password"; // That's really secure! :)

            byte[] saltBytes = PBKDF2Provider.CreateSalt(saltByteSize);
            string saltString = Convert.ToBase64String(saltBytes);

            string pwdHash = PBKDF2Provider.PBKDF2_Keccak_GetHash(password, saltString, iterations, hashByteSize);

            var isValid = PBKDF2Provider.ValidatePassword(password, saltBytes, iterations, 
                hashByteSize, Convert.FromBase64String(pwdHash));

        }
    }
}
