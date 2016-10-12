using LumberJacker.Util;
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
            headPositionSelectButton.Enabled = false;

            this.core.BeginHeadPosSelection(updateUI);
        }

        public void updateUI()
        {
            headPositionSelectButton.Enabled = true;
            headPos.Text = "Head pos: " + this.core.headPosition;
        }

        private void sideButton_Click(object sender, EventArgs e)
        {
            if (this.core.characterSide == Side.LEFT)
            {
                this.core.characterSide = Side.RIGHT;
                sideButton.Text = "Character side: Right";
            }
            else
            {
                this.core.characterSide = Side.LEFT;
                sideButton.Text = "Character side: Left";
            }
        }

        private void beginButton_Click(object sender, EventArgs e)
        {
            sideButton.Enabled = false;
            headPositionSelectButton.Enabled = false;
            startButton.Enabled = false;
        }
    }
}
