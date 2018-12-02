using System.Collections.Generic;
using UnityEngine;

class SteeringFollowPath : ISteering
{
    private List<GameObject> wayPoints = new List<GameObject>();
    private float arriveDistance = 10.0f; //小于该距离认为到达l路点
    private float sqrArriveDistance;
    private float slowDownDistance = 10.0f;
    private int curNode = 0;//当前路点
    public SteeringFollowPath(ICharacterSteerings characterSteerings) : base(characterSteerings)
    {
        sqrArriveDistance = arriveDistance * arriveDistance;
        init();
    }

    public void init() {
        GameObject obj = UnityTool.findGameObject("WayPoints");
        if (obj == null)
            return;
        foreach (Transform child in obj.GetComponentInChildren<Transform>(true))
        {
            wayPoints.Add(child.gameObject);
        }
        if (wayPoints.Count > 0)
            getOrigin().TargetPos = wayPoints[curNode].transform.position;
    }

    public override Vector3 force()
    {
        Vector3 oringnToTarget = getTargetLocatePos() - getOriginPos();
        if (m_characterSteerings.m_isPlane)
            oringnToTarget.y = 0;

        if (curNode == wayPoints.Count - 1)//最后一个路点
        {
            if (oringnToTarget.magnitude > slowDownDistance)
            {
                m_desiredVelocity = oringnToTarget.normalized * m_characterSteerings.m_maxSpeed;
                return m_desiredVelocity - m_characterSteerings.m_velocity;
            }
            else
            {
                m_desiredVelocity = oringnToTarget - m_characterSteerings.m_velocity;
                return m_desiredVelocity - m_characterSteerings.m_velocity;
            }

        }
        else {//不是最后一个路点
            if (oringnToTarget.sqrMagnitude < sqrArriveDistance) {
                getOrigin().TargetPos = wayPoints[++curNode].transform.position;
            }

            m_desiredVelocity = oringnToTarget.normalized * m_characterSteerings.m_maxSpeed;
            return m_desiredVelocity - m_characterSteerings.m_velocity;
        }

    }
}

