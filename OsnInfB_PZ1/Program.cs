using System;
using System.Security.Cryptography;

class Program
{
	static void rand(int a, int n)//Функція генерації послідовності псевдовипадкових чисел
	{
		Random rndm = new Random(n);//Початкове значення вводить користувач
		for (int i = 0; i < a; i++)//цикл виведення послідовності з а псевдовипадкових чисел
		{
			Console.WriteLine(rndm.Next());
		}
	}
	static void rand2(int a, int b, int c, int n)//Функція генерації послідовності псевдовипадкових чисел з границями введеними користувачем
	{
		Random rndm = new Random(n);
		for (int i = 0; i < c; i++)//цикл виведення послідовності з а псевдовипадкових чисел
		{
			Console.WriteLine(rndm.Next(a, b));
		}
	}

	public static byte[] randcr(int length)//Функція генерації криптографічно стійкого числа
	{
		using (var rndNumberGenerator = new RNGCryptoServiceProvider())
		{
			var randomNumber = new byte[length];
			rndNumberGenerator.GetBytes(randomNumber);
			return randomNumber;
		}
	}

	public static void Main()
	{
		while (true)
		{
			Console.WriteLine("Generation of a sequence of pseudorandom numbers - 1\n" +//Меню
							  "Generation of a sequence of pseudo-random numbers with variable limits - 2\n" +
							  "Generation of a cryptographically stable sequence of random numbers - 3\n" +
							  "Exit - 4\nChoose: ");
			int A = Convert.ToInt32(Console.ReadLine());
			if (A == 1)
			{
				Console.WriteLine("Enter the number of numbers in the sequence: ");
				int B = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("Enter seed: ");
				int E = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("");
				Program.rand(B, E);
				Console.WriteLine("");
			}
			else if (A == 2)
			{
				Console.WriteLine("Enter the lower limit: ");
				int C = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("Enter the upper limit: ");
				int B = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("Enter seed: ");
				int E = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("Enter the number of numbers in the sequence: ");
				int D = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("");
				Program.rand2(C, B, D, E);
				Console.WriteLine("");
			}
			else if (A == 3)
			{
				Console.WriteLine("Enter length: ");
				int e = Convert.ToInt32(Console.ReadLine());
				Console.WriteLine("Enter the number of numbers in the sequence: ");
				int j = Convert.ToInt32(Console.ReadLine());
				for (int i = 0; i < j; i++)
				{
					int rnd = BitConverter.ToInt32(randcr(e));
					Console.WriteLine("");
					Console.WriteLine(rnd);
				}
				Console.WriteLine("");
			}
			else if (A == 4)//Вихід
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