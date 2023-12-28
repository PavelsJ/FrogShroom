using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void DisableSceneController()
    {
        gameObject.SetActive(false);
    }

    public void ActivateSceneController()
    {
        gameObject.SetActive(true);
    }
}

  



