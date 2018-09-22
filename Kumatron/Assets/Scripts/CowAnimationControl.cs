using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowAnimationControl : MonoBehaviour
{

    public Animator cowAnimator;
    [SerializeField]
    private AudioClip _cowSound;
    void Start()
    {
        cowAnimator = GetComponent<Animator>();
        this.gameObject.name = "Cow";
    }

    public void CowCanMove(bool moving)
    {
        cowAnimator.SetBool("isWalking", moving);
    }
    public void CowIsFalling(bool falling)
    {
        cowAnimator.SetBool("isFalling", falling);
    }
    public void CowIsAbducting(bool abducting)
    {
        cowAnimator.SetBool("isAbducting", abducting);
    }
    public void PlayCowSound()
    {
        AudioSource.PlayClipAtPoint(_cowSound, this.transform.position);
    }
}