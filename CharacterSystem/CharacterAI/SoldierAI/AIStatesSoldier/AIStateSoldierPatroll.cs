using System;
using System.Collections.Generic;



public class AIStateSoldierPatroll : IAIState
{
    public AIStateSoldierPatroll(ICharacterAI characterAI) : base(characterAI)
    {
    }

    public override void act()
    {
        if (m_characterAI.Character.getCurHP() <= 0)
        {
            m_characterAI.doTransition(ENUM_StateCondition.NoHealth);
        }
        else if (m_characterAI.hasTarget())
        {
            m_characterAI.doTransition(ENUM_StateCondition.SawTarget);
        }
        else {

        }
    }

    public override void fixedUpdate()
    {
    }

    public override void update()
    {
        act();
    }
}

