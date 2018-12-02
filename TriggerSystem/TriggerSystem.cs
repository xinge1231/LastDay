using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ENUM_TriggerType
{
    Null = 0,
    Sight,
    Sound,
    Health,
}

public class TriggerSystem : IGameSystem {

    //private List<ITrigger> m_triggers = new List<ITrigger>();
    //private List<ISensor> m_sensors = new List<ISensor>();

    //存储各个类型的触发器列表(同一种类型触发器可能有多个)
    private Dictionary<ENUM_TriggerType, List<ITrigger>> m_triggers = new Dictionary<ENUM_TriggerType, List<ITrigger>>();
    //存储各个类型的感知器列表
    private Dictionary<ENUM_TriggerType, List<ISensor>> m_sensors = new Dictionary<ENUM_TriggerType, List<ISensor>>();

    //存储需要移除的感知器，例如角色死亡时
    //private List<ITrigger> m_triggersToRemove = new List<ITrigger>();
    //存储需要移除的触发器，例如触发器超时时
    //private List<ISensor> m_sensorsToRemove = new List<ISensor>();

    public TriggerSystem(GameManager gameManager) : base(gameManager)
    {
    }

    //注册触发器
    public void registerTrigger(ITrigger trigger) {
        if (!m_triggers.ContainsKey(trigger.TriggerType))
        {
            m_triggers.Add(trigger.TriggerType, new List<ITrigger>());
            
        }

        m_triggers[trigger.TriggerType].Add(trigger);
    }

    //注册感知器
    public void registerSensor(ISensor sensor) {
        if (!m_sensors.ContainsKey(sensor.TriggerType))
        {
            m_sensors.Add(sensor.TriggerType, new List<ISensor>());
        }
        m_sensors[sensor.TriggerType].Add(sensor);
    }

    //更新或移除触发器
    public void updateTriggers() {
        List<ITrigger> triggersToRemove = new List<ITrigger>();

        foreach (List<ITrigger> triggerList in m_triggers.Values) {
            foreach (ITrigger trigger in triggerList) {
                if (trigger.BeRemoved)
                {
                    triggersToRemove.Add(trigger);
                }
                else {
                    trigger.update();
                }
            }
        }

        foreach (ITrigger trigger in triggersToRemove) {
            m_triggers[trigger.TriggerType].Remove(trigger);
        }

        
    }

    public void tryTriggers() {
        Debug.Log(m_triggers.Count);
        List<ISensor> sensorsToRemove = new List<ISensor>();
        foreach (ENUM_TriggerType triggerType in m_triggers.Keys) {
            if (m_sensors.ContainsKey(triggerType)) {
                foreach (ISensor sensor in m_sensors[triggerType])
                {
                    if (sensor.GameObject != null) //例如角色死亡时
                    {
                        foreach (ITrigger trigger in m_triggers[triggerType])
                            trigger.notify(sensor);
                    }
                    else {
                        sensorsToRemove.Add(sensor);
                    }
                }

            }
        }

        foreach (ISensor sensor in sensorsToRemove) {
            m_sensors[sensor.TriggerType].Remove(sensor);
        }
    }

	
	public override void update () {
        updateTriggers();
        tryTriggers();
	}
}
