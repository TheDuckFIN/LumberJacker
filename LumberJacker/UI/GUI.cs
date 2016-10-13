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
            this.core.SetActions(DisableInput, EnableInput, UpdateTexts);

            InitializeComponent();
        }

        public void DisableInput()
        {
            headPositionSelectButton.Enabled = false;
            sideButton.Enabled = false;
            startButton.Enabled = false;
            speedSelector.Enabled = false;
            scanRadiusSelector.Enabled = false;
        }

        public void EnableInput()
        {
            headPositionSelectButton.Enabled = true;
            sideButton.Enabled = true;
            startButton.Enabled = true;
            speedSelector.Enabled = true;
            scanRadiusSelector.Enabled = true;
        }

        public void UpdateTexts()
        {
            headPos.Text = "Head pos: " + this.core.bot.headPosition;
        }

        private void headPositionSelector_Click(object sender, EventArgs e)
        {
            headPositionSelectButton.Enabled = false;

            this.core.BeginHeadPosSelection();
        }
        
        private void sideButton_Click(object sender, EventArgs e)
        {
            if (this.core.bot.characterStartingSide == Side.LEFT)
            {
                this.core.bot.characterStartingSide = Side.RIGHT;
                sideButton.Text = "Character side: Right";
            }
            else
            {
                this.core.bot.characterStartingSide = Side.LEFT;
                sideButton.Text = "Character side: Left";
            }
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            this.core.StartBot();
        }
        
        private void speedSelector_ValueChanged(object sender, EventArgs e)
        {
            this.core.bot.waitTime = (int)((NumericUpDown)sender).Value;
        }

        private void scanRadiusSelector_ValueChanged(object sender, EventArgs e)
        {
            this.core.bot.scanRadius = (int)((NumericUpDown)sender).Value;
        }
    }
}
