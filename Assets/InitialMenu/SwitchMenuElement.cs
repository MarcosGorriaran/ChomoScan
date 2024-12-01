using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMenuElement : MonoBehaviour
{
    [SerializeField]
    GameObject logedInElement;
    [SerializeField]
    GameObject logedOutElement;


    private void OnDestroy()
    {
        LogOutHandler.OnLogOutRequest -= OnLogOut;
        APIFormHandler.OnLogin -= OnLogIn;
    }
    private void Start()
    {
        if(APIFormHandler.logedInUser != null)
        {
            OnLogIn();
        }
        else
        {
            OnLogOut();
        }
        LogOutHandler.OnLogOutRequest += OnLogOut;
        APIFormHandler.OnLogin += OnLogIn;
    }
    private void OnLogOut()
    {
        logedInElement.SetActive(false);
        logedOutElement.SetActive(true);
    }
    private void OnLogIn()
    {
        logedInElement.SetActive(true);
        logedOutElement.SetActive(false);
    }
}
