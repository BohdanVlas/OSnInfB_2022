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
    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("");
            int q = Convert.ToInt32(Console.ReadLine());
            if (q == 1)
            {
                Console.WriteLine("");
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
                    key = Encoding.UTF8.GetBytes(s);
                    if (hash.Equals(Convert.ToBase64String(Md5hash(key))))
                    {
                        Console.WriteLine(s);
                        break;
                    }
                }
            }
            else if (q == 3)
            {
                Console.WriteLine("Under development");
                continue;
            }
            else if (q == 4)
            {
                Console.WriteLine("Under development");
                continue;
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