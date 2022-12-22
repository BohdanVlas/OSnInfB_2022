using System;
using System.Text;
using System.Security.Cryptography;

class Program
{
    public class RSAWithRSAParameterKey
    {
        private readonly static string CspContainerName = "RsaContainer";

        public static byte[] CreateSignature(byte[] dataToSign)
        {
            var cspParams = new CspParameters
            {
                KeyContainerName = CspContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };

            using (var rsa = new RSACryptoServiceProvider(2048, cspParams))
            {
                rsa.PersistKeyInCsp = true;

                var rsaFormatter = new RSAPKCS1SignatureFormatter(rsa);

                rsaFormatter.SetHashAlgorithm(nameof(SHA512));

                byte[] hashOfData;
                using (var sha512 = SHA512.Create())
                {
                    hashOfData = sha512.ComputeHash(dataToSign);
                }
                return rsaFormatter.CreateSignature(hashOfData);
            }
        }

        public static bool VerifySignature(string publicKeyPath, byte[] data, byte[] signature)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText(publicKeyPath));
                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm(nameof(SHA512));

                byte[] hashOfData;
                using (var sha512 = SHA512.Create())
                {
                    hashOfData = sha512.ComputeHash(data);
                }

                return rsaDeformatter.VerifySignature(hashOfData, signature);
            }
        }
    }

    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nCreate Signature - 1\n" +
                             "Verify Signature - 2\n" +
                             "Exit - 3\n");
            int q = Convert.ToInt32(Console.ReadLine());
            if (q == 1)
            {
                Console.WriteLine("Enter the name of the file with data: ");
                string path = Console.ReadLine();
                byte[] data = File.ReadAllBytes("C:/Users/Bogdan/source/repos/OsnInfB_PZ9/" + path + ".txt");
                byte[] sign = RSAWithRSAParameterKey.CreateSignature(data);
                File.WriteAllBytes("C:/Users/Bogdan/source/repos/OsnInfB_PZ9/" + path + "_sign.txt", sign);
            }
            else if (q == 2)
            {
                Console.WriteLine("Enter the name of the file with data: ");
                string path = Console.ReadLine();
                byte[] data = File.ReadAllBytes("C:/Users/Bogdan/source/repos/OsnInfB_PZ9/" + path + ".txt");
                Console.WriteLine("Enter the name of the file with key: ");
                string name = Console.ReadLine();
                string keypath = "C:/Users/Bogdan/source/repos/OsnInfB_PZ9/"+ name +".xml";
                Console.WriteLine("Enter the name of the file with signature: ");
                string nam = Console.ReadLine();
                byte[] sign = File.ReadAllBytes("C:/Users/Bogdan/source/repos/OsnInfB_PZ9/" + nam + ".txt");
                if (RSAWithRSAParameterKey.VerifySignature(keypath, data, sign))
                {
                    Console.WriteLine("True");
                }
                else
                {
                    Console.WriteLine("False");
                }
            }
            else if (q == 3)
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