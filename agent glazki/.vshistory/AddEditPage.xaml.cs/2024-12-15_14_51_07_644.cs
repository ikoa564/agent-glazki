using Microsoft.Win32;
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
    /// Логика взаимодействия для AddEditPage.xaml
    /// </summary>
    public partial class AddEditPage : Page
    {
        private Agent _currentAgent = new Agent();
        private ProductSale _currentProductSale = new ProductSale();
        public AddEditPage(Agent SelectedAgent)
        {
            InitializeComponent();


            if (SelectedAgent != null)
            {
                _currentAgent = SelectedAgent;
                ComboType.SelectedIndex = _currentAgent.AgentTypeID - 1;
            }
            var _currentProductSaleForAgent = AbdeevGlazkiSaveEntities.GetContext().ProductSale
                .Where(p => p.AgentID == _currentAgent.ID).ToList();
            ProductSaleListView.ItemsSource = _currentProductSaleForAgent;

            //var productNames = AbdeevGlazkiSaveEntities.GetContext()
            //    .ProductSale
            //    .ToList() // Загружаем данные в память
            //    .Select(p => p.ProductName) // Выбираем только ProductName
            //    .Distinct() // Убираем дубликаты, если нужно
            //    .ToList();

            var productNames = AbdeevGlazkiSaveEntities.GetContext().Product.Select(p => p.Title).ToList();
            ProductNameComboBox.ItemsSource = productNames;

            DataContext = _currentAgent;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentAgent.Title))
                errors.AppendLine("Укажите наименование агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.Address))
                errors.AppendLine("Укажите адрес агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.DirectorName))
                errors.AppendLine("Укажите ФИО директора");
            if (ComboType.SelectedItem == null)
                errors.AppendLine("Укажите тип агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.Priority.ToString()))
                errors.AppendLine("Укажите приоритет агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.INN))
                errors.AppendLine("Укажите ИНН агента");
            else if (_currentAgent.INN.Length != 10)
                errors.AppendLine("Укажите 10 символов ИНН");
            if (string.IsNullOrWhiteSpace(_currentAgent.KPP))
                errors.AppendLine("Укажите КПП агента");
            else if (_currentAgent.KPP.Length != 9)
                errors.AppendLine("Укажите 9 символов КПП");
            if (string.IsNullOrWhiteSpace(_currentAgent.Logo)) ;
            else if (_currentAgent.Logo.Length >= 100)
                errors.AppendLine("Укажите короткий путь для картинки! (100 символов)");
            if (_currentAgent.Priority <= 0)
                errors.AppendLine("Укажите положительный приоритет агента");
            if (string.IsNullOrWhiteSpace(_currentAgent.Phone))
                errors.AppendLine("Укажите телефон агента");
            else
            {
                string ph = _currentAgent.Phone.Replace("(", "").Replace("-", "").Replace("+", "");
                if (((ph[1] == '9' || ph[1] == '4' || ph[1] == '8') && ph.Length != 11) || (ph[1] == '3' && ph.Length != 12))
                    errors.AppendLine("Укажите правильно телефон агента");
            }
            if (string.IsNullOrWhiteSpace(_currentAgent.Email))
                errors.AppendLine("Укажите почту агента");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            _currentAgent.AgentTypeID = ComboType.SelectedIndex + 1;

            if (_currentAgent.ID == 0)
                AbdeevGlazkiSaveEntities.GetContext().Agent.Add(_currentAgent);
            try
            {
                AbdeevGlazkiSaveEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void ChangePictureBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog myOpenFileDialog = new OpenFileDialog();
            if (myOpenFileDialog.ShowDialog() == true)
            {
                string path = myOpenFileDialog.FileName;

                // Находим индекс слова "agents"
                int index = path.LastIndexOf("agents");

                // Извлекаем подстроку, начиная от слова "agents"
                path = "\\" + path.Substring(index);
                _currentAgent.Logo = path;
                LogoImage.Source = new BitmapImage(new Uri(myOpenFileDialog.FileName));
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var currentRealizeProduct = AbdeevGlazkiSaveEntities.GetContext().ProductSale.ToList();
            currentRealizeProduct = currentRealizeProduct.Where(p => p.AgentID == _currentAgent.ID).ToList();

            var currentAgentPriorityHistory = AbdeevGlazkiSaveEntities.GetContext().AgentPriorityHistory.ToList();
            currentAgentPriorityHistory = currentAgentPriorityHistory.Where(p => p.AgentID == _currentAgent.ID).ToList();

            var currentShop = AbdeevGlazkiSaveEntities.GetContext().Shop.ToList();
            currentShop = currentShop.Where(p => p.AgentID == _currentAgent.ID).ToList();


            if (currentRealizeProduct.Count != 0)
                MessageBox.Show("Невозможно выполнить удаление, т.к. существуют информация о реализации продукции");
            else
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        AbdeevGlazkiSaveEntities.GetContext().Agent.Remove(_currentAgent);
                        if (currentAgentPriorityHistory.Count != 0)
                        {
                            for (int i = 0; currentAgentPriorityHistory.Count == i; i++)
                                AbdeevGlazkiSaveEntities.GetContext().AgentPriorityHistory.Remove(currentAgentPriorityHistory[i]);
                        }
                        if (currentShop.Count != 0)
                        {
                            for (int i = 0; currentShop.Count == i; i++)
                                AbdeevGlazkiSaveEntities.GetContext().Shop.Remove(currentShop[i]);
                        }
                        AbdeevGlazkiSaveEntities.GetContext().SaveChanges();
                        Manager.MainFrame.GoBack();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
        }

        private void UpdateProductSale()
        {
            var _currentProductSale = AbdeevGlazkiSaveEntities.GetContext().ProductSale
                .Where(p => p.AgentID == _currentAgent.ID).ToList();

            //_currentProductSale = _currentProductSale.Where(p =>
            //    p.ProductName.ToLower().Contains(SearchTB.Text.ToLower())).ToList();

            ProductSaleListView.ItemsSource = _currentProductSale;
        }

        private void ProductSale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DeleteProductSale_Click(object sender, RoutedEventArgs e)
        {
            var selected = ProductSaleListView.SelectedItems;

            if (selected.Count >= 1)
            {
                if (MessageBox.Show("Вы точно хотите выполнить удаление?", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        //AbdeevGlazkiSaveEntities.GetContext().ProductSale.Remove(selected);
                        AbdeevGlazkiSaveEntities.GetContext().SaveChanges();
                        UpdateProductSale();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message.ToString());
                    }
                }
            }
            else
                MessageBox.Show("Выберите позиции!");

        }

        private void AddProductSale_Click(object sender, RoutedEventArgs e)
        {
            //var _currentProductSale = AbdeevGlazkiSaveEntities.GetContext().ProductSale.ToList();
            StringBuilder errors = new StringBuilder();
            if (ProductNameComboBox.SelectedIndex == -1)
                errors.AppendLine("Выберите наименование продукта");
            if (string.IsNullOrWhiteSpace(ProductCountTB.Text))
                errors.AppendLine("Укажите количество продукта");
            if (DatePickerProduct.SelectedDate == null || DatePickerProduct.SelectedDate.Value.Date > DateTime.Today)
                errors.AppendLine("Укажите дату продажи или дату, которая идет на предыдущие дни");

            if (errors.Length > 0)
                MessageBox.Show(errors.ToString());
            else
            {
                _currentProductSale.ProductID = ProductNameComboBox.SelectedIndex + 1;

                _currentProductSale.AgentID = _currentAgent.ID;
                _currentProductSale.ProductCount = Convert.ToInt32(ProductCountTB.Text);
                _currentProductSale.SaleDate = DatePickerProduct.SelectedDate.Value;

                if (_currentProductSale.ID == 0)
                    AbdeevGlazkiSaveEntities.GetContext().ProductSale.Add(_currentProductSale);
                try
                {
                    AbdeevGlazkiSaveEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");
                    UpdateProductSale();
                    ProductCountTB.Clear();
                    DatePickerProduct.SelectedDate = null;
                    ProductNameComboBox.SelectedIndex = -1;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }

        }

    }
}
