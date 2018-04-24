﻿//Made by luisquid11
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

    public void OnUploadVideoFilesClicked()
    {
        Directory.CreateDirectory(persistentDataPath + "/Video/" + uploadName);
        File.Copy(pathVideo, persistentDataPath + "/Video/" + uploadName + "/" + uploadName + Path.GetExtension(pathVideo));
        File.Copy(pathVideoInfo, persistentDataPath + "/Video/" + uploadName + "/" + uploadName + ".txt");
        File.Copy(pathImg, persistentDataPath + "/Video/" + uploadName + "/" + uploadName + Path.GetExtension(pathImg));
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

    public void OnUploadImageClicked()
    {
        var extensions = new[] { new ExtensionFilter("Image File", "img", "png") };
        pathImg = GameObject.Find("TXTMSH_Thumbnail").GetComponent<TextMeshProUGUI>().text = StandaloneFileBrowser.OpenFilePanel("Upload Game Thumbnail", "", "", false)[0];
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
