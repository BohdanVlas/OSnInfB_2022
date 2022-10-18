using System;
using System.Text;
using System.Linq;
using System.Diagnostics;
using System.Security.Cryptography;

class SaltedHash
{
    public static byte[] SaltForHash()
    {
        using (var rndNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[32];
            rndNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    private static byte[] Combine(byte[] first, byte[] second)
    {
        var ret = new byte[first.Length + second.Length];
        Buffer.BlockCopy(first, 0, ret, 0, first.Length);
        Buffer.BlockCopy(second, 0, ret, first.Length,
        second.Length);
        return ret;
    }
    public static byte[] HashPassWithSalt(byte[] password, byte[] salt)
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Combine(password, salt));
        }
    }
}
class PBKDF2
{
    public static byte[] SaltForHash()
    {
        using (var rndNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[32];
            rndNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    public static byte[] HashPassword(byte[] toBeHashed, byte[] salt, int numberOfRounds)
    {
        using (var rfc2898 = new Rfc2898DeriveBytes(toBeHashed, salt, numberOfRounds))
        {
            return rfc2898.GetBytes(20);
        }
    }
}

class Program
{
    static byte[] hashpassword(string passwordToHash, byte[] salt, int numberOfRounds)
    {
        var hashedPassword = PBKDF2.HashPassword(Encoding.UTF8.GetBytes(passwordToHash), salt, numberOfRounds);
        return hashedPassword;
    }
    private static void HashPassword(string passwordToHash, int numberOfRounds)
{
    var sw = new Stopwatch();
    sw.Start();
    var hashedPassword = PBKDF2.HashPassword(
    Encoding.UTF8.GetBytes(passwordToHash), PBKDF2.SaltForHash(), numberOfRounds);
    sw.Stop();
    Console.WriteLine();
    Console.WriteLine("Password to hash : " + passwordToHash);
    Console.WriteLine("Hashed Password : " +
    Convert.ToBase64String(hashedPassword));
    Console.WriteLine("Iterations <" + numberOfRounds + ">Elapsed Time: " + sw.ElapsedMilliseconds + "ms");
}
public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nPassword hashing with salt- 1\n" +
                              "Hashing a password with the addition of salt a certain number of iterations- 2\n" +
                              "User registration/authorization by login/password- 3\n" +
                              "Exit - 4\n");
            int q = Convert.ToInt32(Console.ReadLine());
            if (q == 1)
            {
                Console.WriteLine("Enter your password: ");
                string password = Console.ReadLine();
                byte[] salt = SaltedHash.SaltForHash();
                byte[] pass = Encoding.UTF8.GetBytes(password);
                string hashpass = Convert.ToBase64String(SaltedHash.HashPassWithSalt(pass, salt));
                Console.WriteLine(hashpass);
            }
            else if (q == 2)
            {
                Console.WriteLine("Enter your password: ");
                string password = Console.ReadLine();
                HashPassword(password, 60000);
                HashPassword(password, 110000);
                HashPassword(password, 160000);
                HashPassword(password, 210000);
                HashPassword(password, 260000);
                HashPassword(password, 310000);
                HashPassword(password, 3600000);
                HashPassword(password, 4100000);
                HashPassword(password, 4600000);
                HashPassword(password, 5100000);
            }
            else if (q == 3)
            {
                byte[] key = File.ReadAllBytes("C:/Users/Bogdan/source/repos/OsnInfB_PZ5/kkeeyy.txt");
                Console.WriteLine("Register now - 1\n" +
                                  "Log in - 2\n");
                int t = Convert.ToInt32(Console.ReadLine());
                if (t == 1)
                {
                    Console.WriteLine("Enter your login:");
                    string login = Console.ReadLine();
                    Console.WriteLine("Enter your password:");
                    string pass = Console.ReadLine();
                    string pas = Convert.ToBase64String(hashpassword(pass, key, 60000));
                    string path = "C:/Users/Bogdan/source/repos/OsnInfB_PZ5/" + login + ".txt";
                    File.WriteAllText(path, pas);
                }
                else if (t == 2)
                {
                    Console.WriteLine("Enter your login:");
                    string login = Console.ReadLine();
                    string path = "C:/Users/Bogdan/source/repos/OsnInfB_PZ5/" + login + ".txt";
                    string password = File.ReadAllText(path);
                    Console.WriteLine("Enter your password:");
                    string pass = Console.ReadLine();
                    string pas = Convert.ToBase64String(hashpassword(pass, key, 60000));
                    if (password.Equals(pas))
                    {
                        Console.WriteLine("Congratulations");
                    }
                    else
                    {
                        Console.WriteLine("Try again");
                    }
                }
                else
                {
                    continue;
                }
            }
            else if (q == 4)
            {
                break;
            }
            else
            {
                continue;
            }
        }
    }
}