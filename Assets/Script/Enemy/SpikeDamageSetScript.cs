using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeDamageSetScript : MonoBehaviour
{
    [SerializeField] Animator TransAnimation;
    public Transform playerTransform;
    
    IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.tag =="Frog")
        { 
            playerTransform.GetComponent<MoveBehaviourScript>().StopMovement();
            TransAnimation.SetTrigger("End");

            yield return new WaitForSeconds(1);

            if (playerTransform != null)
            {
                LoadSavedPosition();
            }

            TransAnimation.SetTrigger("Start");
            playerTransform.GetComponent<MoveBehaviourScript>().ResumeMovement();

        }
        else
        {
            Debug.Log("No Change");
        }
        
    }

    private void LoadSavedPosition()
    {
        if (PlayerPrefs.HasKey("SavedPositionX") && PlayerPrefs.HasKey("SavedPositionY"))
        {
            float savedPosX = PlayerPrefs.GetFloat("SavedPositionX");
            float savedPosY = PlayerPrefs.GetFloat("SavedPositionY");
            
            Vector3 savedPosition = new Vector3(savedPosX, savedPosY);

            playerTransform.position = savedPosition;

        }
        else
        {
            Debug.LogWarning("Saved position data is missing.");
        }
    }


    
}
    
