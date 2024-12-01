using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parchment : MonoBehaviour
{
    #region Variables
    private bool playerInTrigger = false;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private GameObject potion1;
    [SerializeField] private GameObject potion2;
    [SerializeField] private GameObject potion3;
    [SerializeField] private GameObject plus;
    SoundManager soundManager;

    #endregion
    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }
    private void Start()
    {
        cameraObj.SetActive(false);
        potion1.SetActive(false);
        potion2.SetActive(false);
        potion3.SetActive(false);
        plus.SetActive(false);
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
        if (playerInTrigger && Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
    }
    private void Look()
    {
        soundManager.PlaySound(soundManager.OpenBook);
        cameraObj.SetActive(true);
        potion1.SetActive(true);
        potion2.SetActive(true);
        potion3.SetActive(true);
        plus.SetActive(true);
        Time.timeScale = 0;
    }
    private void Close()
    {
        soundManager.PlaySound(soundManager.OpenBook);
        cameraObj.SetActive(false);
        potion1.SetActive(false);
        potion2.SetActive(false);
        potion3.SetActive(false);
        plus.SetActive(false);
        Time.timeScale = 1;
    }
}
