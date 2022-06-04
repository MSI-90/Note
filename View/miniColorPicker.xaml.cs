using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace Note.View
{
    /// <summary>
    /// Логика взаимодействия для miniColorPicker.xaml
    /// </summary>
    public partial class miniColorPicker : Window
    {
        MyColor myDelegate;
        public miniColorPicker(MyColor sender)
        {   
            myDelegate = sender;
            InitializeComponent();
            cmbColors.ItemsSource = typeof(Colors).GetProperties();
        }

        private void cmbColors_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) 
        {
            Color selectedColor = (Color)(cmbColors.SelectedItem as PropertyInfo).GetValue(null, null);
            myDelegate(selectedColor);
        }
    }
}