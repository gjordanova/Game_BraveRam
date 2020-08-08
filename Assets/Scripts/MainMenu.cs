using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    [SerializeField] GameObject[] InGameObjects;
    public static MainMenu Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    public void  OnStart()
    {
        HideOrShow(true);
    }


    public void ExitGame()
    {
        Application.Quit();
    }



    public void HideOrShow(bool isActive)
    {
        foreach(var item in InGameObjects)
        {
            item.SetActive(isActive);
        }
    }
}
