using UnityEngine;
using System.Collections;
using Pathfinding;

public class SoldierBuilder : ICharacterBuilder
{
    private SoldierBuildParam m_soldierBuildParam = null;

    public override void setBuildParam(ICharacterBuildParam characterBuildParam)
    {
        m_soldierBuildParam = characterBuildParam as SoldierBuildParam;
    }

    public override void loadAsset()
    {
        GameObject obj = FactoryManager.getAssetFactory().loadSoldier(m_soldierBuildParam.character.AssetName);
        obj.transform.position = m_soldierBuildParam.spawnPosition;
        obj.name = string.Format("Soldier[{0}]", ++m_soldierBuildParam.characterID);
        m_soldierBuildParam.character.setGameObject(obj);
    }

    public override void setAI()
    {
        m_soldierBuildParam.character.getGameObject().AddComponent<Seeker>();
        m_soldierBuildParam.character.getGameObject().AddComponent<SimpleSmoothModifier>();

        SoldierAI soldierAI = new SoldierAI(m_soldierBuildParam.character);
        //soldierAI.setTarget(m_soldierBuildParam.target);
        soldierAI.init();
        m_soldierBuildParam.character.AI = soldierAI;
    }

 

    public override void setCharacterAttr()
    {
        SoldierAttr soldierAttr = new SoldierAttr();
        soldierAttr.setAttrStragegy(new SoldierAttrStrategy());
        m_soldierBuildParam.character.CharacterAttr = soldierAttr;
    }

    public override void setWeapon()
    {
        ICharacter owner = m_soldierBuildParam.character;
        IWeapon weapon = FactoryManager.getWeaponFactory().createWeapon(m_soldierBuildParam.weapon);
        weapon.setOwner(owner);
        owner.setWeapon(weapon);

        owner.getGameObject().AddComponent<IKController>();//添加IK控制脚本(注意，是添加在拥有Animator的角色上，不是武器上)
        Vector3 weaponPos = UnityTool.findChildGameObject(owner.getGameObject(), "MainWeaponPos").transform.localPosition;
        UnityTool.attach(owner.getGameObject(), weapon.getGameObject(),weaponPos);

    }

    public override void setSteerings()
    {
        ICharacterSteerings steering = new SoldierSteerings();
        steering.Character = m_soldierBuildParam.character;
        steering.initSteerings();
        m_soldierBuildParam.character.Steerings = steering;
    }

    public override void setSensors()
    {
        GameObject obj = m_soldierBuildParam.character.getGameObject();
        SightSensor sightSensor = new SightSensor(obj);
        SoundSensor soundSensor = new SoundSensor(obj);
        GameManager.Instance.registerSensor(sightSensor);
        GameManager.Instance.registerSensor(soundSensor);
    }
}
