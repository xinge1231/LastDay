using UnityEngine;
using System.Collections;
using Pathfinding;

public class SoldierAI : ICharacterAI
{
    //private ICharacter m_target = null;


    public SoldierAI(ICharacter character) : base(character)
    {
    }

    public override void init()
    {
        IAIState patrollState = new AIStateSoldierPatroll(this);
        IAIState chaseState = new AIStateSoldierChase(this);
        IAIState attackState = new AIStateSoldierAttack(this);
        IAIState deadState = new AIStateSoldierDead(this);
        m_states.Add(patrollState.StateType, patrollState);
        m_states.Add(chaseState.StateType, chaseState);
        m_states.Add(attackState.StateType, attackState);
        m_states.Add(deadState.StateType, deadState);

        CurStateType = patrollState.StateType;
        CurState = patrollState;

        m_states[ENUM_State.Patrolling].addTransiton(ENUM_StateCondition.SawTarget, ENUM_State.Chasing);
        m_states[ENUM_State.Patrolling].addTransiton(ENUM_StateCondition.NoHealth, ENUM_State.Dead);
        m_states[ENUM_State.Chasing].addTransiton(ENUM_StateCondition.LostTarget, ENUM_State.Patrolling);
        m_states[ENUM_State.Chasing].addTransiton(ENUM_StateCondition.InAttackRange, ENUM_State.Attacking);
        m_states[ENUM_State.Chasing].addTransiton(ENUM_StateCondition.NoHealth, ENUM_State.Dead);
        m_states[ENUM_State.Attacking].addTransiton(ENUM_StateCondition.OutAttackRange, ENUM_State.Chasing);
        m_states[ENUM_State.Attacking].addTransiton(ENUM_StateCondition.NoHealth, ENUM_State.Dead);
    }
    public override void attack()
    {
        base.attack();
    }

    public override void chase()
    {
        //Character.chase(m_target);
    }

    public override void moveTo(Vector3 position)
    {
        base.moveTo(position);
    }

    public void setTarget(ICharacter target)
    {
        //m_target = target;
    }


}
