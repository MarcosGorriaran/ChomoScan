using APILibraryDaltonismo.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class APIFormHandler : MonoBehaviour
{
    protected const float LimitTime = 10f;
    public static Patient logedInUser { get; set; }
    [SerializeField]
    protected ConectionURL conectionURL;
    [SerializeField]
    TMP_Text errorMessage;
    protected Coroutine requestTask;
    protected Coroutine limitCountdown;
    public delegate void LogInAction();
    public static event LogInAction OnLogin;

    private void OnDestroy()
    {
        LogOutHandler.OnLogOutRequest -= EnableFormElements;
        OnLogin -= DisableFormElements;
    }
    protected void InvokeLoginEvent()
    {
        OnLogin.Invoke();
    }
    protected void CorrectMessage(string message)
    {
        errorMessage.gameObject.SetActive(true);
        errorMessage.text = message;
        errorMessage.color = Color.green;
    }
    protected void WrongMessage(string message)
    {
        errorMessage.gameObject.SetActive(true);
        errorMessage.text = message;
        errorMessage.color = Color.red;
    }
    protected void ResetMessage()
    {
        errorMessage.gameObject.SetActive(false);
        errorMessage.text = string.Empty;
    }
    public abstract void CheckLogin();
    protected abstract IEnumerator LoginRequest();
    protected abstract void DisableFormElements();
    protected abstract void EnableFormElements();
    protected abstract bool CheckEmptyValues();
    protected IEnumerator Countdown()
    {
        yield return new WaitForSeconds(LimitTime);
        StopCoroutine(requestTask);
        WrongMessage("Ha habido un problema en la conexion a la API");
        EnableFormElements();
    }
}
