/*
 * 敌人对象的配件安装方法实现
 */
using UnityEngine;
using System.Collections;

public class EnemyBuilder : ICharacterBuilder
{
    private EnemyBuildParam m_enemyBuildParam = null;
    public override void setBuildParam(ICharacterBuildParam characterBuildParam)
    {
        m_enemyBuildParam = characterBuildParam as EnemyBuildParam;
    }

    public override void loadAsset()
    {
        //加载模型资源并创建游戏对象
        GameObject obj = FactoryManager.getAssetFactory().loadEnemy(m_enemyBuildParam.character.AssetName);
        //设置出生地点
        obj.transform.position = m_enemyBuildParam.spawnPosition;
        //设置游戏对象名称
        obj.name = string.Format("Enemy[{0}]", ++EnemyBuildParam.enemyID);
        //设置对象层级
        //obj.layer = LayerMask.NameToLayer("Enemy");
        //将角色类对象和创建的游戏对象对应起来
        m_enemyBuildParam.character.setGameObject(obj);
    }

    public override void setAI()
    {
        EnemyAI enemyAI = new EnemyAI(m_enemyBuildParam.character);
        //enemyAI.setTarget(m_enemyBuildParam.target);
        enemyAI.init();
        m_enemyBuildParam.character.AI = enemyAI;
    }


    public override void setCharacterAttr()
    {
        EnemyAttr enemyAttr = new EnemyAttr();
        enemyAttr.setAttrStragegy(new EnemyAttrStrategy());
        m_enemyBuildParam.character.CharacterAttr = enemyAttr;
    }

    public override void setWeapon()
    {
        //if (m_enemyBuildParam.weapon == ENUM_Weapon.Null)
            //return;
        IWeapon weapon = FactoryManager.getWeaponFactory().createWeapon(m_enemyBuildParam.weapon);
        weapon.setOwner(m_enemyBuildParam.character);
        //UnityTool.attachToRefPos(m_enemyBuildParam.character.getGameObject(), weapon.getGameObject(), "mesh_R_Forearm", Vector3.zero);
        if(m_enemyBuildParam.weapon!=ENUM_Weapon.Fist)
            UnityTool.attach(m_enemyBuildParam.character.getGameObject(), weapon.getGameObject(), Vector3.zero+new Vector3(0,1,0));
        m_enemyBuildParam.character.setWeapon(weapon);
    }

    public override void setSteerings()
    {
        ICharacterSteerings steering = new EnemySteerings();
        steering.Character = m_enemyBuildParam.character;
        steering.initSteerings();
        m_enemyBuildParam.character.Steerings = steering;
    }

    public override void setSensors()
    {
        GameObject obj = m_enemyBuildParam.character.getGameObject();
        SightSensor sightSensor = new SightSensor(obj);
        SoundSensor soundSensor = new SoundSensor(obj);
        GameManager.Instance.registerSensor(sightSensor);
        GameManager.Instance.registerSensor(soundSensor);
    }
}
