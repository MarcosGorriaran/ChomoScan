using APILibraryDaltonismo.Controllers;
using APILibraryDaltonismo.Model.DTO;
using APILibraryDaltonismo.Model;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Http;
using System;
using UI.Dates;

public class RegisterHandler : APIFormHandler
{
    PatientController patientController;
    [SerializeField]
    TMP_InputField dniInput;
    [SerializeField]
    TMP_InputField passwordInput;
    [SerializeField]
    TMP_InputField repeatPassword;
    [SerializeField]
    TMP_InputField nameInput;
    [SerializeField]
    DatePicker birthDate;
    [SerializeField]
    TMP_InputField cityInput;
    [SerializeField]
    TMP_InputField countryInput;

    


    private void Start()
    {
        birthDate.Config.DateRange.RestrictToDate = true;
        birthDate.Config.DateRange.ToDate = DateTime.Now;
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(conectionURL.URL);
        patientController = new PatientController(client);
        LogOutHandler.OnLogOutRequest += EnableFormElements;
    }
    public override void CheckLogin()
    {
        ResetMessage();
        DisableFormElements();
        StartCoroutine(LoginRequest());
        StartCoroutine(Countdown());
    }
    protected override IEnumerator LoginRequest()
    {
        if (CheckEmptyValues())
        {
            WrongMessage("No puede dejar en blanco los campos obligatorios");
            EnableFormElements();
            yield break;
        }
        else if (passwordInput.text != repeatPassword.text)
        {
            WrongMessage("Las contraseñas introducidas no son iguales");
            EnableFormElements();
            yield break;
        }
        Patient createdPatient = new Patient()
        {
            DNI = dniInput.text,
            Password = passwordInput.text,
            BirthDate = birthDate.VisibleDate,
            Name = nameInput.text,
            City = cityInput.text,
            Country = countryInput.text
        };
        Task<ResponseDTO<object>> request = patientController.AddPatientRequest(createdPatient);
        yield return new WaitUntil(()=>request.IsCompleted);
        ResponseDTO<object> responseDTO = request.Result;
        if (request.Exception != null)
        {
            WrongMessage(request.Exception.Message);
        }else if (!responseDTO.IsSuccess)
        {
            WrongMessage("Peticion ha fallado, usuario ya existe");
        }
        else
        {
            CorrectMessage("Se ha registrado");
            logedInUser = createdPatient;
            InvokeLoginEvent();
            StopCoroutine(limitCountdown);
            yield break;
        }
        EnableFormElements();
        StopCoroutine(limitCountdown);
    }
    protected override void EnableFormElements()
    {
        dniInput.interactable = true;
        passwordInput.interactable = true;
        repeatPassword.interactable = true;
        nameInput.interactable = true;
        birthDate.Enable();
        birthDate.enabled = true;
        cityInput.interactable = true;
        countryInput.interactable = true;
        GetComponent<Button>().interactable = true;
    }
    protected override void DisableFormElements()
    {
        dniInput.interactable = false;
        passwordInput.interactable = false;
        repeatPassword.interactable = false;
        nameInput.interactable = false;
        birthDate.Disable();
        birthDate.enabled = false;
        cityInput.interactable = false;
        countryInput.interactable = false;
        GetComponent<Button>().interactable = false;
        

    }
    protected override bool CheckEmptyValues()
    {
        List<string> values = new List<string>() { dniInput.text.Trim(),
        passwordInput.text.Trim(), repeatPassword.text.Trim()};
        return values.Contains(string.Empty);
    }
}
