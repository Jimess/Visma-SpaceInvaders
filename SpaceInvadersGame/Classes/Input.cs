using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace SpaceInvadersGame.Classes
{
    class Input
    {
        //load keyboard buttons
        private static Hashtable keyboard = new Hashtable();

        //check if button is pressed
        public static bool GetKeyPressed(Keys key)
        {
            if (keyboard[key] != null)
            {
                return (bool)keyboard[key];
            }

            return false;
        }

        // if button is pressed the key state in the hashtable is changed
        public static void ChangeState(Keys key, bool state)
        {
            keyboard[key] = state;
        }
    }
}
