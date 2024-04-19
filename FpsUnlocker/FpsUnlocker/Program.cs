using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.Title = "Easy FPS Unlocker"; // Replace with your desired console title

        while (true)
        {
            Console.Clear(); // Clear the console for a cleaner display
            DisplayArrowIcon();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("  FPS Unlocker App is running.");
            Console.WriteLine();
            Console.ForegroundColor= ConsoleColor.Blue;
            Console.WriteLine(" --------------------");
            Console.ForegroundColor= ConsoleColor.DarkBlue;
            Console.WriteLine("|  1. Set Target FPS |");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("|  2. Exit           |");
            Console.WriteLine(" --------------------");

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("Enter Your Choice(): ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    SetTargetFps();
                    break;
                case "2":
                    ExitApplication();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Invalid choice. Please enter a valid option.");

                    break;
            }
        }
    }

    static void DisplayArrowIcon()
    {
        // ASCII art representation of an arrow icon
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine();
        Console.WriteLine("         *Unlocker*    ");
        Console.WriteLine("                    ");
        Console.WriteLine("      ┏┳┓╋╋┏┓╋╋╋╋┏┓       ");
        Console.WriteLine("      ┃┃┣━┳┫┃┏━┳━┫┣┳━┳┳┓  ");
        Console.WriteLine("      ┃┃┃┃┃┃┗┫╋┃━┫━┫┻┫┏┛  ");
        Console.WriteLine("      ┗━┻┻━┻━┻━┻━┻┻┻━┻┛   ");
        Console.WriteLine("  *this script make by 0989 /* 14/1/24");
        Console.WriteLine();
    }

    static void SetTargetFps()
    {
        int targetFps = GetTargetFpsFromUser();

        if (targetFps > 0)
        {
            string robloxPlayerFilePath = GetRobloxPlayerFilePath();

            if (robloxPlayerFilePath == null)
            {
                Console.WriteLine("Error: Roblox player folder path not found or invalid.");
                return;
            }

            string clientSettingFolderPath = Path.Combine(robloxPlayerFilePath, "ClientSettings");

            // Create the ClientSettings folder if it doesn't exist
            if (!Directory.Exists(clientSettingFolderPath))
            {
                try
                {
                    Directory.CreateDirectory(clientSettingFolderPath);
                    Console.WriteLine("ClientSettings folder created.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating ClientSettings folder: {ex.Message}");
                    return;
                }
            }

            string jsonFilePath = Path.Combine(clientSettingFolderPath, "ClientAppSettings.json");

            if (File.Exists(jsonFilePath))
            {
                UpdateExistingJsonFile(jsonFilePath, targetFps);
            }
            else
            {
                WriteJsonFile(jsonFilePath, targetFps);
            }

            Console.WriteLine($"ClientAppSettings.json with DFIntTaskSchedulerTargetFps {targetFps} has been updated/created.");
        }
    }

    static void ExitApplication()
    {
        Console.WriteLine("Exiting Application.");
        Environment.Exit(0);
    }

    static int GetTargetFpsFromUser()
    {
        int targetFps;
        while (true)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter target FPS (60, 120, or anothor): ");
            Console.ForegroundColor= ConsoleColor.Red;
            if (int.TryParse(Console.ReadLine(), out targetFps) && targetFps > 0)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid positive integer.");
            }
        }
        return targetFps;
    }

    static string GetRobloxPlayerFilePath()
    {
        // Replace this path with the actual path to the Roblox player folder on your system
        string robloxPlayerFolderPath = @"C:\Users\Lenovo\AppData\Local\Roblox\Versions\version-88cfc23f4e7d4e4b";

        if (Directory.Exists(robloxPlayerFolderPath))
        {
            return robloxPlayerFolderPath;
        }
        else
        {
            Console.WriteLine("Error: Roblox player folder not found.");
            return null;
        }
    }

    static void WriteJsonFile(string filePath, int targetFps)
    {
        try
        {
            var jsonContent = new { DFIntTaskSchedulerTargetFps = targetFps };
            string jsonString = JsonConvert.SerializeObject(jsonContent, Formatting.Indented);
            File.WriteAllText(filePath, jsonString);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error writing JSON file: {ex.Message}");
        }
    }

    static void UpdateExistingJsonFile(string filePath, int newTargetFps)
    {
        try
        {
            // Read existing JSON content
            string existingJson = File.ReadAllText(filePath);

            // Deserialize existing content
            var existingContent = JsonConvert.DeserializeObject<dynamic>(existingJson);

            // Update the FPS value
            existingContent.DFIntTaskSchedulerTargetFps = newTargetFps;

            // Serialize and write back to the file
            string updatedJson = JsonConvert.SerializeObject(existingContent, Formatting.Indented);
            File.WriteAllText(filePath, updatedJson);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating JSON file: {ex.Message}");
        }
    }
}
