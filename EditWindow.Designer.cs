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
            this.prevkeyText = new System.Windows.Forms.Label();
            this.labelNewValue = new System.Windows.Forms.Label();
            this.labelPrevValue = new System.Windows.Forms.Label();
            this.labelKey = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.acceptButton = new System.Windows.Forms.Button();
            this.newKeyText = new System.Windows.Forms.TextBox();
            this.labelNewKey = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // newValueText
            // 
            this.newValueText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newValueText.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newValueText.Location = new System.Drawing.Point(122, 71);
            this.newValueText.Multiline = true;
            this.newValueText.Name = "newValueText";
            this.newValueText.Size = new System.Drawing.Size(351, 61);
            this.newValueText.TabIndex = 0;
            // 
            // prevValueText
            // 
            this.prevValueText.AutoSize = true;
            this.prevValueText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prevValueText.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prevValueText.Location = new System.Drawing.Point(122, 49);
            this.prevValueText.Name = "prevValueText";
            this.prevValueText.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.prevValueText.Size = new System.Drawing.Size(351, 19);
            this.prevValueText.TabIndex = 5;
            this.prevValueText.Text = "previous value placeholder";
            // 
            // prevkeyText
            // 
            this.prevkeyText.AutoSize = true;
            this.prevkeyText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.prevkeyText.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prevkeyText.Location = new System.Drawing.Point(122, 0);
            this.prevkeyText.Name = "prevkeyText";
            this.prevkeyText.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.prevkeyText.Size = new System.Drawing.Size(351, 19);
            this.prevkeyText.TabIndex = 4;
            this.prevkeyText.Text = "key placeholder";
            // 
            // labelNewValue
            // 
            this.labelNewValue.AutoSize = true;
            this.labelNewValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNewValue.Location = new System.Drawing.Point(3, 68);
            this.labelNewValue.Name = "labelNewValue";
            this.labelNewValue.Padding = new System.Windows.Forms.Padding(2);
            this.labelNewValue.Size = new System.Drawing.Size(113, 67);
            this.labelNewValue.TabIndex = 3;
            this.labelNewValue.Text = "New Value:";
            // 
            // labelPrevValue
            // 
            this.labelPrevValue.AutoSize = true;
            this.labelPrevValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelPrevValue.Location = new System.Drawing.Point(3, 49);
            this.labelPrevValue.Name = "labelPrevValue";
            this.labelPrevValue.Padding = new System.Windows.Forms.Padding(2);
            this.labelPrevValue.Size = new System.Drawing.Size(113, 19);
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
            this.labelKey.Size = new System.Drawing.Size(113, 19);
            this.labelKey.TabIndex = 1;
            this.labelKey.Text = "Previous Key:";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
            this.tableLayoutPanel1.Controls.Add(this.labelNewKey, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelKey, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelPrevValue, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelNewValue, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.prevkeyText, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.prevValueText, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.newValueText, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.acceptButton, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.newKeyText, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
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
            this.acceptButton.TabIndex = 1;
            this.acceptButton.Text = "Apply";
            this.acceptButton.UseVisualStyleBackColor = true;
            this.acceptButton.Click += new System.EventHandler(this.AcceptButton_Click);
            // 
            // newKeyText
            // 
            this.newKeyText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.newKeyText.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.newKeyText.Location = new System.Drawing.Point(122, 22);
            this.newKeyText.Name = "newKeyText";
            this.newKeyText.Size = new System.Drawing.Size(351, 24);
            this.newKeyText.TabIndex = 2;
            // 
            // labelNewKey
            // 
            this.labelNewKey.AutoSize = true;
            this.labelNewKey.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelNewKey.Location = new System.Drawing.Point(3, 19);
            this.labelNewKey.Name = "labelNewKey";
            this.labelNewKey.Padding = new System.Windows.Forms.Padding(2);
            this.labelNewKey.Size = new System.Drawing.Size(113, 30);
            this.labelNewKey.TabIndex = 7;
            this.labelNewKey.Text = "New Key:";
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
        private System.Windows.Forms.Label prevkeyText;
        private System.Windows.Forms.Label labelNewValue;
        private System.Windows.Forms.Label labelPrevValue;
        private System.Windows.Forms.Label labelKey;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button acceptButton;
        private System.Windows.Forms.TextBox newKeyText;
        private System.Windows.Forms.Label labelNewKey;
    }
}