using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
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
using VendingMachines.Desktop.Services;

namespace VendingMachines.Desktop.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            //MessageBox.Show("TOKEN=" + ApiService.Token);
            //var me = Services.ApiService.Get<JsonDocument>("/api/auth/me").RootElement;

            //var name = me.GetProperty("name").GetString();
            //var role = me.GetProperty("role").GetString();

            //MessageBox.Show($"Здравствуйте, {name}, вы вошли под ролью {role}!");
        }
    }
}
