using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System;
using System.Security.Cryptography;
using System.IO;

public class SCR_AdminAccess : MonoBehaviour {

    public string activeUser = "";

    [Header("UI")]
    public GameObject loginPanel;
    public GameObject optionsMenu;
    public GameObject panelVideoUpload;
    public GameObject panelGameUpload;
    public GameObject panelModelUpload;
    public GameObject panelNewUser;
    [Header("New User")]
    public Text txtErrorNewUser;
    public InputField newUser_User;
    public InputField newUser_Password;
    public InputField newUser_ConfirmPassword;
    public GameObject popupSuccess;

    private string mySalt = "LBBB21~13RSBM"; // NEVER CHANGE THIS. THIS WILL HELP MAKE THE HASH OF THE PASSWORDS
    private string userPath = "";
    private string userFileName= "Users.txt";
    [Header("Login")]
    public Text txtErrorLogin;
    public InputField login_User;
    public InputField login_Password;

    [Header("MenuOptions")]
    public Text txtBienvenido;

    void Start()
    {
        userPath = Application.persistentDataPath + "/Datos/";
        ShowLogin();
    }

    public void Login()
    {
        if (!string.IsNullOrEmpty(login_User.text) && !string.IsNullOrEmpty(login_Password.text))
        {
            //Check if the user matches
            if (File.Exists(userPath + userFileName))
            {
                string[] lineas = File.ReadAllLines(userPath + userFileName);
                JSONUser user;
                bool isUserCorrect = false;
                foreach (string linea in lineas)
                {
                    user = JsonUtility.FromJson<JSONUser>(linea);
                    if (user != null && user.username == login_User.text)
                    {
                        isUserCorrect = true;
                        if (user.password == PasswordHashed(login_Password.text))
                        {
                            //Login Successfull
                            activeUser = user.username;
                            txtErrorLogin.text = "Usuario creado exitosamente";
                            OpenOptionsMenu();
                            break;
                        }
                        else
                            txtErrorLogin.text = "Contraseña incorrecta";
                    }
                }

                if (!isUserCorrect)
                {
                    txtErrorLogin.text = "Usuario incorrecto.";
                }
            }
        }
        else
            txtErrorLogin.text = "Por favor llena todos los campos";

    }

    public void CreateNewUser()
    {
        bool loginSuccessful = false;
        //Read all the users and check if passwords match
        if (!string.IsNullOrEmpty(newUser_User.text))
        {
            if (newUser_Password.text == newUser_ConfirmPassword.text && !string.IsNullOrEmpty(newUser_Password.text))
            {
                bool doesUserExist = false;
                int newUserID = 0;
                //Check if the user already exists
                if (File.Exists(userPath + userFileName))
                {
                    string[] lineas = File.ReadAllLines(userPath + userFileName);
                    JSONUser user;
                    foreach (string linea in lineas)
                    {
                        Debug.Log("linea: " + linea);
                        user = JsonUtility.FromJson<JSONUser>(linea);
                        if (user != null && user.username == newUser_User.text)
                        {
                            doesUserExist = true;
                            newUserID = user.id + 1;
                        }
                    }
                }
                //...

                if (!doesUserExist)
                {
                    DateTime localDate = DateTime.Now;
                    JSONUser user = new JSONUser
                    {
                        id = newUserID,
                        username = newUser_User.text,
                        password = PasswordHashed(newUser_Password.text),
                        createdBy = activeUser,
                        fechaDeCreacion = localDate.ToString()
                };

                    try
                    {
                        GameArcade.FileReader.WriteTextAtPath(userPath, userFileName, JsonUtility.ToJson(user), true);
                        Debug.Log("User saved at: " + userPath);
                        loginSuccessful = true;
                    }
                    catch
                    {
                        txtErrorNewUser.text = "Hubo un error al crear el usuario";
                    }

                }
                else
                {
                    txtErrorNewUser.text = "El usuario no está disponible.";
                }
            }
            else
                txtErrorNewUser.text = "Las contraseñas no coinciden.";
        }
        else
            txtErrorNewUser.text = "Por favor, ingrese un usuario.";


        //if Login was a success, open options
        if (loginSuccessful)
            OpenPopUp();

    }

    public string PasswordHashed(string password)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(mySalt + password);
        SHA256Managed hashstring = new SHA256Managed();
        byte[] hash = hashstring.ComputeHash(bytes);
        string passwordHash = string.Empty;
        foreach (byte x in hash)
        {
            passwordHash += String.Format("{0:x2}", x);
        }

        return passwordHash;
    }

    IEnumerator WaitToTurnOffNewUserPopUp()
    {
        yield return new WaitForSeconds(1.5f);
        popupSuccess.SetActive(true);
        OpenOptionsMenu();
    }

    //**** UI FUNCTIONS ****\\
    #region UI_FUNCTIONS

    public void ShowLogin()
    {
        activeUser = "";
        loginPanel.SetActive(true);
        txtErrorLogin.text = "";

        login_Password.text = "";
        login_User.text = "";
    }
    public void OpenCreateUser()
    {
        loginPanel.SetActive(false);
        optionsMenu.SetActive(false);
        panelVideoUpload.SetActive(false);
        panelGameUpload.SetActive(false);
        panelModelUpload.SetActive(false);
        popupSuccess.SetActive(false);
        panelNewUser.SetActive(true);

        newUser_Password.text = "";
        newUser_User.text = "";
        txtErrorNewUser.text = "";
        newUser_ConfirmPassword.text = "";
    }
    public void OpenUploadVideo()
    {
        loginPanel.SetActive(false);
        optionsMenu.SetActive(false);
        panelVideoUpload.SetActive(true);
        panelGameUpload.SetActive(false);
        panelModelUpload.SetActive(false);
        panelNewUser.SetActive(false);
    }
    public void OpenUploadGame()
    {
        loginPanel.SetActive(false);
        optionsMenu.SetActive(false);
        panelVideoUpload.SetActive(false);
        panelGameUpload.SetActive(true);
        panelModelUpload.SetActive(false);
        panelNewUser.SetActive(false);
    }
    public void OpenUploadModel()
    {
        loginPanel.SetActive(false);
        optionsMenu.SetActive(false);
        panelVideoUpload.SetActive(false);
        panelGameUpload.SetActive(false);
        panelModelUpload.SetActive(true);
        panelNewUser.SetActive(false);
    }
    public void OpenOptionsMenu()
    {
        loginPanel.SetActive(false);
        optionsMenu.SetActive(true);
        panelVideoUpload.SetActive(false);
        panelGameUpload.SetActive(false);
        panelModelUpload.SetActive(false);
        panelNewUser.SetActive(false);
        txtBienvenido.text = "Bienvenid@, " + activeUser;
    }
    public void OpenPopUp()
    {
        popupSuccess.SetActive(true);
        StartCoroutine(WaitToTurnOffNewUserPopUp());
    }

    public void SelectNext(InputField inptfld)
    {
        inptfld.Select();
    }
    public void SelectNext(Button btn)
    {
        btn.Select();
    }
    #endregion
}
