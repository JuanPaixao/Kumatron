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

    // Update is called once per frame
    void Update()
    {

    }
    public void ChickenCanMove(bool moving)
    {
        chickenAnimator.SetBool("Walking", moving);
    }
}
