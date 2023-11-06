﻿using System;
using System.Collections.Generic;

public class Table_StartClassStat : Table_Base
{
    [Serializable] 
    public class Info
    {
        public int               m_nID                   ;
        public string            m_sClass               ;
        public int               m_iPlayerLevel             ;
        public int               m_iVigorLevel              ;
        public int               m_iAttunementLevel             ;
        public int               m_iEnduranceLevel              ;
        public int               m_iVitalityLevel               ;
        public int               m_iStrengthLevel               ;
        public int               m_iDexterityLevel              ;
        public int               m_iIntelligenceLevel           ;
        public int               m_iFaithLevel          ;
        public int               m_iLuckLevel           ;

        public int               m_iLeftHand1Id             ;
        public int               m_iLeftHand2Id         ;
        public int               m_iLeftHand3Id             ;
        public int               m_iRightHand1Id            ;
        public int               m_iRightHand2Id            ;
        public int               m_iRightHand3Id            ;
        public int               m_iHeadArmorId             ;
        public int               m_iChestArmorId            ;
        public int               m_iHandArmorId             ;
        public int               m_iLegArmorId              ;
        public int               m_iSpell1Id                ;
        public int               m_iSpell2Id                ;
        public int               m_iRindId                  ;
        public int               m_iToolItemId              ;

        public string            m_sClassDescrition             ;
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

        _reader.get(_nRow, ref _info.m_nID               );
        _reader.get(_nRow, ref _info.m_sClass            );
        _reader.get(_nRow, ref _info.m_iPlayerLevel      );
        _reader.get(_nRow, ref _info.m_iVigorLevel       );
        _reader.get(_nRow, ref _info.m_iAttunementLevel  );
        _reader.get(_nRow, ref _info.m_iEnduranceLevel   );
        _reader.get(_nRow, ref _info.m_iVitalityLevel    );
        _reader.get(_nRow, ref _info.m_iStrengthLevel    );
        _reader.get(_nRow, ref _info.m_iDexterityLevel   );
        _reader.get(_nRow, ref _info.m_iIntelligenceLevel);
        _reader.get(_nRow, ref _info.m_iFaithLevel       );
        _reader.get(_nRow, ref _info.m_iLuckLevel        );

        _reader.get(_nRow, ref _info.m_iLeftHand1Id      );
        _reader.get(_nRow, ref _info.m_iLeftHand2Id      );
        _reader.get(_nRow, ref _info.m_iLeftHand3Id      );
        _reader.get(_nRow, ref _info.m_iRightHand1Id     );
        _reader.get(_nRow, ref _info.m_iRightHand2Id     );
        _reader.get(_nRow, ref _info.m_iRightHand3Id     );
        _reader.get(_nRow, ref _info.m_iHeadArmorId      );
        _reader.get(_nRow, ref _info.m_iChestArmorId     );
        _reader.get(_nRow, ref _info.m_iHandArmorId      );
        _reader.get(_nRow, ref _info.m_iLegArmorId       );
        _reader.get(_nRow, ref _info.m_iSpell1Id         );
        _reader.get(_nRow, ref _info.m_iSpell2Id         );
        _reader.get(_nRow, ref _info.m_iRindId           );
        _reader.get(_nRow, ref _info.m_iToolItemId       );

        _reader.get(_nRow, ref _info.m_sClassDescrition  );

        return true;
    }
}

