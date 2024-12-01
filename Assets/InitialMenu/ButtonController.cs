using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Importar para gestionar las escenas

public class ButtonController : MonoBehaviour
{
    
    // Método para cerrar el juego
    public void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Detener el juego en el editor
#else
            Application.Quit(); // Cerrar el juego en una compilación
#endif
    }

    // Método para ir a la escena "Resultados"
    public void GoToResults()
    {
        SceneManager.LoadScene("ResultMenu"); // Cambia el nombre de la escena si es diferente
    }

    // Método para ir a la escena "Dungeon"
    public void GoToDungeon()
    {
        SceneManager.LoadScene("Dungeon"); // Cambia el nombre de la escena si es diferente
    }

    public void GoToLogin()
    {
        SceneManager.LoadScene("LoginMenu");
    }
    public void GoToRegister()
    {
        SceneManager.LoadScene("RegisterMenu");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
