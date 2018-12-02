/*
 * 所有角色共用的建造参数(类似结构体的作用，用public即可)
 */

using UnityEngine;
using System.Collections;

public abstract class ICharacterBuildParam {

    public ICharacter character = null;
    public int characterID = 0;
    public Vector3 spawnPosition = Vector3.zero;
    public ENUM_Weapon weapon = ENUM_Weapon.Null;
}
