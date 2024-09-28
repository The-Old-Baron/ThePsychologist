using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassScene : MonoBehaviour, IInteractive
{
    public string sceneName;

    public void OpenNewScene()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Interact()
    {
        Debug.Log("Scene is passed");
        OpenNewScene();
    }
}
