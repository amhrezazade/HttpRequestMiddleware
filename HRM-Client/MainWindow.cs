

using HRMDomain.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using System.Text;

namespace HRM_Client
{
    public partial class MainWindow : Form
    {
        private readonly HubService _hubService;
        private readonly RequestService _requestService;
        public MainWindow()
        {
            InitializeComponent();
            _hubService = new HubService();
            _requestService = new RequestService();
            _hubService.OnHandleRequest += HandleRequest;
        }

        private void Log(string message)
        {
            txtLog.Text += DateTime.Now.ToString() + " >  " + message + "\r\n";
        }

        private async Task HandleRequest(Request request)
        {
            //Log("Request : Id = " + request.Id.ToString());
            Response response = await _requestService.HandleRequest(request);
            string json = JsonConvert.SerializeObject(response);
            await _hubService.SendMessage(json);
            //Log("Responsed Id =" + request.Id + " - Result :" + response.StatusCode.ToString());

        }


        private void mainWindow_Load(object sender, EventArgs e)
        {
            if (!_hubService.Ready)
            {
                MessageBox.Show("Application Failed to init. Check Config.json", "Lunch Error");
                Close();
            }
            timer.Enabled = true;
            pnlMain.Dock = DockStyle.Fill;
            txtLog.Dock = DockStyle.Fill;
        }

        private async void timer_Tick(object sender, EventArgs e)
        {
            lblState.Text = _hubService.State.ToString();

            if (_hubService.State == HubConnectionState.Disconnected)
                await _hubService.Start();

        }

        private async void btnStart_Click(object sender, EventArgs e)
        {
            timer.Enabled = !timer.Enabled;
            if (timer.Enabled)
                btnStart.Text = "Stope";
            else
            {
                lblState.Text = "Stopped";
                btnStart.Text = "Start";
                await _hubService.Stope();
            }

        }

        private void lblState_TextChanged(object sender, EventArgs e)
        {
            Log("Hub Connection :" + lblState.Text);
        }
    }
}