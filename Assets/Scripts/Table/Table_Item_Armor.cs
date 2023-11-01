using System;
using System.Collections.Generic;

public class Table_Item_Armor : Table_Base
{
    [Serializable] 
    public class Info
    {
        public int               m_nID                                                   ;
        public string            m_sName                                                   ;
        public string            m_sIconPath                                                                             ;
        public int               m_iArmorType                                                   ; // 1 - helmet, 2-torso, 3- hand, 4-leg
        public float             m_iDamage_Reduction_Physical                                                   ;
        public float             m_iDamage_Reduction_VSStrike                                                   ;
        public float             m_iDamage_Reduction_VSSlash                                                   ;
        public float             m_iDamage_Reduction_Thrust                                                   ;
        public float             m_iDamage_Reduction_Magic                                                   ;
        public float             m_iDamage_Reduction_Fire                                                   ;
        public float             m_iDamage_Reduction_Lightning                                                   ;
        public float             m_iDamage_Reduction_Dark                                                   ;
        public int               m_iResistance_Bleeding                                                   ;
        public int               m_iResistance_Posion                                                   ;
        public int               m_iResistance_Frost                                                   ;
        public int               m_iResistance_Curse                                                   ;
        public float m_fResistance_Poise                                                   ;
        public int               m_iDurability                                                   ;
        public float               m_fWeight                                                   ;
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

        _reader.get(_nRow, ref _info.m_nID                          );
        _reader.get(_nRow, ref _info.m_sName                        );
        _reader.get(_nRow, ref _info.m_sIconPath                    );
        _reader.get(_nRow, ref _info.m_iArmorType                   );
        _reader.get(_nRow, ref _info.m_iDamage_Reduction_Physical   );
        _reader.get(_nRow, ref _info.m_iDamage_Reduction_VSStrike   );
        _reader.get(_nRow, ref _info.m_iDamage_Reduction_VSSlash    );
        _reader.get(_nRow, ref _info.m_iDamage_Reduction_Thrust     );
        _reader.get(_nRow, ref _info.m_iDamage_Reduction_Magic      );
        _reader.get(_nRow, ref _info.m_iDamage_Reduction_Fire       );
        _reader.get(_nRow, ref _info.m_iDamage_Reduction_Lightning  );
        _reader.get(_nRow, ref _info.m_iDamage_Reduction_Dark       );
        _reader.get(_nRow, ref _info.m_iResistance_Bleeding         );
        _reader.get(_nRow, ref _info.m_iResistance_Posion           );
        _reader.get(_nRow, ref _info.m_iResistance_Frost            );
        _reader.get(_nRow, ref _info.m_iResistance_Curse            );
        _reader.get(_nRow, ref _info.m_fResistance_Poise            );
        _reader.get(_nRow, ref _info.m_iDurability                  );
        _reader.get(_nRow, ref _info.m_fWeight                      );

        return true;
    }
}

