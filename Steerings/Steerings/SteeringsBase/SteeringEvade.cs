using UnityEngine;
using System.Collections;

public class SteeringEvade : ISteering
{
    public SteeringEvade(ICharacterSteerings characterSteerings) : base(characterSteerings)
    {
    }

    public override Vector3 force()
    {
        float targetSpeed = getTargetCharacter().getCurSpeed();
        float preTime = (getTargetLocatePos() - getOriginPos()).magnitude / (m_characterSteerings.m_maxSpeed + targetSpeed);
        m_desiredVelocity = (getOriginPos() - (getTargetLocatePos() + getTargetCharacter().getGameObject().transform.forward * targetSpeed * preTime)).normalized * m_characterSteerings.m_maxSpeed;
        return m_desiredVelocity - m_characterSteerings.m_velocity;
    }
}
