/*
 * 查找UI对象
 */
using UnityEngine;
using System.Collections;

public class UITool{

    private static GameObject m_canvasObj = null;

    //寻找Canvas下的指定UI界面对象
    public static GameObject findUIGameObject(string UIName)
    {
        if (m_canvasObj == null)
            //保存当前画布，避免重新搜索
            m_canvasObj = UnityTool.findGameObject("Canvas");
        if (m_canvasObj == null) //当前场景无Canvas
            return null;
        return UnityTool.findChildGameObject(m_canvasObj, UIName);
    }

    //获取某个UI对象下的指定UI对象的UI组件
    public static T getUIComponent<T>(GameObject container, string UIName)where T : UnityEngine.Component
    {
        GameObject childGameObject = UnityTool.findChildGameObject(container, UIName);
        if (childGameObject == null)
            return null;
        T tempObj = childGameObject.GetComponent<T> ();
        if (tempObj == null)
            return null;
        return tempObj;
    }
}
