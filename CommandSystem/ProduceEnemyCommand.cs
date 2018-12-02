using UnityEngine;
using System.Collections;

public class ProduceEnemyCommand : ICommand
{
    Vector3 m_spawnPosition = GameObject.Find("EnemyPosition").transform.position;
    //测试目标
    ENUM_Enemy m_enemyType = ENUM_Enemy.Zombie;
    ENUM_Weapon m_weaponType = ENUM_Weapon.Null;
    public override void execute()
    {
        int randomX = Random.Range(1, 10);
        int randomY = Random.Range(0, 180);
        int randomZ = Random.Range(1, 10);
        m_spawnPosition += new Vector3(randomX, 0, randomZ);
        IEnemy enemey = FactoryManager.getCharacterFactory().createEnemy(m_enemyType, m_weaponType, m_spawnPosition);
        setTargetCharacter(enemey);
        setSteerings(enemey);
        enemey.getGameObject().transform.Rotate(new Vector3(0, randomY, 0));
        GameManager.Instance.addCharacter(enemey);

        //事件通知。用于界面显示敌人数量
        GameManager.Instance.notifyGameEvent(ENUM_GameEvent.EnemyCount, null);
    }
    //为传入的角色设置目标
    public void setTargetCharacter(ICharacter character) {
        //ICharacter m_target = GameManager.Instance.findSoldierByGameObject(GameObject.Find("Player[1]").GetInstanceID());
        ICharacter m_target = GameManager.Instance.getPlayer();
        character.TargetCharacter = m_target;
    }
    //为传入的角色开启指定操控行为
    public void setSteerings(ICharacter character) {
        //character.Steerings.enableSteering(ENUM_Steering.Seek);
        //character.Steerings.enableSteering(ENUM_Steering.Flee);
        //character.Steerings.enableSteering(ENUM_Steering.Arrive);
        character.Steerings.enableSteering(ENUM_Steering.Pursuit);
        //character.Steerings.enableSteering(ENUM_Steering.Evade);
        //character.Steerings.enableSteering(ENUM_Steering.Wander);
        //character.Steerings.enableSteering(ENUM_Steering.FollowPath);
        character.Steerings.enableSteering(ENUM_Steering.CollisionAvoidance);
        character.Steerings.enableSteering(ENUM_Steering.Separation);
        character.Steerings.enableSteering(ENUM_Steering.Alignment);
        character.Steerings.enableSteering(ENUM_Steering.Cohesion);

        character.Steerings.setWeight(ENUM_Steering.Pursuit, 3);
        character.Steerings.setWeight(ENUM_Steering.Separation, 1);
        character.Steerings.setWeight(ENUM_Steering.CollisionAvoidance, 2);
    }
}
