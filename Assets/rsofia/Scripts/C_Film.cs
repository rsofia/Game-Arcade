using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace GameArcade.Subclasses
{
    public class C_Film : C_Subclasses, ISelectHandler
    {
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
        public int[] generos;
        [HideInInspector]
        public int categoria;

        public void Init(string _name,  string _filePath,  bool _isYoutubeVideo = false)
        {
            nombre = _name;
            filmPath = _filePath + nombre;
            isYoutubeVideo = _isYoutubeVideo;

            //Leer png de un archivo png o jpg dentro de una carpeta
            Texture2D _icon = FileReader.GetImageInsideFolder(filmPath);
            if (_icon != null)
                AssignImage(_icon);

            //Leer informacion de un archivo de texto
            string[] lines = FileReader.ReadAllLinesFromTxtAtPath(filmPath);
            if (lines != null && lines.Length > 0)
            {
                JSONVideoDetails videoDetails = JsonUtility.FromJson<JSONVideoDetails>(lines[0]);
                director = videoDetails.director;
                nombre = videoDetails.title;
                generos = videoDetails.genres;
                categoria = videoDetails.category;
                videoInfo = "<b>Director: " + director + "</b>\n" + videoDetails.sinopsis;

            }
            else
                Debug.Log("Lines was null");


            txtNombre.text = nombre;
        }

        public virtual void Init(Sprite _icon, string _name, string _filmPath, string _videoInfo)
        {
            nombre = _name;
            filmPath = _filmPath;
            txtNombre.text = nombre;
            icono.sprite = _icon;
            videoInfo = _videoInfo;
        }

        private MenuManager menuManager;

        public void OnSelect(BaseEventData eventData)
        {
            if (menuManager == null)
                menuManager = FindObjectOfType<MenuManager>();
            menuManager.FillInfoWith(GetComponent<C_Film>());
           // Debug.Log(gameObject.name + " was selected");
        }

    }
}
