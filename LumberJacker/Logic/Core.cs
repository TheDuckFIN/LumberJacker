using Gma.System.MouseKeyHook;
using LumberJacker.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LumberJacker
{
    public class Core
    {
        private IKeyboardMouseEvents globalHook;

        public Vector2 headPosition;
        public Side characterSide;

        public Action updateUI;


        public Core()
        {
            this.globalHook = Hook.GlobalEvents();
            this.headPosition = new Vector2(0, 0);
            this.characterSide = Side.LEFT;
        }
        
        public void BeginHeadPosSelection(Action updateUI)
        {
            this.updateUI = updateUI;
            this.globalHook.MouseClick += GlobalHook_MouseClick;
        }

        private void GlobalHook_MouseClick(object sender, MouseEventArgs e)
        {
            //Set head position as the click position
            this.headPosition.x = e.Location.X;
            this.headPosition.y = e.Location.Y;

            System.Diagnostics.Debug.Write("Mouse pos: " + this.headPosition + ", color: " + Win32.GetPixelColor(this.headPosition.x, this.headPosition.y));

            this.updateUI();

            //Disable hook - for now
            this.globalHook.MouseClick -= GlobalHook_MouseClick;
        }
    }
}
