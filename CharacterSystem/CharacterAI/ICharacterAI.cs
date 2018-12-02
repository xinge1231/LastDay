/*
 * 角色AI控制器(AI状态(用于决策)拥有者，可通过角色类的各种方法(操控方法，决策方法等)来实现一些通用AI供AI状态调用)
 */
using UnityEngine;
using System.Collections;
using Pathfinding;
using System.Collections.Generic;

public enum ENUM_StateCondition{
    Null = 0,
    SawTarget,
    LostTarget,
    InAttackRange,
    OutAttackRange,
    NoHealth,
}

public enum ENUM_State {
    Null = 0,
    Idle,
    Patrolling, //巡逻
    Chasing, //追逐
    Attacking,
    Dead,
}

public abstract class ICharacterAI{
    protected Dictionary<ENUM_State, IAIState> m_states = new Dictionary<ENUM_State, IAIState>(); //保存所有状态对象。(不用重复产生)
    protected IAIState m_curState = null;
    public IAIState CurState
    {
        get { return m_curState; }
        set { m_curState = value; }
    }
    private ENUM_State m_curStateType = ENUM_State.Null;
    public ENUM_State CurStateType
    {
        get { return m_curStateType; }
        set { m_curStateType = value; }
    }
    private ICharacter m_character = null;
    public ICharacter Character {
        get { return m_character; }
        set { m_character = value; }
    }
    private Vector3 m_oldTargetPos; //上一个目标位置
    public UnityEngine.Vector3 OldTargetPos
    {
        get { return m_oldTargetPos; }
        set { m_oldTargetPos = value; }
    }
    public bool m_targetChanged = false; //目标位置是否有变化
    public bool TargetChanged
    {
        get { return m_targetChanged; }
        set { m_targetChanged = value; }
    }
    //寻路插件相关
    public Seeker m_seeker = null;
    public Path m_path = null;
    public float m_nextWaypointDistance = 3;
    public int m_curWaypoint = 0;
    public float m_calInterval = 0.3f; //计算间隔
    public float m_timer = 0.3f;


    public ICharacterAI(ICharacter character){
        Character = character;
        //寻路插件相关
        m_seeker = Character.getGameObject().GetComponent<Seeker>();
        if (m_seeker != null)
            m_seeker.pathCallback += onPathComplete;
    }

    //子类可以在此定制和初始化状态机(通过设置转换矩阵：各个状态的字典)
    public abstract void init();


    //根据当前状态和转换条件进行状态切换
    public void doTransition(ENUM_StateCondition stateCondition) {
        ENUM_State newStateType = CurState.Transitions[stateCondition];
        CurState = m_states[newStateType];
        CurStateType = newStateType;
    }

    #region 寻路相关

    //开始寻路
    public void startPathFinding() {
        m_timer -= Time.deltaTime;


        if (m_path == null || TargetChanged)
        {
            if (m_timer <= 0)
            {
                OldTargetPos = m_character.TargetPos;
                TargetChanged = false;
                m_timer = m_calInterval;
                m_seeker.StartPath(Character.getPosition(), Character.TargetPos); //计算寻路路径
            }
        }

    }

    //寻路路径计算完毕回调函数
    public void onPathComplete(Path path)
    {
        if (!path.error)
        {
            m_path = path; //必须有路径才能寻路
            m_curWaypoint = 0;
        }
    }

    //执行寻路(要使用寻路的AI状态每物理帧调用一次)
    public void excuteRouting() {
        if (m_path == null) 
            return;

        //表示目标有变，可以重新计算路径
        if (Character.TargetPos!= OldTargetPos)
            TargetChanged = true;

        //到达终点，需要重新计算路径(将原路径置空)
        if (m_curWaypoint >= m_path.vectorPath.Count)
        {
            m_path = null;
            return;
        }
        //角色移动
        Vector3 direction = (m_path.vectorPath[m_curWaypoint] - Character.getPosition()).normalized;
        Character.moveBy(direction * Character.CharacterAttr.MaxSpeed * Time.fixedDeltaTime);
        //角色转向
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        Character.getGameObject().transform.rotation = Quaternion.Slerp(Character.getGameObject().transform.rotation, targetRotation, Time.deltaTime * Character.CharacterAttr.MaxRotateSpeed);
        //如果当前位置与当前路点小于一个给定值，可以转向下一个路点
        if (Vector3.Distance(Character.getPosition(), m_path.vectorPath[m_curWaypoint]) < m_nextWaypointDistance)
        {
            m_curWaypoint++;
            return;
        }
    }

    #endregion

    #region 行为
    //执行攻击
    public virtual void attack()
    {
        Character.attack();
    }

    public virtual void moveTo(Vector3 position)
    {
        Character.moveTo(position);
    }

    //开启操控行为
    public void enableSteering(ENUM_Steering emSteering) {
        Character.Steerings.enableSteering(emSteering);
    }

    //禁用操控行为
    public void disableSteering(ENUM_Steering emSteering) {
        Character.Steerings.disableSteering(emSteering);
    }
    //public virtual void chase() { }
    //public void killed() { }
    #endregion

    #region 判断
    //是否存在当前目标
    public bool hasTarget()
    {
        return Character.Target == null;
    }

    //是否丢失目标
    public bool lostTarget()
    {
        return Character.Target == null;
    }

    //是否进入攻击范围
    public bool enterAttackRange()
    {
        float distance = Vector3.Distance(Character.getPosition(), Character.TargetPos);
        if (distance < Character.Weapon.getAtkRange())
        {
            return true;
        }
        return false;
    }
    #endregion


    public void fixedUpdate() {
        CurState.fixedUpdate();
    }

    public void update() {
        m_timer -= Time.deltaTime;
        CurState.update();
    }



    public void release() {

    }
}
