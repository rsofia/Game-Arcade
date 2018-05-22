﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameArcade.Subclasses
{
    public class C_Subclasses : ParentOfAll
    {
        [Header("UI")]
        public Image icono;
        public Sprite banner;
        public Text txtNombre;
        
        protected void AssignImage(Texture2D img)
        {
            icono.sprite = Sprite.Create(img, new Rect(0, 0, img.width, img.height), Vector3.zero);
        }

        protected void AssignBanner(Texture2D img)
        {
            banner = Sprite.Create(img, new Rect(0, 0, img.width, img.height), Vector3.zero);
        }




    }
}
