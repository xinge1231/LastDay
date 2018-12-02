using UnityEngine;
using System.Collections;

public class PlayerAssault : IPlayer{

    public PlayerAssault() {
        m_playerType = ENUM_Player.PlayerAssault;
        m_assetName = "PlayerAssault";
    }
}
