using ChatWP.Entities;

namespace ChatWP
{
    public partial class CreateContact : Form
    {
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
                Id = Guid.NewGuid().ToString(),
                Name = textBox1.Text,
                Host = textBox2.Text,
                Port = Convert.ToInt32(textBox3.Text)
            };

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
