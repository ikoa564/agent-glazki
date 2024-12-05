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

namespace agent_glazki
{
    /// <summary>
    /// Логика взаимодействия для AgentsPage.xaml
    /// </summary>
    public partial class AgentsPage : Page
    {
        public AgentsPage()
        {
            InitializeComponent();
            var currentAgents = AbdeevGlazkiSaveEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = currentAgents;
            ComboSort.SelectedIndex = 0;
            ComboType.SelectedIndex = 0;

            UpdateAgents();
        }
        private void UpdateAgents()
        {
            var currentAgents = AbdeevGlazkiSaveEntities.GetContext().Agent.ToList();

            if (ComboType.SelectedIndex == 1)
                currentAgents = currentAgents.Where(p => p.AgentTypeID==1).ToList();
            if (ComboType.SelectedIndex == 2)
                currentAgents = currentAgents.Where(p => p.AgentTypeID==2).ToList();
            if (ComboType.SelectedIndex == 3)
                currentAgents = currentAgents.Where(p => p.AgentTypeID==3).ToList();
            if (ComboType.SelectedIndex == 4)
                currentAgents = currentAgents.Where(p => p.AgentTypeID==4).ToList();
            if (ComboType.SelectedIndex == 5)
                currentAgents = currentAgents.Where(p => p.AgentTypeID==5).ToList();

            if (ComboSort.SelectedIndex == 1)
                AgentListView.ItemsSource = currentAgents.OrderBy(p => p.Title).ToList();
            if (ComboSort.SelectedIndex == 2)
                AgentListView.ItemsSource = currentAgents.OrderByDescending(p => p.Title).ToList();
            if (ComboSort.SelectedIndex == 3)
                AgentListView.ItemsSource = currentAgents.OrderBy(p => p.Discount).ToList();
            if (ComboSort.SelectedIndex == 4)
                AgentListView.ItemsSource = currentAgents.OrderByDescending(p => p.Discount).ToList();
            if (ComboSort.SelectedIndex == 5)
                AgentListView.ItemsSource = currentAgents.OrderBy(p => p.Priority).ToList();
            if (ComboSort.SelectedIndex == 6)
                AgentListView.ItemsSource = currentAgents.OrderByDescending(p => p.Priority).ToList();

            currentAgents = currentAgents.Where(p => p.Title.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            currentAgents = currentAgents.Where(p => p.Email.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();
            currentAgents = currentAgents.Where(p => p.Phone.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            AgentListView.ItemsSource = currentAgents.ToList();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateAgents();

        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();

        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateAgents();

        }
    }
}
