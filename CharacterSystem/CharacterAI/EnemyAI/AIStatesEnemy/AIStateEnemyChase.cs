using UnityEngine;
using System.Collections;
using Pathfinding;

public class AIStateEnemyChase : IAIState
{

    public AIStateEnemyChase(ICharacterAI characterAI) : base(characterAI)
    {
        StateType = ENUM_State.Chasing;
    }

    public override void act()
    {
        if (m_characterAI.Character.getCurHP() <= 0)
        {
            m_characterAI.doTransition(ENUM_StateCondition.NoHealth);
        }
        else if (m_characterAI.lostTarget())
        {
            m_characterAI.doTransition(ENUM_StateCondition.LostTarget);
            m_characterAI.enableSteering(ENUM_Steering.Wander);
        }
        else if (m_characterAI.enterAttackRange())
        {
            m_characterAI.doTransition(ENUM_StateCondition.InAttackRange);
        }
        else
        {
            m_characterAI.startPathFinding();
        }
    }

    public override void fixedUpdate()
    {
        m_characterAI.excuteRouting();
    }

    public override void update()
    {
        act();
    }

    
    
}
