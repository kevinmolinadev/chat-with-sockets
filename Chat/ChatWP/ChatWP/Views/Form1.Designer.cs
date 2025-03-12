using ChatWP.Entities;

namespace ChatWP.Views
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_send_message = new Button();
            tbx_message = new TextBox();
            btn_add_conversation = new Button();
            lb_contacts = new Label();
            lb_user = new Label();
            list_contacts = new ListBox();
            list_messages = new ListView();
            SuspendLayout();
            // 
            // btn_send_message
            // 
            btn_send_message.BackColor = SystemColors.ActiveCaption;
            btn_send_message.Cursor = Cursors.Hand;
            btn_send_message.Enabled = false;
            btn_send_message.ForeColor = SystemColors.ButtonHighlight;
            btn_send_message.Location = new Point(711, 381);
            btn_send_message.Name = "btn_send_message";
            btn_send_message.Size = new Size(77, 35);
            btn_send_message.TabIndex = 2;
            btn_send_message.Text = "Send";
            btn_send_message.UseVisualStyleBackColor = false;
            btn_send_message.Click += button1_Click;
            // 
            // tbx_message
            // 
            tbx_message.Enabled = false;
            tbx_message.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tbx_message.Location = new Point(173, 384);
            tbx_message.Name = "tbx_message";
            tbx_message.Size = new Size(532, 29);
            tbx_message.TabIndex = 3;
            tbx_message.TextChanged += textBox1_TextChanged;
            // 
            // btn_add_conversation
            // 
            btn_add_conversation.BackColor = SystemColors.ActiveCaption;
            btn_add_conversation.Cursor = Cursors.Hand;
            btn_add_conversation.Enabled = false;
            btn_add_conversation.ForeColor = SystemColors.ButtonHighlight;
            btn_add_conversation.Location = new Point(92, 12);
            btn_add_conversation.Name = "btn_add_conversation";
            btn_add_conversation.Size = new Size(75, 33);
            btn_add_conversation.TabIndex = 4;
            btn_add_conversation.Text = "Add";
            btn_add_conversation.UseVisualStyleBackColor = false;
            btn_add_conversation.Visible = false;
            btn_add_conversation.Click += button2_Click;
            // 
            // lb_contacts
            // 
            lb_contacts.AutoSize = true;
            lb_contacts.Cursor = Cursors.Hand;
            lb_contacts.Font = new Font("Segoe UI", 12.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lb_contacts.Location = new Point(13, 17);
            lb_contacts.Name = "lb_contacts";
            lb_contacts.Size = new Size(79, 23);
            lb_contacts.TabIndex = 6;
            lb_contacts.Text = "Contacts";
            lb_contacts.Click += label1_Click;
            // 
            // lb_user
            // 
            lb_user.AutoSize = true;
            lb_user.Font = new Font("Segoe UI Semibold", 14.25F, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, 0);
            lb_user.Location = new Point(178, 15);
            lb_user.Name = "lb_user";
            lb_user.RightToLeft = RightToLeft.No;
            lb_user.Size = new Size(0, 25);
            lb_user.TabIndex = 7;
            lb_user.TextAlign = ContentAlignment.MiddleLeft;
            lb_user.UseWaitCursor = true;
            lb_user.Click += label1_Click_1;
            // 
            // list_contacts
            // 
            list_contacts.Cursor = Cursors.Hand;
            list_contacts.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            list_contacts.FormattingEnabled = true;
            list_contacts.Location = new Point(13, 51);
            list_contacts.Name = "list_contacts";
            list_contacts.Size = new Size(154, 361);
            list_contacts.TabIndex = 8;
            list_contacts.SelectedIndexChanged += list_contacts_SelectedIndexChanged;
            // 
            // list_messages
            // 
            list_messages.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            list_messages.Location = new Point(173, 51);
            list_messages.Name = "list_messages";
            list_messages.Size = new Size(615, 327);
            list_messages.TabIndex = 5;
            list_messages.UseCompatibleStateImageBehavior = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 428);
            Controls.Add(list_contacts);
            Controls.Add(lb_user);
            Controls.Add(lb_contacts);
            Controls.Add(list_messages);
            Controls.Add(btn_add_conversation);
            Controls.Add(tbx_message);
            Controls.Add(btn_send_message);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_send_message;
        private TextBox tbx_message;
        private Button btn_add_conversation;
        private Label lb_contacts;
        private Label lb_user;
        private ListBox list_contacts;
        private ListView list_messages;
    }
}
