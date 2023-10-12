﻿using System;
using System.Collections.Generic;

public class Table_Boss : Table_Base
{
    [Serializable] 
    public class Info
    {
        public int    m_nID;
        public string m_sName;
        public int    m_iClass;
        public int    m_iRewardGold;
        public int    m_iRewardItem;
        public string m_sPrefabPath;
        public int    m_iAI;
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
        _reader.get(_nRow, ref _info.m_sName);
        _reader.get(_nRow, ref _info.m_iClass);
        _reader.get(_nRow, ref _info.m_iRewardGold);
        _reader.get(_nRow, ref _info.m_iRewardItem);
        _reader.get(_nRow, ref _info.m_sPrefabPath);
        _reader.get(_nRow, ref _info.m_iAI);

        return true;                 
    }
}

