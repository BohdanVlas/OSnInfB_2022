using System;
using System.Text;
using System.Security.Cryptography;

class Program
{
    static byte[] GenerateRandomNumber(int len)
    {
        using (var rndmnumbgen = new RNGCryptoServiceProvider())
        {
            var randomNumber = new byte[len];
            rndmnumbgen.GetBytes(randomNumber);
            return randomNumber;
        }
    }

    static byte[] EncryptAES(byte[] data, byte[] key, byte[] iv)
    {
        using (var aes = new AesCryptoServiceProvider())
        {
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, aes.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }

    static byte[] DecryptAES(byte[] data, byte[] key, byte[] iv)
    {
        using (var aes = new AesCryptoServiceProvider())
        {
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, aes.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }

    static byte[] EncryptDES(byte[] data, byte[] key, byte[] iv)
    {
        using (var des = new DESCryptoServiceProvider())
        {
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            des.Key = key;
            des.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, des.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }

    static byte[] DecryptDES(byte[] data, byte[] key, byte[] iv)
    {
        using (var des = new DESCryptoServiceProvider())
        {
            des.Mode = CipherMode.CBC;
            des.Padding = PaddingMode.PKCS7;
            des.Key = key;
            des.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }

    static byte[] EncryptTDES(byte[] data, byte[] key, byte[] iv)
    {
        using (var tdes = new TripleDESCryptoServiceProvider())
        {
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.PKCS7;
            tdes.Key = key;
            tdes.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, tdes.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }

    static byte[] DecryptTDES(byte[] data, byte[] key, byte[] iv)
    {
        using (var tdes = new TripleDESCryptoServiceProvider())
        {
            tdes.Mode = CipherMode.CBC;
            tdes.Padding = PaddingMode.PKCS7;
            tdes.Key = key;
            tdes.IV = iv;
            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, tdes.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(data, 0, data.Length);
                //.FlushFinalBlock();
                return memoryStream.ToArray();
            }
        }
    }



    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nAES, DES, TDES- 1\n" +
                              "AES, DES, TDES with hash- 2\n" +
                              "Exit - 3\n");
            int q = Convert.ToInt32(Console.ReadLine());
            if (q == 1)
            {
                byte[] key = GenerateRandomNumber(32);
                var iv = GenerateRandomNumber(16);
                const string original = "TEXT for example: text";
                var encryptedAES = EncryptAES(Encoding.UTF8.GetBytes(original), key, iv);
                var decryptedAES = DecryptAES(encryptedAES, key, iv);
                var decryptedMes = Encoding.UTF8.GetString(decryptedAES);
                Console.WriteLine("AES\n");
                Console.WriteLine("Original Text - " + original);
                Console.WriteLine("Encrypted Text - " + Convert.ToBase64String(encryptedAES));
                Console.WriteLine("Decrypted Text - " + decryptedMes);
                byte[] keyy = GenerateRandomNumber(8);
                var ivv = GenerateRandomNumber(8);
                var encryptedDES = EncryptDES(Encoding.UTF8.GetBytes(original), keyy, ivv);
                var decryptedDES = DecryptDES(encryptedDES, keyy, ivv);
                var decryptedMess = Encoding.UTF8.GetString(decryptedDES);
                Console.WriteLine("DES\n");
                Console.WriteLine("Original Text - " + original);
                Console.WriteLine("Encrypted Text - " + Convert.ToBase64String(encryptedDES));
                Console.WriteLine("Decrypted Text - " + decryptedMess);
                byte[] keeyy = GenerateRandomNumber(16);
                var encryptedTDES = EncryptTDES(Encoding.UTF8.GetBytes(original), keeyy, ivv);
                var decryptedTDES = DecryptTDES(encryptedTDES, keeyy, ivv);
                var decryptedMesss = Encoding.UTF8.GetString(decryptedTDES);
                Console.WriteLine("Triple-DES\n");
                Console.WriteLine("Original Text - " + original);
                Console.WriteLine("Encrypted Text - " + Convert.ToBase64String(encryptedTDES));
                Console.WriteLine("Decrypted Text - " + decryptedMesss);
            }
            else if (q == 2)
            {
                Console.WriteLine("Enter your password: ");
                string pwd = Console.ReadLine();
                byte[] salt = new byte[8];
                using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                {
                    rngCsp.GetBytes(salt);
                }
                Rfc2898DeriveBytes keyy = new Rfc2898DeriveBytes(pwd, salt, 60000);
                Rfc2898DeriveBytes ivv = new Rfc2898DeriveBytes(pwd, salt, 60000);
                byte[] key = keyy.GetBytes(32);
                byte[] iv = ivv.GetBytes(16);
                const string original = "TEXT for example: text";
                var encryptedAES = EncryptAES(Encoding.UTF8.GetBytes(original), key, iv);
                var decryptedAES = DecryptAES(encryptedAES, key, iv);
                var decryptedMes = Encoding.UTF8.GetString(decryptedAES);
                Console.WriteLine("AES\n");
                Console.WriteLine("Original Text - " + original);
                Console.WriteLine("Encrypted Text - " + Convert.ToBase64String(encryptedAES));
                Console.WriteLine("Decrypted Text - " + decryptedMes);
                byte[] keeyy = keyy.GetBytes(8);
                byte[] iiv = ivv.GetBytes(8);
                var encryptedDES = EncryptDES(Encoding.UTF8.GetBytes(original), keeyy, iiv);
                var decryptedDES = DecryptDES(encryptedDES, keeyy, iiv);
                var decryptedMess = Encoding.UTF8.GetString(decryptedDES);
                Console.WriteLine("DES\n");
                Console.WriteLine("Original Text - " + original);
                Console.WriteLine("Encrypted Text - " + Convert.ToBase64String(encryptedDES));
                Console.WriteLine("Decrypted Text - " + decryptedMess);
                byte[] kkeeyy = keyy.GetBytes(16);
                var encryptedTDES = EncryptTDES(Encoding.UTF8.GetBytes(original), kkeeyy, iiv);
                var decryptedTDES = DecryptTDES(encryptedTDES, kkeeyy, iiv);
                var decryptedMesss = Encoding.UTF8.GetString(decryptedTDES);
                Console.WriteLine("Triple-DES\n");
                Console.WriteLine("Original Text - " + original);
                Console.WriteLine("Encrypted Text - " + Convert.ToBase64String(encryptedTDES));
                Console.WriteLine("Decrypted Text - " + decryptedMesss);
            }
            else if (q == 3)
            {
                break;
            }
            else
            {
                Console.WriteLine("Try again");
                continue;
            }
        }
    }
}