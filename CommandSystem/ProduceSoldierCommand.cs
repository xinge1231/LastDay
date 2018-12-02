using UnityEngine;
using System.Collections;

public class ProduceSoldierCommand : ICommand
{
    Vector3 m_spawnPosition = GameObject.Find("SoldierPosition").transform.position;
    //测试目标
    ENUM_Soldier m_soldierType = ENUM_Soldier.Assault;
    ENUM_Weapon m_weaponType = ENUM_Weapon.AK;
    public override void execute()
    {
        int randomX = Random.Range(1, 10);
        int randomZ = Random.Range(1, 10);
        m_spawnPosition += new Vector3(randomX, 0, randomZ);
        ISoldier soldier = FactoryManager.getCharacterFactory().createSoldier(m_soldierType, m_weaponType, m_spawnPosition);
        //setTarget(soldier);
        //setSteerings(soldier);
        GameManager.Instance.addCharacter(soldier);

        //事件通知。用于界面显示敌人数量
        //GameManager.Instance.notifyGameEvent(ENUM_GameEvent.EnemyCount, null);
    }

    public void setTarget(ICharacter character)
    {
        ICharacter m_target = GameManager.Instance.getPlayer();
        character.TargetCharacter = m_target;
    }
    public void setSteerings(ICharacter character)
    {
        //enemey.getSteeringManager().enableSteering(ENUM_Steering.Seek);
        //enemey.getSteeringManager().enableSteering(ENUM_Steering.Flee);
        //character.getSteeringManager().enableSteering(ENUM_Steering.Arrive);
    }
}
