using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyButton : MonoBehaviour
{
    [SerializeField] Animator TransAnimation;
    public int sceneSequence;

    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(LoadMainSceneWithDelay());
        }
    }

    IEnumerator LoadMainSceneWithDelay()
    {
        TransAnimation.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneSequence);
    }
}