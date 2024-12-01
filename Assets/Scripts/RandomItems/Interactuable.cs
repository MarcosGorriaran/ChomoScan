using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuable : MonoBehaviour
{
    #region Variables
    private bool playerInTrigger = false;
    [SerializeField] private GameObject cameraObj;


    #endregion
    private void Start()
    {
        cameraObj.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.O))
        {
            Look();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }
    private void Look()
    {
        cameraObj.SetActive(true);


        Time.timeScale = 0;
    }
    private void Close()
    {
        cameraObj.SetActive(false);
        Time.timeScale = 1;
    }
}
