using ChatWP.Controllers;
using ChatWP.Dtos;
using ChatWP.Entities;
using System.Text.Json;

namespace ChatWP.Views
{
    public partial class Form1 : Form
    {

        private readonly SocketController _sockerController;
        private readonly UserConnection _user;
        private UserConnection _targetUser;
        private Dictionary<string, List<string>> _chats = new Dictionary<string, List<string>>();
        private List<string> _chat = new List<string>();
        public Form1(SocketController socketController, UserConnection user)
        {
            InitializeComponent();
            _sockerController = socketController;
            _sockerController.OnMessageReceived = LoadMessagesFromServer;
            _user = user;
            LoadUsers();
            ConfigChatList();
        }


        private void ConfigChatList()
        {
            list_messages.View = View.Details;
            list_messages.Columns.Add("Mensajes", -2, HorizontalAlignment.Left);
            list_messages.HeaderStyle = ColumnHeaderStyle.None;
        }

        private void InsertToChat(string chatId, string message)
        {
            if (_targetUser?.Id == chatId)
            {
                _chat.Add(message);
                list_messages.Items.Add(new ListViewItem(message));
            }
            else
            {
                int index = list_contacts.Items.Cast<UserConnection>().ToList().FindIndex(user => user.Id == chatId);
                var contact = list_contacts.Items.Cast<UserConnection>().FirstOrDefault(user => user.Id == chatId);
                if (index == -1 || contact is null) return;
                _chats[chatId].Add(message);
                contact.PublicName = $"💬 {contact.Name}";
                list_contacts.Items[index] = contact;
                list_contacts.Refresh();

            }
        }

        private void InsertToContacts(UserConnection user)
        {
            list_contacts.Items.Add(user);
            _chats.Add(user.Id, []);
        }

        private void LoadChat()
        {
            list_messages.Items.Clear();

            foreach (var message in _chat)
            {
                list_messages.Items.Add(new ListViewItem(message));
            }
        }


        private void LoadUsers()
        {
            _sockerController.SendMessage(_user.Id, "_", "/users", MessageType.GetUsers);
        }

        private void LoadMessagesFromServer(SocketResponse response)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => LoadMessagesFromServer(response)));
                return;
            }

            switch (response.Type)
            {
                case MessageType.Register:
                    InsertToContacts(new UserConnection
                    {
                        Id = response.FromUser,
                        Name = response.Data,
                        Host = _sockerController.Host,
                        Port = _sockerController.Port
                    });
                    break;
                case MessageType.GetUsers:
                    _chats.Clear();
                    list_contacts.Items.Clear();
                    var users = JsonSerializer.Deserialize<List<UserConnection>>(response.Data);
                    if (users is null) break;
                    foreach (var user in users)
                    {
                        if (user.Id == _user.Id) continue;
                        user.Host = _sockerController.Host;
                        user.Port = _sockerController.Port;
                        InsertToContacts(user);
                    }
                    break;
                case MessageType.SendMessage:
                    InsertToChat(response.FromUser, response.Data);
                    break;
                default:
                    throw new Exception("Invalid MessageType");
            }
            ;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        //Button to send message
        private void button1_Click(object sender, EventArgs e)
        {
            InsertToChat(_targetUser.Id, $"{tbx_message.Text,100}");
            _sockerController.SendMessage(_user.Id, _targetUser.Id, tbx_message.Text, MessageType.SendMessage);
            tbx_message.Text = String.Empty;

            if (_targetUser.PublicName != _targetUser.Name)
            {
                _targetUser.PublicName = _targetUser.Name;
                list_contacts.Items[list_contacts.SelectedIndex] = _targetUser;
                list_contacts.Refresh();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbx_message.Text))
            {
                btn_send_message.Enabled = false;

            }
            else
            {
                btn_send_message.Enabled = true;
            }
        }

        //Button to create a contact
        private void button2_Click(object sender, EventArgs e)
        {
            using CreateContact form = new();
            if (form.ShowDialog() == DialogResult.OK)
            {
                var user = form.user!;
                list_contacts.Items.Add(user);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void list_contacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_contacts.SelectedItem != null)
            {
                UserConnection user = (UserConnection)list_contacts.SelectedItem;

                _targetUser = user;

                lb_user.Text = user.Name;

                tbx_message.Enabled = true;

                _chat = _chats[_targetUser.Id];
                LoadChat();
            }
        }
    }
}
