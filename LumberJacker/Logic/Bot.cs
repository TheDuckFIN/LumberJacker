using LumberJacker.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LumberJacker.Logic
{
    public class Bot
    {
        //Configuration variables
        public Vector2 headPosition;
        public Side characterStartingSide;
        public int sideDifference;
        public int waitTime;
        public int scanRadius;

        private bool shouldExit;

        public Bot()
        {
            //Initialize default variables
            this.headPosition = new Vector2(0, 0);
            this.characterStartingSide = Side.LEFT;
            this.sideDifference = 120;
            this.waitTime = 1000;
            this.scanRadius = 30;
        }

        public void Run()
        {
            //Reset exit status - We don't want it to tick only once!
            this.shouldExit = false;

            //Some temporary variables for this run only
            Side currentSide = this.characterStartingSide;

            //Main loop
            while (!this.shouldExit)
            {
                
                int diff = XDifference(currentSide);

                bool isThereBranch = analyzeBranchExistance(this.headPosition.x + diff, this.headPosition.y - 15, this.scanRadius);

                if (isThereBranch)
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

        public void Stop()
        {
            this.shouldExit = true;
        }

        /*
         *
         * Private methods
         *
         */
         
        private int XDifference(Side currentSide)
        {
            if (this.characterStartingSide == Side.LEFT && currentSide == Side.RIGHT)
            {
                return this.sideDifference;
            }
            else if (this.characterStartingSide == Side.RIGHT && currentSide == Side.LEFT)
            {
                return -this.sideDifference;
            }
            else
            {
                return 0;
            }
        }
        
        private bool analyzeBranchExistance(int x, int y, int range)
        {
            Rectangle rect = new Rectangle(0, 0, 1, range);
            Bitmap bmp = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);
            g.CopyFromScreen(x, y - range, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

            for (int i = 0; i < range; i++)
            {
                if (bmp.GetPixel(0, i).B < 160) return true;
            }

            return false;
        }


    }
}
