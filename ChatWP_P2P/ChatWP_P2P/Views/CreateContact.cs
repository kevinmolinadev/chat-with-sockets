using System.ComponentModel;
using ChatWP_P2P.Entities;

namespace ChatWP_P2P.Views
{
    public partial class CreateContact : Form
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public UserConnection? user { get; private set; }

        public CreateContact()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("All fields are required.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
                
            user = new UserConnection
            {
                Name = textBox1.Text,
                Host = textBox2.Text,
                Port = Convert.ToInt32(textBox3.Text)
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
