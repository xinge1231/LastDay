using UnityEngine;
using System.Collections;

public class SoldierAssault : ISoldier {

    public SoldierAssault() {
        m_emSoldier = ENUM_Soldier.Assault;
        m_assetName = "SoldierAssault";
    }
}
