using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameArcade.Subclasses
{
    public class C_VideoPreview : C_Film
    {
        public Button btnPlayVideo;
        public override void Init(Sprite _icon, string _name, string _filmPath, string _videoInfo)
        {
            base.Init(_icon, _name, _filmPath, _videoInfo);
            btnPlayVideo.onClick.RemoveAllListeners();
            btnPlayVideo.onClick.AddListener(() => FindObjectOfType<ArcadeManager>().GoToVideoInfo(this));
        }

    }
}

