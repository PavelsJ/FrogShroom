using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewLevelScript : MonoBehaviour
{
   public int sceneSequence;
   public Transform objectToSave;
   [SerializeField] private AudioSource MushroomSound;
   [SerializeField] Animator TransAnimation;
   public Animator animator;

   IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.tag =="Frog")
        {
            TransAnimation.SetTrigger("End");
            MushroomSound.Play();

            yield return new WaitForSeconds(1);

            TransformObjectToZero();

            PlayerPrefs.SetFloat("SavedPositionX", objectToSave.position.x);
            PlayerPrefs.SetFloat("SavedPositionY", objectToSave.position.y);

            SceneManager.LoadScene(sceneSequence);
            
        }
        else
        {
            Debug.Log("myObject is null!");
        }
    }
    
    public void TransformObjectToZero()
    {
        GameObject.FindGameObjectWithTag("Frog").transform.position = new Vector2(0, 0);
    }
}
