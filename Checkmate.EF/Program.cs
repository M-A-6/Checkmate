using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Checkmate.EF
{
    class Program
    {
        static void Main(string[] args)
        {

            EfRepository<Request> requestRepository = new EfRepository<Request>();
            Request req= requestRepository.GetById(50);

            req.BookletSize = 100;
            Request requpdate = requestRepository.Update(req);


            Console.WriteLine("---------" );

            EfRepository<User> userRepository = new EfRepository<User>();
            string hasvaltrue = SecurityUser.CreateHash("P@ss12345");
            string hasvalfalse = SecurityUser.CreateHash("P@ss123456");

            var user= userRepository.GetAll().Where(u => u.Email.ToLower() == "test150@email.com" && u.PasswordHash == hasvaltrue ).ToList();

            var userfalse = userRepository.GetAll().Where(u => u.Email.ToLower() == "test150@email.com" && u.PasswordHash == hasvalfalse).Count();



            Console.WriteLine("---------" + user.FirstOrDefault().Id + "--------" + user.FirstOrDefault().Email +"-------" + user.FirstOrDefault().PasswordHash);

        }
    }

    class SecurityUser
    {
        public static string CreateHash(string sPassword)
        {
            MD5CryptoServiceProvider md5Crypt = new MD5CryptoServiceProvider();
            byte[] passBytes = Encoding.UTF8.GetBytes(sPassword);
            passBytes = md5Crypt.ComputeHash(passBytes);
            StringBuilder strEncryptPass = new StringBuilder();
            foreach (byte passwordByte in passBytes)
            {
                strEncryptPass.Append(passwordByte.ToString("x2").ToLower());
            }
            return strEncryptPass.ToString();
        }
    }
}
