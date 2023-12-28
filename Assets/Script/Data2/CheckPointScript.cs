using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    public Transform objectToSave;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.tag =="Frog")
        {
            PlayerPrefs.SetFloat("SavedPositionX", objectToSave.position.x);
            PlayerPrefs.SetFloat("SavedPositionY", objectToSave.position.y);
            Debug.Log("Checkpoint");
        }
    }
}

