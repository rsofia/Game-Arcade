using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_SearchGames : MonoBehaviour {

    public Button btnSearch;
    public Dropdown dbOptions;

    void Start()
    {
        CloseSearchOptions();
    }


    public void OpenSearchOptions()
    {
        btnSearch.gameObject.SetActive(false);
        dbOptions.gameObject.SetActive(true);
    }

    public void CloseSearchOptions()
    {
        btnSearch.gameObject.SetActive(true);
        dbOptions.gameObject.SetActive(false);
    }
}
