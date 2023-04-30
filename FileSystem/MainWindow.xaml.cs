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
using System.IO;
using Microsoft.Win32;
using System.Diagnostics;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;
using static System.Net.Mime.MediaTypeNames;
using System.Collections.ObjectModel;

namespace FileSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>"Не удается найти указанный файл
    public partial class MainWindow : Window
    {
        
        private string PathCatalogy { get; set; }
        private bool can = false;
        public ObservableCollection<string> allFiles { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog selectCatalogy = new FolderBrowserDialog();
            selectCatalogy.RootFolder = Environment.SpecialFolder.UserProfile;
            selectCatalogy.ShowNewFolderButton = false;
            if(selectCatalogy.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                MessageBox.Show("Вот ты пупсик");
            }
            PathCatalogy = selectCatalogy.SelectedPath;
            if(PathCatalogy != null)
            {
                showPath.Text = PathCatalogy;
                showPath.Visibility = Visibility.Visible;
                can = true;
                createFile.Opacity = 1;
            }
            allFiles = new ObservableCollection<string> ((Directory.GetFiles("C:\\Users\\user\\Documents\\", "*.txt", searchOption: SearchOption.TopDirectoryOnly)).ToList());
            DataContext = this;
            /* foreach (var tap in fap)
            {
                allFiles.Add(tap);
            }*/


            /*FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.UserProfile;
            dialog.SelectedPath = "C:\\Users\\user\\Documents";
            dialog.Description = "Веберите или создайте папку для сохранения файлов. Казел";
            dialog.ShowNewFolderButton = false;
            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PathCatalogy = dialog.SelectedPath;
            }
            else
            {
                MessageBox.Show("Не была выбрана папка", "", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(textFile.Text.Length < 1) { return; }
            Directory.CreateDirectory(PathCatalogy + $"\\{textFile.Text}");
            PathCatalogy = PathCatalogy + $"\\{textFile.Text}";
            textFile.Clear();*/
        }

        private /*async*/ void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(nameFile.Text.Length < 1) { MessageBox.Show("Неа"); return; }

            if (!can) { MessageBox.Show("Выбере место сохранения"); return; }
            if(!File.Exists(PathCatalogy))
            {
                using(StreamWriter stream = new StreamWriter(PathCatalogy + $"\\{nameFile.Text}.txt"))
                {
                    stream.WriteLine(textBox.Text);
                    textBox.Clear();
                    nameFile.Clear();

                }
            }
            allFiles = new ObservableCollection<string>((Directory.GetFiles("C:\\Users\\user\\Documents\\", "*.txt", searchOption: SearchOption.TopDirectoryOnly)).ToList());
            listFiles.ItemsSource = allFiles;   


            /*
             string textInFile = textBox.Text;

             FileStream stream = new FileStream(PathCatalogy +"\\hui.txt", FileMode.OpenOrCreate);
             byte[] buffer = Encoding.Default.GetBytes(textInFile);
             await stream.WriteAsync(buffer, 0, buffer.Length);*/



        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ListView listView = sender as System.Windows.Controls.ListView;
            var file = listView.SelectedItem;
            var srt = File.ReadAllLines(file.ToString());
            string lines = default(string);
            foreach( var line in srt )
            {
                lines += line;
            }

            string zz = file.ToString().Remove(file.ToString().Length - 4);
            nameFile.Text = zz.Substring(PathCatalogy.Length + 1);
            textBox.Text = lines;
        }  
    }
}
