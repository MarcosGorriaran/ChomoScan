    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSelector : MonoBehaviour
{
    #region Variables
    public List<GameObject> selectors;
    public List<GameObject> books;
    private int currentIndex = 0;
    private bool playerInTrigger = false;
    [SerializeField] private GameObject cameraObj;
    private bool isLooking = false;
    SoundManager soundManager;

    [SerializeField] private GameObject arrow;
    [SerializeField] private GameObject arrow2;
    [SerializeField] private GameObject enter;

    [SerializeField] private DrinkPot potDrinked;

    private HashSet<int> rotatedBooks = new HashSet<int>(); // Índices de libros rotados

    private int rotatedBookCount = 0; // Contador de libros rotados
    private const int maxRotatedBooks = 4; // Límite de libros que se pueden rotar
    private bool booksRotated;

    [SerializeField] private GameObject shelf; // La estantería que se moverá
    private DaltonicManager daltonicManager;
    #endregion
    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }
    void Start()
    {
        daltonicManager = GameObject.FindObjectOfType<DaltonicManager>();
        foreach (GameObject selector in selectors)
        {
            selector.SetActive(false);
        }

        foreach (GameObject book in books)
        {
            book.SetActive(true);
        }

        if (cameraObj != null)
        {
            cameraObj.SetActive(false);
            arrow.SetActive(false);
            arrow2.SetActive(false);
            enter.SetActive(false);
        }
    }

    void Update()
    {
        if (isLooking && potDrinked.potionDrinked)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeSelection(-1);
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeSelection(1);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (!rotatedBooks.Contains(currentIndex)) // Solo rota si no está rotado
                {
                    Comprobation();
                    RotateBook(books[currentIndex]);
                    rotatedBooks.Add(currentIndex); // Marca como rotado
                }
            }

        }

        if (playerInTrigger && Input.GetKeyDown(KeyCode.O))
        {
            Look();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Close();
        }
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

    private void Look()
    {
        if (!booksRotated)
        {
            if (cameraObj != null)
            {
                cameraObj.SetActive(true);
                arrow.SetActive(true);
                arrow2.SetActive(true);
                enter.SetActive(true);
                isLooking = true;
                Time.timeScale = 0;
            }

            if (selectors.Count > 0)
            {
                UpdateSelectorVisibility();
            }
        }
    }

    private void Close()
    {
        if (cameraObj != null)
        {
            cameraObj.SetActive(false);
            arrow.SetActive(false);
            arrow2.SetActive(false);
            enter.SetActive(false);
            isLooking = false;
            Time.timeScale = 1;
        }

        foreach (GameObject book in books)
        {
            book.SetActive(true);
        }

        foreach (GameObject selector in selectors)
        {
            selector.SetActive(false);
        }
    }

    private void ChangeSelection(int direction)
    {
        selectors[currentIndex].SetActive(false);
        do
        {
            currentIndex = (currentIndex + direction + selectors.Count) % selectors.Count;
        } while (rotatedBooks.Contains(currentIndex)); // Salta libros rotados

        UpdateSelectorVisibility();
    }

    private void UpdateSelectorVisibility()
    {
        if (!rotatedBooks.Contains(currentIndex))
        {
            selectors[currentIndex].SetActive(true);
        }
    }

    private void RotateBook(GameObject book)
    {
        if (book != null)
        {
            // Ajusta la rotación en el eje X a -60 grados, manteniendo Y y Z
            book.transform.localEulerAngles = new Vector3(-60f, 0f, -90f);
            // Marca el libro como rotado
            selectors[currentIndex].SetActive(false);
            rotatedBooks.Add(currentIndex);

            // Incrementa el contador de libros rotados
            rotatedBookCount++;

            // Verifica si se alcanzó el límite de libros rotados
            if (rotatedBookCount >= maxRotatedBooks)
            {
                booksRotated = true;
                Close(); // Llama a la función de cerrar
                daltonicManager.ShowResults();
                StartCoroutine(MoveShelf()); // Inicia la corrutina para mover la estantería
            }
            soundManager.PlaySound(soundManager.tapBook);
        }
    }

    private void Comprobation()
    {
        // Verifica si el índice actual está dentro del rango de los libros
        if (currentIndex >= 0 && currentIndex < books.Count)
        {
            // Diccionario para asociar cada índice con su función correspondiente
            Dictionary<int, System.Action> bookActions = new Dictionary<int, System.Action>()
            {
                { 0, daltonicManager.SumDaltonic },
                { 1, daltonicManager.SumProtan },
                { 2, daltonicManager.SumNormal },
                { 3, daltonicManager.SumDeutan },
                { 4, daltonicManager.SumDaltonic },
                { 5, daltonicManager.SumDaltonic },
                { 6, daltonicManager.SumNormal },
                { 7, daltonicManager.SumNormal },
                { 8, daltonicManager.SumNormal }
            };

            // Verifica si existe una acción para el índice actual
            if (bookActions.ContainsKey(currentIndex))
            {
                // Ejecuta la acción asociada con el índice
                bookActions[currentIndex].Invoke();
            }
            else
            {
                Debug.LogWarning("No hay acción asignada para el índice: " + currentIndex);
            }
        }
        else
        {
            Debug.LogWarning("Índice fuera de rango de los libros.");
        }
    }



    private IEnumerator MoveShelf()
    {
        soundManager.PlaySound(soundManager.MoveShelf);
        soundManager.PlaySound(soundManager.Achievement);
        float elapsedTime = 0f;
        float duration = 2f;
        Vector3 initialPosition = shelf.transform.position;
        Vector3 targetPosition = initialPosition + new Vector3(0f, 0f, 2f);

        while (elapsedTime < duration)
        {
            shelf.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        shelf.transform.position = targetPosition; 
    }
}
