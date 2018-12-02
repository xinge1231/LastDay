using UnityEngine;
using System.Collections;

public class AIStateEnemyIdle : IAIState
{
    public AIStateEnemyIdle(ICharacterAI characterAI) : base(characterAI)
    {
        StateType = ENUM_State.Idle;
    }

    public override void act()
    {
        if (m_characterAI.Character.getCurHP() <= 0)
        {
            m_characterAI.doTransition(ENUM_StateCondition.NoHealth);
            return;
        }

        //if ((m_characterAI.Character.getPosition() - m_characterAI.Character.getTargetPos()).magnitude < 20) {
        if (m_characterAI.hasTarget()) { 
            m_characterAI.doTransition(ENUM_StateCondition.SawTarget);
            m_characterAI.disableSteering(ENUM_Steering.Wander);
        }
    }

    public override void fixedUpdate()
    {
        
    }

    public override void update()
    {
        act();
        //m_characterAI.changeAIState(new AIStateChase(m_characterAI));

    }
}
