/*
 * 某个角色的操控行为管理类
 * 
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public abstract class ICharacterSteerings{

    protected ICharacter m_character = null;
    public ICharacter Character
    {
        get
        {
            return m_character;
        }

        set
        {
            m_character = value;
        }
    }


    //雷达
    public List<GameObject> neighbors = new List<GameObject>();
    private Collider[] colliders;
    private float checkTimer = 0;
    private float checkInterval = 0.3f;
    private float checkRadius = 50.0f;
    private LayerMask checkedLayer = 1<<9;

    //AI操控属性
    public Dictionary<ENUM_Steering, ISteering> m_steerings;
    public float m_maxSpeed = 5;//最大速度
    public float m_maxForce = 100;//可施加的最大力
    protected float m_sqrMaxSpeed; //最大速度的平方，预先计算存储，节省资源
    public float m_mass = 1; //质量
    public Vector3 m_velocity; //速度
    public float m_damping = 1.9f; //转向速度
    public float m_computeInterval = 0.2f; //操控力计算间隔(为了性能，不需要每帧更新)
    public bool m_isPlane = true; //是否在二维平面。(如果是，计算距离时忽略y值)
    private Vector3 m_steeringForce; //计算的操控力
    protected Vector3 m_acceleration; //计算的加速度
    private float m_timer = 0;//计时器

    public ICharacterSteerings()
    {
        m_sqrMaxSpeed = m_maxSpeed * m_maxSpeed;
    }

    //初始化操控行为
    public void initSteerings() {

        m_steerings = new Dictionary<ENUM_Steering, ISteering>();
        m_steeringForce = new Vector3(0, 0, 0);
        foreach (ENUM_Steering emSteering in Enum.GetValues(typeof(ENUM_Steering)))
        {
            switch (emSteering)
            {
                case ENUM_Steering.Seek:
                    m_steerings.Add(emSteering, new SteeringSeek(this));
                    break;
                case ENUM_Steering.Flee:
                    m_steerings.Add(emSteering, new SteeringFlee(this));
                    break;
                case ENUM_Steering.Arrive:
                    m_steerings.Add(emSteering, new SteeringArrive(this));
                    break;
                case ENUM_Steering.Pursuit:
                    m_steerings.Add(emSteering, new SteeringPursuit(this));
                    break;
                case ENUM_Steering.Evade:
                    m_steerings.Add(emSteering, new SteeringEvade(this));
                    break;
                case ENUM_Steering.Wander:
                    m_steerings.Add(emSteering, new SteeringWander(this));
                    break;
                case ENUM_Steering.FollowPath:
                    m_steerings.Add(emSteering, new SteeringFollowPath(this));
                    break;
                case ENUM_Steering.CollisionAvoidance:
                    m_steerings.Add(emSteering, new SteeringCollisionAvoidance(this));
                    break;
                case ENUM_Steering.Separation:
                    m_steerings.Add(emSteering, new SteeringSeparation(this));
                    break;
                case ENUM_Steering.Alignment:
                    m_steerings.Add(emSteering, new SteeringAlignment(this));
                    break;
                case ENUM_Steering.Cohesion:
                    m_steerings.Add(emSteering, new SteeringCohesion(this));
                    break;
                default:
                    Debug.Log("不存在该操控行为"+ emSteering.ToString());
                    break;
            }
        }
    }


    //启用指定操控行为
    public void enableSteering(ENUM_Steering emSteering)
    {
        m_steerings[emSteering].IsEnable = true;
    }

    //禁用指定操控行为
    public void disableSteering(ENUM_Steering emSteering)
    {
        m_steerings[emSteering].IsEnable = false;
    }

    //设置操控行为权重
    public void setWeight(ENUM_Steering emSteering, float weight) {
        m_steerings[emSteering].Weight = weight;
    }
    //操控行为物理更新
    public void fixedUpdate()
    {

        m_velocity += m_acceleration * Time.fixedDeltaTime;
        if (m_velocity.sqrMagnitude > m_sqrMaxSpeed)
            m_velocity = m_velocity.normalized * m_maxSpeed;

        Vector3 moveDistance;
        moveDistance = m_velocity * Time.fixedDeltaTime;
        if (m_isPlane)
        {
            m_velocity.y = 0;
            moveDistance.y = 0;
        }
        Rigidbody rigidbody = m_character.getRigidbody();
        Transform transform = m_character.getGameObject().transform;
        if (rigidbody == null || rigidbody.isKinematic) //如果没有rigidbody或rigidbody以动力学方式控制运动
        {
            transform.position += moveDistance;
        }
        else
        { //通过rigidbody控制移动(自动插值平滑)
            rigidbody.MovePosition(rigidbody.position + moveDistance);
        }

        //如果速度大于一个阈值，更新朝向 (为了防止抖动)
        if (m_velocity.sqrMagnitude > 0.00001)
        {
            //通过当前朝向与速度方向的插值计算新朝向
            Vector3 newForward = Vector3.Slerp(transform.forward, m_velocity, m_damping * Time.deltaTime);
            if (m_isPlane)
                newForward.y = 0;
            transform.forward = newForward;
        }

    }


    //操控行为逻辑更新
    public void update()
    {
        checkTimer += Time.deltaTime;
        m_timer += Time.deltaTime;
        //指定间隔进行雷达探测
        if (checkTimer > checkInterval) {
            neighbors.Clear();
            //Debug.DrawLine(m_character.getPosition(), m_character.getPosition()+new Vector3(checkRadius,0,0));
            //查找当前角色附近的所有碰撞体（只检测checkedLayer中指定层的碰撞体,同时忽略trigger（否则会返回多个碰撞体））
            colliders = Physics.OverlapSphere(m_character.getPosition(),checkRadius,checkedLayer,QueryTriggerInteraction.Ignore);
           
            foreach (Collider item in colliders) {
                if (item.gameObject != m_character.getGameObject())
                    neighbors.Add(item.gameObject);
            }
            checkTimer = 0;
        }
        
        //每隔指定间隔计算一次总操控力
        if (m_timer > m_computeInterval)
        {
            m_steeringForce = new Vector3(0, 0, 0);
            //遍历各操控行为，并累加操控力（加权）
            foreach (ENUM_Steering steering in m_steerings.Keys)
            {
                if (m_steerings[steering].IsEnable)
                {
                    m_steeringForce += m_steerings[steering].force()*m_steerings[steering].Weight;
                }
            }
            //使总操控力不大于最大值(截断)
            m_steeringForce = Vector3.ClampMagnitude(m_steeringForce, m_maxForce);



            //求加速度
            m_acceleration = m_steeringForce / m_mass;

            m_timer = 0;
        }
    }

}
