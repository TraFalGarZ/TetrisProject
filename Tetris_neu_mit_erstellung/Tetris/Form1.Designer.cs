namespace Tetris
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.timer_block_down = new System.Windows.Forms.Timer(this.components);
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_stop = new System.Windows.Forms.Button();
            this.panel_out = new System.Windows.Forms.Panel();
            this.label_countdown = new System.Windows.Forms.Label();
            this.timer_fall = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.panel_out.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tableLayoutPanel1.BackgroundImage")));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(50, 40);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // timer_block_down
            // 
            this.timer_block_down.Interval = 750;
            this.timer_block_down.Tick += new System.EventHandler(this.timer_block_down_Tick);
            // 
            // btn_start
            // 
            this.btn_start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_start.ForeColor = System.Drawing.Color.Blue;
            this.btn_start.Location = new System.Drawing.Point(705, 59);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 75);
            this.btn_start.TabIndex = 1;
            this.btn_start.TabStop = false;
            this.btn_start.Text = "START";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_stop
            // 
            this.btn_stop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_stop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_stop.ForeColor = System.Drawing.Color.Blue;
            this.btn_stop.Location = new System.Drawing.Point(705, 171);
            this.btn_stop.Name = "btn_stop";
            this.btn_stop.Size = new System.Drawing.Size(75, 75);
            this.btn_stop.TabIndex = 1;
            this.btn_stop.TabStop = false;
            this.btn_stop.Text = "STOP";
            this.btn_stop.UseVisualStyleBackColor = true;
            this.btn_stop.Click += new System.EventHandler(this.btn_stop_Click);
            // 
            // panel_out
            // 
            this.panel_out.AllowDrop = true;
            this.panel_out.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_out.Controls.Add(this.tableLayoutPanel1);
            this.panel_out.Cursor = System.Windows.Forms.Cursors.Cross;
            this.panel_out.Location = new System.Drawing.Point(239, 35);
            this.panel_out.Margin = new System.Windows.Forms.Padding(1);
            this.panel_out.Name = "panel_out";
            this.panel_out.Size = new System.Drawing.Size(72, 58);
            this.panel_out.TabIndex = 2;
            // 
            // label_countdown
            // 
            this.label_countdown.AutoSize = true;
            this.label_countdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_countdown.Location = new System.Drawing.Point(129, 35);
            this.label_countdown.Name = "label_countdown";
            this.label_countdown.Size = new System.Drawing.Size(20, 24);
            this.label_countdown.TabIndex = 4;
            this.label_countdown.Text = "3";
            // 
            // timer_fall
            // 
            this.timer_fall.Interval = 1000;
            this.timer_fall.Tick += new System.EventHandler(this.timer_fall_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(726, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "timerfall";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(828, 562);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label_countdown);
            this.Controls.Add(this.panel_out);
            this.Controls.Add(this.btn_stop);
            this.Controls.Add(this.btn_start);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Deactivate += new System.EventHandler(this.Form1_Deactivate);
            this.Activated += new System.EventHandler(this.Form1_Activated);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.Form1_PreviewKeyDown);
            this.panel_out.ResumeLayout(false);
            this.panel_out.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Timer timer_block_down;
        private System.Windows.Forms.Button btn_stop;
        private System.Windows.Forms.Panel panel_out;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label label_countdown;
        private System.Windows.Forms.Timer timer_fall;
        private System.Windows.Forms.Button button1;

    }
}

