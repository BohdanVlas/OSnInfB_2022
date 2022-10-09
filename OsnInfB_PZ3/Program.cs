using System;
using System.Text;
using System.Linq;
using System.Security.Cryptography;
class Program
{
    static byte[] Md5hash(byte[] mes)
    {
        using(var md5 = MD5.Create())
        {
            return md5.ComputeHash(mes);
        }
    }
    static byte[] Sha1hash(byte[] mes)
    {
        using(var sha1 = SHA1.Create())
        {
            return sha1.ComputeHash(mes);
        }
    }
    static byte[] Sha256hash(byte[] mes)
    {
        using(var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(mes);
        }
    }
    static byte[] Sha384hash(byte[] mes)
    {
        using (var sha384 = SHA384.Create())
        {
            return sha384.ComputeHash(mes);
        }
    }
    static byte[] Sha512hash(byte[] mes)
    {
        using (var sha512 = SHA512.Create())
        {
            return sha512.ComputeHash(mes);
        }
    }
    static IEnumerable<string> GetAllPermutations(IEnumerable<char> chars, int n)
    {
        HashSet<char> curr = new HashSet<char>(chars);
        foreach (var s in GetAllPermutationsRec(n, curr))
            yield return s;
    }
    static IEnumerable<string> GetAllPermutationsRec(int n, HashSet<char> curr)
    {
        if (n == 0)
            yield return "";
        foreach (var c in curr.ToList())
        {
            curr.Remove(c);
            foreach (var s in GetAllPermutationsRec(n - 1, curr))
                yield return c + s;
            curr.Add(c);
        }
    }
    static byte[] HmacMD5hash(byte[] mes, byte[] key)
    {
        using (var hmac = new HMACMD5(key))
        {
            return hmac.ComputeHash(mes);
        }
    }
    static byte[] HmacSha1hash(byte[] mes, byte[] key)
    {
        using (var hmac = new HMACSHA1(key))
        {
            return hmac.ComputeHash(mes);
        }
    }
    static byte[] HmacSha256hash(byte[] mes, byte[] key)
    {
        using (var hmac = new HMACSHA256(key))
        {
            return hmac.ComputeHash(mes);
        }
    }
    static byte[] HmacSha512hash(byte[] mes, byte[] key)
    {
        using (var hmac = new HMACSHA512(key))
        {
            return hmac.ComputeHash(mes);
        }
    }
    public static byte[] rndcr()
    {
        using (var rndNumberGenerator = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[32];
            rndNumberGenerator.GetBytes(randomNumber);
            return randomNumber;
        }
    }
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nCalculation of message hash codes - 1\n" +
                              "Password recovery - 2\n" +
                              "Authentication hash code message - 3\n" +
                              "User registration/authorization by login/password - 4\n" +
                              "Exit - 5\n");
            int q = Convert.ToInt32(Console.ReadLine());
            if (q == 1)
            {
                Console.WriteLine("Enter your message:");
                string mess = Console.ReadLine();
                Console.WriteLine("MD5\n" + "Hash: " + Convert.ToBase64String(Md5hash(Encoding.UTF8.GetBytes(mess))));
                Console.WriteLine("SHA 1\n" + "Hash: " + Convert.ToBase64String(Sha1hash(Encoding.UTF8.GetBytes(mess))));
                Console.WriteLine("SHA 256\n" + "Hash: " + Convert.ToBase64String(Sha256hash(Encoding.UTF8.GetBytes(mess))));
                Console.WriteLine("SHA 384\n" + "Hash: " + Convert.ToBase64String(Sha384hash(Encoding.UTF8.GetBytes(mess))));
                Console.WriteLine("SHA 512\n" + "Hash: " + Convert.ToBase64String(Sha512hash(Encoding.UTF8.GetBytes(mess))));
            }
            else if (q == 2)
            {
                string hash = "po1MVkAE7IjUUwu61XxgNg==";
                var chars = new[] { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
                var seq = GetAllPermutations(chars, 8);
                foreach (var s in seq)
                {
                    Console.WriteLine(s);
                    byte[] key = new byte[8];
                    key = Encoding.Unicode.GetBytes(s);
                    if (hash.Equals(Convert.ToBase64String(Md5hash(key))))
                    {
                        Console.WriteLine(s);
                        break;
                    }
                }
            }
            else if (q == 3)
            {
                string read_data = File.ReadAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ3/mess.txt");
                byte[] keyy = rndcr();
                File.WriteAllBytes("C:/Users/Bogdan/source/repos/OsnInfB_PZ3/key.txt", keyy);
                string hashMD5 = Convert.ToBase64String(HmacMD5hash(Encoding.UTF8.GetBytes(read_data), keyy));
                Console.WriteLine("HMAC MD5\n" + "Hash: " + hashMD5);
                File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ3/mess_hmacmd5.txt", hashMD5);
                string hashSha1 = Convert.ToBase64String(HmacSha1hash(Encoding.UTF8.GetBytes(read_data), keyy));
                Console.WriteLine("HMAC SHA 1\n" + "Hash: " + hashSha1);
                File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ3/mess_hmacsha1.txt", hashSha1);
                string hashSha256 = Convert.ToBase64String(HmacSha256hash(Encoding.UTF8.GetBytes(read_data), keyy));
                Console.WriteLine("HMAC SHA 256\n" + "Hash: " + hashSha256);
                File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ3/mess_hmacsha256.txt", hashSha256);
                string hashSha512 = Convert.ToBase64String(HmacSha512hash(Encoding.UTF8.GetBytes(read_data), keyy));
                Console.WriteLine("HMAC SHA 512\n" + "Hash: " + hashSha512);
                File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ3/mess_hmacsha512.txt", hashSha512);
                string hasMD5 = Convert.ToBase64String(Md5hash(Encoding.UTF8.GetBytes(read_data)));
                string hasSha1 = Convert.ToBase64String(Sha1hash(Encoding.UTF8.GetBytes(read_data)));
                string hasSha256 = Convert.ToBase64String(Sha256hash(Encoding.UTF8.GetBytes(read_data)));
                string hasSha512 = Convert.ToBase64String(Sha512hash(Encoding.UTF8.GetBytes(read_data)));
                if (hashMD5.Equals(hasMD5) || hashSha1.Equals(hasSha1) || hashSha256.Equals(hasSha256) || hashSha512.Equals(hasSha512))
                {
                    byte[] keey = rndcr();
                    File.WriteAllBytes("C:/Users/Bogdan/source/repos/OsnInfB_PZ3/key.txt", keey);
                    string hahMD5 = Convert.ToBase64String(HmacMD5hash(Encoding.UTF8.GetBytes(read_data), keey));
                    Console.WriteLine("HMAC MD5\n" + "Hash: " + hahMD5);
                    File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ3/mess_hmacmd5.txt", hahMD5);
                    string hahSha1 = Convert.ToBase64String(HmacSha1hash(Encoding.UTF8.GetBytes(read_data), keey));
                    Console.WriteLine("HMAC SHA 1\n" + "Hash: " + hahSha1);
                    File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ3/mess_hmacsha1.txt", hahSha1);
                    string hahSha256 = Convert.ToBase64String(HmacSha256hash(Encoding.UTF8.GetBytes(read_data), keey));
                    Console.WriteLine("HMAC SHA 256\n" + "Hash: " + hahSha256);
                    File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ3/mess_hmacsha256.txt", hahSha256);
                    string hahSha512 = Convert.ToBase64String(HmacSha512hash(Encoding.UTF8.GetBytes(read_data), keey));
                    Console.WriteLine("HMAC SHA 512\n" + "Hash: " + hahSha512);
                    File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ3/mess_hmacsha512.txt", hahSha512);
                }
            }
            else if (q == 4)
            {
                byte[] key = File.ReadAllBytes("C:/Users/Bogdan/source/repos/OsnInfB_PZ3/kkeeyy.txt");
                Console.WriteLine("Register now - 1\n" +
                                  "Log in - 2\n");
                int t = Convert.ToInt32(Console.ReadLine());
                if (t == 1)
                {
                    Console.WriteLine("Enter your login:");
                    string login = Console.ReadLine();
                    Console.WriteLine("Enter your password:");
                    string pass = Console.ReadLine();
                    string pas = Convert.ToBase64String(HmacSha512hash(Encoding.UTF8.GetBytes(pass), key));
                    string path = "C:/Users/Bogdan/source/repos/OsnInfB_PZ3/" + login + ".txt";
                    File.WriteAllText(path, pas);
                }
                else if (t == 2)
                {
                    Console.WriteLine("Enter your login:");
                    string login = Console.ReadLine();
                    string path = "C:/Users/Bogdan/source/repos/OsnInfB_PZ3/" + login + ".txt";
                    string password = File.ReadAllText(path);
                    Console.WriteLine("Enter your password:");
                    string pass = Console.ReadLine();
                    string pas = Convert.ToBase64String(HmacSha512hash(Encoding.UTF8.GetBytes(pass), key));
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
            else if (q == 5)
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