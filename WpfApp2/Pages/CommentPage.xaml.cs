using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
	public partial class CommentPage : Page {
		private Post post;
		public ObservableCollection<Comment> Comments { get; set; }

		public CommentPage(Post post) {
			InitializeComponent();

			Comments = new ObservableCollection<Comment>(post.Comments.ToList());

			this.post = post;

			DataContext = this;
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			string commentText = tbCommentText.Text.Trim();
			if (commentText.Length == 0)
				return;

			Comment comment = new Comment() {
				Sender = MainWindow.User.ID,
				Post = post.ID,
				Message = commentText,
				Date = DateTime.Now,
			};

			MainWindow.Connection.Comments.Add(comment);
			MainWindow.Connection.SaveChanges();

			Comments.Add(comment);

			tbCommentText.Clear();
		}
	}
}
