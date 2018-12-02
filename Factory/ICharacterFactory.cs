using UnityEngine;
using System.Collections;

public abstract class ICharacterFactory{
    public abstract ISoldier createSoldier(ENUM_Soldier emSoldier, ENUM_Weapon emWeapon, Vector3 spawnPosition);
    public abstract IEnemy createEnemy(ENUM_Enemy emEnemy,ENUM_Weapon emWeapon,Vector3 spawnPosition);
    public abstract IPlayer createPlayer(ENUM_Player emPlayer,ENUM_Weapon emWeapon,Vector3 spawnPosition);
}
