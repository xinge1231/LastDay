/*
 * 追逐行为
 * 
 */
using UnityEngine;
using System.Collections;

public class SteeringPursuit : ISteering
{
    public SteeringPursuit(ICharacterSteerings characterSteerings) : base(characterSteerings)
    {
    }

    public override Vector3 force()
    {
        if (getTargetCharacter() == null) {
            return Vector3.zero;
        }
        
        Vector3 originToTarget = getTargetCharacterPos() - getOriginPos();
        //点积，用于计算追逐者和逃避者面向的夹角
        float dotValue = Vector3.Dot(getOrigin().getGameObject().transform.forward, getTargetCharacter().getGameObject().transform.forward);

        //符合条件时采用直接靠近行为
        if (Vector3.Dot(originToTarget,getOrigin().getGameObject().transform.forward)>0 && dotValue < -0.95f) {
            m_desiredVelocity = (getTargetCharacterPos()-getOriginPos()).normalized * m_characterSteerings.m_maxSpeed;
            return m_desiredVelocity - m_characterSteerings.m_velocity;
        }
        
        float targetSpeed = getTargetCharacter().getCurSpeed();//获取速率
        
        //提前时间和两者距离成正比，和两者速度成反比
        float preTime = (getTargetCharacterPos() - getOriginPos()).magnitude / (m_characterSteerings.m_maxSpeed + targetSpeed);
       //通过预测后的目标位置来计算期望速度
        m_desiredVelocity = ((getTargetCharacterPos() + getTargetCharacter().getGameObject().transform.forward*targetSpeed* preTime) - getOriginPos()).normalized * m_characterSteerings.m_maxSpeed;
        return m_desiredVelocity - m_characterSteerings.m_velocity;
    }
}
