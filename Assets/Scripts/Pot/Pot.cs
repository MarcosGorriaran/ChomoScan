using System;
using UnityEngine;
using UnityEngine.UI;

public class Pot : MonoBehaviour
{
    #region Variables
    private bool playerInTrigger = false;
    [SerializeField] private GameObject potCamera;
    [SerializeField] private GameObject potArrows;
    [SerializeField] private GameObject potParticles;
    [SerializeField] private GameObject greenPotion;
    private int[] combination = new int[4]; // La combinaci�n tiene 4 elementos
    private int currentStep = 0;  // Paso actual en la combinaci�n
    private bool isInPotionCreationMode = false; // Estado para saber si el jugador est� ingresando la combinaci�n
    public bool hasEnteredFullCombination = false; // Controla si el jugador ya ingres� los 4 d�gitos

    // Referencias a las im�genes de flechas en la UI
    [SerializeField] private GameObject arrow1Image;
    [SerializeField] private GameObject arrow2Image;
    [SerializeField] private GameObject arrow3Image;
    [SerializeField] private GameObject arrow4Image;

    private DaltonicManager daltonicManager;

    [SerializeField] private GameObject light1;
    [SerializeField] private GameObject light2;
    #endregion
    SoundManager soundManager;
    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    private void Start()
    {
        potCamera.SetActive(false);
        potArrows.SetActive(false);
        potParticles.SetActive(false);
        greenPotion.SetActive(false);
        light1.SetActive(true);
        light2.SetActive(false);
        ResetArrowRotations();
        daltonicManager = GameObject.FindObjectOfType<DaltonicManager>();
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
        if (playerInTrigger && Input.GetKeyDown(KeyCode.O) && !isInPotionCreationMode && !hasEnteredFullCombination)
        {
            StartPotionCreation();
        }

        if (isInPotionCreationMode)
        {
            HandlePotionCreation();
        }

        // Cierra la olla si el jugador presiona Escape o si ha completado los 4 pasos
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePot(resetCombination: true); // Reinicia la combinaci�n si el jugador sale sin completar
        }
    }

    // Inicia el proceso de creaci�n de la poci�n
    private void StartPotionCreation()
    {
        soundManager.PlaySound(soundManager.Cauldron);
        isInPotionCreationMode = true;
        potCamera.SetActive(true);
        potArrows.SetActive(true);
        Time.timeScale = 0;  // Pausa el juego
    }

    // Maneja la entrada de la combinaci�n
    private void HandlePotionCreation()
    {
        if (currentStep < combination.Length)
        {
            // Detecta las teclas de flechas y las asigna a la combinaci�n
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                combination[currentStep] = 1;
                UpdateArrowRotation(currentStep, 180); // Flecha arriba, rotaci�n Z = 180
                Debug.Log("Flecha arriba presionada (1)");
                currentStep++;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                combination[currentStep] = 2;
                UpdateArrowRotation(currentStep, 0); // Flecha abajo, rotaci�n Z = 0
                Debug.Log("Flecha abajo presionada (2)");
                currentStep++;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                combination[currentStep] = 3;
                UpdateArrowRotation(currentStep, -90); // Flecha izquierda, rotaci�n Z = -90
                Debug.Log("Flecha izquierda presionada (3)");
                currentStep++;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                combination[currentStep] = 4;
                UpdateArrowRotation(currentStep, 90); // Flecha derecha, rotaci�n Z = 90
                Debug.Log("Flecha derecha presionada (4)");
                currentStep++;
            }
        }

        // Verifica si la combinaci�n est� completa
        if (currentStep >= combination.Length)
        {
            light1.SetActive(false);
            light2.SetActive(true);
            ComprobateCombination();
            hasEnteredFullCombination = true; // Marca que el jugador ingres� los 4 d�gitos
            potParticles.SetActive(true);
            greenPotion.SetActive(true);
            soundManager.PlaySound(soundManager.PotionFusion);
            soundManager.PlaySound(soundManager.Achievement);
            Debug.Log("Combinaci�n completa ingresada y almacenada: " + string.Join(", ", combination));
            ClosePot(resetCombination: false); // Cierra sin resetear ya que complet� la combinaci�n
        }
    }

    // Actualiza la rotaci�n de la flecha seg�n el �ndice de la flecha (0, 1, 2, 3)
    private void UpdateArrowRotation(int index, float rotationZ)
    {
        switch (index)
        {
            case 0:
                arrow1Image.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
                arrow1Image.SetActive(true);
                break;
            case 1:
                arrow2Image.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
                arrow2Image.SetActive(true);
                break;
            case 2:
                arrow3Image.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
                arrow3Image.SetActive(true);
                break;
            case 3:
                arrow4Image.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);
                arrow4Image.SetActive(true);
                break;
        }
    }

    private void ResetArrowRotations()
    {
        arrow1Image.SetActive(false);
        arrow2Image.SetActive(false);
        arrow3Image.SetActive(false);
        arrow4Image.SetActive(false);
    }

    // Cierra la olla y vuelve al estado normal del juego
    private void ClosePot(bool resetCombination)
    {
        soundManager.StopSound();
        ResetArrowRotations();
        potCamera.SetActive(false);
        potArrows.SetActive(false);
        isInPotionCreationMode = false;  // Resetea el estado de entrada al caldero
        Time.timeScale = 1;  // Reactiva el juego

        if (resetCombination)
        {
            currentStep = 0;  // Reinicia el paso actual solo si el jugador sali� sin completar
            Array.Clear(combination, 0, combination.Length); // Borra la combinaci�n actual
            Debug.Log("Combinaci�n reiniciada. Debes ingresar los 4 d�gitos de nuevo.");
            ResetArrowRotations();  // Resetea la rotaci�n de todas las flechas
        }
    }

    // M�todo para obtener la combinaci�n almacenada
    public int[] GetCombination()
    {
        return combination;
    }

    public void ComprobateCombination()
    {
        if (combination[0] != 4) daltonicManager.SumDaltonic();
        if (combination[2] != 2)
        {
            daltonicManager.SumDaltonic();
            daltonicManager.SumProtan();
        }
        if (combination[3] != 2) daltonicManager.SumProtan();
        daltonicManager.ShowResults();
    }

    // M�todo para verificar si el jugador ya ingres� la combinaci�n
    public bool HasEnteredFullCombination()
    {
        return hasEnteredFullCombination;
    }
}
