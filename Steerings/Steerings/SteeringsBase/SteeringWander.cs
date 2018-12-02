/*
 * 游荡行为(角色是活塞，目标是圆周上的点)
 * 
 */
using UnityEngine;


class SteeringWander : ISteering
{
    private float wanderRadius;//wander圈半径，徘徊半径
    private float wanderDistance;//wander距离，角色到圆心距离
    private float wanderJitter; //每秒加随机位移的最大值
    private Vector3 circleTarget;
    private Vector3 wanderTarget; //角色目标(用于产生游荡行为)

    public SteeringWander(ICharacterSteerings characterSteerings) : base(characterSteerings)
    {
        wanderRadius = Random.value * 100.0f;
        wanderDistance = Random.value * 20.0f;
        wanderJitter = Random.value * 10.0f;
        circleTarget = new Vector3(wanderRadius * 0.707f, 0, wanderRadius * 0.707f);
    }

    public override Vector3 force()
    {
       
        //计算随机位移
        Vector3 randomDisplacement = new Vector3((Random.value - 0.5f)*2*wanderJitter,(Random.value-0.5f)*2*wanderJitter,(Random.value-0.5f)*2*wanderJitter);
        if (m_characterSteerings.m_isPlane) {
            randomDisplacement.y = 0;
        }
        //将随机位移加到初始位置
        circleTarget += randomDisplacement;
        //新位置可能不在圆周上，将其投影到圆周上
        circleTarget = wanderRadius * circleTarget.normalized;
        //将计算的值应用到世界坐标
        wanderTarget = m_characterSteerings.m_velocity.normalized * wanderDistance + circleTarget + getOriginPos();

        m_desiredVelocity = (wanderTarget - getOriginPos()).normalized * m_characterSteerings.m_maxSpeed;
        return m_desiredVelocity - m_characterSteerings.m_velocity;
    }
}

