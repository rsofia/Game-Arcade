using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using GameArcade.Subclasses;

namespace GameArcade
{
    public class ArcadeManager : ParentOfAll
    {
        public MenuManager menuManager;

        [Header("Menu Games")]
        public static string gameExesPath = "D:/rsofia/Documents/3DMX/VIII/Proyecto de Titulacion II/Game Arcade/Arcade/Games/";
        public GameObject gameBtnPrefab;
        public Transform parentMenuGame;

        [Header("Menu Films")]
        public static string filmPath = "D:/rsofia/Documents/3DMX/VIII/Proyecto de Titulacion II/Game Arcade/Arcade/Film/";
        public GameObject filmBtnPrefab;
        public Transform parentMenuFilm;
        public C_VideoInfo videoInfo;

        [Header("Video Player")]
        public VideoManager videoManager;

        private List<string> folders = new List<string>();
        private Dictionary<int, C_Game> allGames = new Dictionary<int, C_Game>();
        private Dictionary<int, C_Game> allFilms = new Dictionary<int, C_Game>();


        private SCR_FileManager scrFileManager;

        void Start()
        {
            scrFileManager = FindObjectOfType<SCR_FileManager>();

            //Check for games and videos only at start of app
            LoadAllGames();
            LoadAllVideos();
        }

        public void LoadAllGames()
        {
            Debug.Log("Here I Am");
            //Esto de aqui ya lo habia hecho en una clase llamada FileReader. La funcion es
            // ReadAllFoldersAtPath y recibe como argumento el path y regresa una lista de
            // todos los folders dentro - Sofia
            Debug.Log(SCR_FileManager.persistentDataPath + "/Games/");
            if(Directory.Exists(SCR_FileManager.persistentDataPath + "/Games/"))
            {
                string [] tempDirectories = scrFileManager.GetAllFoldersFromPath(SCR_FileManager.persistentDataPath + "/Games/");

                for(int i = 0; i < tempDirectories.Length; i++)
                {
                    Debug.Log(tempDirectories[i]);
                }
            }

            else
            {
                Debug.Log("I do not exist");
                Directory.CreateDirectory(SCR_FileManager.persistentDataPath + "/Games/");
            }

            //folders = FileReader.ReadAllFoldersAtPath(gameExesPath);

            ////Delete any that might exist
            //for(int i = parentMenuGame.childCount-1; i >= 0; i--)
            //{
            //    Destroy(parentMenuGame.GetChild(i).gameObject);
            //}

            //foreach (string folderName in folders)
            //{
            //    string[] exeName = FileReader.GetExeInsideFolder(gameExesPath + folderName);
            //    if (exeName.Length == 1)
            //    {
            //        GameObject btn = Instantiate(gameBtnPrefab, parentMenuGame);
            //        Texture2D img = FileReader.GetImageInsideFolder(gameExesPath + folderName);
            //        btn.GetComponent<C_Game>().Init(folderName, img, exeName[0]);
            //        btn.GetComponent<Button>().onClick.AddListener(() => OpenGame(exeName[0]));
            //    }
            //    else
            //    {
            //        //No button will be created if the file has errors
            //        Debug.Log("You either have no exes or too many exes inside folder at path " + folderName);
            //    }
            //}
            //folders.Clear();
        }

        public void LoadAllVideos()
        {
            folders = FileReader.ReadAllFoldersAtPath(filmPath);

            //Delete any buttons that might exist
            for (int i = parentMenuFilm.childCount -1; i >= 0; i--)
            {
                Destroy(parentMenuFilm.GetChild(i).gameObject);
            }

            foreach (string fileName in folders)
            {
                //Instanciar objeto de video junto con su info
                GameObject btn = Instantiate(filmBtnPrefab, parentMenuFilm);
                btn.GetComponent<C_Film>().Init(fileName, filmPath);
                btn.GetComponent<Button>().onClick.AddListener(() => GoToVideoInfo(btn.GetComponent<C_Film>()));
            }

            float parentHeight = (folders.Count % 5) == 0 ? (folders.Count / 5)  * 170 : ((folders.Count / 5)+1) * 170;
            parentMenuFilm.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, parentHeight);

            folders.Clear();
        }

        //This will be called when the button of the game is pressed
        public void OpenGame(string _gamePath)
        {
            SCR_ExecuteExe.RunGame(_gamePath);
        }

        public void GoToVideoInfo(C_Film film)
        {
            videoInfo.Init(film);
            menuManager.OpenSubmenu(2);
        }

        public void PlayVideo(string _url)
        {
            //Opens window
            menuManager.OpenSubmenuWithoutClosingOthers(3);

            //Calls video manager to play the video
            videoManager.PrenderVideo(_url);
        }       

    }
}

