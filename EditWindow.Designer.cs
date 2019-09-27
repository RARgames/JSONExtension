namespace JSONExtension
{
    partial class EditWindow
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
            this.newValueText = new System.Windows.Forms.TextBox();
            this.prevValueText = new System.Windows.Forms.Label();
            this.keyText = new System.Windows.Forms.Label();
            this.labelNewValue = new System.Windows.Forms.Label();
            this.labelPrevValue = new System.Windows.Forms.Label();
            this.labelKey = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.acceptButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // newValueText
            // 
            this.newValueText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newValueText.Location = new System.Drawing.Point(122, 37);
            this.newValueText.Multiline = true;
            this.newValueText.Name = "newValueText";
            this.newValueText.Size = new System.Drawing.Size(351, 95);
            this.newValueText.TabIndex = 0;
            // 
            // prevValueText
            // 
            this.prevValueText.AutoSize = true;
            this.prevValueText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prevValueText.Location = new System.Drawing.Point(122, 17);
            this.prevValueText.Name = "prevValueText";
            this.prevValueText.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.prevValueText.Size = new System.Drawing.Size(351, 17);
            this.prevValueText.TabIndex = 5;
            this.prevValueText.Text = "previous value placeholder";
            // 
            // keyText
            // 
            this.keyText.AutoSize = true;
            this.keyText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.keyText.Location = new System.Drawing.Point(122, 0);
            this.keyText.Name = "keyText";
            this.keyText.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.keyText.Size = new System.Drawing.Size(351, 17);
            this.keyText.TabIndex = 4;
            this.keyText.Text = "key placeholder";
            // 
            // labelNewValue
            // 
            this.labelNewValue.AutoSize = true;
            this.labelNewValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNewValue.Location = new System.Drawing.Point(3, 34);
            this.labelNewValue.Name = "labelNewValue";
            this.labelNewValue.Padding = new System.Windows.Forms.Padding(2);
            this.labelNewValue.Size = new System.Drawing.Size(113, 101);
            this.labelNewValue.TabIndex = 3;
            this.labelNewValue.Text = "New Value:";
            // 
            // labelPrevValue
            // 
            this.labelPrevValue.AutoSize = true;
            this.labelPrevValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPrevValue.Location = new System.Drawing.Point(3, 17);
            this.labelPrevValue.Name = "labelPrevValue";
            this.labelPrevValue.Padding = new System.Windows.Forms.Padding(2);
            this.labelPrevValue.Size = new System.Drawing.Size(113, 17);
            this.labelPrevValue.TabIndex = 2;
            this.labelPrevValue.Text = "Previous Value:";
            // 
            // labelKey
            // 
            this.labelKey.AutoSize = true;
            this.labelKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelKey.Location = new System.Drawing.Point(3, 0);
            this.labelKey.Name = "labelKey";
            this.labelKey.Padding = new System.Windows.Forms.Padding(2);
            this.labelKey.Size = new System.Drawing.Size(113, 17);
            this.labelKey.TabIndex = 1;
            this.labelKey.Text = "Key:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.labelKey, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelPrevValue, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelNewValue, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.keyText, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.prevValueText, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.newValueText, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.acceptButton, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(476, 178);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // acceptButton
            // 
            this.acceptButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.acceptButton.Location = new System.Drawing.Point(391, 145);
            this.acceptButton.Margin = new System.Windows.Forms.Padding(10);
            this.acceptButton.Name = "acceptButton";
            this.acceptButton.Size = new System.Drawing.Size(75, 23);
            this.acceptButton.TabIndex = 6;
            this.acceptButton.Text = "Apply";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // EditWindow
            // 
            this.AcceptButton = this.acceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(476, 178);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MinimumSize = new System.Drawing.Size(492, 217);
            this.Name = "EditWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "JSON Extension Edit Window";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox newValueText;
        private System.Windows.Forms.Label prevValueText;
        private System.Windows.Forms.Label keyText;
        private System.Windows.Forms.Label labelNewValue;
        private System.Windows.Forms.Label labelPrevValue;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button acceptButton;
    }
}