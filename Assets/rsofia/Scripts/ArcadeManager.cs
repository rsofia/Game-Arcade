using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using GameArcade.Subclasses;

namespace GameArcade
{
    public enum FilmGenre
    {
        ACCION,
        AVENTURA,
        COMEDIA,
        CIENCIA_FICCION,
        DOCUMENTAL,
        DRAMA,
        MUSICAL,
        HISTORIA,
        TERROR,
        OTRO
    }

    public enum VideoCategories
    {
        CORTOMETRAJE,
        TRAILER,
        DEMO_REEL,
        BEHIND_THE_SCENES
    }

    public class ArcadeManager : ParentOfAll
    {
        public MenuManager menuManager;

        [Header("Menu Games")]
        public static string gameExesPath;
        public GameObject gameBtnPrefab;
        public Transform parentMenuGame;

        [Header("Menu Films")]
        public static string filmPath;
        public GameObject scrollCategoriaPrefab;
        public GameObject txtTitlePrefab;
        public GameObject filmBtnPrefab;
        public Transform parentMenuFilm;
        public C_VideoInfo videoInfo;

        [Header("Video Player")]
        public VideoManager videoManager;

        private List<string> folders = new List<string>();
        private Dictionary<int, GameObject> genres = new Dictionary<int, GameObject>();
        private Dictionary<int, C_Game> allGames = new Dictionary<int, C_Game>();
        private Dictionary<string, C_Film> allFilms = new Dictionary<string, C_Film>();


        private SCR_FileManager scrFileManager;

        public string[] generoFilm = { "Acción", "Aventura ", "Comedia", "Terror" };
            

        void Start()
        {
            scrFileManager = FindObjectOfType<SCR_FileManager>();
            if (string.IsNullOrEmpty(SCR_FileManager.persistentDataPath))
                SCR_FileManager.persistentDataPath = Application.persistentDataPath;
            gameExesPath = SCR_FileManager.persistentDataPath + "/Games/";
            filmPath = SCR_FileManager.persistentDataPath + "/Video/";

            //Check for games and videos only at start of app
            LoadAllGames();
            LoadAllVideos();
        }

#region LOAD
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
            for (int i = parentMenuFilm.childCount - 1; i >= 0; i--)
            {
                Destroy(parentMenuFilm.GetChild(i).gameObject);
            }

            //Get all the genres inside
            genres.Clear();
            allFilms.Clear();
            foreach (string fileName in folders)
            {
                C_Film film = new C_Film();
                film.Init(fileName, filmPath);
                allFilms.Add(film.nombre, film);
                if(!genres.ContainsKey(film.categoria))
                {
                    //Title
                    GameObject txtTitle = Instantiate(txtTitlePrefab, parentMenuFilm);
                    txtTitle.GetComponent<Text>().text = (((VideoCategories)film.categoria).ToString()).Replace("_", " ");
                    //Scroll Category (horizontal)
                    GameObject scrollCat = GameObject.Instantiate(scrollCategoriaPrefab, parentMenuFilm);
                    scrollCat.name = ((VideoCategories)film.categoria).ToString();
                    genres.Add(film.categoria, scrollCat);
                }
            }

            foreach(KeyValuePair <string, C_Film > pair in allFilms)
            {
                foreach(KeyValuePair < int, GameObject > cat in genres)
                {
                    if (pair.Value.categoria == cat.Key)
                    {
                        GameObject btn = Instantiate(filmBtnPrefab, cat.Value.transform.Find("Viewport").Find("Content"));
                        btn.GetComponent<C_Film>().Init(pair.Value);
                        btn.GetComponent<Button>().onClick.AddListener(() => GoToVideoInfo(btn.GetComponent<C_Film>()));
                        break;
                    }
                }               
            }

            //After the initialization, find the first one to select it and display on banner
            //Its the second child of the parentMenuFilm because the first one is a text
            if(parentMenuFilm.transform.childCount >= 2)
            {
                parentMenuFilm.transform.GetChild(1).Find("Viewport").GetChild(0).GetChild(0).GetComponent<C_Film>().CallOnSelect();
            }

            folders.Clear();
        }
#endregion

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

