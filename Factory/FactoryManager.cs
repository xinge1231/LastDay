using UnityEngine;
using System.Collections;

public static class FactoryManager{

    private static ICharacterFactory m_characterFactory = null;
    private static IAssetFactory m_assetFactory = null;
    private static IWeaponFactory m_weaponFactory = null;

    public static IAssetFactory getAssetFactory() {
        if (m_assetFactory == null)
            m_assetFactory = new ResourceAssetFactory();
        return m_assetFactory;
    }

    public static ICharacterFactory getCharacterFactory() {
        if (m_characterFactory == null)
            m_characterFactory = new CharacterFactory();
        return m_characterFactory;
    }

    public static IWeaponFactory getWeaponFactory() {
        if (m_weaponFactory == null)
            m_weaponFactory = new WeaponFactory();
        return m_weaponFactory;
    }
}
