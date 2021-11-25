using UnityEngine;

public class SeaTick : MonoBehaviour
{
    public Animator animator;
    private void Awake() 
    {
        animator = GetComponent<Animator>();
    }
    public void SetSeaNextStageTick()
    {
        animator.SetBool("SetNextStage", true);
    }
    public void CloseSeaNextStageTick()
    {
        animator.SetBool("SetNextStage", false);
    }
}
