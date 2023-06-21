using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelemtModelChanger : MonoBehaviour
{
    public List<GameObject> helmetModels;

    private void Awake()
    {
        GetAllHelemtModels();
    }

    private void GetAllHelemtModels()
    {
        int childrenGameObjects = transform.childCount;

        for (int i = 0; i < childrenGameObjects; i++)
        {
            helmetModels.Add(transform.GetChild(i).gameObject);
        }

    }

    public void UnEquipAllHelmetModels()
    {
        foreach (GameObject helmetModel in helmetModels)
        {
            helmetModel.SetActive(false);
        }
    }

    public void EquipHelmetModelByName(string helmetName)
    {
        foreach (GameObject helmetModel in helmetModels)
        {
            if(helmetModel.name ==  helmetName)
            {
                helmetModel.SetActive(true);
            }
        }
    }
}
