using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_ViewModel : MonoBehaviour {

    public string fbxPath = "D:/rsofia/Documents/3DMX/VIII/AL-RecycleGame/Assets/Imports/Models/Bote_Basura.FBX";

    [Header("Sky")]
    public Material[] skyboxes;
    public string[] nombreSkyboxes;


    void Start () {
        ModelImporter.Importer.Import(fbxPath);
	}
	
    private void FillDropDownWithSkyboxes()
    {

    }

    public void ChangeHDR()
    {

    }

}
