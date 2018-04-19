﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameArcade.Subclasses
{
    public class C_Film : C_Subclasses, ISelectHandler
    {
        public enum CATEGORIA
        {
            COMEDIA,
            TERROR,
            DOCUMENTAL
        }

        [HideInInspector]
        public string nombre;
        [HideInInspector]
        public string filmPath;
        [HideInInspector]
        public string videoInfo;
        [HideInInspector]
        public bool isYoutubeVideo = false;
        [HideInInspector]
        public string director = "";
        [HideInInspector]
        public CATEGORIA categoria;

        public void Init(string _name,  string _filePath,  bool _isYoutubeVideo = false)
        {
            nombre = _name;
            filmPath = _filePath + nombre;
            txtNombre.text = nombre;
            isYoutubeVideo = _isYoutubeVideo;

            //Leer png de un archivo png o jpg dentro de una carpeta
            Texture2D _icon = FileReader.GetImageInsideFolder(filmPath);
            if (_icon != null)
                AssignImage(_icon);

            //Leer informacion de un archivo de texto
            string[] lines = FileReader.ReadAllLinesFromTxtAtPath(filmPath);
            if (lines != null)
            {
                if (lines.Length >= 1)
                    categoria = (CATEGORIA)int.Parse(lines[0]);
                if (lines.Length >= 2)
                    director = lines[1];
                videoInfo = "<b>" + director + "</b>\n";
                for (int i = 1; i < lines.Length; i++)
                {
                    videoInfo += lines[i] + "\n";
                }

            }
            else
                Debug.Log("Lines was null");
        }

        public virtual void Init(Sprite _icon, string _name, string _filmPath, string _videoInfo, CATEGORIA _cat)
        {
            nombre = _name;
            filmPath = _filmPath;
            txtNombre.text = nombre;
            icono.sprite = _icon;
            videoInfo = _videoInfo;
            categoria = _cat;
        }

        private MenuManager menuManager;

        public void OnSelect(BaseEventData eventData)
        {
            if (menuManager == null)
                menuManager = FindObjectOfType<MenuManager>();
            menuManager.FillInfoWith(GetComponent<C_Film>());
            Debug.Log(gameObject.name + " was selected");
        }

    }
}
