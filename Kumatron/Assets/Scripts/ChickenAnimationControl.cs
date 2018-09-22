using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimationControl : MonoBehaviour
{

    public Animator chickenAnimator;
    [SerializeField]
    private AudioClip _chickenSound;
    [SerializeField]
    private AudioClip _chickenSoundRooster;
    void Start()
    {
        chickenAnimator = GetComponent<Animator>();
        this.gameObject.name = "Chicken";
    }

    public void ChickenCanMove(bool moving)
    {
        chickenAnimator.SetBool("isWalking", moving);
    }

    public void ChickenIsAbducting(bool abducting)
    {
        chickenAnimator.SetBool("isAbducting", abducting);
    }
    public void ChickenIsFalling(bool falling)
    {
        chickenAnimator.SetBool("isFalling", falling);
    }
    public void PlayChikenSound()
    {
        AudioSource.PlayClipAtPoint(_chickenSound, this.transform.position);
    }
    public void PlayChikenSoundRooster()
    {
        AudioSource.PlayClipAtPoint(_chickenSoundRooster, this.transform.position);
    }
}
