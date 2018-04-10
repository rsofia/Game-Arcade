using UnityEngine;
using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;

public class SCR_ExecuteExe : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        //Process.Start("C:/Users/Alumno/Desktop/JijiLol.exe");
	}

    // Update is called once per frame
    void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            RunGame("C:/Users/Alumno/Desktop/JijiLol.exe");
        }
	}

    public void RunGame(string _direction)
    {
        Process.Start(_direction);
    }
}
