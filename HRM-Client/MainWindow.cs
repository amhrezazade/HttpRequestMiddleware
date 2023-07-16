namespace HRM_Client
{
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void mainWindow_Load(object sender, EventArgs e)
        {
            txtLog.Dock = DockStyle.Fill;

        }

    }
}