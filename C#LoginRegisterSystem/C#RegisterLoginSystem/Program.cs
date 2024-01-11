using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

public class LoginRegisterManager
{
    private UserInfoList userInfoList;
    private string userSavedFile = "userData.json";

    public LoginRegisterManager()
    {
        userInfoList = new UserInfoList();
        LoadUserData();
    }


    //register method 
    public void Register()
    {
        //we ask for username and password and we get the values the user put.
        Console.WriteLine("Enter your username:");
        string registerUsername = Console.ReadLine();
        Console.WriteLine("Enter your password:");
        string registerPassword = Console.ReadLine();
        //we check if the username already exists
        if (DoesUserExistRegister(registerUsername))
        {
            Console.WriteLine("Name already exists");
        }
        else
        {
            //we create a new user and add it to the list
            UserInfo newUser = new UserInfo()
            {
                Username = registerUsername,
                Password = registerPassword,
            };

            Console.WriteLine("Registered New User, Also Welcome " + registerUsername);
            userInfoList.UsersInfoList.Add(newUser);
            SaveUserData();
        }
    }
    //login method
    public void Login()
    {
        Console.WriteLine("Enter your username:");
        string loginUsername = Console.ReadLine();
        Console.WriteLine("Enter your password:");
        string loginPassword = Console.ReadLine();

        //we check if the username and password match
        if (DoesUserExistLogin(loginUsername, loginPassword))
        {
            Console.WriteLine("You are now logged in " + loginUsername);
        }
        else
        {
            Console.WriteLine("Wrong Username or Password");
        }
    }
    //checking if a username already exists in registration method
    private bool DoesUserExistRegister(string username)
    {
        return userInfoList.UsersInfoList.Exists(user => user.Username == username);
    }
    //checking if a username already exists in login method
    private bool DoesUserExistLogin(string username, string password)
    {
        return userInfoList.UsersInfoList.Exists(user => user.Username == username && user.Password == password);
    }
    //we save data to JSON
    private void SaveUserData()
    {
        string json = JsonConvert.SerializeObject(userInfoList, Formatting.Indented);
        File.WriteAllText(userSavedFile, json);
    }
    //we load data from JSON
    private void LoadUserData()
    {
        if (File.Exists(userSavedFile))
        {
            string json = File.ReadAllText(userSavedFile);
            userInfoList = JsonConvert.DeserializeObject<UserInfoList>(json);
        }
    }
    //the user info such as username and password
    public class UserInfo
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    //a list of all the userinfo
    public class UserInfoList
    {
        public List<UserInfo> UsersInfoList { get; set; } = new List<UserInfo>();
    }
    class Program
    {
        static void Main()
        {
            LoginRegisterManager manager = new LoginRegisterManager();
            
            while (true)
            {
                //presenting the user with a choice and then reading the input
                Console.WriteLine("Enter your Choice:");
                Console.WriteLine("1. Register");
                Console.WriteLine("2. Login");
                Console.WriteLine("3. Exit");

                string choice = Console.ReadLine();
                //switch statement that handles the different user choises
                switch (choice)
                {
                    case "1":
                        manager.Register();
                        break;
                    case "2":
                        manager.Login();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Please choose a correct number between 1,2 or 3.");
                        break;
                }
            }
        }
    }
}
