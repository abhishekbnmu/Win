namespace WinFormsApp1
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            dataGridView = new DataGridView();
            dataGridView3 = new DataGridView();
            button4 = new Button();
            dataGridView4 = new DataGridView();
            bindingSource1 = new BindingSource(components);
            textBox2 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(63, 45);
            button1.Name = "button1";
            button1.Size = new Size(203, 73);
            button1.TabIndex = 0;
            button1.Text = "Load Data Source";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(63, 141);
            button2.Name = "button2";
            button2.Size = new Size(203, 58);
            button2.TabIndex = 1;
            button2.Text = "Load Release Order File";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(63, 323);
            button3.Name = "button3";
            button3.Size = new Size(203, 68);
            button3.TabIndex = 2;
            button3.Text = "Generate Report 1";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // dataGridView
            // 
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Location = new Point(308, 45);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersWidth = 51;
            dataGridView.Size = new Size(791, 188);
            dataGridView.TabIndex = 3;
            dataGridView.CellContentClick += dataGridView1_CellContentClick;
            // 
            // dataGridView3
            // 
            dataGridView3.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView3.Location = new Point(308, 254);
            dataGridView3.Name = "dataGridView3";
            dataGridView3.RowHeadersWidth = 51;
            dataGridView3.Size = new Size(791, 188);
            dataGridView3.TabIndex = 6;
            dataGridView3.CellContentClick += dataGridView3_CellContentClick;
            // 
            // button4
            // 
            button4.Location = new Point(63, 521);
            button4.Name = "button4";
            button4.Size = new Size(203, 79);
            button4.TabIndex = 7;
            button4.Text = "Generate Report 2";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // dataGridView4
            // 
            dataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView4.Location = new Point(308, 468);
            dataGridView4.Name = "dataGridView4";
            dataGridView4.RowHeadersWidth = 51;
            dataGridView4.Size = new Size(791, 188);
            dataGridView4.TabIndex = 8;
            dataGridView4.CellContentClick += dataGridView4_CellContentClick;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(23, 662);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(1138, 66);
            textBox2.TabIndex = 10;
            textBox2.Text = resources.GetString("textBox2.Text");
            textBox2.TextChanged += textBox2_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1173, 740);
            Controls.Add(textBox2);
            Controls.Add(dataGridView4);
            Controls.Add(button4);
            Controls.Add(dataGridView3);
            Controls.Add(dataGridView);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Program";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView4).EndInit();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private Button button2;
        private Button button3;
        private DataGridView dataGridView;
        private DataGridView dataGridView3;
        private Button button4;
        private DataGridView dataGridView4;
        private BindingSource bindingSource1;
        private TextBox textBox2;
    }
}
