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
using System.Windows.Shapes;

namespace agent_glazki
{
    /// <summary>
    /// Логика взаимодействия для EditPriority.xaml
    /// </summary>
    public partial class EditPriority : Window
    {
        private Agent _currentAgent = new Agent();
        public EditPriority(Agent SelectedAgent)
        {
            InitializeComponent();
            if (SelectedAgent != null)
            {
                _currentAgent = SelectedAgent;
            }
            DataContext = _currentAgent;

        }

        private void EditPriorityBtn_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Вы точно хотите изменить приоритет?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                             
                    TBoxPriority.Text = _currentAgent.Priority.ToString();

                    MessageBox.Show("Приоритет изменен");
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }
    }
}
