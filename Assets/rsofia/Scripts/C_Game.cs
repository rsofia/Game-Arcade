using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameArcade.Subclasses
{
    public class C_Game : ParentOfAll
    {
        private string nombre;
        private string gamePath;

        [Header("UI")]
        public Image icono;
        public Text txtNombre;

        public void Init(string _name, Sprite _icon, string _gamePath)
        {
            nombre = _name;
            gamePath = _gamePath;
            icono.sprite = _icon;
            txtNombre.text = nombre;
        }

    }
}
