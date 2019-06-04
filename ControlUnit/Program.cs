using Entities;
using Microsoft.AspNetCore.SignalR.Client;
using Services;
using Services.Helpers;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using static Services.ViewModels.RealtimeViewModel;

namespace ControlUnit
{
    class Program
    {
        #region Fields

        private static HubConnection _communicationHubConnection;

        private static SerialPort _XPort;

        private static SerialPort _YPort;

        private static int _lastX;

        private static int _lastY;

        private static int _delay = 1000;

        private static int _platsForSeeding = 3;

        private static int _botWidth = 13000;

        private static int _botLength = 32000;

        private static bool _isFull;

        private static FarmBot _farmBot;

        private static Parameters _parameters;

        private static FarmBotService _farmBotService;

        private static ParametersService _parametersService;

        private enum SerialPortMessage
        {
            X1min,
            X2min,
            Ymin,
            Zmin,
            X1max,
            X2max,
            Ymax,
            Zmax
        }

        #endregion

        static void Main(string[] args)
        {
            // Init XPort
            _XPort = new SerialPort();

            _XPort.PortName = "COM9";
            _XPort.BaudRate = 9600;
            _XPort.Parity = Parity.None;
            _XPort.DataBits = 8;
            _XPort.StopBits = StopBits.One;
            _XPort.ReadTimeout = 60000;

            // Init YPort
            _YPort = new SerialPort();

            _YPort.PortName = "COM8";
            _YPort.BaudRate = 9600;
            _YPort.Parity = Parity.None;
            _YPort.DataBits = 8;
            _YPort.StopBits = StopBits.One;
            _YPort.ReadTimeout = 60000;

            // Get comport names
            Console.WriteLine("Enter X motors com port name (ex: COM4):");

            _XPort.PortName = Console.ReadLine();

            // Get comport names
            Console.WriteLine("Enter Y, Z motors com port name (ex: COM4):");

            _YPort.PortName = Console.ReadLine();

            // Message

            // Open ports
            //_XPort.Open();
            //_YPort.Open();

            Console.WriteLine("OK....");

            // Connect
            Task.Run(Connect);

            // Get current parameters
            _farmBotService = new FarmBotService();
            _parametersService = new ParametersService();

            // Get farm bot
            Task.Run(GetFarmBot);

            // Get parameters
            Task.Run(GetParameters);

            // End
            Console.ReadLine();
        }

        #region Commands

        private static async Task GetFarmBot()
        {
            try
            {
                _farmBot = await _farmBotService
                    .GetByIdAsync(TempData.FarmBotId);

                _lastX = _farmBot.LastX;
                _lastY = _farmBot.LastY;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private static async Task GetParameters()
        {
            try
            {
                _parameters = await _parametersService
                    .GetByIdAsync(TempData.FarmBotId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private static async Task Connect()
        {
            // Connect to communication hub
            try
            {
                _communicationHubConnection = new HubConnectionBuilder()
                    .WithUrl("https://farmbotapi.azurewebsites.net/communicationhub")
                    .Build();

                await _communicationHubConnection.StartAsync();

                // Remote control command
                _communicationHubConnection.On<string>("RemoteControl", RemoteControlCommand);

                // Seeding command
                _communicationHubConnection.On<Plant, int>("Seeding", SeedingCommand);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Control motors
        /// </summary>
        /// <param name="message"></param>
        private static void RemoteControlCommand(string message)
        {
            // Convert direction
            Direction direction = (Direction)Enum.Parse(typeof(Direction), message);

            // Check command
            switch (direction)
            {
                case Direction.Forward:

                    _XPort.Write(Forward(32767));

                    break;

                case Direction.Backward:

                    _XPort.Write(Backward(32767));

                    break;

                case Direction.Left:

                    _YPort.Write(Left(14000));

                    break;

                case Direction.Right:

                    _YPort.Write(Right(14000));

                    break;

                case Direction.Up:

                    _YPort.Write(Up(23000));

                    break;

                case Direction.Down:

                    _YPort.Write(Down(23000));

                    break;

                case Direction.Home:

                    _XPort.Write(Backward(32767));

                    WaitX1Min();

                    _YPort.Write(Left(15000));

                    WaitYMin();

                    _YPort.Write(Up(23000));

                    WaitZMin();

                    break;

                case Direction.Stop:

                    _XPort.Write(StopX());
                    _YPort.Write(StopY());
                    _YPort.Write(StopZ());

                    break;
            }
        }

        private static void RemoteControlCommand(Direction direction)
            => RemoteControlCommand(direction.ToString());

        private static void RemoteControlCommand(int impulsesX, int impulsesY)
        {
            _XPort.Write(Forward(impulsesX));

            WaitX();

            _YPort.Write(Right(impulsesY));

            WaitY();
        }

        private static string Left(int distance)
        {
            return $"YL{distance}x";
        }

        private static string Right(int distance)
        {
            return $"YR{distance}x";
        }

        private static string Forward(int distance)
        {
            return $"XF{distance}x";
        }

        private static string Backward(int distance)
        {
            return $"XB{distance}x";
        }

        private static string Up(int distance)
        {
            return $"ZU{distance}x";
        }

        private static string Down(int distance)
        {
            return $"ZD{distance}x";
        }

        private static string StopZ()
        {
            return $"ZSx";
        }

        private static string StopX()
        {
            return $"XSx";
        }

        private static string StopY()
        {
            return $"YSx";
        }

        /// <summary>
        /// Start plants seeding process
        /// </summary>
        /// <param name="plant"></param>
        private static async void SeedingCommand(Plant plant, int totalForSeeding)
        {
            _platsForSeeding = totalForSeeding;

            // Update seeded plants count

            _parameters.SeededPlants += totalForSeeding;

            await _parametersService.UpdateAsync(_parameters);

            // Check if is not full
            if (_isFull)
                return;

            // Reset motors
            RemoteControlCommand(Direction.Home);

            Delay();

            // Go to last position
            RemoteControlCommand(_lastX, _lastY);

            Delay();

            // Seeding
            int plantX;
            int plantY;

            // Convert cm to impulses
            int rowDistance = plant.RowDistance * 190;
            int plantDistance = plant.PlantDistance * 150;
            int seedDepth = plant.SeedDepth * 380;

            // Check bounds
            if (_lastX == 0)
            {
                _XPort.Write(Forward(rowDistance / 2));

                WaitX();
            }

            // Seed
            for (int i = 0; i < _platsForSeeding; i++)
            {
                plantY = _lastY + plantDistance;
                plantX = _lastX;

                // Check bounds
                if (plantX + (1.5 * rowDistance) > _botLength)
                {
                    _isFull = true;
                    break;
                }

                // Seed
                if((plantY + (0.5 * plantDistance)) <= _botWidth)
                {
                    SeedPlant(rowDistance, plantDistance, seedDepth);

                    _lastX = plantX;
                    _lastY = plantY;
                }
                else
                {
                    SeedPlant(rowDistance, plantDistance, seedDepth, true);

                    _lastX = _lastX + rowDistance;
                    _lastY = plantDistance / 2;
                }
            }

            // Save last position on database
            _farmBot.LastX = _lastX;
            _farmBot.LastY = _lastY;

            await _farmBotService.UpdateAsync(_farmBot);

            // Reset motors
            Delay(2500);

            RemoteControlCommand(Direction.Home);

            // Update seeded plants count
            _parameters.SeededPlants = _parameters.SeededPlants + _platsForSeeding;

            await _parametersService.UpdateAsync(_parameters);
        }

        private static void SeedPlant(int impulsesX, int impulsesY, int impulsesZ, bool isNewRow = false)
        {
            // Continue
            if (!isNewRow)
            {
                // If is first plant
                if (_lastY == 0)
                    impulsesY = impulsesY / 2;

                _YPort.Write(Right(impulsesY));

                WaitY();

                _YPort.Write(Down(impulsesZ));

                WaitZ();

                _YPort.Write(Up(impulsesZ));

                WaitZ();
            }

            // New row
            else
            {
                _YPort.Write(Left(_botWidth));

                WaitYMin();

                _XPort.Write(Forward(impulsesX));

                WaitX();

                _YPort.Write(Right(impulsesY / 2));

                WaitY();

                _YPort.Write(Down(impulsesZ));

                WaitZ();

                _YPort.Write(Up(impulsesZ));

                WaitZ();
            }
        }

        #endregion

        #region Waitings

        private static void WaitX1Min()
        {
            string result = ReadPort(_XPort);

            if (result == SerialPortMessage.X1min.ToString())
                return;

            else
                WaitX1Min();
        }

        private static void WaitX2Min()
        {
            string result = ReadPort(_XPort);

            if (result == SerialPortMessage.X2min.ToString())
                return;

            else
                WaitX2Min();
        }

        private static void WaitYMin()
        {
            string result = ReadPort(_YPort);

            if (result == SerialPortMessage.Ymin.ToString())
                return;

            else
                WaitYMin();
        }

        private static void WaitZMin()
        {
            string result = ReadPort(_YPort);

            if (result == SerialPortMessage.Zmin.ToString())
                return;

            else
                WaitZMin();
        }

        private static void WaitY()
        {
            string result = ReadPort(_YPort);

            if (result[0] == 'Y')
                return;

            else
                WaitY();
        }

        private static void WaitX()
        {
            string result = ReadPort(_XPort);

            if (result[0] == 'X' && result[1] == '1')
                return;

            else
                WaitX();
        }

        private static void WaitZ()
        {
            string result = ReadPort(_YPort);

            if (result[0] == 'Z')
                return;

            else
                WaitZ();
        }

        #endregion

        #region Helpers

        private static SerialPortMessage ParseSeialPortMessage(string serialPortMessage)
        {
            return (SerialPortMessage)Enum.Parse(typeof(SerialPortMessage), serialPortMessage);
        }

        private static void Delay(int delay = 1000)
        {
            Thread.Sleep(_delay);
        }

        private static string ReadPort(SerialPort serialPort)
        {
            string result = null;

            try
            {
                while (string.IsNullOrEmpty(result))
                    result = serialPort.ReadLine();

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return result;
        }

        #endregion
    }
}
