/*
 * 资源工厂接口(子类工厂可以是不同形式加在资源的工厂)
 */

using UnityEngine;
using System.Collections;

public abstract class IAssetFactory {


    public abstract GameObject loadSoldier(string AssetName);

    public abstract GameObject loadEnemy(string AssetName);

    public abstract GameObject loadWeapon(string AssetName);

    public abstract GameObject loadEffect(string AssetName);

    public abstract AudioClip loadAudioClip(string ClipName);

    public abstract Sprite loadSprite(string SpriteName);
}
