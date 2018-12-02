using UnityEngine;
using System.Collections;
using Pathfinding;

public class AIStateSoldierChase : IAIState
{

    public AIStateSoldierChase(ICharacterAI characterAI) : base(characterAI)
    {
        StateType = ENUM_State.Chasing;
    }

    public override void act()
    {
        if (m_characterAI.Character.getCurHP() <= 0)
        {
            m_characterAI.doTransition(ENUM_StateCondition.NoHealth);
            return;
        }

        if (m_characterAI.lostTarget()) { 
            m_characterAI.doTransition(ENUM_StateCondition.LostTarget);
            return;
        }

        m_characterAI.startPathFinding();
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
