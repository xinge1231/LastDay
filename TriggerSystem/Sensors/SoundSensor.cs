/*
 * 角色的听觉感知器
 * 
 */
using UnityEngine;
using System.Collections;

public class SoundSensor : ISensor
{
    private float m_hearDistance = 50.0f;
    public SoundSensor(GameObject gameobject) : base(gameobject)
    {
        m_triggerType = ENUM_TriggerType.Sound;
    }

    public override void callback(ITrigger trigger)
    {
        Debug.Log("callback");
        ICharacter character = GameManager.Instance.findCharacterByGameObject(m_gameObject);
        //character.TargetCharacter = GameManager.Instance.findCharacterByGameObject(trigger.GameObject);
        character.Target = trigger.GameObject;

        Debug.DrawLine(m_gameObject.transform.position, trigger.GameObject.transform.position, Color.red);
    }
}
