﻿using Org.BouncyCastle.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Human_Resource_Management_Libraly.Cryptography
{
    public static class HashHelper
    {
       
            private const int SaltByteSize = 32;
            private const int HashByteSize = 64; // to match the size of the PBKDF2-HMAC-SHA-512 hash 
            private const int Pbkdf2Iterations = 1000000; //3 giây
            private const int IterationIndex = 0;
            private const int SaltIndex = 1;
            private const int Pbkdf2Index = 2;

            private static readonly SecureRandom _cryptoRandom = new SecureRandom();

            public static string HashPassword(string password)
            {            
                byte[] salt = new byte[SaltByteSize];
                _cryptoRandom.NextBytes(salt);    
                
                var hash = GetPbkdf2Bytes(password, salt, Pbkdf2Iterations, HashByteSize);
                return Pbkdf2Iterations + ":" +
                       Convert.ToBase64String(salt) + ":" +
                       Convert.ToBase64String(hash);
            }

            public static bool ValidatePassword(string password, string correctHash)
            {
                char[] delimiter = { ':' };
                var split = correctHash.Split(delimiter);
                var iterations = Int32.Parse(split[IterationIndex]);
                var salt = Convert.FromBase64String(split[SaltIndex]);
                var hash = Convert.FromBase64String(split[Pbkdf2Index]);

                var testHash = GetPbkdf2Bytes(password, salt, iterations, hash.Length);
                return SlowEquals(hash, testHash);
            }

            private static bool SlowEquals(byte[] a, byte[] b)
            {
                var diff = (uint)a.Length ^ (uint)b.Length;
                for (int i = 0; i < a.Length && i < b.Length; i++)
                {
                    diff |= (uint)(a[i] ^ b[i]);
                }
                return diff == 0;
            }

            private static byte[] GetPbkdf2Bytes(string password, byte[] salt, int iterations, int outputBytes)
            {
                Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(
                    password:password, 
                    salt:salt,
                    iterations: iterations,
                    HashAlgorithmName.SHA512);

                pbkdf2.IterationCount = iterations;           
                
                return pbkdf2.GetBytes(outputBytes);
            }
        
    }
}
