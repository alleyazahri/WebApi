using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using WpfApplication1.Data;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeJiraTaskList();
        }

        private void InitializeJiraTaskList()
        {
            // Add columns
            var gridView = new GridView();
            jiraTaskList.View = gridView;
            var width = this.Width - 100;
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Task To Follow",
                DisplayMemberBinding = new Binding("DevTask"),
                Width = width * .15
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "My Task",
                DisplayMemberBinding = new Binding("MyTask"),
                Width = width * .15
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Status",
                Width = width * .1,
                DisplayMemberBinding = new Binding("Status")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Name",
                CellTemplate = GetDataTemplate("TaskName"),
                Width = width * .25
                //DisplayMemberBinding = new Binding("TaskName")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Description",
                CellTemplate = GetDataTemplate("TaskDescription"),
                Width = width * .35
                //new TextBlock() { TextWrapping = TextWrapping.Wrap })
            });

            // Populate list
            JiraController jc = new JiraController();
            jiraTaskList.ItemsSource = //jc.ParseJiraTasks();
            new List<JiraIssue>()
            {
                new JiraIssue { DevTask = "XWESVC-123", MyTask = "XWESVC-124", Status = "Test", TaskDescription = "Stuff", TaskName = "Things"},
                new JiraIssue
                {
                    DevTask = "XWESVC-321",
                    MyTask = "XWESVC-322",
                    Status = "Beta",
                    TaskDescription = "Here will be a ridiculously long task description just for fun and we shall see how it works and if it word wraps and such and things and stuff and yeah..................You should do things Here will be a ridiculously long task description just for fun and we shall see how it works and if it word wraps and such and things and stuff and yeah..................You should do things Here will be a ridiculously long task description just for fun and we shall see how it works and if it word wraps and such and things and stuff and yeah..................You should do things Here will be a ridiculously long task description just for fun and we shall see how it works and if it word wraps and such and things and stuff and yeah..................You should do things Here will be a ridiculously long task description just for fun and we shall see how it works and if it word wraps and such and things and stuff and yeah..................You should do things Here will be a ridiculously long task description just for fun and we shall see how it works and if it word wraps and such and things and stuff and yeah..................You should do things Here will be a ridiculously long task description just for fun and we shall see how it works and if it word wraps and such and things and stuff and yeah..................You should do things",
                    TaskName = "Here is a very long task name that is hopefully longer than the default size for this particular column"
                }
            };
            //var thingie = jiraTaskList.Items[0] as ListViewItem;
            //jiraTaskList.Style = new Style()
            //{
            //    Triggers =
            //    {
            //        new DataTrigger {Setters = {new Setter(BackgroundProperty, Brushes.Aqua)}, Value = "Test"}
            //    },
            //    Resources = new ResourceDictionary()
            //};
        }

        private static DataTemplate GetDataTemplate(string bindingElement)
        {
            DataTemplate template = new DataTemplate();
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(TextBlock));
            factory.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Right);
            factory.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
            factory.SetBinding(TextBlock.TextProperty, new Binding(bindingElement));
            factory.SetValue(TextBlock.BackgroundProperty, new SolidColorBrush() { Color = Color.FromRgb(7, 203, 224) });
            template.VisualTree = factory;

            return template;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var gridstuffs = jiraTaskList.View as GridView;
            if (gridstuffs != null && e.NewSize.Width > 360)
            {
                gridstuffs.Columns.Last().Width = e.NewSize.Width - 360;
            }
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Open a new window with more information about the task! =D
            var s = jiraTaskList.SelectedItem as JiraIssue;
            var taskWindow = new TaskWindow();
            taskWindow.taskWindowLabel.Content = s.DevTask;

            taskWindow.taskWindowTextBlock.Text = "All sorts of stuff"; // TextBox() { Text = "All sorts of stuff" }};
            taskWindow.Show();
        }
    }
}