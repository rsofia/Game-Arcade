using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameArcade.Subclasses
{
    public class C_Model : C_Subclasses
    {
        private string nombre;
        private string modelPath;
        private string nombreModelador;
        private int yearMade;

        public void Init(string _name, Texture2D _icon, string _modelPath, string _nombreModelador, int _year)
        {
            nombreModelador = _nombreModelador;
            modelPath = _modelPath;
            nombre = _name;
            txtNombre.text = nombre;
            yearMade = _year;
            if (_icon != null)
                AssignImage(_icon);
        }

    }
}

