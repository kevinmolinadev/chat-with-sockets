using ChatWP_P2P.Dtos;
using ChatWP_P2P.Entities;
using ChatWP_P2P.Enums;
using ChatWP_P2P.Services;

namespace ChatWP_P2P.Views
{
    public partial class Form1 : Form
    {

        private readonly SocketService _socketService;
        private readonly UserConnection _user;
        private  UserConnection _targetUser = null!;
        private readonly Dictionary<string, List<string>> _chats = new Dictionary<string, List<string>>();
        private List<string> _chat = [];
        public Form1(SocketService socketService, UserConnection user)
        {
            InitializeComponent();
            _socketService = socketService;
            _socketService.OnMessageReceived = LoadMessagesFromServer;
            _user = user;
            label1.Text = user.Name;
            label2.Text = $"{user.Host}:{user.Port}";
            ConfigChatList();
            this.FormClosing += FromClosingEvent!;
        }


        private void FromClosingEvent(object sender, FormClosingEventArgs e)
        {
            foreach(UserConnection user in list_contacts.Items)
            {
                _socketService.SendMessage(_user, user.Host, user.Port, MessageType.Close.ToString());
            }
        }

        private void ConfigChatList()
        {
            list_messages.View = View.Details;
            list_messages.Columns.Add("Mensajes", -2, HorizontalAlignment.Left);
            list_messages.HeaderStyle = ColumnHeaderStyle.None;
        }

        private void InsertToChat(string chatId, string message)
        {
            if (_targetUser?.Host == chatId)
            {
                _chat.Add(message);
                list_messages.Items.Add(new ListViewItem(message));
            }
            else
            {
                var index = list_contacts.Items.Cast<UserConnection>().ToList().FindIndex(user => user.Host == chatId);
                var contact = list_contacts.Items.Cast<UserConnection>().FirstOrDefault(user => user.Host == chatId);
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
            _chats.Add(user.Host, []);
        }

        private void EraseChat(UserConnection user)
        {
            _chats.Remove(user.Host);
            _chat.Clear();
            lb_user.Text = String.Empty;
            var index = list_contacts.Items.Cast<UserConnection>().ToList().FindIndex(u => u.Host == user.Host);
            list_contacts.Items.RemoveAt(index);
            list_contacts.Refresh();
            if (_targetUser.Host != user.Host) return;
            LoadChat();
            tbx_message.Enabled = false;
            btn_send_message.Enabled = false;
        }

        private void LoadChat()
        {
            list_messages.Items.Clear();

            foreach (var message in _chat)
            {
                list_messages.Items.Add(new ListViewItem(message));
            }
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
                    if (_chats.ContainsKey(response.User.Host)) break;
                    InsertToContacts(response.User);
                    break;
                case MessageType.SendMessage:
                    if (!_chats.ContainsKey(response.User.Host))
                    {
                        InsertToContacts(response.User);
                    }
                    InsertToChat(response.User.Host, response.Message);
                    break;
                case MessageType.Close:
                    EraseChat(response.User);
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
            InsertToChat(_targetUser.Host, $"{tbx_message.Text,100}");
            _socketService.SendMessage(_user,_targetUser.Host, _targetUser.Port, tbx_message.Text);
            tbx_message.Text = String.Empty;

            if (_targetUser.PublicName == _targetUser.Name) return;
            
            _targetUser.PublicName = _targetUser.Name;
            list_contacts.Items[list_contacts.SelectedIndex] = _targetUser;
            list_contacts.Refresh();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btn_send_message.Enabled = !String.IsNullOrEmpty(tbx_message.Text);
        }

        //Button to create a contact
        private void button2_Click(object sender, EventArgs e)
        {
            using CreateContact form = new();
            if (form.ShowDialog() != DialogResult.OK) return;
            var user = form.user!;
            InsertToContacts(user);
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void list_contacts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (list_contacts.SelectedItem == null) return;
            var user = (UserConnection)list_contacts.SelectedItem;

            _targetUser = user;

            lb_user.Text = user.Name;

            tbx_message.Enabled = true;

            _chat = _chats[_targetUser.Host];
            LoadChat();
        }
    }
}
