using UnityEngine;
using System.Collections;

public enum ENUM_Soldier{
    Null = 0,
    Assault = 1,
    Sniper = 2,
    Medic = 3,
    BombMaster = 4,
    Scout = 5,
    Cover = 6,
    Captain = 7,
    Max,
}
public abstract class ISoldier : ICharacter{
    protected ENUM_Soldier m_emSoldier = ENUM_Soldier.Null;

    public ENUM_Soldier getSoldierType() {
        return m_emSoldier;
    }

}
