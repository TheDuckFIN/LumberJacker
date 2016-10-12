using Gma.System.MouseKeyHook;
using LumberJacker.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LumberJacker
{
    public class Core
    {
        private IKeyboardMouseEvents globalHook;
        private bool botEnabled = false;

        public Vector2 headPosition;
        public Side characterSide;
        public int branchHeight = 30;
        public int sideDifference = 120;
        public int waitTime = 200;

        public Action updateUI;


        public Core()
        {
            this.globalHook = Hook.GlobalEvents();
            this.headPosition = new Vector2(0, 0);
            this.characterSide = Side.LEFT;
        }
        
        public void StartBot()
        {
            this.globalHook.KeyPress += GlobalHook_KeyPress;

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                
                Side currentSide = characterSide;
                int diff = 0;

                while(true)
                {
                    if (botEnabled)
                    {
                        if (characterSide == Side.LEFT && currentSide == Side.RIGHT)
                        {
                            diff = sideDifference;
                        }
                        else if (characterSide == Side.RIGHT && currentSide == Side.LEFT)
                        {
                            diff = -sideDifference;
                        }
                        else
                        {
                            diff = 0;
                        }

                        Color pixelColor = Win32.GetPixelColor(this.headPosition.x + diff, this.headPosition.y - branchHeight);
                        
                        System.Diagnostics.Debug.WriteLine("Color: " + pixelColor);

                        if (pixelColor.G < 180 && pixelColor.B < 180)
                        {
                            if (currentSide == Side.LEFT)
                            {
                                SendKeys.SendWait("{RIGHT}");
                                currentSide = Side.RIGHT;
                            }
                            else
                            {
                                SendKeys.SendWait("{LEFT}");
                                currentSide = Side.LEFT;
                            }
                        }
                        else
                        {
                            if (currentSide == Side.LEFT)
                            {
                                SendKeys.SendWait("{LEFT}");
                            }
                            else
                            {
                                SendKeys.SendWait("{RIGHT}");
                            }
                        }

                        Thread.Sleep(this.waitTime);
                    }
                }

            }).Start();
        }

        public void BeginHeadPosSelection(Action updateUI)
        {
            this.updateUI = updateUI;
            this.globalHook.MouseClick += GlobalHook_MouseClick;
        }

        /*
         *
         * Hook listeners!
         *
         */

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

        private void GlobalHook_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 's' || e.KeyChar == 'S')
            {
                botEnabled = !botEnabled;
            }
        }
    }
}
