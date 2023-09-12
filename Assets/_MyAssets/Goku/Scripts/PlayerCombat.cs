using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public List<AttackSO> combo;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;
    private PlayerInput playerInput;
    private InputAction attackAction;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["AttackButton"];
    }

    // Update is called once per frame
    void Update()
    {
        if (attackAction.triggered)
        {
            Attack();
        }

        ExitAttack();
    }

    void Attack()
    {
        if(Time.time - lastComboEnd > 1f && comboCounter <= combo.Count)
        {
            CancelInvoke("EndCombo");

            if(Time.time - lastClickedTime >= 1f)
            {
                anim.runtimeAnimatorController = combo[comboCounter].animatorOV;
                anim.Play("Attack01", 0, 0);
                comboCounter++;
                lastClickedTime = Time.time;    

                if(comboCounter >= combo.Count)
                {
                    comboCounter = 0;
                    lastComboEnd = Time.time;
                }
            }
        }
    }

    void ExitAttack()
    {
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            Invoke("EndCombo", 1);
        }
    }

    void EndCombo()
    {
        comboCounter = 0;
        lastComboEnd = Time.time;   
    }
}
