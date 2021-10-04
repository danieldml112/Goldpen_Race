using System.Collections.Generic;
using UnityEngine;

public static class FSMStateExtensions
{
    public static IEnumerable<FSMIStateBehaviour<FSMTStateMachine>> GetStateBehaviours<FSMTStateMachine>(this FSMTStateMachine stateMachine, Animator animator)
    {
        StateMachineBehaviour[] behaviours = animator.GetBehaviours<StateMachineBehaviour>();
        foreach (StateMachineBehaviour behaviour in behaviours)
        {
            FSMIStateBehaviour<FSMTStateMachine> stateBehaviour = behaviour as FSMIStateBehaviour<FSMTStateMachine>;
            if (stateBehaviour == null)
                continue;

            yield return stateBehaviour;
        }
    }

    public static void ConfigureAllStateBehaviours<FSMTStateMachine>(this FSMTStateMachine stateMachine, Animator animator)
    {
        foreach (FSMIStateBehaviour<FSMTStateMachine> behaviour in stateMachine.GetStateBehaviours(animator))
            behaviour.Initialize(animator, stateMachine);
    }

    public static void DisableAllStateBehaviours<FSMTStateMachine>(this FSMTStateMachine stateMachine, Animator animator)
    {
        foreach (FSMIStateBehaviour<FSMTStateMachine> behaviour in stateMachine.GetStateBehaviours(animator))
            behaviour.Disable();
    }

    public static void EnableAllStateBehaviours<FSMTStateMachine>(this FSMTStateMachine stateMachine, Animator animator)
    {
        foreach (FSMIStateBehaviour<FSMTStateMachine> behaviour in stateMachine.GetStateBehaviours(animator))
            behaviour.Enable();
    }
}
