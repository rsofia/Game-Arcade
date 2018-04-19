using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameArcade.Subclasses
{
    public class C_VideoPreview : C_Film
    {
        public Button btnPlayVideo;
        public override void Init(Sprite _icon, string _name, string _filmPath, string _videoInfo, CATEGORIA _cat)
        {
            base.Init(_icon, _name, _filmPath, _videoInfo, _cat);
            btnPlayVideo.onClick.RemoveAllListeners();
            btnPlayVideo.onClick.AddListener(() => FindObjectOfType<ArcadeManager>().GoToVideoInfo(this));
        }

    }
}

