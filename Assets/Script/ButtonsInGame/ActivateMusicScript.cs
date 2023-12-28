using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateMusicScript : MonoBehaviour
{
    void Start()
    {
        SceneController.instance.ActivateSceneController();
    }
}
