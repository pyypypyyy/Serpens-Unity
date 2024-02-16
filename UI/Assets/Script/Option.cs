using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public void OpenOption()
    {
        GameObject Menu = GameObject.FindGameObjectWithTag("Menu").transform.gameObject;
        GameObject GameSettings = GameObject.FindGameObjectWithTag("GameSettings").transform.gameObject;

        Menu.transform.GetChild(0).gameObject.SetActive(false);
        GameSettings.transform.GetChild(0).gameObject.SetActive(true);
    }
}
