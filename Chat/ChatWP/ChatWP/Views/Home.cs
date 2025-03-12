using ChatWP.Controllers;
using ChatWP.Dtos;
using ChatWP.Entities;

namespace ChatWP.Views
{
    public partial class Home : Form
    {
        private readonly SocketController _socketController;
        private string _host;
        private int _port;
        public Home(SocketController socketController)
        {
            InitializeComponent();
            _socketController = socketController;
            _socketController.OnMessageReceived = LoadMessageFromServer;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoadMessageFromServer(SocketResponse response)
        {

            if (InvokeRequired)
            {
                Invoke(new Action(() => LoadMessageFromServer(response)));
                return;
            }

            var user = new UserConnection
            {
                Id = response.FromUser,
                Name = response.Data,
                Host = _host,
                Port = _port
            };

            this.Hide();
            _socketController.OnMessageReceived = null;
            var form1 = new Form1(_socketController, user);

            form1.FormClosed += (s, args) => {
                this.Close();
            };

            form1.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = textBox1.Text;
            _host = textBox2.Text;
            if (String.IsNullOrEmpty(name) || String.IsNullOrEmpty(_host) || String.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("All fields are required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            _port = int.Parse(textBox3.Text);
            _socketController.ConnectToServer(name,_host,_port);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
