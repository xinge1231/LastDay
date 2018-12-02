/*
 * 角色工厂
 * 功能：创建角色对象
 */
using UnityEngine;
using System.Collections;

public class CharacterFactory : ICharacterFactory
{
    //建造者系统
    private CharacterBuilderSystem m_characterBuilderSystem = new CharacterBuilderSystem(GameManager.Instance);

    public override IEnemy createEnemy(ENUM_Enemy emEnemy, ENUM_Weapon emWeapon, Vector3 spawnPosition)
    {
        //建造参数
        EnemyBuildParam buildParam = new EnemyBuildParam();
        switch (emEnemy) {
            case ENUM_Enemy.Zombie:
                buildParam.character = new EnemyZombie();
                break;
            default:
                Debug.Log("不存在该Enemy类型");
                return null;
        }
        buildParam.weapon = emWeapon;
        buildParam.spawnPosition = spawnPosition;

        EnemyBuilder enemyBuilder = new EnemyBuilder();
        enemyBuilder.setBuildParam(buildParam);
        m_characterBuilderSystem.construct(enemyBuilder);

        return buildParam.character as IEnemy;
    }

    public override ISoldier createSoldier(ENUM_Soldier emSoldier, ENUM_Weapon emWeapon, Vector3 spawnPosition)
    {
        SoldierBuildParam buildParam = new SoldierBuildParam();
        switch (emSoldier) {
            case ENUM_Soldier.Assault:
                buildParam.character = new SoldierAssault(); 
                break;
            default:
                Debug.Log("不存在该Soldier类型");
                return null;
        }
        buildParam.weapon = emWeapon;
        buildParam.spawnPosition = spawnPosition;

        SoldierBuilder soldierBuilder = new SoldierBuilder();
        soldierBuilder.setBuildParam(buildParam);
        m_characterBuilderSystem.construct(soldierBuilder);

        return buildParam.character as ISoldier;
    }

    public override IPlayer createPlayer(ENUM_Player emPlayer, ENUM_Weapon emWeapon, Vector3 spawnPosition) {
        PlayerBuildParam buildParam = new PlayerBuildParam();
        switch (emPlayer) {
            case ENUM_Player.PlayerAssault:
                buildParam.character = new PlayerAssault();
                break;
            default:
                Debug.Log("不存在该Player类型");
                return null;
        }
        buildParam.weapon = emWeapon;
        buildParam.spawnPosition = spawnPosition;
        PlayerBuilder playerBuilder = new PlayerBuilder();
        playerBuilder.setBuildParam(buildParam);
        m_characterBuilderSystem.construct(playerBuilder);
        return buildParam.character as IPlayer;
    }
}
