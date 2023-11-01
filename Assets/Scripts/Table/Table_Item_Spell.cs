using System;
using System.Collections.Generic;

public class Table_Item_Spell : Table_Base
{
    [Serializable] 
    public class Info
    {
        public int               m_iID                                      ;
        public string            m_sName                                                   ;
        public string            m_sIconPath                                                                             ;
        public string            m_sItem_Decription                                                                             ;
        public int               m_iSpell_Type                                                   ;
        public int               m_iCost_FP                                                   ;
        public int               m_iSlots                                                   ;
        public int               m_iRequirment_Intelligence                                                   ;
        public int               m_iRequirment_Faith                                                   ;
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

            m_Dictionary.Add(info.m_iID, info);
        }

        return;
    }

    protected bool Read(CSVReader _reader, Info _info, int _nRow, int _nStartCol)
    {
        if (_reader.reset_row(_nRow, _nStartCol) == false)
            return false;

        _reader.get(_nRow, ref _info.m_iID                     );
        _reader.get(_nRow, ref _info.m_sName                   );
        _reader.get(_nRow, ref _info.m_sIconPath               );
        _reader.get(_nRow, ref _info.m_sItem_Decription        );
        _reader.get(_nRow, ref _info.m_iSpell_Type             );
        _reader.get(_nRow, ref _info.m_iCost_FP                );
        _reader.get(_nRow, ref _info.m_iSlots                  );
        _reader.get(_nRow, ref _info.m_iRequirment_Intelligence);
        _reader.get(_nRow, ref _info.m_iRequirment_Faith       );

        return true;
    }
}

