using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ClassStats 
{
    public string m_sPlayerClass;
    public int m_sClassLevel;

    [TextArea]
    public string m_sClassDecription;

    [Header("Class Stats")]
    public int m_iVigorLevel;
    public int m_iAttunementLevel;
    public int m_iEnduranceLevel;
    public int m_iVitalityLevel;
    public int m_iStrengthLevel;
    public int m_iDexterityLevel;
    public int m_iIntelligenceLevel;
    public int m_iFaithLevel;
    public int m_iLuckLevel;
}
