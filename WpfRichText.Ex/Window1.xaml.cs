using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Linq.Expressions;
using WpfRichText.Ex.Commands;
using System.IO;

namespace WpfRichText.Ex
{
   
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.DataContext = new PageViewModel();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MenuItem_About_Prog(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Данное приложения было создано во время прохождения курса по Современным технологиям прграммирования в НТТУ КПИ на 3-ем курсе. \n Программа создана студентом ИА-31. \n Автор: Загорулько Андрей Викторович ");
        }
        private void MenuItem_File_Open(object sender, RoutedEventArgs e)
        {
            

            //MainMenu mnuFileMenu = new MainMenu();
            //this.Menu = mnuFileMenu;
            //MenuItem MenuItemFile = new MenuItem("&File");
            //MenuItemFile.MenuItems.Add("Open", new System.EventHandler(this.MenuOpen_Click));
            //mnuFileMenu.MenuItems.Add(MenuItemFile);
        }
        private void MenuItem_File_Save(object sender, RoutedEventArgs e)
        {
            FileStream fs = new FileStream("d:\\test.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter w = new StreamWriter(fs);
            w.Write("Test write ");
            w.Flush();
            w.Close(); 
        }
    }

    #region PageViewModel
    public class PageViewModel : ObservableBase
    {  
        public DelegateCommand GetXamlCommand { get; private set; }

        #region Constructor
        public PageViewModel()
        {
            GetXamlCommand = new DelegateCommand(() =>
            {
                MessageBox.Show(this.Text);
            });
        }
        #endregion
        
        #region Name
        private string text = string.Empty;
        public string Text
        {
            get
            {
                return text;
            }
            set
            {
                text = value;
                this.RaisePropertyChanged(p => p.Text);
            }
        }
        #endregion
    }
    #endregion

    #region Observable
    public abstract class ObservableBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public static class ObservableBaseEx
    {
        public static void RaisePropertyChanged<T, TProperty>(this T observableBase, Expression<Func<T, TProperty>> expression) where T : ObservableBase
        {
            observableBase.RaisePropertyChanged(observableBase.GetPropertyName(expression));
        }

        public static string GetPropertyName<T, TProperty>(this T owner, Expression<Func<T, TProperty>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            if (memberExpression == null)
            {
                var unaryExpression = expression.Body as UnaryExpression;
                if (unaryExpression != null)
                {
                    memberExpression = unaryExpression.Operand as MemberExpression;

                    if (memberExpression == null)
                        throw new NotImplementedException();
                }
                else
                    throw new NotImplementedException();
            }

            var propertyName = memberExpression.Member.Name;
            return propertyName;
        }
    }
    #endregion
}
