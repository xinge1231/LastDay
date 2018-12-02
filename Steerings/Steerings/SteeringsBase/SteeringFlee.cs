/*
 * 逃离行为
 * 
 */
using UnityEngine;
using System.Collections;

public class SteeringFlee : ISteering
{
    private float m_fearDistance = 20;
    public SteeringFlee(ICharacterSteerings characterSteerings) : base(characterSteerings)
    {
    }

    public override Vector3 force()
    {
        if (getTargetCharacter() == null)
        {
            return Vector3.zero;
        }
        //只考虑平面
        Vector3 tmpPos = new Vector3(getOriginPos().x, 0, getOriginPos().z);
        Vector3 targetPos = new Vector3(getTargetLocatePos().x, 0, getTargetLocatePos().z);

        //进入感知距离(恐惧范围)，开始逃离
        if (Vector3.Distance(tmpPos, targetPos) <= m_fearDistance) {
            m_fearDistance = 100; //增加恐惧范围
            float maxSpeed = m_characterSteerings.m_maxSpeed + 10; //为了测试，增加了最大速度
            m_desiredVelocity = (tmpPos - targetPos).normalized * maxSpeed;
            return m_desiredVelocity - m_characterSteerings.m_velocity;
        }
        return new Vector3(0, 0, 0);
    }
}
