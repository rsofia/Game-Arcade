using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class JSONUser {

    public int id;
    public string username;
    public string password; // this is SHA-256
}
