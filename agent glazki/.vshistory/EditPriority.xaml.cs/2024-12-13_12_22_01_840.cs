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
using System.Windows.Shapes;

namespace agent_glazki
{
    /// <summary>
    /// Логика взаимодействия для EditPriority.xaml
    /// </summary>
    public partial class EditPriority : Window
    {
        public EditPriority(int maxPriority)
        {
            InitializeComponent();
            TBoxPriority.Text = maxPriority.ToString();
        }

        private void EditPriorityBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

    }
}
