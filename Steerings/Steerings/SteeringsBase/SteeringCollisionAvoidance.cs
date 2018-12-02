using System;
using System.Collections.Generic;
using UnityEngine;

class SteeringCollisionAvoidance : ISteering
{
    private float MAX_SEE_RANGE = 10.0f;
    private float avoidanceForce = 10.0f;
    private List<GameObject> allColliders = new List<GameObject>();
    public SteeringCollisionAvoidance(ICharacterSteerings characterSteerings) : base(characterSteerings)
    {
        init();
    }
    public void init() {
        GameObject obj = UnityTool.findGameObject("Obstacles");
        foreach(Transform child in obj.GetComponentInChildren<Transform>()) {
            allColliders.Add(child.gameObject);
        }
    }
    public override Vector3 force()
    {
        RaycastHit hit;
        //当前视线距离(标量)
        float curSeeRange =  MAX_SEE_RANGE * (m_characterSteerings.m_velocity.magnitude / m_characterSteerings.m_maxSpeed);
        //当前位置加上视野距离后的向量（延伸向量）
        Vector3 ahead = getOriginPos() + m_characterSteerings.m_velocity.normalized * curSeeRange;
        Debug.DrawLine(getOriginPos(), ahead);

        if (avoidanceForce > m_characterSteerings.m_maxForce)
            avoidanceForce = m_characterSteerings.m_maxForce;

        //如果视线向量与障碍物相交，则返回躲避操控力。
        if (Physics.Raycast(getOriginPos(), m_characterSteerings.m_velocity.normalized, out hit, curSeeRange)){
            //if(hit.collider.gameObject.layer !=8 && hit.collider.gameObject.layer!=9)
            return (ahead - hit.collider.transform.position) * avoidanceForce;
        }
        return Vector3.zero;
    }
}

