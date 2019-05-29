using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenTokSDK;

namespace ControlUnit
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Connect
            int ApiKey = 46335352;
            string ApiSecret = "57147251cea4ecee2f43d5ba271be573bbb4c4ec";
            var OpenTok = new OpenTok(ApiKey, ApiSecret);

            Session session = OpenTok.CreateSession();

            string token = OpenTok.GenerateToken(session.Id);

            Console.WriteLine("OpenTok Token => " + token);
            Console.WriteLine("OpenTok SessionId => " + session.Id);
        }
    }
}
