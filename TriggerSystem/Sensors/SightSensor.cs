/*
 * 
 * 角色的视觉感知器
 * 
 * 
 */

using UnityEngine;
using System.Collections;

public class SightSensor : ISensor {
    private float m_viewFiled = 45.0f; //视域角度
    public float ViewFiled
    {
        get { return m_viewFiled; }
        set { m_viewFiled = value; }
    }
    private float m_ViewDistance = 50.0f; //视野范围
    public float ViewDistance
    {
        get { return m_ViewDistance; }
        set { m_ViewDistance = value; }
    }
    public SightSensor(GameObject gameobject) : base(gameobject)
    {
        TriggerType = ENUM_TriggerType.Sight;
    }

    public override void callback(ITrigger trigger)
    {
        ICharacter character = GameManager.Instance.findCharacterByGameObject(m_gameObject);

        //character.moveTo(trigger.GameObject.transform.position);
        //character.TargetCharacter = GameManager.Instance.findCharacterByGameObject(trigger.GameObject);
        character.Target = trigger.GameObject;


        Debug.DrawLine(m_gameObject.transform.position, trigger.GameObject.transform.position, Color.red);
        
    }

}
