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

    private string pathExe;
    private string pathDataFolder;
    private string pathImg;
    private string uploadName;

    public string [] GetAllFoldersFromPath(string _path)
    {
        string[] DirectoriesInPath = Directory.GetDirectories(_path);

        return DirectoriesInPath;
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

    public void OpenFile()
    {
        //StandaloneFileBrowser.OpenFilePanel("Open File", "", "", false);
        GO_AddGame.SetActive(!GO_AddGame.activeSelf);
    }

    public void OnUploadExeClicked()
    {
        var extensions = new[] {
                new ExtensionFilter("Executable File", "exe"),
            };

        pathExe = GameObject.Find("TXTMSH_Exe").GetComponent<TextMeshProUGUI>().text = StandaloneFileBrowser.OpenFilePanel("Upload Exe", "", extensions, false)[0];

        uploadName = Path.GetFileNameWithoutExtension(pathExe);

        if(Directory.Exists(persistentDataPath + "/Games/" + uploadName))
        {
            Debug.Log("YA EXISTE SALIR SALIR");
        }
        //File.Copy(StandaloneFileBrowser.OpenFilePanel("Upload Exe", "", extensions, false)[0], persistentDataPath + "/Games/Prueba.exe");
    }

    public void OnUploadDataFolderClicked()
    {
        pathDataFolder = GameObject.Find("TXTMSH_DataFolder").GetComponent<TextMeshProUGUI>().text = StandaloneFileBrowser.OpenFolderPanel("Upload Data Folder", "", false)[0];
        Debug.Log(pathDataFolder);
    }

    public void OnUploadImageClicked()
    {
        var extensions = new[] { new ExtensionFilter("Image File", "img", "png") };
        pathImg = GameObject.Find("TXTMSH_Thumbnail").GetComponent<TextMeshProUGUI>().text = StandaloneFileBrowser.OpenFilePanel("Upload Game Thumbnail", "", "", false)[0];
        Debug.Log(pathImg);
    }

    public void OnUploadFilesClicked()
    {
        Directory.CreateDirectory(persistentDataPath + "/Games/" + uploadName);
        File.Copy(pathExe, persistentDataPath + "/Games/" + uploadName + "/" + uploadName + ".exe");
        CopyAllFromDirectory(pathDataFolder, persistentDataPath + "/Games/" + uploadName + "/" + uploadName + "_Data/");
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

    void Update () 
	{
		
	}
}
