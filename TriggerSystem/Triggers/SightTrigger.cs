using UnityEngine;
using System.Collections;

public class SightTrigger : ITrigger {
    public SightTrigger(GameObject gameObject) : base(gameObject)
    {
        m_triggerType = ENUM_TriggerType.Sight;
    }

    public override bool isTouch(ISensor sensor)
    {
        if (sensor.TriggerType != ENUM_TriggerType.Sight)
            return false;
        
        RaycastHit hitInfo;
        Vector3 rayDirection =  GameObject.transform.position - sensor.GameObject.transform.position;//注意头尾方向
        rayDirection.y = 0; //这里只考虑二维？
        
        float viewField = Vector3.Angle(rayDirection, sensor.GameObject.transform.forward);
        //感知器位置到触发器位置的方向与感知器正前方的夹角 小于感知器的视域时
        if (viewField < (sensor as SightSensor).ViewFiled){
            //如果在视域范围内，检测是否有障碍物(起始位置加一个y轴向量，表示眼睛位置),方向为感知器到触发器的方向
            if (Physics.Raycast(sensor.GameObject.transform.position + new Vector3(0, 1, 0), rayDirection, out hitInfo, (sensor as SightSensor).ViewDistance))
            {
                //如果是触发器所在的对象，说明可以看到
                if (hitInfo.collider.gameObject == GameObject)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
