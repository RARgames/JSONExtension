using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JSONExtension
{
    public partial class EditWindow : Form
    {
        public EditWindow(string key, string value)
        {
            InitializeComponent();
            keyText.Text = key;
            prevValueText.Text = value;
            newValueText.Text = value;
            this.Cursor = new Cursor(Cursor.Current.Handle);
            this.Location = Cursor.Position;
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            JSONExtensionPackage.settings.EditEntry(keyText.Text, keyText.Text, newValueText.Text);
            Dispose();
        }
    }
}
