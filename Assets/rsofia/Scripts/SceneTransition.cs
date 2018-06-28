using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour {

    public string mainMenuScene = "Demo_Menu";
    public string uploadScene = "Demo_Upload";
    public string modelViewerScene = "ModelViewer";

    public void GoToSceneMainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void GoToSceneUpload()
    {
        SceneManager.LoadScene(uploadScene);
    }

    public void GoToViewModel()
    {
        SceneManager.LoadScene(modelViewerScene);
    }
    
	
}
