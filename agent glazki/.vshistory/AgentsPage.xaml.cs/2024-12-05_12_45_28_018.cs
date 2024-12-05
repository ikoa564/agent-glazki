﻿using System;
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
        }
        currentAgents.Where(a => a.AgentType.Contains(TBoxSearch.Text)).ToList();
        private void UpdateAgents()
        {
            var currentAgents = AbdeevGlazkiSaveEntities.GetContext().Agent.ToList();]

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
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
