using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FreezePositionButton : MonoBehaviour
{
    private bool isGameFrozen = false;
    public Button freezeButton; 
    public Button cancelButton; 
    public Button menuButton;
    public Button skipTutorialButton;

    void Start()
    {
        freezeButton.onClick.AddListener(FreezeObjectPosition);
        cancelButton.onClick.AddListener(UnToggleFreezeGame);
        menuButton.onClick.AddListener(UnToggleFreezeGame);
        skipTutorialButton.onClick.AddListener(UnToggleFreezeGame);
    }

   
    private void FreezeObjectPosition()
    {
       ToggleFreezeGame();
    }

    void ToggleFreezeGame()
    {
        isGameFrozen = !isGameFrozen;

        if (isGameFrozen)
        {
            Time.timeScale = 0f; // Set time scale to 0 to freeze the game
        }
        else
        {
            Time.timeScale = 1f; 
        }
    }
    void UnToggleFreezeGame()
    {
        isGameFrozen = false;
        Time.timeScale = 1f;
    }
    
}

