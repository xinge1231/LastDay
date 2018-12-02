using UnityEngine;
using System.Collections;

public class SteeringArrive : ISteering{
    private float arrivalDistance = 0.3f;
    private float characterRadius = 1.2f;
    private float slowDownDistance = 10.0f;

    public SteeringArrive(ICharacterSteerings characterSteerings) : base(characterSteerings)
    {
    }

    public override Vector3 force()
    {
        if (getTargetCharacter() == null)
        {
            return Vector3.zero;
        }
        //到目标的位移向量
        Vector3 localToTarget = getTargetLocatePos() -getOriginPos();
        if (m_characterSteerings.m_isPlane)
            localToTarget.y = 0;

        float distance = localToTarget.magnitude; //距离 = 向量长度
        //大于设置减速半径时
        if (distance > slowDownDistance)
        {
            //预期速度是 距离向量方向*最大速度
            m_desiredVelocity = localToTarget.normalized * m_characterSteerings.m_maxSpeed;
            return m_desiredVelocity - m_characterSteerings.m_velocity;
        }
        else {
            //预期速度是 距离向量-当前速度(原理？)
            m_desiredVelocity = localToTarget - m_characterSteerings.m_velocity;
            return m_desiredVelocity - m_characterSteerings.m_velocity;
        }
    }
}
