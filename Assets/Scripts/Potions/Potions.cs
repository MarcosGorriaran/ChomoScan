using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potions : MonoBehaviour
{
    #region Variables
    private bool playerInTrigger = false;
    [SerializeField] private GameObject cameraObj;
    [SerializeField] private GameObject potionNum1;
    [SerializeField] private GameObject potionNum2;
    [SerializeField] private GameObject potionNum3;
    [SerializeField] private Pot pot;  // Referencia al script Pot


    private bool playerLooking = false;

    public bool potionsSelected = false;
    [SerializeField] public GameObject msg;

    private List<int> selectedPotions = new List<int>(); // Lista para rastrear las pociones seleccionadas

    private DaltonicManager daltonicManager;

    private Animator animator;
    private SoundManager soundManager;
    #endregion

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }
    private void Start()
    {
        cameraObj.SetActive(false);
        potionNum1.SetActive(false);
        potionNum2.SetActive(false);
        potionNum3.SetActive(false);
        daltonicManager = GameObject.FindObjectOfType<DaltonicManager>();
        animator = GetComponentInChildren<Animator>();
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
        // Solo permite interactuar si aún no se han seleccionado dos pociones
        if (selectedPotions.Count < 2)
        {
            if (playerInTrigger && Input.GetKeyDown(KeyCode.O))
            {
                Look();
            }

            // Si el jugador presiona Escape, cerramos el menú
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Close();
            }

            // Si la combinación está completa, habilitamos las opciones de las pociones
            if (pot.HasEnteredFullCombination() && playerLooking)
            {
                ChoosePotion();
            }
        }
    }

    private void Look()
    {
        playerLooking = true;
        cameraObj.SetActive(true);
        if (pot.HasEnteredFullCombination())
        {
            potionNum1.SetActive(true);
            potionNum2.SetActive(true);
            potionNum3.SetActive(true);
        }
        Time.timeScale = 0; // Pausa el juego
    }

    private void ChoosePotion()
    {
        // Detectar la tecla "1" para seleccionar la primera poción
        if (Input.GetKeyDown(KeyCode.Alpha1) && !selectedPotions.Contains(1))
        {
            soundManager.PlaySound(soundManager.PotionFusion);
            Debug.Log("Poción 1 seleccionada");
            selectedPotions.Add(1);  // Añadir la poción seleccionada a la lista
        }

        // Detectar la tecla "2" para seleccionar la segunda poción
        if (Input.GetKeyDown(KeyCode.Alpha2) && !selectedPotions.Contains(2))
        {
            soundManager.PlaySound(soundManager.PotionFusion);
            Debug.Log("Poción 2 seleccionada");
            selectedPotions.Add(2);  // Añadir la poción seleccionada a la lista
        }

        // Detectar la tecla "3" para seleccionar la tercera poción
        if (Input.GetKeyDown(KeyCode.Alpha3) && !selectedPotions.Contains(3))
        {
            soundManager.PlaySound(soundManager.PotionFusion);
            Debug.Log("Poción 3 seleccionada");
            selectedPotions.Add(3);  // Añadir la poción seleccionada a la lista
        }

        // Si ya se han seleccionado dos pociones, cerramos el menú
        if (selectedPotions.Count >= 2)
        {
            animator.SetBool("changeColor", true);
            Comprobation();
            potionsSelected = true;
            msg.SetActive(true);
            soundManager.PlaySound(soundManager.Achievement);
            Close();
        }
    }

    private void Close()
    {
        playerLooking = false;
        cameraObj.SetActive(false);
        potionNum1.SetActive(false);
        potionNum2.SetActive(false);
        potionNum3.SetActive(false);
        Time.timeScale = 1;  // Reactiva el juego
    }
    private void Comprobation()
    {
        if (selectedPotions.Contains(3))
        {
            daltonicManager.SumDaltonic();
        }
        daltonicManager.ShowResults();
    }
}
