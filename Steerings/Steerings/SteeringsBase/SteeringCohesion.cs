using System;
using System.Collections.Generic;
using UnityEngine;

public class SteeringCohesion : ISteering
{
    public SteeringCohesion(ICharacterSteerings characterSteerings) : base(characterSteerings)
    {
    }

    public override Vector3 force()
    {
        Vector3 force = new Vector3(0, 0, 0);
        Vector3 centerPos = new Vector3(0, 0, 0);
        foreach (GameObject neighbor in m_characterSteerings.neighbors) {
            if (neighbor != null) {
                centerPos += neighbor.transform.position;
            }
        }
        int neighborCount = m_characterSteerings.neighbors.Count;
        if (neighborCount > 0) {
            centerPos /= (float) neighborCount;
            m_desiredVelocity = (centerPos - getTargetLocatePos()).normalized * m_characterSteerings.m_maxSpeed;
            force =  m_desiredVelocity - m_characterSteerings.m_velocity;
        }
        return force;
    }
}

