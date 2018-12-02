using UnityEngine;
using System.Collections;

public class AIStateEnemyrAttack : IAIState {
    public AIStateEnemyrAttack(ICharacterAI characterAI) : base(characterAI)
    {
        StateType = ENUM_State.Attacking;
    }

    public override void act()
    {
        m_characterAI.attack();
    }

    public override void fixedUpdate()
    {
    }
    public override void update()
    {
        act();
    }


}
