using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_ViewModel : MonoBehaviour {

    public static string fbxPath = "";

    [Header("Sky")]
    public Material[] skyboxes;
    public string[] nombreSkyboxes;
    public GameObject modelo;

    private float maxSize = 80;
    private float minSize = 5;


    void Start ()
    {
        if(!string.IsNullOrEmpty(fbxPath))
        {
            modelo = ModelImporter.Importer.Import(fbxPath);
            modelo.transform.position = Vector3.zero;
            modelo.transform.localScale = Vector3.one;
            ZoomCamera();
        }
        
	}

    private void ZoomCamera()
    {
        Camera.main.fieldOfView = Mathf.Clamp(maxSize - (modelo.transform.position - Camera.main.transform.position).magnitude, minSize, maxSize);
    }
	
    private void FillDropDownWithSkyboxes()
    {

    }

    public void ChangeHDR()
    {

    }

}
