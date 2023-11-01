using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class CharacterPreviewCamera : MonoBehaviour
{
    RectTransform myImg;

    Vector3 changePos;
    Vector3 m_StandCharacterPreviewPos = new Vector3(1.25f, 1.46f, -3.68f);
    Vector3 m_HairZoomPreviewPos = new Vector3(1.25f, 1.7f, -3.9f);
    Vector3 m_AllZoomPreviewPos = new Vector3(1.25f, 0.87f, -2.87f);

    Vector3 m_offPosSet = Vector3.zero;
    Vector3 m_offRotSet = Vector3.zero;


    public bool m_isGameScene = false;
    PlayerManager m_playerManager;

    private void Awake()
    {
        myImg = GetComponent<RectTransform>();
        if(m_isGameScene)
        {

        }
        else // Lobby
        {
            ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera.None);
        }
    }

    private void Start()
    {
        if (m_isGameScene)
        {
            m_playerManager = Managers.Object.m_MyPlayer;

            m_offPosSet = m_playerManager.transform.position - transform.position;
            m_offRotSet = m_playerManager.transform.eulerAngles - transform.eulerAngles;
        }
    }

    public void LateUpdate()
    {
        if (m_isGameScene)
        {
            // 항상 캐릭터의 정면을 바라보게

            transform.position = m_playerManager.transform.position - m_offPosSet;
            transform.eulerAngles = m_playerManager.transform.eulerAngles - m_offRotSet;

        }
    }


    public void ChangeCameraPreviewTransform(E_CharacterCreationPreviewCamera type)
    {
        changePos = m_StandCharacterPreviewPos;

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
            case E_CharacterCreationPreviewCamera.All:
                changePos = m_AllZoomPreviewPos;
                break;
            default:
                break;
        }

        StopAllCoroutines();
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
