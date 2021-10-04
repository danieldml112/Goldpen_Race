using UnityEngine;
using UnityEngine.Animations;

public class FSMStateMachineBehaviour<FSMTStateMachine> : StateMachineBehaviour, FSMIStateBehaviour<FSMTStateMachine>
{
    
    #region FSMIStateBehaviour
    
    void FSMIStateBehaviour<FSMTStateMachine>.Initialize(Animator animator, FSMTStateMachine stateMachine)
    {
        this._fsm = stateMachine;
        OnInitialized();
    }

    void FSMIStateBehaviour<FSMTStateMachine>.Enable()
    {
        enabled = true;
    }

    void FSMIStateBehaviour<FSMTStateMachine>.Disable()
    {
        DisableAndExit();
    }
    
    #endregion

    #region StateMachineBehaviour

    public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (active || !enabled)
            return;

        OnStateEntered();
        active = true;
    }
    
    public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!active || !enabled)
            return;

        active = false;
        OnStateExited();
    }

    public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!enabled)
            return;

        OnStateUpdated();
    }
    
    #endregion

    #region FSMStateMachineBehaviour
    
    protected FSMTStateMachine FSM => _fsm;

    FSMTStateMachine _fsm;
    bool active = false;
    bool enabled = true;

    void OnDisable()
    {
        DisableAndExit();
    }

    void DisableAndExit()
    {
        if (!enabled)
            return;

        if (active)
        {
            OnStateExited();
            active = false;
        }
        
        enabled = false;
    }
    
    #region Virtual Methods
    
    protected virtual void OnInitialized()
    {
    }

    protected virtual void OnStateEntered()
    {
    }

    protected virtual void OnStateExited()
    {
    }

    protected virtual void OnStateUpdated()
    {
    }
    
    #endregion
 
    #endregion
    
}