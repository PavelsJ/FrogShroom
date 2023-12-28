using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowObjectScript : MonoBehaviour
{
    

    public GameObject canvasToOpen;
    public Button openButton;
    public Button closeButton;

    private void Start()
    {
        openButton.onClick.AddListener(ToggleGameObject);
        closeButton.onClick.AddListener(UnToggleGameObject);
        
        canvasToOpen.gameObject.SetActive(false);
    }

    private void ToggleGameObject()
    {
        canvasToOpen.gameObject.SetActive(!canvasToOpen.gameObject.activeSelf);
    }
    private void UnToggleGameObject()
    {
        canvasToOpen.gameObject.SetActive(false);
    }
}

