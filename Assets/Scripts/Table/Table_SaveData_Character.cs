using System;
using System.Collections.Generic;

public class Table_SaveData_Character : Table_Base
{
    [Serializable] 
    public class Info
    {
        public int m_nID;

        public string m_sCharacterName;
        public int m_iPlayerLevel;
        public int m_iVigorLevel;
        public int m_iAttunementLevel;
        public int m_iEnduranceLevel;
        public int m_iVitalityLevel;
        public int m_iStrengthLevel;
        public int m_iDexterityLevel;
        public int m_iIntelligenceLevel;
        public int m_iFaithLevel;
        public int m_iLuckLevel;

        // Left Weapon
        public int m_iLeftHand1Id;
        public int m_iLeftHand2Id;
        public int m_iLeftHand3Id;

        // Right Weapon
        public int m_iRightHand1Id;
        public int m_iRightHand2Id;
        public int m_iRightHand3Id;

        // Arrow Ammo
        public int m_iArrow1ID;
        public int m_iArrow2ID;

        // Bolt
        public int m_iBolt1ID;
        public int m_iBolt2ID;

        // Armor
        public int m_iHeadArmorId;
        public int m_iChestArmorId;
        public int m_iHandArmorId;
        public int m_iLegArmorId;

        // Spell
        public int m_iSpellId;

        // Ring
        public int m_iRing1Id;
        public int m_iRing2Id;
        public int m_iRing3Id;
        public int m_iRing4Id;

        // Tool
        public int m_iToolItemId1 ;
        public int m_iToolItemId2 ;
        public int m_iToolItemId3 ;
        public int m_iToolItemId4 ;
        public int m_iToolItemId5 ;
        public int m_iToolItemId6 ;
        public int m_iToolItemId7 ;
        public int m_iToolItemId8 ;
        public int m_iToolItemId9 ;
        public int m_iToolItemId10;

        // Pledge
        public int m_iCurrentPledgeID;

        // Pos
        public float xPosition                                        ;
        public float yPosition                                        ;
        public float zPosition                                        ;

        // index
        public int   m_iCurrentRightWeaponIndex                                        ;
        public int   m_iCurrentLeftWeaponIndex                                        ;
        public int   m_iCurrentAmmoArrowIndex                                        ;
        public int   m_iCurrentAmmoBoltIndex                                        ;
        public int   m_iCurrentConsumableItemndex                                        ;
    }

    public Dictionary<int, Info> m_Dictionary = new Dictionary<int, Info>();

    public Info Get(int _nID)
    {
        if (m_Dictionary.ContainsKey(_nID))
            return m_Dictionary[_nID];

        return null;
    }

    public void Init_Binary(string _strName)// 파일 읽어오기
    {
        Load_Binary<Dictionary<int, Info>>(_strName, ref m_Dictionary);
    }

    public void Save_Binary(string _strName) // 파일 만들기
    {
        Save_Binary(_strName, m_Dictionary);
    }

    public void Init_CSV(string _strName, int _nStartRow, int _nStartCol)
    {
        CSVReader reader = GetCSVReader(_strName);

        for(int row = _nStartRow; row < reader.row; ++row)
        {
            Info info = new Info();

            if (Read(reader, info, row, _nStartCol) == false)
                break;

            m_Dictionary.Add(info.m_nID, info);
        }

        return;
    }

    protected bool Read(CSVReader _reader, Info _info, int _nRow, int _nStartCol)
    {
        if (_reader.reset_row(_nRow, _nStartCol) == false)
            return false;

        _reader.get(_nRow, ref _info.m_nID);

        _reader.get(_nRow, ref _info.m_sCharacterName);

        _reader.get(_nRow, ref _info.m_iPlayerLevel);
        _reader.get(_nRow, ref _info.m_iVigorLevel);
        _reader.get(_nRow, ref _info.m_iAttunementLevel);
        _reader.get(_nRow, ref _info.m_iEnduranceLevel);
        _reader.get(_nRow, ref _info.m_iVitalityLevel);
        _reader.get(_nRow, ref _info.m_iStrengthLevel);
        _reader.get(_nRow, ref _info.m_iDexterityLevel);
        _reader.get(_nRow, ref _info.m_iIntelligenceLevel);
        _reader.get(_nRow, ref _info.m_iFaithLevel);
        _reader.get(_nRow, ref _info.m_iLuckLevel);

        _reader.get(_nRow, ref _info.m_iLeftHand1Id);
        _reader.get(_nRow, ref _info.m_iLeftHand2Id);
        _reader.get(_nRow, ref _info.m_iLeftHand3Id);

        _reader.get(_nRow, ref _info.m_iRightHand1Id);
        _reader.get(_nRow, ref _info.m_iRightHand2Id);
        _reader.get(_nRow, ref _info.m_iRightHand3Id);

        _reader.get(_nRow, ref _info.m_iArrow1ID);
        _reader.get(_nRow, ref _info.m_iArrow2ID);
        _reader.get(_nRow, ref _info.m_iBolt1ID);
        _reader.get(_nRow, ref _info.m_iBolt2ID);


        _reader.get(_nRow, ref _info.m_iHeadArmorId);
        _reader.get(_nRow, ref _info.m_iChestArmorId);
        _reader.get(_nRow, ref _info.m_iHandArmorId);
        _reader.get(_nRow, ref _info.m_iLegArmorId);

        _reader.get(_nRow, ref _info.m_iSpellId);

        _reader.get(_nRow, ref _info.m_iRing1Id);
        _reader.get(_nRow, ref _info.m_iRing2Id);
        _reader.get(_nRow, ref _info.m_iRing3Id);
        _reader.get(_nRow, ref _info.m_iRing4Id);

        _reader.get(_nRow, ref _info.m_iToolItemId1 );
        _reader.get(_nRow, ref _info.m_iToolItemId2 );
        _reader.get(_nRow, ref _info.m_iToolItemId3 );
        _reader.get(_nRow, ref _info.m_iToolItemId4 );
        _reader.get(_nRow, ref _info.m_iToolItemId5 );
        _reader.get(_nRow, ref _info.m_iToolItemId6 );
        _reader.get(_nRow, ref _info.m_iToolItemId7 );
        _reader.get(_nRow, ref _info.m_iToolItemId8 );
        _reader.get(_nRow, ref _info.m_iToolItemId9 );
        _reader.get(_nRow, ref _info.m_iToolItemId10);

        _reader.get(_nRow, ref _info.xPosition                   );
        _reader.get(_nRow, ref _info.yPosition                   );
        _reader.get(_nRow, ref _info.zPosition                   );

        _reader.get(_nRow, ref _info.m_iCurrentRightWeaponIndex  );
        _reader.get(_nRow, ref _info.m_iCurrentLeftWeaponIndex   );
        _reader.get(_nRow, ref _info.m_iCurrentAmmoArrowIndex    );
        _reader.get(_nRow, ref _info.m_iCurrentAmmoBoltIndex     );
        _reader.get(_nRow, ref _info.m_iCurrentConsumableItemndex);

        return true;
    }
}

