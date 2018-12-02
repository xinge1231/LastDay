using UnityEngine;
using System.Collections;

public class EnemyAI : ICharacterAI
{
    private ICharacter m_target = null;
    public EnemyAI(ICharacter character) : base(character)
    {
    }


    public override void init()
    {
        IAIState idleState = new AIStateSoldierIdle(this);
        IAIState chaseState = new AIStateSoldierChase(this);
        IAIState attackState = new AIStateSoldierAttack(this);
        IAIState deadState = new AIStateSoldierDead(this);
        m_states.Add(idleState.StateType, idleState);
        m_states.Add(chaseState.StateType, chaseState);
        m_states.Add(attackState.StateType, attackState);
        m_states.Add(deadState.StateType, deadState);

        CurStateType = idleState.StateType;
        CurState = idleState;

        m_states[ENUM_State.Idle].addTransiton(ENUM_StateCondition.SawTarget, ENUM_State.Chasing);
        m_states[ENUM_State.Idle].addTransiton(ENUM_StateCondition.NoHealth, ENUM_State.Dead);
        m_states[ENUM_State.Chasing].addTransiton(ENUM_StateCondition.LostTarget, ENUM_State.Idle);
        m_states[ENUM_State.Chasing].addTransiton(ENUM_StateCondition.InAttackRange, ENUM_State.Attacking);
        m_states[ENUM_State.Chasing].addTransiton(ENUM_StateCondition.NoHealth, ENUM_State.Dead);
        m_states[ENUM_State.Attacking].addTransiton(ENUM_StateCondition.OutAttackRange, ENUM_State.Chasing);
        m_states[ENUM_State.Attacking].addTransiton(ENUM_StateCondition.NoHealth, ENUM_State.Dead);
    }

    public override void moveTo(Vector3 position)
    {
        base.moveTo(position);
    }

    public  void setTarget(ICharacter target) {
        m_target = target;
    }

    public override void attack()
    {
        base.attack();
    }

    public override void chase()
    {
        Character.chase(m_target);
    }
}
