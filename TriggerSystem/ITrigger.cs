using UnityEngine;
using System.Collections;


public abstract class ITrigger {

    //protected TriggerManager m_triggerManager = null;
    protected GameObject m_GameObject = null; //触发器附着的对象
    public UnityEngine.GameObject GameObject
    {
        get { return m_GameObject; }
        set { m_GameObject = value; }
    }
    //protected Vector3 m_position; //触发器位置
    protected float m_radius = 0; //触发器触发半径(范围)
    public float Radius
    {
        get { return m_radius; }
        set { m_radius = value; }
    }
    protected bool m_toBeRemoved = false; //当前触发器是否可被移除
    public bool BeRemoved
    {
        get { return m_toBeRemoved; }
        set { m_toBeRemoved = value; }
    }
    protected ENUM_TriggerType m_triggerType =ENUM_TriggerType.Null;
    public ENUM_TriggerType TriggerType
    {
        get { return m_triggerType; }
        set { m_triggerType = value; }
    }


    public ITrigger(GameObject gameObject) {
        //m_triggerManager = triggerManager;
        GameObject = gameObject;
    }

    //当前触发器能被指定感知器感知到，通知感知器采取行动
    public virtual void notify(ISensor sensor){
        if (isTouch(sensor))
            sensor.callback(this);
    }
    //测试当前触发器能否被指定感知器感知到(判断类型、距离等)
    public virtual bool  isTouch(ISensor sensor) {
        return false;
    }

    //更新触发器内部状态（例如剩余时间等）
    public virtual void update() { }


}
