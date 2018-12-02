using UnityEngine;
using System.Collections;

public class ProducePlayerCommand : ICommand
{
    private static int m_playerCount = 0;
    Vector3 m_spawnPosition = GameObject.Find("PlayerPosition").transform.position;
    ENUM_Player m_playerType = ENUM_Player.PlayerAssault;
    ENUM_Weapon m_weaponType = ENUM_Weapon.AK;
    public override void execute()
    {
        if (m_playerCount > 0)
            return;
        int randomX = Random.Range(1, 10);
        int randomZ = Random.Range(1, 10);
        m_spawnPosition += new Vector3(randomX, 0, randomZ);
        IPlayer player = FactoryManager.getCharacterFactory().createPlayer(m_playerType, m_weaponType, m_spawnPosition);
        GameManager.Instance.setPlayer(player);
    }
}
