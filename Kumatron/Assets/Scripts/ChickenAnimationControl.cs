using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimationControl : MonoBehaviour
{

    public Animator chickenAnimator;
    void Start()
    {
        chickenAnimator = GetComponent<Animator>();
    }

    public void ChickenCanMove(bool moving)
    {
        chickenAnimator.SetBool("isWalking", moving);
    }
    public void ChickenIsFalling(bool falling)
    {
        chickenAnimator.SetBool("isFalling", falling);
    }
    public void ChickenIsAbducting(bool abducting)
    {
        chickenAnimator.SetBool("isAbducting", abducting);
    }
}
