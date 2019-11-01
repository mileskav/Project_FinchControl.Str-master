using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control
    // Description: This program utilizes features built into the Finch Robot
    // Application Type: Console
    // Author: Kavanagh, Miles
    // Date Created: 10/02/2019
    // Last Modified: 10/04/2019
    //
    // **************************************************

    class Program
    {
        /// <summary>
        /// list of commands for user programming
        /// </summary>
        private enum Command
        {
            NONE,
            MOVEFORWARD,
            MOVEBACKWARD,
            STOPMOTORS,
            WAIT,
            TURNRIGHT,
            TURNLEFT,
            LEDON,
            LEDOFF,
            DONE
        }

        static void Main(string[] args)
        {
            DisplayWelcomeScreen();

            DisplayMainMenu();

            DisplayClosingScreen();
        }

        /// <summary>
        /// displays main menu options
        /// </summary>
        static void DisplayMainMenu()
        {
            //
            //instantiate a Finch object
            Finch ros = new Finch();

            bool rosConnected = false;
            bool quitApplication = false;
            string menuChoice;

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get the user's menu choice
                if (!rosConnected)
                {
                    Console.WriteLine("a) Connect Finch Robot");
                    Console.WriteLine("q) Quit");
                    Console.WriteLine();
                    Console.Write("Enter Choice: ");
                    menuChoice = Console.ReadLine().ToLower();
                }
                else
                {
                    Console.WriteLine("a) Connect Finch Robot");
                    Console.WriteLine("b) Talent Show");
                    Console.WriteLine("c) Data Recorder");
                    Console.WriteLine("d) Alarm System");
                    Console.WriteLine("e) User Programming");
                    Console.WriteLine("f) Disconnect Finch Robot");
                    Console.WriteLine("q) Quit");
                    Console.WriteLine();
                    Console.Write("Enter Choice: ");
                    menuChoice = Console.ReadLine().ToLower();
                }

                //
                // process user choice
                switch (menuChoice)
                {
                    case "a":
                        rosConnected = DisplayConnectRos(ros);
                        break;
                    case "b":
                        DisplayTalentShow(ros);
                        break;
                    case "c":
                        DisplayDataSelect(ros);
                        break;
                    case "d":
                        DisplayAlarmSystem(ros);
                        break;
                    case "e":
                        DisplayUserProgramming(ros);
                        break;
                    case "f":
                        DisplayDisconnectRos(ros);
                        break;
                    case "q":
                        if (rosConnected)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Would you like to disconnect and exit? [y/n]");
                            menuChoice = Console.ReadLine();
                            if (menuChoice == "y")
                            {
                                DisplayDisconnectRos(ros);
                                quitApplication = true;
                            }
                            else if (menuChoice == "n")
                            {
                                Console.WriteLine();
                                DisplayContinuePrompt();
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("That is not a valid answer. Please enter 'y' or 'n'.");
                                DisplayContinuePrompt();
                            }
                        }
                        else if (!rosConnected)
                        {
                            quitApplication = true;
                        }
                        break;

                    default:
                        Console.WriteLine("\t\t*************");
                        Console.WriteLine("Please indicate your choice with a letter.");
                        Console.WriteLine("\t\t*************");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region TALENT SHOW

        /// <summary>
        /// displays talent show feature
        /// </summary>
        static void DisplayTalentShow(Finch ros)
        {
            DisplayScreenHeader("Talent Show");

            Console.WriteLine("The Finch robot is ready to show you its talents.");
            DisplayContinuePrompt();

            DisplayLights(ros);
            DisplayDanceScreen(ros);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// displays the lights screen
        /// </summary>
        static void DisplayLights(Finch ros)
        {
            int r;
            int g;
            int b;
            DisplayScreenHeader("Light Show");
            Console.WriteLine();
            Console.WriteLine("Enter a red value: ");
            r = int.Parse(Console.ReadLine());
            while (r > 255)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a value between 0 and 255");
                Console.WriteLine("Enter a red value: ");
                r = int.Parse(Console.ReadLine());
            }

            Console.Clear();
            DisplayScreenHeader("Light Show");
            Console.WriteLine();
            Console.WriteLine("Enter a green value: ");
            g = int.Parse(Console.ReadLine());
            while (g > 255)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a value between 0 and 255");
                Console.WriteLine("Enter a green value: ");
                g = int.Parse(Console.ReadLine());
            }

            DisplayScreenHeader("Light Show");
            Console.WriteLine();
            Console.WriteLine("Enter a blue value: ");
            b = int.Parse(Console.ReadLine());
            while (b > 255)
            {
                Console.WriteLine();
                Console.WriteLine("Please enter a value between 0 and 255");
                Console.WriteLine("Enter a blue value: ");
                b = int.Parse(Console.ReadLine());
            }
            DisplayContinuePrompt();

            Console.Clear();
            DisplayScreenHeader("Light Show");
            Console.WriteLine();
            Console.WriteLine("Now displaying your LED values.");

            DisplayLightShow(ros, r, g, b);

            DisplayContinuePrompt();
        }

        /// <summary>
        /// displays flashing lights
        /// </summary>
        static void DisplayLightShow(Finch ros, int r, int g, int b)
        {

            for (int i = 0; i < 5; i++)
            {
                ros.noteOn(794);
                ros.wait(50);
                ros.noteOff();
                ros.setLED(r, 0, 0);
                ros.wait(200);
                ros.setLED(0, 0, 0);
                ros.wait(200);
            }

            for (int i = 0; i < 5; i++)
            {
                ros.noteOn(880);
                ros.wait(50);
                ros.noteOff();
                ros.setLED(0, g, 0);
                ros.wait(250);
                ros.setLED(0, 0, 0);
                ros.wait(250);
            }

            for (int i = 0; i < 5; i++)
            {
                ros.noteOn(988);
                ros.wait(50);
                ros.noteOff();
                ros.setLED(0, 0, b);
                ros.wait(300);
                ros.setLED(0, 0, 0);
                ros.wait(300);
            }

            for (int i = 0; i < 5; i++)
            {
                ros.noteOn(794);
                ros.wait(50);
                ros.noteOn(880);
                ros.wait(50);
                ros.noteOn(988);
                ros.wait(50);
                ros.noteOff();
                ros.setLED(r, g, b);
                ros.wait(500);
                ros.setLED(0, 0, 0);
                ros.wait(500);

            }
        }

        /// <summary>
        /// displays dance
        /// </summary>
        static void DisplayDanceScreen(Finch ros)
        {
            DisplayScreenHeader("Song & Dance");

            DisplaySong(ros);

            DisplayDance(ros);
        }

        /// <summary>
        /// plays music
        /// </summary>
        static void DisplaySong(Finch ros)
        {
            Console.WriteLine();
            Console.WriteLine("Now Playing: The Greatest Adventure - The Hobbit(1973)");
            ros.noteOn(523);
            ros.wait(500);
            ros.noteOn(698);
            ros.wait(1000);
            ros.noteOn(698);
            ros.wait(500);
            ros.noteOn(659);
            ros.wait(500);
            ros.noteOn(698);
            ros.wait(500);
            ros.noteOn(880);
            ros.wait(1000);
            ros.noteOn(698);
            ros.wait(500);
            ros.noteOn(784);
            ros.wait(1000);
            ros.noteOn(988);
            ros.wait(750);
            ros.noteOn(880);
            ros.wait(250);
            ros.noteOn(784);
            ros.wait(1000);
            ros.noteOff();

        }

        /// <summary>
        /// plays the dance routine for the finch
        /// </summary>
        static void DisplayDance(Finch ros)
        {
            ros.setMotors(100, 0);
            ros.wait(500);
            ros.setMotors(0, -100);
            ros.wait(500);
            ros.setMotors(0, 100);
            ros.wait(500);
            ros.setMotors(-100, 0);
            ros.wait(500);
            ros.setMotors(255, -255);
            ros.wait(1000);
            ros.setMotors(0, 0);
            ros.wait(500);
        }

        #endregion

        #region DATA RECORDER

        /// <summary>
        /// displays the data selection screen
        /// </summary>
        static void DisplayDataSelect(Finch ros)
        {
            string menuChoice;
            Console.Clear();
            Console.WriteLine("Would you like to record light or temperature data?");
            Console.WriteLine();
            Console.WriteLine("a) Light Data");
            Console.WriteLine("b) Temperature Data");
            menuChoice = Console.ReadLine().ToLower();

            //process user choice
            if (menuChoice == "a")
            {
                DisplayLightRecorder(ros);
            }
            else if (menuChoice == "b")
            {
                DisplayTempRecorder(ros);
            }
            else
            {
                Console.WriteLine("That is not a valid choice. Please indicate your choice with the letters 'a' or 'b'.");
                DisplayContinuePrompt();
            }
        }

        /// <summary>
        /// records light
        /// </summary>
        static void DisplayLightRecorder(Finch ros)
        {
            double dataPointFrequency;
            int numberOfDataPoints;


            DisplayScreenHeader("Light Recorder");
            Console.WriteLine();
            Console.WriteLine("I will be asking you for how frequently you would like the Finch Robot to collect your light data.");
            Console.WriteLine("Next I will be asking for how many data points you would like to have collected");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

            dataPointFrequency = DisplayGetDataPointFrequency();
            numberOfDataPoints = DisplayGetNumberOfDataPoints();

            double[] lights = new double[numberOfDataPoints];

            DisplayGetLightData(numberOfDataPoints, dataPointFrequency, lights, ros);

            DisplayLightData(lights);

            Console.WriteLine("Press any key to continue.");
        }

        /// <summary>
        /// displays light data
        /// </summary>
        static void DisplayLightData(double[] lights)
        {
            DisplayScreenHeader("Lights");

            for (int index = 0; index < lights.Length; index++)
            {
                Console.WriteLine($"Light {index + 1}: {lights[index]}");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// gets light data from user
        /// </summary>
        static void DisplayGetLightData(int numberOfDataPoints, double dataPointFrequency, double[] lights, Finch ros)
        {
            DisplayScreenHeader("Get Light Value");
            Console.WriteLine();
            Console.WriteLine("The Finch Robot will now record your light values.");

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                lights[index] = (ros.getLeftLightSensor() + ros.getRightLightSensor()) / 2;
                int milliseconds = (int)(dataPointFrequency * 1000);
                ros.wait(milliseconds);
                Console.WriteLine($"Light {index + 1}: {lights[index]}");
            }

            DisplayContinuePrompt();

        }

        /// <summary>
        /// records temperature
        /// </summary>
        static void DisplayTempRecorder(Finch ros)
        {
            double dataPointFrequency;
            int numberOfDataPoints;


            DisplayScreenHeader("Temperature Recorder");
            Console.WriteLine();
            Console.WriteLine("I will be asking you for how frequently you would like the Finch Robot to collect your temperatures.");
            Console.WriteLine("Next I will be asking for how many temperatures you would like to have collected");
            Console.WriteLine();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();

            dataPointFrequency = DisplayGetDataPointFrequency();
            numberOfDataPoints = DisplayGetNumberOfDataPoints();

            double[] temperatures = new double[numberOfDataPoints];

            DisplayGetTempData(numberOfDataPoints, dataPointFrequency, temperatures, ros);

            DisplayTempData(temperatures);

            Console.WriteLine("Press any key to continue.");
        }

        /// <summary>
        /// displays temperature data
        /// </summary>
        static void DisplayTempData(double[] temperatures)
        {
            DisplayScreenHeader("Temperatures");

            for (int index = 0; index < temperatures.Length; index++)
            {
                double fahrenheit = (double)(temperatures[index] * 1.8) + 32;
                Console.WriteLine($"Temperature {index + 1}: {fahrenheit} °F");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// gets data for temperatures
        /// </summary>
        static void DisplayGetTempData(int numberOfDataPoints, double dataPointFrequency, double[] temperatures, Finch ros)
        {
            DisplayScreenHeader("Get Temperatures");
            Console.WriteLine();
            Console.WriteLine("The Finch Robot will now record your temperatures.");

            for (int index = 0; index < numberOfDataPoints; index++)
            {
                temperatures[index] = ros.getTemperature();
                int milliseconds = (int)(dataPointFrequency * 1000);
                ros.wait(milliseconds);
                double fahrenheit = (double)(temperatures[index] * 1.8) + 32;
                Console.WriteLine($"Temperature {index + 1}: {fahrenheit} °F");
            }

            DisplayContinuePrompt();

        }

        /// <summary>
        /// gets number of data points
        /// </summary>
        static int DisplayGetNumberOfDataPoints()
        {
            int numberOfDataPoints;


            DisplayScreenHeader("Number of Data Points");

            Console.Write("Enter the Number of Data Points: ");
            int.TryParse(Console.ReadLine(), out numberOfDataPoints);
            while (numberOfDataPoints < 1)
            {
                Console.WriteLine("That is not a valid number. Please enter a value of 1 or higher.");
                DisplayContinuePrompt();
                Console.WriteLine();
                Console.Write("Enter the Number of Data Points: ");
                int.TryParse(Console.ReadLine(), out numberOfDataPoints);
            }

            DisplayContinuePrompt();

            return numberOfDataPoints;
        }

        /// <summary>
        /// gets frequency of data points
        /// </summary>
        static double DisplayGetDataPointFrequency()
        {
            double dataPointFrequency;

            DisplayScreenHeader("Data Point Frequency");

            Console.Write("Enter Frequency of Recordings: ");
            double.TryParse(Console.ReadLine(), out dataPointFrequency);
            while (dataPointFrequency <= 0)
            {
                Console.WriteLine("That is not a valid number. Please enter a value higher than 0.");
                DisplayContinuePrompt();
                Console.WriteLine();
                Console.Write("Enter Frequency of Recordings: ");
                double.TryParse(Console.ReadLine(), out dataPointFrequency);
            }

            DisplayContinuePrompt();

            return dataPointFrequency;
        }

        #endregion

        #region ALARM SYSTEM

        /// <summary>
        /// displays alarm system
        /// </summary>
        static void DisplayAlarmSystem(Finch ros)
        {
            DisplayScreenHeader("Alarm System");

            string alarmType = DisplayGetAlarmType();
            int maxSeconds = DisplayGetMaxSeconds();
            double threshold = DisplayGetThreshold(ros, alarmType);

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($"Next, the Finch Robot will monitor your {alarmType} levels.");
            DisplayContinuePrompt();

            bool thresholdExceeded;
            if (alarmType == "light")
            {
                thresholdExceeded = MonitorCurrentLightLevels(ros, threshold, maxSeconds);
                if (thresholdExceeded)
                {
                    ros.noteOn(987);
                    ros.wait(100);
                    ros.noteOn(1200);
                    ros.wait(200);
                    ros.noteOff();
                    Console.WriteLine("Maximum Light Level Exceeded");
                }
            }
            else if (alarmType == "temperature")
            {
                thresholdExceeded = MonitorCurrentTemperatureLevels(ros, threshold, maxSeconds);
                if (thresholdExceeded)
                {
                    ros.noteOn(987);
                    ros.wait(100);
                    ros.noteOn(1200);
                    ros.wait(200);
                    ros.noteOff();
                    Console.WriteLine("Maximum Temperature Exceeded");
                }
            }
            else
            {
                ros.noteOn(987);
                ros.wait(100);
                ros.noteOn(1200);
                ros.wait(200);
                ros.noteOff();
                Console.WriteLine("Maximum Time Exceeded");
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// monitors temperature level
        /// </summary>
        static bool MonitorCurrentTemperatureLevels(Finch ros, double threshold, int maxSeconds)
        {
            bool thresholdExceeded = false;
            double currentTempLevel;
            double seconds = 0;

            while (!thresholdExceeded && seconds <= maxSeconds)
            {
                currentTempLevel = (ros.getTemperature() * 1.8) + 32;

                DisplayScreenHeader("Monitor Temperature Levels");
                Console.WriteLine($"Maximum Temperature Level: {threshold}");
                Console.WriteLine($"Current Temperature Level: {currentTempLevel}");

                if (currentTempLevel > threshold)
                {
                    thresholdExceeded = true;
                }

                ros.wait(500);
                seconds += 0.5;
            }

            return thresholdExceeded;
        }

        /// <summary>
        /// monitors light level
        /// </summary>
        static bool MonitorCurrentLightLevels(Finch ros, double threshold, int maxSeconds)
        {
            bool thresholdExceeded = false;
            int currentLightLevel;
            double seconds = 0;

            while (!thresholdExceeded && seconds <= maxSeconds)
            {
                currentLightLevel = ros.getLeftLightSensor();

                DisplayScreenHeader("Monitor Light Levels");
                Console.WriteLine($"Maximum Light Level: {threshold}");
                Console.WriteLine($"Current Light Level: {currentLightLevel}");

                if (currentLightLevel > threshold)
                {
                    thresholdExceeded = true;
                }

                ros.wait(500);
                seconds += 0.5;
            }

            return thresholdExceeded;
        }

        /// <summary>
        /// gets threshold for alarm
        /// </summary>
        static double DisplayGetThreshold(Finch ros, string alarmType)
        {
            double threshold = 0;
            DisplayScreenHeader("Threshold Value");

            if (alarmType == "light")
            {
                Console.WriteLine($"Current Light Level: {ros.getLeftLightSensor()}");
                Console.WriteLine();
                Console.WriteLine("Enter Maximum Light Level [0-255]: ");
                threshold = double.Parse(Console.ReadLine());
                if (threshold < 1 || threshold > 255)
                {
                    Console.WriteLine("This is not a valid value. Please enter a value between 0 and 255.");
                    Console.WriteLine();
                    Console.WriteLine("Enter Maximum Light Level [0-255]: ");
                    threshold = double.Parse(Console.ReadLine());
                }
            }
            else
            {
                Console.WriteLine($"Current Temperature Level: {(ros.getTemperature() * 1.8) + 32}");
                Console.WriteLine();
                Console.WriteLine("Enter Maximum Temperature [1-120]: ");
                threshold = double.Parse(Console.ReadLine());
                if (threshold < 1 || threshold > 120)
                {
                    Console.WriteLine("This is not a valid value. Please enter a value between 1 and 120.");
                    Console.WriteLine();
                    Console.WriteLine("Enter Maximum Temperature Level [1-120]: ");
                    threshold = double.Parse(Console.ReadLine());
                }
            }

            DisplayContinuePrompt();

            return threshold;
        }

        /// <summary>
        /// gets maximum number of seconds from user
        /// </summary>
        static int DisplayGetMaxSeconds()
        {
            Console.WriteLine("Enter Maximum Number of Seconds: ");
            int maxSeconds = int.Parse(Console.ReadLine());
            if (maxSeconds <= 0)
            {
                Console.WriteLine("That is not a valid value. Please enter second values higher than 0.");
                Console.WriteLine("Enter Maximum Number of Seconds: ");
                maxSeconds = int.Parse(Console.ReadLine());
            }
            return maxSeconds;
        }

        /// <summary>
        /// gets alarm type from user
        /// </summary>
        static string DisplayGetAlarmType()
        {
            string alarmTypeUser;
            Console.WriteLine("Enter Alarm Type [light or temperature]: ");
            alarmTypeUser = Console.ReadLine();
            if (alarmTypeUser != "light" && alarmTypeUser != "temperature")
            {
                Console.WriteLine();
                Console.WriteLine("That is not a valid response. Please respond with either 'light' or 'temperature'.");
                Console.WriteLine();
                Console.WriteLine("Enter Alarm Type [light or temperature]: ");
                alarmTypeUser = Console.ReadLine();
            }

            return alarmTypeUser;

        }

        #endregion

        #region USER PROGRAMMING

        /// <summary>
        /// displays user programming menu
        /// </summary>
        static void DisplayUserProgramming(Finch ros)
        {
            string menuChoice;
            bool quitApplication = false;
            List<Command> commands = new List<Command>();

            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;

            do
            {
                DisplayScreenHeader("User Programming");

                Console.WriteLine("a) Set Command Parameters");
                Console.WriteLine("b) Add Commands");
                Console.WriteLine("c) View Commands");
                Console.WriteLine("d) Execute Commands");
                Console.WriteLine("e) Write Commands to Data File");
                Console.WriteLine("f) Read Commands from Data File");
                Console.WriteLine("q) Quit");
                Console.WriteLine();
                Console.Write("Enter Choice: ");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user choice
                switch (menuChoice)
                {
                    case "a":
                        commandParameters = DisplayGetCommandParameters();
                        break;

                    case "b":
                        DisplayGetFinchCommands(commands);
                        break;

                    case "c":
                        DisplayFinchCommands(commands);
                        break;

                    case "d":
                        DisplayExecuteFinchCommands(ros, commands, commandParameters);
                        break;

                    case "e":
                        DisplayWriteUserProgrammingData(commands);
                        break;

                    case "f":
                        commands = DisplayReadUserProgrammingData();
                        break;

                    case "q":
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine("\t\t*************");
                        Console.WriteLine("Please indicate your choice with a letter.");
                        Console.WriteLine("\t\t*************");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        /// <summary>
        /// writes user programming data to txt file
        /// </summary>
        static void DisplayWriteUserProgrammingData(List<Command> commands)
        {
            string dataPathA = @"Data\Data1.txt";
            string dataPathB = @"Data\Data2.txt";
            string dataPathC = @"Data\Data3.txt";

            List<string> commandsString = new List<string>();

            DisplayScreenHeader("Save Commands to Data File");
            Console.WriteLine("Which save slot would you like to save to?");
            Console.WriteLine("a) Save Slot A");
            Console.WriteLine("b) Save Slot B");
            Console.WriteLine("c) Save Slot C");
            string userResponse = Console.ReadLine().ToLower();

            Console.WriteLine("Ready to save your commands to the data file.");
            DisplayContinuePrompt();

            //
            // create list of command strings
            foreach (Command command in commands)
            {
                commandsString.Add(command.ToString());
            }

            switch (userResponse)
            {
                case "a":
                    File.WriteAllLines(dataPathA, commandsString.ToArray());
                    Console.WriteLine();
                    Console.WriteLine("Commands saved successfully.");
                    break;
                case "b":
                    File.WriteAllLines(dataPathB, commandsString.ToArray());
                    Console.WriteLine();
                    Console.WriteLine("Commands saved successfully.");
                    break;
                case "c":
                    File.WriteAllLines(dataPathC, commandsString.ToArray());
                    Console.WriteLine();
                    Console.WriteLine("Commands saved successfully.");
                    break;
                default:
                    while (userResponse != "a" && userResponse != "b" && userResponse != "c")
                    {
                        Console.WriteLine("This is not a valid response. Please answer with a letter value.");
                    }
                    break;
            }



            DisplayContinuePrompt();
        }

        /// <summary>
        /// reads user programming data from txt file
        /// </summary>
        static List<Command> DisplayReadUserProgrammingData()
        {
            string dataPathA = @"Data\Data1.txt";
            string dataPathB = @"Data\Data2.txt";
            string dataPathC = @"Data\Data3.txt";
            List<Command> commands = new List<Command>();
            string[] commandsString;

            DisplayScreenHeader("Load Commands from Data File");
            Console.WriteLine("Which save slot would you like to load from?");
            Console.WriteLine("a) Save Slot A");
            Console.WriteLine("b) Save Slot B");
            Console.WriteLine("c) Save Slot C");
            string userResponse = Console.ReadLine().ToLower();

            Console.WriteLine("Ready to load the commands from the data file.");
            DisplayContinuePrompt();

            switch (userResponse)
            {
                case "a":
                    commandsString = File.ReadAllLines(dataPathA);
                    //
                    // create a list of Command
                    Command command;
                    foreach (string commandString in commandsString)
                    {
                        Enum.TryParse(commandString, out command);

                        commands.Add(command);
                    }
                    break;
                case "b":
                    commandsString = File.ReadAllLines(dataPathB);
                    //
                    // create a list of Command
                    foreach (string commandString in commandsString)
                    {
                        Enum.TryParse(commandString, out command);

                        commands.Add(command);
                    }
                    break;
                case "c":
                    commandsString = File.ReadAllLines(dataPathC);
                    //
                    // create a list of Command
                    foreach (string commandString in commandsString)
                    {
                        Enum.TryParse(commandString, out command);

                        commands.Add(command);
                    }
                    break;
                default:
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Commands loaded successfully.");
            DisplayContinuePrompt();

            return commands;

        }

        /// <summary>
        /// executes commands
        /// </summary>
        static void DisplayExecuteFinchCommands(Finch ros, List<Command> commands, (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters)
        {
            int motorSpeed = commandParameters.motorSpeed;
            int ledBrightness = commandParameters.ledBrightness;
            int waitMilliseconds = commandParameters.waitSeconds * 1000;

            DisplayScreenHeader("Execute Finch Commands");

            Console.WriteLine("Now, the Finch Robot will perform your commands.");
            DisplayContinuePrompt();

            foreach (Command command in commands)
            {
                switch (command)
                {
                    case Command.NONE:
                        break;
                    case Command.MOVEFORWARD:
                        Console.WriteLine("MOVEFORWARD");
                        ros.setMotors(motorSpeed, motorSpeed);
                        break;
                    case Command.MOVEBACKWARD:
                        Console.WriteLine("MOVEBACKWARD");
                        ros.setMotors(-motorSpeed, -motorSpeed);
                        break;
                    case Command.STOPMOTORS:
                        Console.WriteLine("STOPMOTORS");
                        ros.setMotors(0, 0);
                        break;
                    case Command.WAIT:
                        Console.WriteLine("WAIT");
                        ros.wait(waitMilliseconds);
                        break;
                    case Command.TURNRIGHT:
                        Console.WriteLine("TURNRIGHT");
                        ros.setMotors(0, motorSpeed);
                        break;
                    case Command.TURNLEFT:
                        Console.WriteLine("TURNLEFT");
                        ros.setMotors(motorSpeed, 0);
                        break;
                    case Command.LEDON:
                        Console.WriteLine("LEDON");
                        ros.setLED(ledBrightness, ledBrightness, ledBrightness);
                        break;
                    case Command.LEDOFF:
                        Console.WriteLine("LEDOFF");
                        ros.setLED(0, 0, 0);
                        break;
                    case Command.DONE:
                        break;
                    default:
                        break;
                }
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// displays finch commands
        /// </summary>
        static void DisplayFinchCommands(List<Command> commands)
        {
            DisplayScreenHeader("Finch Robot Commands");

            foreach (Command command in commands)
            {
                Console.WriteLine(command);
            }

            DisplayContinuePrompt();
        }

        /// <summary>
        /// gets commands from user
        /// </summary>
        static void DisplayGetFinchCommands(List<Command> commands)
        {
            Command command = Command.NONE;
            string userResponse;
            DisplayScreenHeader("Finch Robot Commands");

            Console.WriteLine("You will be entering commands for the Finch Robot to execute");
            Console.WriteLine("Available Commands: NONE, MOVEFORWARD, MOVEBACKWARD, STOPMOTORS, WAIT, TURNRIGHT, TURNLEFT, LEDON, LEDOFF");
            Console.WriteLine("Please enter 'DONE' when finished entering commands.");
            Console.WriteLine();

            while (command != Command.DONE)
            {
                Console.WriteLine("Enter Command: ");
                userResponse = Console.ReadLine().ToUpper();
                Enum.TryParse(userResponse, out command);

                switch (command)
                {
                    case Command.MOVEFORWARD:
                        commands.Add(command);
                        break;
                    case Command.MOVEBACKWARD:
                        commands.Add(command);
                        break;
                    case Command.STOPMOTORS:
                        commands.Add(command);
                        break;
                    case Command.WAIT:
                        commands.Add(command);
                        break;
                    case Command.TURNRIGHT:
                        commands.Add(command);
                        break;
                    case Command.TURNLEFT:
                        commands.Add(command);
                        break;
                    case Command.LEDON:
                        commands.Add(command);
                        break;
                    case Command.LEDOFF:
                        commands.Add(command);
                        break;
                    case Command.DONE:
                        Console.WriteLine(userResponse);
                        break;
                    default:
                        Console.WriteLine($"{userResponse} is not a valid command. Please enter one of the available commands:");
                        Console.WriteLine("NONE, MOVEFORWARD, MOVEBACKWARD, STOPMOTORS, WAIT, TURNRIGHT, TURNLEFT, LEDON, LEDOFF, DONE");
                        break;
                }

            }


            DisplayContinuePrompt();
        }

        /// <summary>
        /// gets command parameters from user
        /// </summary>
        static (int motorSpeed, int ledBrightness, int waitSeconds) DisplayGetCommandParameters()
        {
            (int motorSpeed, int ledBrightness, int waitSeconds) commandParameters;
            commandParameters.motorSpeed = 0;
            commandParameters.ledBrightness = 0;
            commandParameters.waitSeconds = 0;
            DisplayScreenHeader("Command Parameters");
            Console.WriteLine("Enter Motor Speed [1-255]: ");
            commandParameters.motorSpeed = int.Parse(Console.ReadLine());

            while (commandParameters.motorSpeed < 1 || commandParameters.motorSpeed > 255)
            {
                Console.WriteLine("This is not a valid value. Please enter a value between 1 and 255.");
                commandParameters.motorSpeed = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Enter LED Brightness [1-255]: ");
            commandParameters.ledBrightness = int.Parse(Console.ReadLine());
            while (commandParameters.ledBrightness < 1 || commandParameters.ledBrightness > 255)
            {
                Console.WriteLine("This is not a valid value. Please enter a value between 1 and 255.");
                commandParameters.ledBrightness = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("Enter Wait in Seconds: ");
            commandParameters.waitSeconds = int.Parse(Console.ReadLine());
            while (commandParameters.waitSeconds <= 0)
            {
                Console.WriteLine("That is not a valid number. Please enter a value higher than 0.");
                commandParameters.waitSeconds = int.Parse(Console.ReadLine());
            }

            Console.WriteLine($"Motor Speed: {commandParameters.motorSpeed}");
            Console.WriteLine($"LED Brightness: {commandParameters.ledBrightness}");
            Console.WriteLine($"Wait: {commandParameters.waitSeconds}");


            return commandParameters;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// display welcome screen
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display closing screen
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// connects finch robot
        /// </summary>
        static bool DisplayConnectRos(Finch ros)
                {
                    bool rosConnected = false;

                    DisplayScreenHeader("Connect Finch Robot");

                    Console.WriteLine("Ready to connect to the Finch Robot.");
                    Console.WriteLine("Please be sure to connect the USB cable to the robot and the computer.");
                    DisplayContinuePrompt();

                    rosConnected = ros.connect();

                    if (rosConnected)
                    {
                        ros.setLED(0, 255, 0);
                        ros.noteOn(15000);
                        ros.wait(1000);
                        ros.noteOff();

                        Console.WriteLine();
                        Console.WriteLine("Finch robot is now connected.");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Unable to connect to the Finch robot.");
                    }


                    DisplayContinuePrompt();

                    return rosConnected;
                }

        /// <summary>
            /// disconnects the finch robot
            /// </summary>
        static void DisplayDisconnectRos(Finch ros)
            {

                DisplayScreenHeader("Disconnect Finch Robot");

                Console.WriteLine("Ready to disconnect the Finch Robot.");
                DisplayContinuePrompt();

                ros.disConnect();

                Console.WriteLine();
                Console.WriteLine("Finch robot is now disconnected.");
                DisplayContinuePrompt();
            }

        #endregion

        #region HELPER METHODS

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
