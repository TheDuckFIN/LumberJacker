using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumberJacker
{
    public class Core
    {
        private IKeyboardMouseEvents globalHook;
        

        public Core()
        {
            this.globalHook = Hook.GlobalEvents();
        }

        public void BeginHeadPosSelection()
        {

        }

    }
}
