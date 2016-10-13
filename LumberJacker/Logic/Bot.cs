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
             
                bool isThereBranch = analyzeBranchExistance(currentSide);

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
        
        private bool analyzeBranchExistance(Side currentSide)
        {
            int xDiff = XDifference(currentSide);

            //Calculate positions
            int xStart = this.headPosition.x + xDiff;
            int yStart = this.headPosition.y - 20; //~20 pixels from nose to sky even if you're a bit inaccurate
            int xEnd = xStart + 1;
            int yEnd = yStart - this.scanRadius;
            
            System.Diagnostics.Debug.WriteLine("Scanning from (" + xStart + ", " + yStart + ") to (" + xEnd + ", " + yEnd + ")");

            return scanForBranch(xStart, yStart, xEnd, yEnd);
        }

        private bool scanForBranch(int xStart, int yStart, int xEnd, int yEnd)
        {
            //Calculate scan area size
            int xSize = Math.Abs(xStart - xEnd);
            int ySize = Math.Abs(yStart - yEnd);
            
            //Create bitmap and graphics element
            Bitmap bmp = new Bitmap(xSize, ySize, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmp);

            //Copy pixels from screen to the bitmap
            g.CopyFromScreen(Math.Min(xStart, xEnd), Math.Min(yStart, yEnd), 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);

            //Scan the area for too dark pixels
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    if (bmp.GetPixel(x, y).B < 160) return true;
                }
            }

            //If didn't find any
            return false;
        }


    }
}
