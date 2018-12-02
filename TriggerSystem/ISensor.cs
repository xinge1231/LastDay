using UnityEngine;
using System.Collections;

public abstract class ISensor {

    //protected TriggerManager m_triggerManager = null;
    protected ENUM_TriggerType m_triggerType;//当前感知器所感知的触发器类型
    public ENUM_TriggerType TriggerType
    {
        get { return m_triggerType; }
        set { m_triggerType = value; }
    }
    protected GameObject m_gameObject; //当前感知器附着的游戏对象
    public UnityEngine.GameObject GameObject
    {
        get { return m_gameObject; }
        set { m_gameObject = value; }
    }
    protected Vector3 m_position;
    public UnityEngine.Vector3 Position
    {
        get { return m_position; }
        set { m_position = value; }
    }
    public ISensor(GameObject gameobject) {
        m_gameObject = gameobject;
    }

    //感知到触发器后的行动(被触发器调用)
    public virtual void callback(ITrigger trigger) { }

	// Update is called once per frame
	void Update () {
	
	}
}
