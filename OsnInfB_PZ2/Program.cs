using System;
using System.IO;
using System.Text;
using System.Linq;

class Program
{
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
        var chars = new[] { 'Q', 'W', 'E', 'R', 'T', 'Y', 'U', 'I', 'O', 'P', 'A', 'S', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'Z', 'X', 'C', 'V', 'B', 'N', 'M', 'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z', 'x', 'c', 'v', 'b', 'n', 'm', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '-', '_', '=', '+', '[', ']', '{', '}', ';', ':', '<', '>', ',', '.', '/', '?', '|', '`', '~', 'ё', 'Ё', '№', '*' };
        while (true)
        {
            Console.WriteLine("Encrypting text from a file - 1\n" +
                              "Decrypting text from a file - 2\n" +
                              "Decrypting a file by the brute force method - 3\n" +
                              "Exit - 4\nChoose: ");
            int A = Convert.ToInt32(Console.ReadLine());
            if (A == 1)
            {
                string read_data = File.ReadAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ2/test.txt");
                byte[] readdata = Encoding.UTF8.GetBytes(read_data);
                byte[] encdata = new byte[readdata.Length];
                byte[] key = Encoding.UTF8.GetBytes("Password");
                for (int i = 0; i < readdata.Length; i++)
                {
                    encdata[i] = (byte)(readdata[i] ^ key[i]);
                };
                File.WriteAllBytes("C:/Users/Bogdan/source/repos/OsnInfB_PZ2/test_1.dat", encdata);
            }
            else if (A == 2)
            {
                byte[] readdata = File.ReadAllBytes("C:/Users/Bogdan/source/repos/OsnInfB_PZ2/test_1.dat");
                byte[] decdata = new byte[readdata.Length];
                byte[] key = Encoding.UTF8.GetBytes("Password");
                for (int i = 0; i < readdata.Length; i++)
                {
                    decdata[i] = (byte)(readdata[i] ^ key[i]);
                };
                string dec_data = Encoding.UTF8.GetString(decdata);
                File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ2/test_2.txt", dec_data);
            }
            else if (A == 3)
            {
                byte[] readdata = File.ReadAllBytes("C:/Users/Bogdan/source/repos/OsnInfB_PZ2/encfile5.dat");
                var seq = GetAllPermutations(chars, 5);
                foreach (var s in seq)
                {
                    byte[] key = new byte[5];
                    key = Encoding.UTF8.GetBytes(s);
                    Console.WriteLine(s);
                    byte[] decdata = new byte[readdata.Length];
                    int k = 0;
                    for (int i = 0; i < readdata.Length; i++)
                    {
                        decdata[i] = (byte)(readdata[i] ^ key[k]);
                        if (k == 4)
                        {
                            k = 0;
                        }
                        k++;
                    };
                    string dec_data = Encoding.UTF8.GetString(decdata);
                    if (dec_data.Contains("Mit21"))
                    {
                        Console.WriteLine(dec_data);
                        Console.WriteLine("If the text is legible and correct, enter 1, if not - any other number\n");
                        int B = Convert.ToInt32(Console.ReadLine());
                        if (B == 1)
                        {
                            File.WriteAllText("C:/Users/Bogdan/source/repos/OsnInfB_PZ2/decfile5.txt", dec_data);
                            File.WriteAllBytes("C:/Users/Bogdan/source/repos/OsnInfB_PZ2/key.txt", key);
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            else if (A == 4)
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