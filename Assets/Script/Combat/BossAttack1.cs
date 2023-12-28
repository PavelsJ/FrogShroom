using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1 : MonoBehaviour
{
    private Animator animator;
    public CameraShakeScript camera;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && !animator.GetBool("BossKluvAttack"))
        {
            animator.SetBool("BossKluvAttack", true);
            StartCoroutine(camera.Shake(0.15f, 0.2f));
        }
    }

    void AnimationComplete()
    {
        animator.SetBool("BossKluvAttack", false);
    }
}

