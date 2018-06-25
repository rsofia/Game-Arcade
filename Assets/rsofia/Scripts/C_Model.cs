using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
namespace GameArcade.Subclasses
{
    public class C_Model : C_Subclasses
    {
        public string nombre;
        public string modelPath;
        public string nombreModelador;

        public void Init( string _modelPath, string _name, Texture2D _icon, string _nombreModelador)
        {
            nombreModelador = _nombreModelador;
            modelPath = _modelPath;
            nombre = _name;
            txtNombre.text = nombre;
            if (_icon != null)
                AssignImage(_icon);
        }

        public void Init(string _modelPath)
        {
            //aqui abrir el archivo y leer la info de el
            //FileReader.
        }

    }
}

