using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameArcade
{
    public class ArcadeManager : ParentOfAll
    {
        public FileReader fileReader;
        public static string gameExesPath = "D:/rsofia/Documents/3DMX/VIII/Proyecto de Titulacion II/Game Arcade/Game-Arcade/Arcade/";

        [Header("Menu")]
        public GameObject gameBtnPrefab;
        public GameObject parentMenuGamePrefab;

        private List<string> folders = new List<string>();

        void Start()
        {
           folders = fileReader.ReadAllFoldersAtPath(gameExesPath);
            foreach(string str in folders)
            {
                GameObject btn = GameObject.Instantiate(gameBtnPrefab, parentMenuGamePrefab.transform);
                btn.GetComponent<Subclasses.C_Game>().Init(str, null, gameExesPath + str);
            }
        }

    }
}

