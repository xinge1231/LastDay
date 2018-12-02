using System;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAlignment : ISteering
{
    public SteeringAlignment(ICharacterSteerings characterSteerings) : base(characterSteerings)
    {
    }

    public override Vector3 force()
    {
        Vector3 force = new Vector3(0, 0, 0);
        Vector3 averageDirection = new Vector3(0, 0, 0);
        foreach (GameObject neighbor in m_characterSteerings.neighbors) {
            if (neighbor != null) {
                averageDirection += neighbor.transform.forward;
            }
        }
        if (m_characterSteerings.neighbors.Count > 0) {
            //计算附近邻居的平均方向向量
            averageDirection /= (float)m_characterSteerings.neighbors.Count;
            //计算操控向量
            force =  averageDirection - getOrigin().getGameObject().transform.forward;
        }
        return force;
    }
}

