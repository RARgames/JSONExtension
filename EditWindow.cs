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
        public EditWindow(string key, string value, bool createNew = false)
        {
            InitializeComponent();
            prevkeyText.Text = key;
            newKeyText.Text = key;
            prevValueText.Text = value;
            newValueText.Text = value;

            this.Cursor = new Cursor(Cursor.Current.Handle);
            this.Location = Cursor.Position; //move window to the cursor location

            newValueText.Select(0, 0); //move cursor to first char of textbox

            if (createNew)
            {
                this.Text = "JSON Extension Create Window";
            }
        }

        private void AcceptButton_Click(object sender, EventArgs e)
        {
            JSONExtensionPackage.settings.EditEntry(prevkeyText.Text, newKeyText.Text, prevValueText.Text, newValueText.Text);
            Dispose();
        }
    }
}
