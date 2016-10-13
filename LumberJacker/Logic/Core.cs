using Gma.System.MouseKeyHook;
using LumberJacker.Logic;
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

namespace LumberJacker
{
    public class Core
    {
        private IKeyboardMouseEvents globalHook;
        
        //References to UI methods
        public Action disableUI;
        public Action enableUI;
        public Action updateTexts;

        //Bot class
        public Bot bot;

        //Booleans to keep track if the bot is ready to be started or currently running
        private bool botShouldStart;
        private bool botRunning;
        
        public Core()
        {
            //Set globalHook to reference Hook class's method GlobalEvents
            this.globalHook = Hook.GlobalEvents();

            //Create the bot object
            this.bot = new Bot();

            //Start listening to keypresses in method GlobalHook_KeyPress
            this.globalHook.KeyPress += GlobalHook_KeyPress;

            //Bot should not start before the user presses start button
            this.botShouldStart = false;
        }
        
        public void StartBot()
        {
            //Disable input in UI
            this.disableUI();

            //Set the bot to be ready to start
            this.botShouldStart = true;
        }

        public void SetActions(Action disableUI, Action enableUI, Action updateTexts)
        {
            this.disableUI = disableUI;
            this.enableUI = enableUI;
            this.updateTexts = updateTexts;
        }

        /*
         *
         * Head position selection
         *
         */

        public void BeginHeadPosSelection()
        {
            this.globalHook.MouseClick += GlobalHook_MouseClick;
        }
        
        private void GlobalHook_MouseClick(object sender, MouseEventArgs e)
        {
            //Set head position as the click position
            this.bot.headPosition.x = e.Location.X;
            this.bot.headPosition.y = e.Location.Y;

            //Enable all UI buttons and update UI text
            this.enableUI();
            this.updateTexts();

            //Disable hook - for now
            this.globalHook.MouseClick -= GlobalHook_MouseClick;
        }

        /*
         *
         * Key listener
         *
         */
        private void GlobalHook_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 's' || e.KeyChar == 'S')
            {
                if (botShouldStart)
                {
                    //Start the bot in a new background thread
                    new Thread(() =>
                    {
                        Thread.CurrentThread.IsBackground = true;
                        this.bot.Run();

                    }).Start();

                    //The bot should not start anymore
                    this.botShouldStart = false;
                    this.botRunning = true;
                }
                else if (!botShouldStart && botRunning)
                {
                    //Tell the bot to stop
                    this.bot.Stop();

                    //It's not running anymore (soon)
                    this.botRunning = false;

                    //Enable UI
                    this.enableUI();
                }
            }
        }
    }
}
