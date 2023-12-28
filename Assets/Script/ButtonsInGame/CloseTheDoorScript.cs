using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseTheDoorScript : MonoBehaviour
{
    public Transform objectToClose;
    public Vector2 targetCoordinates = new Vector2(17f, -5.5f);

    public Transform objectToClose1;
    public Vector2 targetCoordinates1 = new Vector2(51f, -5.5f);

    public GameObject bossHealthBar;
    public GameObject bossMusic;

    public CameraShakeScript camera;

    [SerializeField] Animator bossStartAnimation;
    [SerializeField] Animator bossHealthBarAppear;

    [SerializeField] beakAttackScript crowBeakController;
    [SerializeField] clawsAttack1Script crowClaws1Controller;
    [SerializeField] clawsAttack2Script crowClaws2Controller;

    private bool doorClosed = false;

    private void Start()
    {
        bossHealthBar.gameObject.SetActive(false);
        bossMusic.gameObject.SetActive(false);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null && collision.tag =="Frog" && !doorClosed)
        {
            CloseDoor();
            bossHealthBar.gameObject.SetActive(true);
            doorClosed = true;
            SceneController.instance.DisableSceneController();
            bossMusic.gameObject.SetActive(true);
        }
        else
        {
            
        }
    }

    private void CloseDoor()
    {
        if (objectToClose != null && objectToClose1 != null)
        {
            Vector3 newPosition = new Vector3(targetCoordinates.x, targetCoordinates.y, objectToClose.position.z);
            objectToClose.position = newPosition;

            Vector3 newPosition1 = new Vector3(targetCoordinates1.x, targetCoordinates1.y, objectToClose1.position.z);
            objectToClose1.position = newPosition1;

            if (crowBeakController != null)
            {
                crowBeakController.StartMovingBeak();
                crowBeakController.StartRoutine();
            }

            if (crowClaws1Controller != null)
            {
                crowClaws1Controller.StartMovingClaws1();
            }

            StartCoroutine(SecondLegCoolDown());
            
        }
        else
        {
            Debug.LogError("ObjectToOpen is null!");
        }
    }

    IEnumerator SecondLegCoolDown()
    {
        if (crowClaws2Controller != null)
        {
            yield return new WaitForSeconds(2.5f);
            crowClaws2Controller.StartMovingClaws2();
        }
    }
}
