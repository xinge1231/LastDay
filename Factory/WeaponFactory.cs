using UnityEngine;
using System.Collections;

public class WeaponFactory : IWeaponFactory
{
    public override IWeapon createWeapon(ENUM_Weapon emWeapon)
    {
        IWeapon weapon = null;
        switch (emWeapon) {
            case ENUM_Weapon.Fist:
                weapon = new WeaponFist();
                weapon.setWeaponAttr(new WeaponAttr(50, 10, ENUM_Weapon.Fist));
                return weapon;
            case ENUM_Weapon.AK:
                weapon = new WeaponAK();
                weapon.setWeaponAttr(new WeaponAttr(500,50,ENUM_Weapon.AK));
                break;
            default:
                return null;
        }
        weapon.setGameObject(FactoryManager.getAssetFactory().loadWeapon(weapon.getAssetName()));
        weapon.setupEffect();
        return weapon;
    }
}
