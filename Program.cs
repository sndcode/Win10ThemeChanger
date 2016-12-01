using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace Win10DarkThemeActivator
{
    class Program
    {
        public static void Delete_Key()
        {
            try
            {
                RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", true);
                myKey.DeleteSubKey("AppsUseLightTheme");
            }
            catch { }
        }

        public static void Activate_DarkTheme()
        {
            try
            {
                RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", true);
                if (myKey != null)
                {
                    object keyvalue = 0;
                    myKey.SetValue("AppsUseLightTheme", keyvalue, RegistryValueKind.DWord);
                }
                else
                {
                    myKey.CreateSubKey("AppsUseLightTheme");
                }
            }
            catch { }
        }

        public static void Deactivate_DarkTheme()
        {
            try
            {
                RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", true);
                if (myKey != null)
                {
                    object keyvalue = 1;
                    myKey.SetValue("AppsUseLightTheme", keyvalue, RegistryValueKind.DWord);
                }
                else
                {
                    myKey.CreateSubKey("AppsUseLightTheme");
                }
            }
            catch { }
        }

        public static void ReadThemeValue()
        {
            try
            {
                RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", true);
                if (myKey != null)
                {
                    string value = myKey.GetValue("AppsUseLightTheme").ToString();
                    if (value == "0")
                    {
                        Console.WriteLine("Current activated theme is : DARK ");
                    }
                    else if (value == "1")
                    {
                        Console.WriteLine("Current activated theme is : LIGHT ");
                    }
                }
            }
            catch { }
            GetUserInput();
        }

        static void GetUserInput()
        {
            Console.WriteLine("\n");
            Console.WriteLine("Please enter ENABLE to enable the dark theme, or DISABLE to disable the theme..");
            string input = Console.ReadLine();
            Console.WriteLine("You entered " + input);
            if (input == "enable")
            {
                Console.WriteLine("The theme is getting activated..");
                Activate_DarkTheme();
                Console.WriteLine("Successfully activated!");
                Console.WriteLine("I will close myself now in 5 seconds. Bye Bye !");
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(0);
            }
            else if (input == "disable")
            {
                Console.WriteLine("The theme is getting deactivated..");
                Deactivate_DarkTheme();
                Console.WriteLine("Successfully deactivated!");
                Console.WriteLine("I will close myself now in 5 seconds. Bye Bye !");
                System.Threading.Thread.Sleep(5000);
                Environment.Exit(0);
            }
            else if (input == "read")
            {
                ReadThemeValue();
            }
            else if (input == "delete")
            {
                Delete_Key();
            }
            else
            {
                Console.WriteLine("Pardon ? ... ");
                GetUserInput();
            }
        }

        static void Main(string[] args)
        {
            string app_version = "0.1 RC1";
            Console.Title = "Windows 10 Dark Theme Activator";
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Welcome to the Win10 Dark Theme Activator version " + app_version + "!");
            Console.WriteLine("Created by sandaasu");
            Console.WriteLine("\n");
            ReadThemeValue();
            GetUserInput();
        }
    }
}
