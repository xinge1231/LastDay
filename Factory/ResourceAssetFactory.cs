/*
 * 资源工厂(Resource方式)
 */
using UnityEngine;
using System.Collections;

public class ResourceAssetFactory : IAssetFactory{
    public const string soldierPath = "Soldiers/";
    public const string enemyPath = "Enemys/";
    public const string weaponPath = "Weapons/";
    public const string effectPath = "Effects/";
    public const string audioPath = "Audios/";
    public const string spritePath = "Sprites/";

    public override GameObject loadSoldier(string assetName) {
        return instantiateGameObject(soldierPath + assetName);
    }

    public override GameObject loadEnemy(string assetName)
    {
        return instantiateGameObject(enemyPath + assetName);
    }

    public override GameObject loadWeapon(string assetName)
    {
        return instantiateGameObject(weaponPath + assetName);
    }

    public override GameObject loadEffect(string assetName)
    {
        return instantiateGameObject(effectPath + assetName);
    }

    public override AudioClip loadAudioClip(string assetName)
    {
        Object res = Resources.Load(audioPath + assetName) ;
        if (res == null)
            return null;
        return res as AudioClip;
    }

    public override Sprite loadSprite(string assetName)
    {
        Object res = Resources.Load(spritePath + assetName);
        if (res == null)
            return null;
        return res as Sprite;
    }

    //根据资源路径创建游戏对象
    private GameObject instantiateGameObject(string assetPath) {
        //得到资源
        Object obj = Resources.Load(assetPath);
        if (obj == null)
        {
            Debug.LogWarning("无法加载路径[" + assetPath + "]!");
            return null;
        }
        //通过资源创建游戏对象
        return Object.Instantiate(obj) as GameObject;
    }
}
