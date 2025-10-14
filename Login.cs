using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace AdventureQuest
{
    public class Login
    {


        public void TwoFactorSMS()
        {
            Console.WriteLine("Please put in your phonenumber for 2FA");
            string UserPhoneNumber = Console.ReadLine();
            // PLACEHOLDERS TILL I FIX TWILIO ACCOUNT
            // REPLACE WITH ENVIRONMENTAL VARIABLES
            string accountSID = Environment.GetEnvironmentVariable("TWILIO_SID"); // 
            // REMOVE AUTH TOKEN BEFORE UPLOADING TO GITHUB
            string authToken = Environment.GetEnvironmentVariable("TWILIO_AUTHTOKEN"); //REMOVE THIS AFTERWARDS OR REPLACE 

            // to confirm the auth code to proceed
            string AuthConfirm = "";

            Random random = new Random();
            string authenticatorCode = random.Next(100000, 999999).ToString();
            // PLACEHOLDER FOR TWILIO PHONE NUMBER
            string AuthenticatorTwilioPhone = Environment.GetEnvironmentVariable("TWILIO_PHONENUMBER"); //
            TwilioClient.Init(accountSID, authToken);

            // this is where we will send messages, it uses authenticatortwilio to send to user phonenumber
            var SMSmessage = MessageResource.Create(
            body: $"This is your verification code {authenticatorCode}",
            from: new PhoneNumber(AuthenticatorTwilioPhone),
            to: new PhoneNumber(UserPhoneNumber));

            // prompt for auth

            Console.WriteLine("Please input your code to continue. ");
            AuthConfirm = Console.ReadLine();

            // if 2FA doesnt match then dont let user continue until its right
            while (AuthConfirm != authenticatorCode)
            {
                Console.WriteLine("The code does not match the 2FA");
                AuthConfirm = Console.ReadLine();
            }
            // end loop if authconfirm is same as the 2FA
            if (AuthConfirm == authenticatorCode)
            {
                Console.Clear();
                Console.WriteLine("Authenticator code confirmed, proceeding.");
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
            }

        }
        public void LoginMessage()
        {

            // for the user to put in a password
            string password = "";
            // for the user to input a username
            string UserRegister = "";
            // for the user to choose between login and registers
            string LoginChoice;
            // password for existing user
            string UserPassLogin = "";


            // display login message for user
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("==================================");
            Console.WriteLine("     Welcome to adventure quest!  ");
            Console.WriteLine("==================================");
            Console.WriteLine();
            Console.WriteLine("Please login or register your account.");
            // reset console color and provide way to login or register
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Register");
            Console.Write("Respond with the number of your choice.");
            // choose between login and register

            // keeping option1 barebones for now so i can easily access the rest of the code without going through 2FA
            LoginChoice = Console.ReadLine();
            if (LoginChoice == "1")
            {
                Console.WriteLine("Welcome back hero, you will now be redirected to login page.");
                Console.WriteLine("What is your name?");
                UserRegister = Console.ReadLine();
                Console.WriteLine("What is your password?");
                UserPassLogin = Console.ReadLine();
                Console.WriteLine($"The hero {UserRegister} awakens from his slumber.");

            }
            else if (LoginChoice == "2")
            {
                Console.WriteLine("Welcome new hero, you will now be redirected to register page.");
                Console.WriteLine("What is your name hero?");
                UserRegister = Console.ReadLine();
                Console.WriteLine($"Welcome {UserRegister}, a hero needs a strong password.");


                // checks if the password contains special characters
                bool containingSpecialChar(string password)
                {
                    string specialChars = "!@#$%^&*()-_=+[]{}|;:'\",.<>?/";
                    return password.Any(c => specialChars.Contains(c));
                }
                // checks if password contains a number
                bool ContainsNumber(string password)
                {
                    return password.Any(char.IsDigit);
                }
                // checks if password contains uppercase and lowercase letters
                bool ContainsUppercase(string password) => password.Any(char.IsUpper);
                bool ContainsLowercase(string password) => password.Any(char.IsLower);
                // loop until the password process is finished
                while (true)
                {
                    // password under 8 letters, will display an error message
                    if (password.Length < 8)
                    {
                        Console.WriteLine("Weak. Your password is too short, try again.");
                        password = Console.ReadLine();
                    }
                    // password with no capital letters will display an error
                    else if (!ContainsUppercase(password))
                    {
                        Console.WriteLine("Weak. Your password must contain a capital letter.");
                        password = Console.ReadLine();
                    }
                    // password with no lowercase letters will display an error until corrected
                    else if (!ContainsLowercase(password))
                    {
                        Console.WriteLine("Weak. Your password must contain a small letter, try again.");
                        password = Console.ReadLine();
                    }
                    // password with no numbers will display error
                    else if (!ContainsNumber(password))
                    {
                        Console.WriteLine("Weak. Input a number in your passowrd.");
                        password = Console.ReadLine();
                    }
                    // if password has all of the above but no special characters, display the moderate error message
                    else if (!containingSpecialChar(password))
                    {

                        Console.WriteLine("Moderate. Your password needs to have a special character.");
                        password = Console.ReadLine();
                    }

                    // if password has all of the above, display strong message and end the loop
                    else
                    {
                        Console.WriteLine("Strong. Your password is good");
                        break;
                    }
                }

                // prompt for 2FA before continuing
                TwoFactorSMS();

                Console.WriteLine($"The hero {UserRegister} is born.");
            }



            Console.WriteLine("Your adventure begins now!");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
            Console.ResetColor();


        }
    }

}

