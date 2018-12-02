using UnityEngine;
using System.Collections;

public class SoundTrigger : ITriggerLimitedLife
{
    public SoundTrigger(GameObject gameObject) : base(gameObject)
    {
        m_triggerType = ENUM_TriggerType.Sound;
    }

    public override bool isTouch(ISensor sensor)
    {
        Debug.DrawLine(m_GameObject.transform.position, m_GameObject.transform.position+new Vector3(0,0,Radius));

        if (Vector3.Distance(m_GameObject.transform.position, sensor.GameObject.transform.position) < Radius) {
            return true;
        }
        return false;
    }

}
