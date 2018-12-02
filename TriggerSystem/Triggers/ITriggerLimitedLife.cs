/*
 * 有持续时间的触发器
 * 
 */
using UnityEngine;
using System.Collections;

public abstract class ITriggerLimitedLife : ITrigger
{
    private float m_lifeTime = 100.0f; //剩余时间
    public float LifeTime
    {
        get { return m_lifeTime; }
        set { m_lifeTime = value; }
    }
    public ITriggerLimitedLife(GameObject gameObject) : base(gameObject)
    {
    }

    public override void update()
    {
        if (m_lifeTime <= 0) {
            m_toBeRemoved = true;
        }
    }
}
