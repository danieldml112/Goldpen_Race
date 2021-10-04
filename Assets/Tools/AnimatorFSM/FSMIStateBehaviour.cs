using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface FSMIStateBehaviour<FSMTStateMachine>
{
    void Initialize(Animator animator, FSMTStateMachine stateMachine);
    void Enable();
    void Disable();
}
