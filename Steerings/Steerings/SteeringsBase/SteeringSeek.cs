using UnityEngine;
using System.Collections;

public class SteeringSeek : ISteering
{

    public SteeringSeek(ICharacterSteerings characterSteerings) : base(characterSteerings) { }

    public override Vector3 force()
    {
        if (getTargetCharacter() == null)
        {
            return Vector3.zero;
        }

        //计算预期速度
        m_desiredVelocity = (getTargetLocatePos()- getOriginPos()).normalized * m_characterSteerings.m_maxSpeed;

        if (m_characterSteerings.m_isPlane)
        {
            m_desiredVelocity.y = 0;
        }
        //返回操控向量(预期速度和当前速度的差)
        return m_desiredVelocity - m_characterSteerings.m_velocity;

    }
}
