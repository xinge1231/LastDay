using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/*
 * 操控行为基类
 * 
 */
using System;

public enum ENUM_Steering
{
    Seek = 0,
    Flee = 1,
    Arrive = 2,
    Pursuit = 3,
    Evade = 4,
    Wander = 5,
    FollowPath = 6,
    CollisionAvoidance = 7,
    Separation = 8,
    Alignment = 9,
    Cohesion = 10,
}

public abstract class ISteering  {
    protected ICharacterSteerings m_characterSteerings = null;
    private bool m_isEnable = false; //默认不启用
    private float m_weight = 1; //操控力权值

    protected Vector3 m_desiredVelocity = Vector3.zero;
    public bool IsEnable
    {
        get
        {
            return m_isEnable;
        }

        set
        {
            m_isEnable = value;
        }
    }

    public float Weight
    {
        get
        {
            return m_weight;
        }

        set
        {
            m_weight = value;
        }
    }
    public ISteering(ICharacterSteerings characterSteerings) {
        m_characterSteerings = characterSteerings;
    }

    //获取操控力
    public abstract Vector3 force();

    //获取自身对象
    public ICharacter getOrigin() {
        return m_characterSteerings.Character;
    }

    //获取自身对象位置
    public Vector3 getOriginPos() {
        return m_characterSteerings.Character.getPosition();
    }
    //获取目标角色
    public ICharacter getTargetCharacter() {
        return m_characterSteerings.Character.TargetCharacter;
    }

    //获取目标角色的位置
    public Vector3 getTargetCharacterPos()
    {
        ICharacter target = m_characterSteerings.Character;
        if (target != null)
            return target.TargetPos;
        return Vector3.zero;
    }
    //获取目标地点的位置
    public Vector3 getTargetLocatePos()
    {
        return m_characterSteerings.Character.TargetPos;
    }

}
