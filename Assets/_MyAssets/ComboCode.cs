using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComboCode : MonoBehaviour
{
    private Animator anim;
    private float cooldownTime = 0.8f;
    private float nextFireTime = 0f;
    private static int numOfClicks = 0;
    float lastClickedTime = 0;
    float maxComboDelay = 1;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit01"))
        {
            anim.SetBool("Hit01", false);
        }
        
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit01"))
        {
            anim.SetBool("Hit02", false);
        }
        
        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit01"))
        {
            anim.SetBool("Hit03", false);
            numOfClicks = 0;
        }

        if(Time.time - lastClickedTime < maxComboDelay)
        {
            numOfClicks = 0;
        }

        if(Time.time > nextFireTime)
        {
            if(Input.GetMouseButtonDown(0))
            {
                OnClick();
            }
        }
    }

    void OnClick()
    {
        lastClickedTime = Time.time;
        numOfClicks++;
        
        if(numOfClicks == 1)
        {
            anim.SetBool("Hit01", true);
        }
        numOfClicks = Mathf.Clamp(numOfClicks, 0, 3);

        if(numOfClicks >= 2 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit01"))
        {
            anim.SetBool("Hit01", false);
            anim.SetBool("Hit02", true);
        }

        if(numOfClicks >= 3 && anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7f && anim.GetCurrentAnimatorStateInfo(0).IsName("Hit02"))
        {
            anim.SetBool("Hit02", false);
            anim.SetBool("Hit03", true);
        }
    }
}
