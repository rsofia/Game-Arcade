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

    public string [] GetAllFoldersFromPath(string _path)
    {
        string[] DirectoriesInPath = Directory.GetDirectories(_path);

        return DirectoriesInPath;
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

        //File.Copy(StandaloneFileBrowser.OpenFilePanel("Upload Exe", "", extensions, false)[0], persistentDataPath + "/Games/Prueba.exe");
    }

    public void OnUploadDataFolderClicked()
    {
        pathDataFolder = StandaloneFileBrowser.OpenFolderPanel("Upload Data Folder", "", false)[0];

        Debug.Log(pathDataFolder);
    }

    public void OnUploadImageClicked()
    {
        var extensions = new[] { new ExtensionFilter("Image File", "img", "png") };
        pathImg = StandaloneFileBrowser.OpenFilePanel("Upload Game Thumbnail", "", "", false)[0];
        Debug.Log(pathImg);
    }

    public void OnUploadFilesClicked()
    {
        File.Copy(pathExe, persistentDataPath + "/Games/Prueba.exe");

        foreach (string dirPath in Directory.GetDirectories(pathDataFolder, "*",
                    SearchOption.AllDirectories))
            Directory.CreateDirectory(dirPath.Replace(pathDataFolder, persistentDataPath + "/Games/"));

        //Copy all the files & Replaces any files with the same name
        foreach (string newPath in Directory.GetFiles(pathDataFolder, "*.*",
            SearchOption.AllDirectories))
            File.Copy(newPath, newPath.Replace(pathDataFolder, persistentDataPath + "/Games/"), true);
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
