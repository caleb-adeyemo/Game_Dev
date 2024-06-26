using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] Player player;
    private const string IS_WALKING = "isWalking";
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Update(){
        animator.SetBool(IS_WALKING, player.IsWaling());
    }
}
