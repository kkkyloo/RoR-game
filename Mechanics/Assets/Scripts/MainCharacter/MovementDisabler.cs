using RoR;
using UnityEngine;

public class MovementDisabler : StateMachineBehaviour
{
    public Option enableMovement;

    public enum Option
    {
        Enable,
        Disable
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        var controller = animator.GetComponent<ThirdPersonController>();

        if (controller != null)
        {
            controller.lockMovement = enableMovement == Option.Disable;
        }
        else
        {
            Debug.LogError("ThirdPersonController component is not found on the Animator's GameObject.");
        }
    }
}
