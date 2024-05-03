using UnityEngine;

public class NpcAnimator : MonoBehaviour
{
    [SerializeField] Npc npc;
    private const string IS_WALKING = "IsWalking";
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Update(){
        animator.SetBool(IS_WALKING, npc.IsWalking());
    }
}
