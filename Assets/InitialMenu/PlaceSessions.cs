using APILibraryDaltonismo.Controllers;
using APILibraryDaltonismo.Model;
using APILibraryDaltonismo.Model.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaceSessions : MonoBehaviour
{
    HorizontalLayoutGroup row;
    VerticalLayoutGroup table;
    [SerializeField]
    ConectionURL conectionURL;
    [SerializeField]
    TMP_Text errorText;
    Session[] sessions;
    PatientController patientController;
    TMP_Text cellTextDateGame;
    TMP_Text cellTextColorBlindType;
    TMP_Text cellTextSessionId;
    private static PlaceSessions instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        if (APIFormHandler.logedInUser == null)
        {
            SendErrorMessage("Es necesario una sesion para ver los resultados");
            return;
        }
        HttpClient client = new HttpClient();
        client.BaseAddress = new Uri(conectionURL.URL);
        patientController = new PatientController(client);

        row = GetComponent<HorizontalLayoutGroup>();
        table = GetComponentInParent<VerticalLayoutGroup>();
        StartCoroutine(SearchPatient());
    }
    private void GetTextElements(HorizontalLayoutGroup row)
    {
        cellTextDateGame = row.transform.GetChild(0).GetComponentInChildren<TMP_Text>();
        cellTextColorBlindType = row.transform.GetChild(1).GetComponentInChildren<TMP_Text>();
        cellTextSessionId = row.transform.GetChild(2).GetComponentInChildren<TMP_Text>();
    }
    private IEnumerator SearchPatient()
    {
        Task<ResponseDTO<IEnumerable<Session>>> request = patientController.GetPatientSessionsRequest(APIFormHandler.logedInUser);
        yield return new WaitUntil(()=>request.IsCompleted);
        ResponseDTO<IEnumerable<Session>> responseDTO = request.Result;
        if (!request.Result.IsSuccess)
        {
            SendErrorMessage("Ha habido un problema con la peticion");
        }
        else if(request.Exception!= null)
        {
            SendErrorMessage("Ha habido un problema con la conexion a la API");
        }
        else
        {
            sessions = request.Result.Data.ToArray();
            AddSessionsToTable();
            
        }
        Destroy(row.gameObject);
    }
    private void AddSessionsToTable()
    {
        foreach(Session session in sessions)
        {
            HorizontalLayoutGroup actualRow = Instantiate(row,table.transform);
            GetTextElements(actualRow);
            cellTextColorBlindType.text = session.ColorBlindType;
            cellTextDateGame.text = session.DateGame.ToString();
            cellTextSessionId.text = session.SessionID;
        }
        
    }
    private void SendErrorMessage(string message)
    {
        errorText.gameObject.SetActive(true);
        errorText.text = message;
    }
}
