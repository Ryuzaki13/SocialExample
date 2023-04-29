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

namespace WpfApp2.Pages {
	public partial class AuthorizationPage : Page {
		public AuthorizationPage() {
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			string login = tbLogin.Text.Trim();
			string password = pbPassword.Password.Trim();

			if (login.Length == 0 || password.Length == 0) {
				MessageBox.Show("Введите данные");
				return;
			}

			MainWindow.User = MainWindow.Connection.Accounts.Where(acc => acc.Email == login && acc.Password == password).FirstOrDefault();
			if (MainWindow.User == null) {
				MessageBox.Show("Неправильный логин/пароль");
				return;
			}

			NavigationService.Navigate(new PostPage());
		}
	}
}
