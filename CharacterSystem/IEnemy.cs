using UnityEngine;
using System.Collections;

public enum ENUM_Enemy {
    Null = 0,
    Zombie = 1,
    Ethan = 2,
    Max,
}

public abstract class IEnemy : ICharacter
{
    protected ENUM_Enemy m_enemyType = ENUM_Enemy.Null;

}
