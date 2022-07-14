using System;
using System.Collections.Generic;
using System.Text;

namespace Training_platfomt
{
    public static class Hasher
    {

        public static string GetHash(this string password)
        {
            int mod = 10000000;
            int p = 10;
            string result = "";
            for (int i = 0; i< password.Length; ++i)
            {
                result += password[i] * p % mod;
                p *= p;
            }

            return result;
        }
    }
}
