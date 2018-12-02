/*
 * AI状态类接口
 * 功能：各实现类实现对应状态下的功能和状态转换条件
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class IAIState{
    private Dictionary<ENUM_StateCondition, ENUM_State> m_transitions = new Dictionary<ENUM_StateCondition, ENUM_State>(); //当前状态的 转换条件--新状态 表
    public System.Collections.Generic.Dictionary<ENUM_StateCondition, ENUM_State> Transitions
    {
        get { return m_transitions; }
        set { m_transitions = value; }
    }

    private ENUM_State m_stateType = ENUM_State.Null;
    public ENUM_State StateType
    {
        get { return m_stateType; }
        set { m_stateType = value; }
    }
    protected ICharacterAI m_characterAI = null;

    public IAIState(ICharacterAI characterAI) {
        m_characterAI = characterAI;
    }

    public abstract void fixedUpdate();
    public abstract void update();
    public abstract void act(); //当前状态下的行为

    //添加 转换--状态 键值对
    public void addTransiton(ENUM_StateCondition stateCondition, ENUM_State state)
    {
        if(!m_transitions.ContainsKey(stateCondition))
            m_transitions.Add(stateCondition, state);
    }

    public void removeTransiton(ENUM_StateCondition stateCondition, ENUM_State state)
    {
        if (m_transitions.ContainsKey(stateCondition))
            m_transitions.Remove(stateCondition);
    }

}
