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
        [HideInInspector]
        public string modelFullPath = "/Modelo.fbx";

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
            Debug.Log("Model Path: " + _modelPath);
            modelPath = _modelPath;
            modelFullPath = modelPath + modelFullPath;
            //aqui abrir el archivo y leer la info de el
            string[] lines = FileReader.ReadAllLinesFromTxtAtPath(modelPath);
            if(lines != null && lines.Length >= 1)
            {
                Debug.Log("Linea de modelo: " + lines[0]);
                JSONModelInfo modelInfo = JsonUtility.FromJson<JSONModelInfo>(lines[0]);
                nombre = modelInfo.nombre;
                nombreModelador = modelInfo.nombreModelador;
            }
            txtNombre.text = nombre;
            Texture2D myTexture = FileReader.GetTextureWithName("Thumbnail", modelPath);
            if (myTexture != null)
                AssignImage(myTexture);
        }

    }
}

