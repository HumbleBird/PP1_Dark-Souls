using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CharacterPreviewCamera : MonoBehaviour
{
    RectTransform myImg;

    Vector3 m_StandCharacterPreviewPos = new Vector3(1.25f, 1.46f, -3.68f);
    Vector3 m_HairZoomPreviewPos = new Vector3(1.25f, 1.7f, -3.9f);

    private void Awake()
    {
        myImg = GetComponent<RectTransform>();

        ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
    }

    public void ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera type)
    {
        Vector3 changePos = m_StandCharacterPreviewPos;

        switch (type)
        {
            case E_CharacterCreationPreviewCamera.None:
                changePos = m_StandCharacterPreviewPos;
                break;
            case E_CharacterCreationPreviewCamera.Head:
                changePos = m_HairZoomPreviewPos;
                break;
            case E_CharacterCreationPreviewCamera.Chest:
                break;
            case E_CharacterCreationPreviewCamera.Leg:
                break;
            case E_CharacterCreationPreviewCamera.Hand:
                break;
            case E_CharacterCreationPreviewCamera.Back:
                break;
            default:
                break;
        }

        StartCoroutine(IChangeCameraPreviewTransform(changePos));
    }

    IEnumerator IChangeCameraPreviewTransform(Vector3 pos)
    {
        while (true)
        {
            myImg.localPosition = Vector3.Lerp(myImg.localPosition, pos, Time.deltaTime);

            if(Vector3.Distance(myImg.localPosition, pos) <= 0.01)
            {
                myImg.localPosition = pos;

                yield break;
            }

            yield return null;
        }

    }
}
