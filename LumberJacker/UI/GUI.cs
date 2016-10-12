using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LumberJacker
{
    public partial class GUI : Form
    {
        private Core core;

        public GUI(Core core)
        {
            this.core = core;
            InitializeComponent();
        }

        private void headPositionSelector_Click(object sender, EventArgs e)
        {
            Button pressedButton = (Button)sender;

            pressedButton.Enabled = false;

            this.core.BeginHeadPosSelection();
        }
    }
}
