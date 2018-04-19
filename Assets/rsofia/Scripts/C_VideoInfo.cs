using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameArcade.Subclasses
{
    public class  C_VideoInfo : C_Film
    {
        public TextMeshProUGUI txtInfo;
        public Button btnPlay;
        private string videoPath = "";
        
        private void DisplayInfo()
        {
            txtInfo.text = videoInfo;
            txtNombre.text = nombre;
        }

        public void Init(C_Film _film)
        {
            nombre = _film.nombre;
            icono.sprite = _film.icono.sprite;
            videoInfo = _film.videoInfo;
            filmPath = _film.filmPath;
            isYoutubeVideo = _film.isYoutubeVideo;
            director = _film.director;
            DisplayInfo();

            videoPath = FileReader.GetVideoPathFrom(filmPath);
            if (string.IsNullOrEmpty(videoPath))
                Debug.Log("Video Path was null!");
            btnPlay.onClick.RemoveAllListeners();
            btnPlay.onClick.AddListener(() => FindObjectOfType<ArcadeManager>().PlayVideo(videoPath));
        }

    }
}
