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

namespace DiagnoseApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int diagNum = 0;
        string diagText;
        string resp = "";
        int redir = 0;
        DiagnoseAction diagact;
        public MainWindow()
        {
            diagact = new DiagnoseAction();
            diagact.diagSection = 10;
            
            InitializeComponent();
        }
        private void responseClicked(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            this.resp = btn.Name;
            string[] response = { };
            if (redir != 0 && btn.Name == "Y")
                diagact.diagSection = redir;
            redir = 0;
            if (btn.Name == "back" && diagNum == 0) ;
            else {

                if (btn.Name == "back")
                    response = diagact.ReturnPrev(diagNum).Split('*');
                else
                    response = diagact.DecideWithResponse(this.resp, diagNum).Split('*');
                diagNum = int.Parse(response[1]);
                diagText = response[0];
                if (diagText.Contains("report"))
                {
                    diagText = response[0].Substring(0, response[0].IndexOf("report"));
                    MessageBox.Show(diagText, "Troubleshooting Report", MessageBoxButton.OK, MessageBoxImage.Information);
                    new MenuPage().Show();
                    this.Close();
                }
                else if (diagText.Contains("redirect"))
                {
                    diagText = response[0].Substring(0, response[0].IndexOf("redirect"));
                    questlbl.Text = diagText;
                    redir = diagNum;
                    //diagact.diagSection = diagNum;
                }
                else
                {
                    questlbl.Text = diagText;
                }
            }
        }
    }
}
