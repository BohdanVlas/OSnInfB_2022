using System;
using System.Text;
using System.Security.Cryptography;

class Program
{
    public class RSAWithRSAParameterKey
    {
        private RSAParameters _publicKey;
        private RSAParameters _privateKey;

        public void AssignNewKey()
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                _publicKey = rsa.ExportParameters(false);
                File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ7/key.xml", rsa.ToXmlString(false));
                _privateKey = rsa.ExportParameters(true);
                File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ7/key.xml", rsa.ToXmlString(true));
            }
        }

        const string ContainerName = "MyContainer";

        public void New_Key()
        {
            CspParameters cspParams = new CspParameters(1)
            {
                KeyContainerName = ContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore,
                ProviderName = "Microsoft Strong Cryptographic Provider"
            };
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = true;
                _publicKey = rsa.ExportParameters(false);
                File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ7/Vlasenko.xml", rsa.ToXmlString(false));
            }
        }

        public byte[] EncryptData(byte[] dataToEncrypt)
        {
            byte[] cipherbytes;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ7/key.xml", rsa.ToXmlString(false));
                rsa.ImportParameters(_publicKey);
                cipherbytes = rsa.Encrypt(dataToEncrypt, true);
            }
            return cipherbytes;
        }

        public byte[] DecryptData(byte[] dataToEncrypt)
        {
            byte[] plain;
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.PersistKeyInCsp = false;
                File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ7/key1.xml", rsa.ToXmlString(true));
                rsa.ImportParameters(_privateKey);
                plain = rsa.Decrypt(dataToEncrypt, true);
            }
            return plain;
        }

        public byte[] EncrypttData(byte[] dataToEncrypt)
        {
            byte[] cipherbytes;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ7/key.xml"));
                cipherbytes = rsa.Encrypt(dataToEncrypt, false);
            }
            return cipherbytes;
        }

        public byte[] DecrypttData(byte[] dataToDecrypt)
        {
            byte[] plain;
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.PersistKeyInCsp = false;
                rsa.FromXmlString(File.ReadAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ7/key1.xml"));
                plain = rsa.Decrypt(dataToDecrypt, false);
            }
            return plain;
        }

        public byte[] DecryptttData(byte[] dataToDecrypt)
        {
            byte[] plainBytes;
            var cspParams = new CspParameters
            {
                KeyContainerName = ContainerName,
                Flags = CspProviderFlags.UseMachineKeyStore
            };
            using (var rsa = new RSACryptoServiceProvider(cspParams))
            {
                rsa.PersistKeyInCsp = true;
                plainBytes = rsa.Decrypt(dataToDecrypt, true);
            }
            return plainBytes;
        }
    }



    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nEncryption and Decryption - 1\n" +
                              "Encryption and Decryption with public key from file - 2\n" +
                              "Encryption and Decryption text from or for someone - 3\n" +
                              "Exit - 4\n");
            int q = Convert.ToInt32(Console.ReadLine());
            if (q == 1)
            {
                var rsaParams = new RSAWithRSAParameterKey();
                const string original = "Justtext_forexample";
                rsaParams.AssignNewKey();
                var encrypted = rsaParams.EncryptData(Encoding.UTF8.GetBytes(original));
                var decrypted = rsaParams.DecryptData(encrypted);
                Console.WriteLine("Original Text - " + original);
                Console.WriteLine("Encrypted Text - " + Convert.ToBase64String(encrypted));
                Console.WriteLine("Decrypted Text - " + Encoding.Default.GetString(decrypted));
            }
            else if (q == 2)
            {
                var rsaParams = new RSAWithRSAParameterKey();
                const string original = "Justtext_forexample";
                var encrypted = rsaParams.EncrypttData(Encoding.UTF8.GetBytes(original));
                var decrypted = rsaParams.DecrypttData(encrypted);
                Console.WriteLine("Original Text - " + original);
                Console.WriteLine("Encrypted Text - " + Convert.ToBase64String(encrypted));
                Console.WriteLine("Decrypted Text - " + Encoding.Default.GetString(decrypted));
            }
            else if (q == 3)
            {
                Console.WriteLine("\nKey - 1\n" +
                                 "Encryption - 2\n" +
                                 "Decryption - 3\n");
                int r = Convert.ToInt32(Console.ReadLine());
                var rsaParams = new RSAWithRSAParameterKey();
                if (r == 1)
                {
                    rsaParams.New_Key();
                    Console.WriteLine("Ok");
                }
                else if (r == 2)
                {
                    Console.WriteLine("Name of file\n");
                    string name = Console.ReadLine();
                    string path = "C:/Users/Bogdan/source/repos/OsnInfB_PZ7/" + name + ".xml";
                    Console.WriteLine("Name\n");
                    string namee = Console.ReadLine();
                    string pathh = "C:/Users/Bogdan/source/repos/OsnInfB_PZ7/To " + namee + " from Vlasenko.txt";
                    const string original = "What do you think about the background photo on the introductory page of the site?";
                    var encrypted = rsaParams.EncrypttData(Encoding.UTF8.GetBytes(original));
                    Console.WriteLine("Original Text - " + original);
                    Console.WriteLine("Decrypted Text - " + Encoding.Default.GetString(encrypted));
                    File.WriteAllBytes(pathh, encrypted);
                }
                else if (r == 3)
                {
                    Console.WriteLine("Name of file\n");
                    string name = Console.ReadLine();
                    string path = "C:/Users/Bogdan/source/repos/OsnInfB_PZ7/" + name + ".txt";
                    byte[] original = File.ReadAllBytes(path);
                    var decrypted = rsaParams.DecryptttData(original);
                    Console.WriteLine("Original Text - " + original);
                    Console.WriteLine("Decrypted Text - " + Encoding.Default.GetString(decrypted));
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
        };
    }
}