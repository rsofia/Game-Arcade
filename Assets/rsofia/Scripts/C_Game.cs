using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace GameArcade.Subclasses
{
    public class C_Game : C_Subclasses
    {
        private string nombre;
        private string gamePath;
        public string sinopsis;
        public int dimension;
        public int cam;
        public int[] genres;
        public bool hasDLL;


        public void Init(string _name, Texture2D _icon, string _gamePath)
        {
            nombre = _name;
            gamePath = _gamePath;
            txtNombre.text = nombre;

            if (_icon != null)
                AssignImage(_icon);
        }

    }
}
