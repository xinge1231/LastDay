using UnityEngine;
using System.Collections;

public class AIStateSoldierAttack : IAIState {
    public AIStateSoldierAttack(ICharacterAI characterAI) : base(characterAI)
    {
        StateType = ENUM_State.Attacking;
    }

    public override void act()
    {
        if (m_characterAI.Character.getCurHP() <= 0)
        {
            m_characterAI.doTransition(ENUM_StateCondition.NoHealth);
            return;
        }
        else
        {
            m_characterAI.attack();
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
