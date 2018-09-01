using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullAnimationControl : MonoBehaviour
{

    public Animator bullAnimator;
    void Start()
    {
        bullAnimator = GetComponent<Animator>();
        this.gameObject.name = "Bull";
    }

    public void BullCanMove(bool moving)
    {
        bullAnimator.SetBool("isWalking", moving);
    }
    public void BullIsFalling(bool falling)
    {
        bullAnimator.SetBool("isFalling", falling);
    }
    public void BullIsAbducting(bool abducting)
    {
        bullAnimator.SetBool("isAbducting", abducting);
    }
}