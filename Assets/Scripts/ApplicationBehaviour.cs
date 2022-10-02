using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplicationBehaviour : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("1-1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
