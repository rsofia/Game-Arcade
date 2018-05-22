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


    #region UPLOAD FILES
    /***************************** GAME FILES ********************************/
    public void OnUploadExeClicked()
    {
        var extensions = new[] {
                new ExtensionFilter("Executable File", "exe"),
            };

        pathExe = GameObject.Find("TXTMSH_Exe").GetComponent<TextMeshProUGUI>().text = StandaloneFileBrowser.OpenFilePanel("Upload Exe", "", extensions, false)[0];

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

        pathDataFolder = GameObject.Find("TXTMSH_DataFolder").GetComponent<TextMeshProUGUI>().text = StandaloneFileBrowser.OpenFolderPanel("Upload Data Folder", "", false)[0];

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

    public void OnUploadGameFilesClicked()
    {
        Directory.CreateDirectory(persistentDataPath + "/Games/" + uploadName);
        File.Copy(pathExe, persistentDataPath + "/Games/" + uploadName + "/" + uploadName + ".exe");
        File.Copy(pathImg, persistentDataPath + "/Games/" + uploadName + "/" + uploadName + Path.GetExtension(pathImg));
        CopyAllFromDirectory(pathDataFolder, persistentDataPath + "/Games/" + uploadName + "/" + uploadName + "_Data/");
    }
    /*************************************************************************/

    /***************************** VIDEO FILES *******************************/
    public void OnUploadVideoClicked()
    {
        var extensions = new[] {
                new ExtensionFilter("Video File", "mp4", "asf", "mov", "mpg", "mpeg", "ogv", "uvp8", "wmv"),
            };

        pathVideo = GameObject.Find("TXTMSH_Exe").GetComponent<TextMeshProUGUI>().text = StandaloneFileBrowser.OpenFilePanel("Upload Video File", "", extensions, false)[0];

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
            director = GameObject.Find("INPTFLD_Director").GetComponent<InputField>().text,
            //find input field with directors name
            title = GameObject.Find("INPTFLD_Title").GetComponent<InputField>().text,
            sinopsis = GameObject.Find("INPTFLD_Sinopsis").GetComponent<InputField>().text,
            category = GameObject.Find("DD_Categoria").GetComponent<Dropdown>().value
        };
        title = videoDetails.title;
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
        title = GameObject.Find("INPTFLD_Title").GetComponent<InputField>().text;
        string nombreArchivo = title + "_" + uploadName;
        Directory.CreateDirectory(persistentDataPath + "/Video/" + nombreArchivo);
        File.Copy(pathVideo, persistentDataPath + "/Video/" + nombreArchivo + "/" + title + Path.GetExtension(pathVideo));
        MakeVideoInfo(persistentDataPath + "/Video/" + nombreArchivo + "/" + title + ".txt");
        //File.Copy(pathVideoInfo, persistentDataPath + "/Video/" + uploadName + "/" + uploadName + ".txt");
        File.Copy(pathImg, persistentDataPath + "/Video/" + nombreArchivo + "/" + title + "_Thumbnail" + Path.GetExtension(pathImg));

        File.Copy(pathImg, persistentDataPath + "/Video/" + nombreArchivo + "/" + title + "_Banner" + Path.GetExtension(pathBanner));

        Debug.Log("Video uploaded succesfully!");

        if(GameObject.FindObjectOfType<SCR_AdminAccess>() != null)
        {
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

    }
}
