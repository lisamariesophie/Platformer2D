using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Run(bool run)
    {
        anim.SetBool("isRunning", run);
    }

    public void JumpTakeOff()
    {
        anim.SetTrigger("takeOff");
    }

    public void Jump(bool jump)
    {
        anim.SetBool("isJumping", jump);
    }

    public void WallSlide(bool slide)
    {
        anim.SetBool("isWallSliding", slide);
    }
}
