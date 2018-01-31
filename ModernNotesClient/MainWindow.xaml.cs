using Newtonsoft.Json;
using RestSharp;
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

namespace ModernNotesClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        private readonly string _baseUri = "http://localhost:51382/";

        public MainWindow()
        {   
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var restClient = new RestClient(new Uri(_baseUri));

            var request = new RestRequest("api/notes/getnotes", Method.GET);

            var response = restClient.Execute(request);

            
        }
    }
}
