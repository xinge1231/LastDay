using System;
using System.Collections.Generic;
using UnityEngine;


public class SteeringSeparation : ISteering
{
    private float comfortDistance = 1.0f;
    private float punishFactor = 2.0f;

    public SteeringSeparation(ICharacterSteerings characterSteerings) : base(characterSteerings)
    {
    }

    public override Vector3 force()
    {
        Vector3 force = new Vector3(0, 0, 0);
        foreach (GameObject neighbor in m_characterSteerings.neighbors) {
            if (neighbor!=null) {
                Vector3 toNeighbor = getOriginPos() - neighbor.transform.position;
                float len = toNeighbor.magnitude;
                //计算该邻居导致的操控力(排斥力),与距离成反比
                force += toNeighbor.normalized / len;
                //如果距离小于可接受距离，再乘以一个惩罚因子
                if (len < comfortDistance)
                    force *= punishFactor;
            }
        }
        return force;
    }
}

