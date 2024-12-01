using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogOutHandler : MonoBehaviour
{
    public delegate void LogOutRequest();
    public static event LogOutRequest OnLogOutRequest;
    public static void LogOut()
    {
        APIFormHandler.logedInUser = null;
        OnLogOutRequest.Invoke();
    }
}
