using UnityEngine;
using System.Collections;

public abstract class IWeaponFactory{
    public abstract IWeapon createWeapon(ENUM_Weapon emWeapon);

}
