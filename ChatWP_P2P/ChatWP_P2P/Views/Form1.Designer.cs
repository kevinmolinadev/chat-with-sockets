namespace ChatWP_P2P.Views
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_send_message = new System.Windows.Forms.Button();
            tbx_message = new System.Windows.Forms.TextBox();
            btn_add_conversation = new System.Windows.Forms.Button();
            lb_contacts = new System.Windows.Forms.Label();
            lb_user = new System.Windows.Forms.Label();
            list_contacts = new System.Windows.Forms.ListBox();
            list_messages = new System.Windows.Forms.ListView();
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            SuspendLayout();
            // 
            // btn_send_message
            // 
            btn_send_message.BackColor = System.Drawing.SystemColors.ActiveCaption;
            btn_send_message.Cursor = System.Windows.Forms.Cursors.Hand;
            btn_send_message.Enabled = false;
            btn_send_message.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            btn_send_message.Location = new System.Drawing.Point(711, 381);
            btn_send_message.Name = "btn_send_message";
            btn_send_message.Size = new System.Drawing.Size(77, 35);
            btn_send_message.TabIndex = 2;
            btn_send_message.Text = "Send";
            btn_send_message.UseVisualStyleBackColor = false;
            btn_send_message.Click += button1_Click;
            // 
            // tbx_message
            // 
            tbx_message.Enabled = false;
            tbx_message.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            tbx_message.Location = new System.Drawing.Point(173, 384);
            tbx_message.Name = "tbx_message";
            tbx_message.Size = new System.Drawing.Size(532, 29);
            tbx_message.TabIndex = 3;
            tbx_message.TextChanged += textBox1_TextChanged;
            // 
            // btn_add_conversation
            // 
            btn_add_conversation.BackColor = System.Drawing.SystemColors.ActiveCaption;
            btn_add_conversation.Cursor = System.Windows.Forms.Cursors.Hand;
            btn_add_conversation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            btn_add_conversation.Location = new System.Drawing.Point(92, 12);
            btn_add_conversation.Name = "btn_add_conversation";
            btn_add_conversation.Size = new System.Drawing.Size(75, 33);
            btn_add_conversation.TabIndex = 4;
            btn_add_conversation.Text = "Add";
            btn_add_conversation.UseVisualStyleBackColor = false;
            btn_add_conversation.Visible = false;
            btn_add_conversation.Click += button2_Click;
            // 
            // lb_contacts
            // 
            lb_contacts.AutoSize = true;
            lb_contacts.Cursor = System.Windows.Forms.Cursors.Hand;
            lb_contacts.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)0));
            lb_contacts.Location = new System.Drawing.Point(13, 17);
            lb_contacts.Name = "lb_contacts";
            lb_contacts.Size = new System.Drawing.Size(79, 23);
            lb_contacts.TabIndex = 6;
            lb_contacts.Text = "Contacts";
            lb_contacts.Click += label1_Click;
            // 
            // lb_user
            // 
            lb_user.AutoSize = true;
            lb_user.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)), System.Drawing.GraphicsUnit.Point, ((byte)0));
            lb_user.Location = new System.Drawing.Point(178, 15);
            lb_user.Name = "lb_user";
            lb_user.RightToLeft = System.Windows.Forms.RightToLeft.No;
            lb_user.Size = new System.Drawing.Size(0, 25);
            lb_user.TabIndex = 7;
            lb_user.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            lb_user.UseWaitCursor = true;
            lb_user.Click += label1_Click_1;
            // 
            // list_contacts
            // 
            list_contacts.Cursor = System.Windows.Forms.Cursors.Hand;
            list_contacts.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            list_contacts.FormattingEnabled = true;
            list_contacts.Location = new System.Drawing.Point(13, 51);
            list_contacts.Name = "list_contacts";
            list_contacts.Size = new System.Drawing.Size(154, 361);
            list_contacts.TabIndex = 8;
            list_contacts.SelectedIndexChanged += list_contacts_SelectedIndexChanged;
            // 
            // list_messages
            // 
            list_messages.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            list_messages.Location = new System.Drawing.Point(173, 51);
            list_messages.Name = "list_messages";
            list_messages.Size = new System.Drawing.Size(615, 327);
            list_messages.TabIndex = 5;
            list_messages.UseCompatibleStateImageBehavior = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, ((System.Drawing.FontStyle)(System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)), System.Drawing.GraphicsUnit.Point, ((byte)0));
            label1.Location = new System.Drawing.Point(681, 15);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(0, 25);
            label1.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)0));
            label2.Location = new System.Drawing.Point(429, 9);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(0, 15);
            label2.TabIndex = 10;
            label2.Visible = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(800, 428);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(list_contacts);
            Controls.Add(lb_user);
            Controls.Add(lb_contacts);
            Controls.Add(list_messages);
            Controls.Add(btn_add_conversation);
            Controls.Add(tbx_message);
            Controls.Add(btn_send_message);
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btn_send_message;
        private TextBox tbx_message;
        private System.Windows.Forms.Button btn_add_conversation;
        private Label lb_contacts;
        private Label lb_user;
        private ListBox list_contacts;
        private ListView list_messages;
        private Label label1;
        private System.Windows.Forms.Label label2;
    }
}
