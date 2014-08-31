namespace UPDChat
{
    partial class ChatForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
            this.label1 = new System.Windows.Forms.Label();
            this.tabContainer = new TablessControl();
            this.selectTypeTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.serverRadio = new System.Windows.Forms.RadioButton();
            this.clientRadio = new System.Windows.Forms.RadioButton();
            this.selectContinueBtn = new System.Windows.Forms.Button();
            this.clientSettingsTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.clientPassTxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.usernameTxt = new System.Windows.Forms.TextBox();
            this.clientIpTxt = new System.Windows.Forms.TextBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.clientChatTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.label6 = new System.Windows.Forms.Label();
            this.clientInputTxt = new System.Windows.Forms.RichTextBox();
            this.sendBtn = new System.Windows.Forms.Button();
            this.chatRecievedBox = new System.Windows.Forms.RichTextBox();
            this.serverTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.label10 = new System.Windows.Forms.Label();
            this.serverBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.serverPassTxt = new System.Windows.Forms.TextBox();
            this.serverNameTxt = new System.Windows.Forms.TextBox();
            this.tabContainer.SuspendLayout();
            this.selectTypeTab.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.clientSettingsTab.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.clientChatTab.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.serverTab.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(35, 380);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "created by Thomas Conroy - RIT, Fall 2014";
            // 
            // tabContainer
            // 
            this.tabContainer.Controls.Add(this.selectTypeTab);
            this.tabContainer.Controls.Add(this.clientSettingsTab);
            this.tabContainer.Controls.Add(this.clientChatTab);
            this.tabContainer.Controls.Add(this.serverTab);
            this.tabContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabContainer.Location = new System.Drawing.Point(0, 0);
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new System.Drawing.Size(475, 395);
            this.tabContainer.TabIndex = 3;
            // 
            // selectTypeTab
            // 
            this.selectTypeTab.Controls.Add(this.tableLayoutPanel1);
            this.selectTypeTab.Location = new System.Drawing.Point(4, 22);
            this.selectTypeTab.Name = "selectTypeTab";
            this.selectTypeTab.Padding = new System.Windows.Forms.Padding(3);
            this.selectTypeTab.Size = new System.Drawing.Size(467, 369);
            this.selectTypeTab.TabIndex = 0;
            this.selectTypeTab.Text = "selectTypeTab";
            this.selectTypeTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.groupBox1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.selectContinueBtn, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(461, 363);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(455, 36);
            this.label2.TabIndex = 0;
            this.label2.Text = "Launch Settings";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.serverRadio);
            this.groupBox1.Controls.Add(this.clientRadio);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(25, 61);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(411, 204);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Launch Type";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 148);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(303, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Launches a server instance, allowing others to connect to you.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(389, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Launches a client instance, allowing you to connect to an already running server." +
    "";
            // 
            // serverRadio
            // 
            this.serverRadio.AutoSize = true;
            this.serverRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.serverRadio.Location = new System.Drawing.Point(6, 121);
            this.serverRadio.Name = "serverRadio";
            this.serverRadio.Size = new System.Drawing.Size(73, 24);
            this.serverRadio.TabIndex = 1;
            this.serverRadio.TabStop = true;
            this.serverRadio.Text = "Server";
            this.serverRadio.UseVisualStyleBackColor = true;
            // 
            // clientRadio
            // 
            this.clientRadio.AutoSize = true;
            this.clientRadio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientRadio.Location = new System.Drawing.Point(6, 42);
            this.clientRadio.Name = "clientRadio";
            this.clientRadio.Size = new System.Drawing.Size(67, 24);
            this.clientRadio.TabIndex = 0;
            this.clientRadio.TabStop = true;
            this.clientRadio.Text = "Client";
            this.clientRadio.UseVisualStyleBackColor = true;
            // 
            // selectContinueBtn
            // 
            this.selectContinueBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.selectContinueBtn.Location = new System.Drawing.Point(383, 293);
            this.selectContinueBtn.Name = "selectContinueBtn";
            this.selectContinueBtn.Size = new System.Drawing.Size(75, 67);
            this.selectContinueBtn.TabIndex = 2;
            this.selectContinueBtn.Text = "Continue";
            this.selectContinueBtn.UseVisualStyleBackColor = true;
            this.selectContinueBtn.Click += new System.EventHandler(this.button1_Click);
            // 
            // clientSettingsTab
            // 
            this.clientSettingsTab.Controls.Add(this.tableLayoutPanel3);
            this.clientSettingsTab.Location = new System.Drawing.Point(4, 22);
            this.clientSettingsTab.Name = "clientSettingsTab";
            this.clientSettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.clientSettingsTab.Size = new System.Drawing.Size(467, 369);
            this.clientSettingsTab.TabIndex = 1;
            this.clientSettingsTab.Text = "clientSettingsTab";
            this.clientSettingsTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.groupBox2, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.connectBtn, 0, 2);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(461, 363);
            this.tableLayoutPanel3.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(455, 36);
            this.label5.TabIndex = 1;
            this.label5.Text = "Client Settings";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.clientPassTxt);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.usernameTxt);
            this.groupBox2.Controls.Add(this.clientIpTxt);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(25, 61);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(411, 204);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Client Options";
            // 
            // clientPassTxt
            // 
            this.clientPassTxt.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.clientPassTxt.Location = new System.Drawing.Point(28, 113);
            this.clientPassTxt.Name = "clientPassTxt";
            this.clientPassTxt.PasswordChar = '*';
            this.clientPassTxt.Size = new System.Drawing.Size(207, 29);
            this.clientPassTxt.TabIndex = 5;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label9.Location = new System.Drawing.Point(24, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(273, 20);
            this.label9.TabIndex = 4;
            this.label9.Text = "Server Password (leave blank if none)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label8.Location = new System.Drawing.Point(25, 152);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(83, 20);
            this.label8.TabIndex = 3;
            this.label8.Text = "Username";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label7.Location = new System.Drawing.Point(25, 26);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 20);
            this.label7.TabIndex = 2;
            this.label7.Text = "Server IP";
            // 
            // usernameTxt
            // 
            this.usernameTxt.Location = new System.Drawing.Point(28, 175);
            this.usernameTxt.Name = "usernameTxt";
            this.usernameTxt.Size = new System.Drawing.Size(207, 29);
            this.usernameTxt.TabIndex = 1;
            // 
            // clientIpTxt
            // 
            this.clientIpTxt.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress;
            this.clientIpTxt.Location = new System.Drawing.Point(28, 49);
            this.clientIpTxt.Name = "clientIpTxt";
            this.clientIpTxt.Size = new System.Drawing.Size(207, 29);
            this.clientIpTxt.TabIndex = 0;
            this.clientIpTxt.Text = "127.0.0.1";
            // 
            // connectBtn
            // 
            this.connectBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.connectBtn.Location = new System.Drawing.Point(383, 293);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 67);
            this.connectBtn.TabIndex = 3;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // clientChatTab
            // 
            this.clientChatTab.Controls.Add(this.tableLayoutPanel2);
            this.clientChatTab.Location = new System.Drawing.Point(4, 22);
            this.clientChatTab.Name = "clientChatTab";
            this.clientChatTab.Size = new System.Drawing.Size(467, 369);
            this.clientChatTab.TabIndex = 2;
            this.clientChatTab.Text = "clientChatTab";
            this.clientChatTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Controls.Add(this.label6, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.clientInputTxt, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.sendBtn, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.chatRecievedBox, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(467, 369);
            this.tableLayoutPanel2.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.tableLayoutPanel2.SetColumnSpan(this.label6, 2);
            this.label6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(461, 36);
            this.label6.TabIndex = 1;
            this.label6.Text = "Now Chatting...";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // clientInputTxt
            // 
            this.clientInputTxt.Dock = System.Windows.Forms.DockStyle.Top;
            this.clientInputTxt.Location = new System.Drawing.Point(3, 297);
            this.clientInputTxt.Name = "clientInputTxt";
            this.clientInputTxt.Size = new System.Drawing.Size(367, 69);
            this.clientInputTxt.TabIndex = 2;
            this.clientInputTxt.Text = "";
            this.clientInputTxt.TextChanged += new System.EventHandler(this.clientInputTxt_TextChanged);
            this.clientInputTxt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.clientInputTxt_KeyPress);
            // 
            // sendBtn
            // 
            this.sendBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sendBtn.Location = new System.Drawing.Point(376, 297);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(88, 69);
            this.sendBtn.TabIndex = 3;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // chatRecievedBox
            // 
            this.tableLayoutPanel2.SetColumnSpan(this.chatRecievedBox, 2);
            this.chatRecievedBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chatRecievedBox.Location = new System.Drawing.Point(3, 39);
            this.chatRecievedBox.Name = "chatRecievedBox";
            this.chatRecievedBox.ReadOnly = true;
            this.chatRecievedBox.Size = new System.Drawing.Size(461, 252);
            this.chatRecievedBox.TabIndex = 4;
            this.chatRecievedBox.Text = "";
            // 
            // serverTab
            // 
            this.serverTab.Controls.Add(this.tableLayoutPanel4);
            this.serverTab.Location = new System.Drawing.Point(4, 22);
            this.serverTab.Name = "serverTab";
            this.serverTab.Size = new System.Drawing.Size(467, 369);
            this.serverTab.TabIndex = 3;
            this.serverTab.Text = "serverTab";
            this.serverTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.label10, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.serverBtn, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.groupBox3, 0, 1);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(467, 369);
            this.tableLayoutPanel4.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(3, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(461, 36);
            this.label10.TabIndex = 0;
            this.label10.Text = "Host a Server";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // serverBtn
            // 
            this.serverBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.serverBtn.Location = new System.Drawing.Point(3, 297);
            this.serverBtn.Name = "serverBtn";
            this.serverBtn.Size = new System.Drawing.Size(461, 69);
            this.serverBtn.TabIndex = 1;
            this.serverBtn.Text = "Start";
            this.serverBtn.UseVisualStyleBackColor = true;
            this.serverBtn.Click += new System.EventHandler(this.serverBtn_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.serverPassTxt);
            this.groupBox3.Controls.Add(this.serverNameTxt);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.groupBox3.Location = new System.Drawing.Point(25, 61);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(417, 208);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Server Settings";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label12.Location = new System.Drawing.Point(6, 118);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(234, 20);
            this.label12.TabIndex = 3;
            this.label12.Text = "Password (leave blank for none)";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label11.Location = new System.Drawing.Point(6, 35);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(270, 20);
            this.label11.TabIndex = 2;
            this.label11.Text = "Channel Name (leave blank for none)";
            // 
            // serverPassTxt
            // 
            this.serverPassTxt.Location = new System.Drawing.Point(10, 141);
            this.serverPassTxt.Name = "serverPassTxt";
            this.serverPassTxt.PasswordChar = '*';
            this.serverPassTxt.Size = new System.Drawing.Size(206, 29);
            this.serverPassTxt.TabIndex = 1;
            // 
            // serverNameTxt
            // 
            this.serverNameTxt.Location = new System.Drawing.Point(10, 58);
            this.serverNameTxt.Name = "serverNameTxt";
            this.serverNameTxt.Size = new System.Drawing.Size(206, 29);
            this.serverNameTxt.TabIndex = 0;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 395);
            this.Controls.Add(this.tabContainer);
            this.Controls.Add(this.label1);
            this.Name = "ChatForm";
            this.Text = "UDPChat";
            this.tabContainer.ResumeLayout(false);
            this.selectTypeTab.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.clientSettingsTab.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.clientChatTab.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.serverTab.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private TablessControl tabContainer;
        private System.Windows.Forms.TabPage selectTypeTab;
        private System.Windows.Forms.TabPage clientSettingsTab;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TabPage clientChatTab;
        private System.Windows.Forms.TabPage serverTab;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton serverRadio;
        private System.Windows.Forms.RadioButton clientRadio;
        private System.Windows.Forms.Button selectContinueBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.RichTextBox clientInputTxt;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.RichTextBox chatRecievedBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox clientPassTxt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox usernameTxt;
        private System.Windows.Forms.TextBox clientIpTxt;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button serverBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox serverPassTxt;
        private System.Windows.Forms.TextBox serverNameTxt;
    }
}

