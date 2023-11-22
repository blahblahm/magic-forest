using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainManu : MonoBehaviour
{

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
    }
}
