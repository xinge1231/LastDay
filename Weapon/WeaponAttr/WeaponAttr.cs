using UnityEngine;
using System.Collections;

public enum ENUM_Weapon
{
    Null = 0,

    //近战武器
    Fist = 1,
    Claw = 2,
    
    //远程武器
    AK = 11,
    MachineGun = 12,

    Max,
}

public class WeaponAttr{
    private int atkPower = 0;
    private int atkRange = 0;
    private ENUM_Weapon weaponType = ENUM_Weapon.Null;



    public WeaponAttr(int atkPower, int atkRange, ENUM_Weapon emWeapon) {
        AtkPower = atkPower;
        AtkRange = atkRange;
        WeaponType = emWeapon;
    }

    public int AtkPower { get { return atkPower; } set { atkPower = value; } }
    public int AtkRange { get { return atkRange; } set { atkRange = value; } }
    public ENUM_Weapon WeaponType { get {return weaponType; } set { weaponType = value; } }
}
