using System;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.Security.Principal;

class User
{
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public string[] Roles { get; set; }
}

class Protector
{
    private static Dictionary<string, User> _users = new Dictionary<string, User>();

    public static User Register(string userName, string password, string[] roles = null)
    {
        if (_users.ContainsKey(userName))
        {
            Console.WriteLine("Цей логін вже використовується");
            return null;
        }
        else
        {
            byte[] salt = SaltForHash();
            User user = new User();
            user.Login = userName;
            user.Salt = Convert.ToBase64String(salt);
            user.Roles = roles;
            byte[] pass = HashPassword(Encoding.UTF8.GetBytes(password), salt, 30);
            user.PasswordHash = Convert.ToBase64String(pass);
            _users.Add(user.Login, user);
            return user;
        }
    }

    public static bool CheckPassword(string userName, string password)
    {
        if (_users.ContainsKey(userName))
        {
            byte[] salt = Encoding.UTF8.GetBytes(_users[userName].Salt);
            byte[] pass = HashPassword(Encoding.UTF8.GetBytes(password), salt, 30);
            string Password = Convert.ToBase64String(pass);
            if (_users[userName].PasswordHash.Equals(Password))
            {
                return true;
            }
            else
            { 
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public static void LogIn(string userName, string password)
    {
        if (CheckPassword(userName, password))
        {
            Console.WriteLine("Congratulatios\n");
        }
        else
        {
            Console.WriteLine("Try again\n");
        }
    }

    public static void OnlyForAdminsFeature()
    {
        if (Thread.CurrentPrincipal == null)
        {
            throw new SecurityException("Thread.CurrentPrincipal cannot be null.");
        }
        if (!Thread.CurrentPrincipal.IsInRole("Admins"))
        {
            throw new SecurityException("User must be a member of Admins to access this feature.");
        }
        Console.WriteLine("You have access to this secure feature.");
    }

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


    public static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nRegister now - 1\n" +
                             "Log in - 2\n" +
                             "Exit - 3\n");
            int q = Convert.ToInt32(Console.ReadLine());
            if (q == 1)
            {
                Console.WriteLine("Enter Login: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Password: ");
                string pass = Console.ReadLine();
                string[] role = null;
                User smt = Protector.Register(name, pass, role);
                if (smt == null)
                {
                    Console.WriteLine("Try again\n");
                }
                else
                {
                    Console.WriteLine("Congratulatios\n");
                }
            }
            else if (q == 2)
            {
                Console.WriteLine("Enter Login: ");
                string name = Console.ReadLine();
                Console.WriteLine("Enter Password: ");
                string pass = Console.ReadLine();
                Protector.LogIn(name, pass);
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