using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
	public partial class PostPage : Page {
		public ObservableCollection<Post> Posts { get; set; }
		public ObservableCollection<SortItem> SortItems { get; set; } = new ObservableCollection<SortItem>() {
			new SortItem() { Name = "Сначала самые последние", Description = new SortDescription("Date", ListSortDirection.Descending) },
			new SortItem() { Name = "Сначала самые популярные", Description = new SortDescription("Reactions.Count", ListSortDirection.Descending) },
		};
		public SortItem SelectedSortItem { get; set; }

		public PostPage() {
			InitializeComponent();

			Posts = new ObservableCollection<Post>(MainWindow.Connection.Posts.OrderByDescending(p => p.Date).ToList());

			DataContext = this;
		}

		private void Sort() {
			var view = CollectionViewSource.GetDefaultView(lvPosts.ItemsSource);
			if (view == null)
				return;

			view.SortDescriptions.Clear();
			view.SortDescriptions.Add(SelectedSortItem.Description);
		}

		private void SendMessage(object sender, RoutedEventArgs e) {
			string message = tbMessage.Text.Trim();
			if (message.Length == 0)
				return;

			Post post = new Post() {
				Sender = MainWindow.User.ID,
				Message = message,
				Date = DateTime.Now,
			};

			MainWindow.Connection.Posts.Add(post);
			MainWindow.Connection.SaveChanges();

			Posts.Add(post);

			tbMessage.Clear();
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			if (lvPosts.SelectedItem == null)
				return;

			var selectedPost = lvPosts.SelectedItem as Post;
			if (selectedPost != null) {
				Reaction reaction = new Reaction() {
					Sender = MainWindow.User.ID,
					Post = selectedPost.ID,
				};

				MainWindow.Connection.Reactions.Add(reaction);
				MainWindow.Connection.SaveChanges();
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e) {
			if (lvPosts.SelectedItem == null)
				return;

			NavigationService.Navigate(new CommentPage(lvPosts.SelectedItem as Post));
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			Sort();
		}
	}
}
