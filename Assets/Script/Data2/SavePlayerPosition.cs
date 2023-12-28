using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerPosition : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating("SavePosition", 3f, 3f); // Save position every 10 seconds
    }

    private void SavePosition()
    {
        // Check if the player is not on a special object before saving
        if (GameObject.FindGameObjectWithTag("Frog").GetComponent<MoveBehaviourScript>().IsGrounded())
        { 
            PlayerPrefs.SetFloat("SavedPositionX", transform.position.x);
            PlayerPrefs.SetFloat("SavedPositionY", transform.position.y);
            PlayerPrefs.Save();
            Debug.Log("Player position saved.");
            
        }
        else
        {
           Debug.Log("Player is on a special object. Position not saved.");
        }
    }

   
}
