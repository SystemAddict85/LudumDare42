using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionalAnimation : MonoBehaviour {
    
    protected enum DirectionalPriority { FRONT, BACK, SIDE } ;
    protected Animator anim;

    protected DirectionalPriority currentPriority = DirectionalPriority.FRONT;
    private bool readyToLook = true;

    [SerializeField]
    private float frontSideThreshold = 0f;

    [SerializeField]
    private bool isSpriteFacingRight = true;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update () {

        if (readyToLook)
        {
            StartCoroutine(WaitToLook());

            var direction = GetPlayerDirection();

            var priority = SetDirectionPriority(direction);

            if (priority != currentPriority)
            {
                currentPriority = priority;
                UpdateAnimator();
            }
        }
	}

    private IEnumerator WaitToLook()
    {
        readyToLook = false;
        yield return new WaitForSeconds(.2f);
        readyToLook = true;        
    }

    protected virtual void UpdateAnimator()
    {
        switch (currentPriority)
        {
            case DirectionalPriority.FRONT:
                anim.SetTrigger("showFront");
                break;
            case DirectionalPriority.BACK:
                anim.SetTrigger("showBack");
                break;
            case DirectionalPriority.SIDE:
            default:
                anim.SetTrigger("showSide");
                break;
        }
    }

    private DirectionalPriority SetDirectionPriority(Vector3 dir)
    {
        var priority = currentPriority;

        if(Mathf.Abs(dir.x) - Mathf.Abs(dir.z) > Mathf.Abs(frontSideThreshold))
        {
            priority = DirectionalPriority.SIDE;
            var rend = GetComponentInChildren<SpriteRenderer>();
            if(dir.x > 0)
            {
                rend.flipX = !isSpriteFacingRight;
            } else
            {
                rend.flipX = isSpriteFacingRight;
            }
        }
        else if(dir.z > 0)
        {
            if(transform.forward.z > 0)
            {
                priority = DirectionalPriority.FRONT;
            }
            else
            {
                priority = DirectionalPriority.BACK;
            }
        }
        else if(dir.z < 0)
        {
            if (transform.forward.z > 0)
            {
                priority = DirectionalPriority.BACK;
            }
            else
            {
                priority = DirectionalPriority.FRONT;
            }
        }

        return priority;
    }

    private Vector3 GetPlayerDirection()
    {
        return PlayerManager.Player.transform.position - transform.position;
    }
}
