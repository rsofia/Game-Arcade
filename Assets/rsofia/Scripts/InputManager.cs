using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameArcade.Inputs
{
    public class InputManager : ParentOfAll
    {
        public MenuManager menuManager;

        private void Update()
        {
            //Recibir Inputs
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                menuManager.OpenMainMenu();
            }

        }

    }
}

