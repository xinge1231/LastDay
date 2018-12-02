using UnityEngine;
using System.Collections;

public class PlayerBuilder : ICharacterBuilder {
    private PlayerBuildParam m_playerBuildParam = null;

    public override void setBuildParam(ICharacterBuildParam characterBuildParam)
    {
        m_playerBuildParam = characterBuildParam as PlayerBuildParam;
    }

    public override void loadAsset()
    {
        GameObject obj = FactoryManager.getAssetFactory().loadSoldier(m_playerBuildParam.character.AssetName);
        obj.transform.position = m_playerBuildParam.spawnPosition;
        obj.name = string.Format("Player[{0}]", ++PlayerBuildParam.soldierID);
        obj.layer = LayerMask.NameToLayer("Player");
        m_playerBuildParam.character.setGameObject(obj);

        //m_playerBuildParam.character.getRigidbody().isKinematic = true;
    }

    public override void setAI()
    {
        
    }


    public override void setCharacterAttr()
    {
        PlayerAttr playerAttr = new PlayerAttr();
        playerAttr.setAttrStragegy(new PlayerAttrStrategy());
        m_playerBuildParam.character.CharacterAttr = playerAttr;
    }

    public override void setWeapon()
    {
        if (m_playerBuildParam.weapon == ENUM_Weapon.Null)
            return;
        ICharacter owner = m_playerBuildParam.character;
        IWeapon weapon = FactoryManager.getWeaponFactory().createWeapon(m_playerBuildParam.weapon);
        weapon.setOwner(owner);
        owner.setWeapon(weapon);

        owner.getGameObject().AddComponent<IKController>();//添加IK控制脚本(注意，是添加在拥有Animator的角色上，不是武器上)
        Vector3 weaponPos = UnityTool.findChildGameObject(owner.getGameObject(), "MainWeaponPos").transform.localPosition;
        UnityTool.attach(owner.getGameObject(), weapon.getGameObject(), weaponPos);

    }

    public override void setSteerings()
    {
        //ICharacterSteerings steering = new PlayerSteerings();
        //steering.setCharacter(m_playerBuildParam.character);
        //steering.initSteerings();
        //m_playerBuildParam.character.setSteering(steering);
    }

    public override void setSensors()
    {
        SightTrigger sightTrigger = new SightTrigger(m_playerBuildParam.character.getGameObject());
        //SoundTrigger soundTrigger = new SoundTrigger(m_playerBuildParam.character.getGameObject());
        //trigger.Radius = 20.0f;
        GameManager.Instance.registerTrigger(sightTrigger);

    }
}
