//Made by luisquid11
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.UI;
using SFB;

public class SCR_FileManager : MonoBehaviour {

    public static string persistentDataPath;

    public GameObject GO_AddGame;

    //Game Files
    private string pathExe;
    private string pathDataFolder;

    //Video Files
    private string pathVideo;
    private string pathVideoInfo;

    //Model Files
    private string pathModel;
    
    private string pathImg;
    private string uploadName = "";
    private string pathBanner;
    private string title = "";

    public GameObject loader;

    [Header("Game UI")]
    public TextMeshProUGUI txtGamePath;
    public TextMeshProUGUI txtGameDataPath;
    public TextMeshProUGUI txtGameThumbnail;
    public InputField inptfldGameTitle;
    public InputField inptfldGameSinopsis;
    public Transform gameGenreWrap;
    public Transform gameDimensionWrap;
    public Transform gameCameraWrap;
    public TextMeshProUGUI txtGameDLL;
    private bool doesGameUseDLL = false;
    public GameObject dllPanel;
    public GameObject dllYesNo;
    public GameObject dllAdd;

    [Header("Video UI")]
    public InputField inptfldVideoTitle;
    public InputField inptfldVideoDirector;
    public InputField inptfldVideoSinopsis;
    public Dropdown ddVideoCategoria;
    public TextMeshProUGUI txtVideoPath;
    public TextMeshProUGUI txtVideoThumbnail;
    public TextMeshProUGUI txtVideoBanner;
    public Transform genreWrap;
    
    #region UPLOAD FILES
    /***************************** GAME FILES ********************************/
    public void OnUploadExeClicked()
    {
        var extensions = new[] {
                new ExtensionFilter("Executable File", "exe"),
            };

        pathExe = txtGamePath.GetComponent<TextMeshProUGUI>().text = StandaloneFileBrowser.OpenFilePanel("Upload Exe", "", extensions, false)[0];

        uploadName = Path.GetFileNameWithoutExtension(pathExe);

        if (Directory.Exists(persistentDataPath + "/Games/" + uploadName))
        {
            Debug.Log("YA EXISTE SALIR SALIR");
        }
    }

    public void OnUploadDataFolderClicked()
    {
        if (uploadName == "")
        {
            Debug.Log("Exe hasnt been uploaded");
            return;
        }

        pathDataFolder = txtGameDataPath.GetComponent<TextMeshProUGUI>().text = StandaloneFileBrowser.OpenFolderPanel("Upload Data Folder", "", false)[0];

        if (pathDataFolder.Contains(uploadName))
        {
            Debug.Log("YES");
        }

        else
        {
            Debug.Log("NO");
        }
        Debug.Log(pathDataFolder);
    }

    private void MakeGameInfo(string path)
    {
        JSONGameInfo gameInfo = new JSONGameInfo()
        {
            title = inptfldGameTitle.text,
            sinopsis = inptfldGameSinopsis.GetComponent<InputField>().text
        };

        foreach(C_GameCamera cam in FindObjectsOfType<C_GameCamera>())
        {
            if (cam.GetComponent<Toggle>().isOn)
            {
                gameInfo.camera = (int)cam.camType;
                break;
            }
        }

        foreach (C_GameDimension dim in FindObjectsOfType<C_GameDimension>())
        {
            if (dim.GetComponent<Toggle>().isOn)
            {
                gameInfo.dimension = (int)dim.dim;
                break;
            }
        }

        //get selected toggles
        int counter = 0;
        C_GameGenre[] genres = FindObjectsOfType<C_GameGenre>();
        foreach (C_GameGenre genre in genres)
        {
            if (genre.GetComponent<Toggle>().isOn)
            {
                counter++;
            }
        }

        //Establich size of array
        gameInfo.genres = new int[counter];

        foreach (C_GameGenre genre in genres)
        {
            if (genre.GetComponent<Toggle>().isOn)
            {
                if (counter - 1 >= 0)
                    gameInfo.genres[counter - 1] = (int)genre.genre;
                else
                    break;
                counter--;
            }
        }

        //Aqui en un futuro encriptar

        TextWriter textWriter = new StreamWriter(path, false);
        textWriter.WriteLine(JsonUtility.ToJson(gameInfo));
        textWriter.Close();

    }


    public void OpenDLLWindow()
    {
        dllPanel.SetActive(true);
        dllYesNo.SetActive(true);
    }

    public void OnYesDLL()
    {
        dllYesNo.SetActive(false);
        dllAdd.SetActive(true);
        doesGameUseDLL = true;
    }

    public void OnUploadGameFilesClicked()
    {
        loader.SetActive(true);
        title = inptfldGameTitle.text;
        title = title.Replace(" ", "_");
        string nombreArchivo = title + "_" + uploadName;

        Directory.CreateDirectory(persistentDataPath + "/Games/" + nombreArchivo);
        File.Copy(pathExe, persistentDataPath + "/Games/" + nombreArchivo + "/" + nombreArchivo + ".exe");
        File.Copy(pathImg, persistentDataPath + "/Games/" + nombreArchivo + "/Thumbnail"+ Path.GetExtension(pathImg));
        MakeGameInfo(persistentDataPath + "/Games/" + nombreArchivo + "/" + nombreArchivo + ".txt");
        CopyAllFromDirectory(pathDataFolder, persistentDataPath + "/Games/" + nombreArchivo + "/" + nombreArchivo + "_Data/");

        if(doesGameUseDLL)
        {
            File.Copy(txtGameDLL.text, persistentDataPath + "/Games/" + nombreArchivo + "/UnityEngine.dll");
        }

        txtGamePath.text = "Selecciona el ejecutable del juego";
        txtGameDataPath.text = "Selecciona la carpeta Data del juego";
        txtGameThumbnail.text = "Selecciona el icono de tu proyecto";
        inptfldGameSinopsis.text = "";
        inptfldGameTitle.text = "";
        txtGameDLL.text = "Selecciona el DLL del juego";
        doesGameUseDLL = false;
        foreach (Toggle t in gameGenreWrap.GetComponentsInChildren<Toggle>())
        {
            t.isOn = false;
        }
        gameDimensionWrap.GetComponentInChildren<Toggle>().isOn = true;
        gameCameraWrap.GetComponentInChildren<Toggle>().isOn = true;

        if (GameObject.FindObjectOfType<SCR_AdminAccess>() != null)
        {
            loader.SetActive(false);
            FindObjectOfType<SCR_AdminAccess>().OpenOptionsMenu();
        }
        else
        {
            Debug.Log("No esta en la escena indicada");
        }
    }
    /*************************************************************************/

    /***************************** VIDEO FILES *******************************/
    public void OnUploadVideoClicked()
    {
        var extensions = new[] {
                new ExtensionFilter("Video File", "mp4", "asf", "mov", "mpg", "mpeg", "ogv", "uvp8", "wmv"),
            };

        pathVideo = txtVideoPath.text = StandaloneFileBrowser.OpenFilePanel("Upload Video File", "", extensions, false)[0];

        uploadName = Path.GetFileNameWithoutExtension(pathVideo);

        if (Directory.Exists(persistentDataPath + "/Videos/" + uploadName))
        {
            Debug.Log("YA EXISTE SALIR SALIR");
        }
    }

    public void OnUploadVideoInfo()
    {
        if (uploadName == "")
        {
            Debug.Log("Video hasnt been uploaded");
            return;
        }

        var extensions = new[] {
                new ExtensionFilter("Text File", "txt"),
            };

        pathVideoInfo = GameObject.Find("TXTMSH_DataFolder").GetComponent<TextMeshProUGUI>().text = StandaloneFileBrowser.OpenFilePanel("Upload Video Info", "", extensions, false)[0];

        Debug.Log(pathVideoInfo);
    }

    private void MakeVideoInfo(string path)
    {
        JSONVideoDetails videoDetails = new JSONVideoDetails()
        {
            //find input field with movie title
            director = inptfldVideoDirector.text,
            //find input field with directors name
            title = inptfldVideoTitle.text,
            sinopsis = inptfldVideoSinopsis.text,
            category = ddVideoCategoria.value
        };
        //get selected toggles
        int counter = 0;
        C_FilmGenre[] genres = FindObjectsOfType<C_FilmGenre>();
        foreach (C_FilmGenre genre in genres)
        {
            if(genre.GetComponent<Toggle>().isOn)
            {
                counter++;
            }
        }

        //Establich size of array
        videoDetails.genres = new int[counter];

        foreach (C_FilmGenre genre in genres)
        {
            if(genre.GetComponent<Toggle>().isOn)
            {
                if (counter - 1 >= 0)
                    videoDetails.genres[counter - 1] = (int)genre.genre;
                else
                    break;
                counter--;
            }
        }
        
        //Aqui en un futuro encriptar

        TextWriter textWriter = new StreamWriter(path, false);
        textWriter.WriteLine(JsonUtility.ToJson(videoDetails));
        textWriter.Close();

    }

    
    public void OnUploadVideoFilesClicked()
    {
        loader.SetActive(true);
        title = inptfldVideoTitle.text;
        title = title.Replace(" ", "_");
        string nombreArchivo = title + "_" + uploadName;
        Directory.CreateDirectory(persistentDataPath + "/Video/" + nombreArchivo);
        File.Copy(pathVideo, persistentDataPath + "/Video/" + nombreArchivo + "/" + title + Path.GetExtension(pathVideo));
        MakeVideoInfo(persistentDataPath + "/Video/" + nombreArchivo + "/" + title + ".txt");
        //File.Copy(pathVideoInfo, persistentDataPath + "/Video/" + uploadName + "/" + uploadName + ".txt");
        File.Copy(pathImg, persistentDataPath + "/Video/" + nombreArchivo + "/Thumbnail" + Path.GetExtension(pathImg));

        File.Copy(pathBanner, persistentDataPath + "/Video/" + nombreArchivo + "/Banner" + Path.GetExtension(pathBanner));
        
        Debug.Log("Video uploaded succesfully!");

        //Reset Paths
        inptfldVideoTitle.text = "";
        inptfldVideoDirector.text = "";
        inptfldVideoSinopsis.text = "";
        ddVideoCategoria.value = 0;
        txtVideoPath.GetComponent<TextMeshProUGUI>().text = "Selecciona el vídeo a subir";
        txtVideoThumbnail.text = "Selecciona el icono de tu proyecto";
        txtVideoBanner.text = "Selecciona el banner de tu proyecto";
        foreach(Toggle t in genreWrap.GetComponentsInChildren<Toggle>())
        {
            t.isOn = false;
        }



        if (GameObject.FindObjectOfType<SCR_AdminAccess>() != null)
        {
            loader.SetActive(false);
            FindObjectOfType<SCR_AdminAccess>().OpenOptionsMenu();
        }
        else
        {
            Debug.Log("No esta en la escena indicada");
        }

    }
    /*************************************************************************/

    /***************************** MODEL FILES *******************************/
    public void OnUploadModelClicked()
    {

    }

    public void OnUploadModelInfoClicked()
    {

    }

    public void OnUploadModelFilesClicked()
    {

    }
    /*************************************************************************/

    public void OnUploadImageClicked(bool isbanner = false)
    {
        var extensions = new[] { new ExtensionFilter("Image File", "img", "png") };
        if(!isbanner)
        {
            pathImg = GameObject.Find("TXTMSH_Thumbnail").GetComponent<TextMeshProUGUI>().text = StandaloneFileBrowser.OpenFilePanel("Upload Thumbnail", "", "", false)[0];
        }
        else
        {
            pathBanner = GameObject.Find("TXTMSH_Banner").GetComponent<TextMeshProUGUI>().text = StandaloneFileBrowser.OpenFilePanel("Upload Banner", "", "", false)[0];
        }
        Debug.Log(pathImg);
    }

    public void CopyAllFromDirectory(string _sourcePath, string _destinationPath)
    {
        foreach (string dirPath in Directory.GetDirectories(_sourcePath, "*",
                   SearchOption.AllDirectories))
            Directory.CreateDirectory(dirPath.Replace(_sourcePath, _destinationPath));

        //Copy all the files & Replaces any files with the same name
        foreach (string newPath in Directory.GetFiles(_sourcePath, "*.*",
            SearchOption.AllDirectories))
            File.Copy(newPath, newPath.Replace(_sourcePath, _destinationPath), true);
    }
    #endregion

    #region READ FILES
    public string[] GetAllFoldersFromPath(string _path)
    {
        string[] DirectoriesInPath = Directory.GetDirectories(_path);

        return DirectoriesInPath;
    }
    #endregion

    public void ResetPaths()
    {
        pathExe = "";
        pathDataFolder = "";
        pathImg = "";
        uploadName = "";

        GameObject.Find("TXTMSH_Exe").GetComponent<TextMeshProUGUI>().text = "Upload your game exe here...";
        GameObject.Find("TXTMSH_DataFolder").GetComponent<TextMeshProUGUI>().text = "Upload your data folder here...";
        GameObject.Find("TXTMSH_Thumbnail").GetComponent<TextMeshProUGUI>().text = "Upload your thumbnail image here...";
    }
    public void OpenFile()
    {
        //StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);
        GO_AddGame.SetActive(!GO_AddGame.activeSelf);
    }

    void Start () 
	{
        persistentDataPath = Application.persistentDataPath;
        Debug.Log(persistentDataPath);

        Debug.Log("Here I Am");
        Debug.Log(persistentDataPath + "/Games/");
        if (Directory.Exists(persistentDataPath + "/Games/"))
        {
            string[] tempDirectories = GetAllFoldersFromPath(persistentDataPath + "/Games/");

            for (int i = 0; i < tempDirectories.Length; i++)
            {
                Debug.Log(tempDirectories[i]);
            }
        }

        else
        {
            Debug.Log("I do not exist");
            Directory.CreateDirectory(persistentDataPath + "/Games/");
        }

        dllPanel.SetActive(false);
        dllYesNo.SetActive(false);
        dllAdd.SetActive(false);
    }
}
