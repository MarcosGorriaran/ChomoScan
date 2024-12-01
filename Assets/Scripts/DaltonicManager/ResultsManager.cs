using APILibraryDaltonismo.Controllers;
using APILibraryDaltonismo.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI normalLabel;
    [SerializeField] private ConectionURL url;
    [SerializeField] private TextMeshProUGUI protanLabel;
    [SerializeField] private TextMeshProUGUI deutanLabel;
    [SerializeField] private TextMeshProUGUI daltonicLabel;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        //normalLabel.text = $"{GameData.NormalPoints:F1}%";
        protanLabel.text = $"{GameData.ProtanPoints:F1}%";
        deutanLabel.text = $"{GameData.DeutanPoints:F1}%";
        daltonicLabel.text = $"{GameData.DaltonicPoints:F1}%";

        string dominantColorBlindness = GameData.DeutanPoints > GameData.ProtanPoints ? "deuteranopia": "protanopia";
        Session session = new Session()
        {
            player = APIFormHandler.logedInUser,
            DateGame = DateTime.Now,
            ColorBlindType = dominantColorBlindness
        };
        StartCoroutine(UploadToApi(session));
    }

    private IEnumerator UploadToApi(Session sessionResult)
    {
        HttpClient client = new HttpClient();
        client.BaseAddress = new System.Uri(url.URL);
        SessionController controller= new SessionController(client);
        Task request = controller.AddScoreRequest(sessionResult);

        yield return new WaitUntil(()=>request.IsCompleted);

        if (request.Exception != null)
        {
            Debug.Log(request.Exception.Message);
        }
        
    }
}
