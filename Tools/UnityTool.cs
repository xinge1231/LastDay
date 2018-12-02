/*
 * 查找游戏对象(利用Transform类的分层管理)
 */
using UnityEngine;
using System.Collections;

public class UnityTool{

    //寻找当前场景中的对象(只查找第一层)
    public static GameObject findGameObject(string gameObjectName) {
        GameObject obj = GameObject.Find(gameObjectName);
        if (obj == null) {
            Debug.LogWarning("场景中找不到[" + gameObjectName + "]对象");
            return null;
        }
        return obj;
    }

    //查找指定游戏对象下的指定子对象
    public static GameObject findChildGameObject(GameObject container, string gameObjectName) {
        if (container == null) {
            Debug.LogError("父对象为null，无法查找子对象");
            return null;
        }

        Transform transform= null;
        if (container.name == gameObjectName)
        {
            transform = container.transform;
        }
        else {
            Transform[] children = container.transform.GetComponentsInChildren<Transform>();
            foreach (Transform child in children) {
                if (child.name == gameObjectName) {
                    if (transform == null)
                        transform = child;
                    else {
                        Debug.LogWarning("游戏对象[" + container.name + "]下找到重复的组件名称[" + gameObjectName + "]!");
                    }
                }
            }
        }
        if (transform == null) {
            Debug.LogError("游戏对象[" + container.name + "]下找不到子对象[" + gameObjectName + "]");
            return null;
        }
        return transform.gameObject;
    }

    // 将对象(子)附加到另一个对象(父)的指定位置
    public static void attach(GameObject parentObj, GameObject childObj, Vector3 pos)
    {
        childObj.transform.parent = parentObj.transform;
        childObj.transform.localPosition = pos;
    }

    // 将对象(子)附加到另一个对象(爷)下的指定名称的子对象下(父)的指定位置
    public static void attachToRefPos(GameObject parentObj, GameObject childObj, string refPointName, Vector3 pos)
    {
        //在爷对象下搜索指定父对象
        bool bFinded = false;
        Transform[] allChildren = parentObj.transform.GetComponentsInChildren<Transform>();
        Transform refTransform = null;
        foreach (Transform child in allChildren)
        {
            if (child.name == refPointName)
            {
                if (bFinded == true)
                {
                    Debug.LogWarning("物件[" + parentObj.transform.name + "]有重复参考点[" + refPointName + "]");
                    continue;
                }
                bFinded = true;
                refTransform = child;
            }
        }

        //  是否找到
        if (bFinded == false)
        {
            Debug.LogWarning("物件[" + parentObj.transform.name + "]內不存在参考点[" + refPointName + "]");
            attach(parentObj, childObj, pos);
            return;
        }

        childObj.transform.parent = refTransform;
        childObj.transform.localPosition = pos;
        childObj.transform.localScale = Vector3.one;
        childObj.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
