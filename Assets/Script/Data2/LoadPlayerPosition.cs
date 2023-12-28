using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;


public class LoadPlayerPosition : MonoBehaviour
{
    public Transform playerTransform; 

    private void Start()
    {
        if (playerTransform == null)
        {
            Debug.LogError("Player Transform is not assigned.");
            return;
        }

        LoadSavedPosition();
        
    }

    private void LoadSavedPosition()
    {
        if (PlayerPrefs.HasKey("SavedPositionX") && PlayerPrefs.HasKey("SavedPositionY"))
        {
            float savedPosX = PlayerPrefs.GetFloat("SavedPositionX");
            float savedPosY = PlayerPrefs.GetFloat("SavedPositionY");
            
            Vector3 savedPosition = new Vector3(savedPosX, savedPosY);

            playerTransform.position = savedPosition;

            Debug.Log("Player position data is loaded.");
        }
        else
        {
            Debug.LogWarning("Saved position data is missing.");
        }
    }



}
