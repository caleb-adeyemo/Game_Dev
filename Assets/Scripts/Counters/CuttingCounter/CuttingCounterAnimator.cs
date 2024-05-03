using UnityEngine;

public class CuttingCounterAnimator : MonoBehaviour
{
    [SerializeField] CuttingCounter cuttingCounter;
    private const string CUT = "Cut";
    private Animator animator;

    private void Awake(){
        animator = GetComponent<Animator>();
    }
    private void Start(){
        cuttingCounter.OnCut += Cut;
    }

    private void Cut(object sender, System.EventArgs e){
        animator.SetTrigger(CUT);
    }

}

