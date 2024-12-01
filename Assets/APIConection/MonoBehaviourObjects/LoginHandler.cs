using APILibraryDaltonismo.Controllers;
using APILibraryDaltonismo.Model;
using APILibraryDaltonismo.Model.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginHandler : APIFormHandler
{
    PatientController patientController;
    [SerializeField]
    TMP_InputField userInput;
    [SerializeField]
    TMP_InputField passwordInput;

    
    void Start()
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(conectionURL.URL);
        patientController = new PatientController(client);
        LogOutHandler.OnLogOutRequest += EnableFormElements;
    }

    public override void CheckLogin()
    {
        ResetMessage();
        DisableFormElements();
        requestTask = StartCoroutine(LoginRequest());
        limitCountdown = StartCoroutine(Countdown());
    }
    protected override IEnumerator LoginRequest()
    {

        Patient patient = new Patient()
        {
            DNI = userInput.text,
            Password = passwordInput.text
        };
        Task<ResponseDTO<Patient>> requestTask = patientController.CheckLoginRequest(patient);
        yield return new WaitUntil(()=>requestTask.IsCompleted);
        ResponseDTO<Patient> responseResult = requestTask.Result;
        if (CheckEmptyValues())
        {
            WrongMessage("Los campos obligatorios no pueden estar vacios");
        }
        else if (requestTask.Exception != null)
        {
            WrongMessage("Ha habido un problema de conexion a la API");
        }
        else if (!responseResult.IsSuccess)
        {
            WrongMessage("Peticion ha fracasado en efectuarse, revise usuario y contraseña");
        }
        else
        {
            CorrectMessage("Ha iniciado sesion");
            Patient logedInUser = responseResult.Data;
            logedInUser.Password = passwordInput.text;
            APIFormHandler.logedInUser = logedInUser;
            StopCoroutine(limitCountdown);
            InvokeLoginEvent();
            yield break;
        }
        
        EnableFormElements();
        StopCoroutine(limitCountdown);
    }
    protected override void DisableFormElements()
    {
        userInput.interactable = false;
        passwordInput.interactable = false;
        GetComponent<Button>().interactable = false;
    }
    protected override void EnableFormElements()
    {
        userInput.interactable = true;
        passwordInput.interactable = true;
        GetComponent<Button>().interactable = true;
    }
    protected override bool CheckEmptyValues()
    {
        return userInput.text.Trim() == string.Empty || passwordInput.text.Trim() == string.Empty;
    }
}
