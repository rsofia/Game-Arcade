using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_AdminAccess : MonoBehaviour {

    public GameObject loginPanel;
    public string activeUser = "";

    void Start()
    {
        ShowLogin();
    }

	public void ShowLogin()
    {
        loginPanel.SetActive(true);
    }

    public void Login()
    {
        //Read all the users and check if passwords match

    }
}
