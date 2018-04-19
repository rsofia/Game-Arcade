using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameArcade
{
    public class MenuManager : ParentOfAll
    {
        [Tooltip("All the submenus in the game arcade. Ex. Film Menu, Games Menu")]
        public GameObject[] submenus;
        [Tooltip("The main menu. Ex. Menu to with buttons to open film or games menu")]
        public GameObject mainMenu;
        public Subclasses.C_VideoPreview videoPreview;

        private void Start()
        {
            //Close all at start
            OpenMainMenu();
        }

        //Turns off all the sumenus
        private void CloseSubMenus()
        {
            foreach(GameObject obj in submenus)
            {
                obj.SetActive(false);
            }
        }

        //turn off all submenus and opens the main menu
        public void OpenMainMenu()
        {
            CloseSubMenus();
            mainMenu.SetActive(true);
        }

        private void CloseMainMenu()
        {
            mainMenu.SetActive(false);
        }

        //Open the specified submenu and closes the rest
        // and closes the main menu
        public void OpenSubmenu(GameObject _submenu)
        {
            foreach(GameObject sub in submenus)
            {
                if (_submenu == sub)
                    sub.SetActive(true);
                else
                    sub.SetActive(false);
            }
            
            CloseMainMenu();
        }

        //Open the specified submenu and closes the rest, but by index,
        // and closes the main menu
        public void OpenSubmenu(int _index)
        {
            for(int i = 0; i < submenus.Length; i++)
            {
                if (i == _index)
                    submenus[i].SetActive(true);
                else
                    submenus[i].SetActive(false);
            }

            CloseMainMenu();
        }

        //Opens a submenu but keeps the others open too
        public void OpenSubmenuWithoutClosingOthers(int _index)
        {
            if (_index >= 0 && _index < submenus.Length)
            {
                submenus[_index].SetActive(true);
            }
        }

        //Close a specified submenu
        public void CloseSubmenu(int _index)
        {
            if(_index >= 0 && _index < submenus.Length)
            {
                submenus[_index].SetActive(false);
            }
        }

        public void FillInfoWith(Subclasses.C_Film _film)
        {
            videoPreview.Init(_film.icono.sprite, _film.nombre, _film.filmPath);
        }

    }
}

