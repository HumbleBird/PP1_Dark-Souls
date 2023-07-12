using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TableManager 
{


    public void Init()
    {
#if UNITY_EDITOR

#else
        m_Camera.Init_Binary("Camera");
#endif
    }

    public void Save()
    {


#if UNITY_EDITOR
        AssetDatabase.Refresh();
#endif
    }

    public void Clear()
    {

    }
}
