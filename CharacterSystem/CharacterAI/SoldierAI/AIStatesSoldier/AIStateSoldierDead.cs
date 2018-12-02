using UnityEngine;
using System.Collections;

public class AIStateSoldierDead : IAIState
{
    public AIStateSoldierDead(ICharacterAI characterAI) : base(characterAI)
    {
        StateType = ENUM_State.Dead;
    }

    public override void act()
    {
        m_characterAI.Character.IsKilled= true;
    }

    public override void fixedUpdate()
    {
    }

    public override void update()
    {
        act();
    }
}
