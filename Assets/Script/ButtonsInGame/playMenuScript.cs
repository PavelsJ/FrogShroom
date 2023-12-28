using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playMenuScript : MonoBehaviour
{
    [SerializeField] Animator TransAnimation;

    public void OnMenuButton ()
    {
        StartCoroutine(MenuButton());
    }

    IEnumerator MenuButton()
    {
        TransAnimation.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(3);
    }

    public void OnEnterButton ()
    {
        StartCoroutine(EnterButton());
    }

    IEnumerator EnterButton()
    {
        TransAnimation.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0);
    }
}
