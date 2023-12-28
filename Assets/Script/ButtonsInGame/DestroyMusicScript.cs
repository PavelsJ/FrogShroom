using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMusicScript : MonoBehaviour
{
    void Start()
    {
        SceneController.instance.DisableSceneController();
    }
}
