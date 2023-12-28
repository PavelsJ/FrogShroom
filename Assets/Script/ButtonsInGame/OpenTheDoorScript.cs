using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheDoorScript : MonoBehaviour
{
    public Transform objectToOpen1;
    public Vector2 targetCoordinates1 = new Vector2(51f, 0f);
    public GameObject bossHealthBar;

    public void OpenDoor()
    {   
        if (objectToOpen1 != null)
        {
            Vector3 newPosition1 = new Vector3(targetCoordinates1.x, targetCoordinates1.y, objectToOpen1.position.z);
            objectToOpen1.position = newPosition1;

            bossHealthBar.gameObject.SetActive(false);
        }
    }
    
}
