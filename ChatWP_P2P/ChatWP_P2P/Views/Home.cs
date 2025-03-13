using ChatWP_P2P.Entities;
using ChatWP_P2P.Services;

namespace ChatWP_P2P.Views
{
    public partial class Home : Form
    {
        private readonly SocketService _socketService;
        public Home(SocketService socketService)
        {
            InitializeComponent();
            _socketService = socketService;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = textBox1.Text;
            if (!String.IsNullOrEmpty(name))
            {
                _socketService.Start(name);

                this.Hide();
                var form1 = new Form1(_socketService, new UserConnection
                {
                    Name = name,
                    Host = _socketService.Ip,
                    Port = _socketService.Port
                });


                form1.FormClosed += (s, args) => { this.Invoke(new Action(this.Close)); };

                form1.Show();
            }
            else
            {
                MessageBox.Show("The Name is required", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
