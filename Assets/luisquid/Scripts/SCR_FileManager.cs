//Made by luisquid11
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_FileManager : MonoBehaviour {

    public static string persistentDataPath;
    
    public string [] GetAllFoldersFromPath(string _path)
    {
        string[] DirectoriesInPath = Directory.GetDirectories(_path);

        return DirectoriesInPath;
    }

	void Start () 
	{
        persistentDataPath = Application.persistentDataPath;
	}
	
	void Update () 
	{
		
	}
}
