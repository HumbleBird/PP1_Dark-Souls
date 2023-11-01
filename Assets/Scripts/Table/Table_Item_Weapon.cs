using System;
using System.Collections.Generic;

public class Table_Item_Weapon : Table_Base
{
    [Serializable] 
    public class Info
    {
        public int              m_nID;
        public string           m_sName;
        public string           m_sIconPath                          ;
        public string           m_sPrefabPath                          ;
        public int              m_iWeaponType                          ;
        public string           m_sAttackType                          ;
        public string           m_sSkill                                      ;
        public int              m_iCostFP                                      ;
        public int              m_iRequirement_Strength                                 ;
        public int              m_iRequirement_Dexterity                                 ;
        public int              m_iRequirement_Intelligence                                 ;
        public int              m_iRequirement_Faith                                 ;
        public int              m_iParameter_Bonus_Strength                                 ;
        public int              m_iParameter_Bonus_Dexterity                                 ;
        public int              m_iParameter_Bonus_Intelligence                     ;
        public int              m_iParameter_Bonus_Faith                                ;
        public int              m_iAttack_Physical                                ;
        public int              m_iAttack_Magic                                           ;
        public int              m_iAttack_Fire                                           ;
        public int              m_iAttack_Lightning                                           ;
        public int              m_iAttack_Dark                                           ;
        public int              m_iAttack_Critical                                           ;
        public int              m_iAttack_Spell_Buff                                           ;
        public int              m_iAttack_Range                                           ;
        public float            m_iDamage_Reduction_Physical                     ;
        public float            m_iDamage_Reduction_Magic                     ;
        public float            m_iDamage_Reduction_Fire                     ;
        public float            m_iDamage_Reduction_Lightning                     ;
        public float            m_iDamage_Reduction_Dark                     ;

        public int              m_iAuxiliary_Effects_Bleeding                ;
        public int              m_iAuxiliary_Effects_Posion                  ;
        public int              m_iAuxiliary_Effects_Frost                   ;
        public int              m_iAuxiliary_Effects_Curse                   ;
        public int              m_iDurability                            ;
        public float            m_fWeight                            ;
        public int              m_iStability                            ;
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
        _reader.get(_nRow, ref _info.m_sIconPath                     );
        _reader.get(_nRow, ref _info.m_sPrefabPath                   );
        _reader.get(_nRow, ref _info.m_iWeaponType                   );
        _reader.get(_nRow, ref _info.m_sAttackType                   );
        _reader.get(_nRow, ref _info.m_sSkill                        );
        _reader.get(_nRow, ref _info.m_iCostFP                       );
        _reader.get(_nRow, ref _info.m_iRequirement_Strength         );
        _reader.get(_nRow, ref _info.m_iRequirement_Dexterity        );
        _reader.get(_nRow, ref _info.m_iRequirement_Intelligence     );
        _reader.get(_nRow, ref _info.m_iRequirement_Faith            );
        _reader.get(_nRow, ref _info.m_iParameter_Bonus_Strength     );
        _reader.get(_nRow, ref _info.m_iParameter_Bonus_Dexterity    );
        _reader.get(_nRow, ref _info.m_iParameter_Bonus_Intelligence );
        _reader.get(_nRow, ref _info.m_iParameter_Bonus_Faith        );
        _reader.get(_nRow, ref _info.m_iAttack_Physical              );
        _reader.get(_nRow, ref _info.m_iAttack_Magic                 );
        _reader.get(_nRow, ref _info.m_iAttack_Fire                  );
        _reader.get(_nRow, ref _info.m_iAttack_Lightning             );
        _reader.get(_nRow, ref _info.m_iAttack_Dark                  );
        _reader.get(_nRow, ref _info.m_iAttack_Critical              );
        _reader.get(_nRow, ref _info.m_iAttack_Spell_Buff            );
        _reader.get(_nRow, ref _info.m_iAttack_Range                 );
        _reader.get(_nRow, ref _info.m_iDamage_Reduction_Physical    );
        _reader.get(_nRow, ref _info.m_iDamage_Reduction_Magic       );
        _reader.get(_nRow, ref _info.m_iDamage_Reduction_Fire        );
        _reader.get(_nRow, ref _info.m_iDamage_Reduction_Lightning   );
        _reader.get(_nRow, ref _info.m_iDamage_Reduction_Dark        );

        _reader.get(_nRow, ref _info.m_iAuxiliary_Effects_Bleeding   );
        _reader.get(_nRow, ref _info.m_iAuxiliary_Effects_Posion     );
        _reader.get(_nRow, ref _info.m_iAuxiliary_Effects_Frost      );
        _reader.get(_nRow, ref _info.m_iAuxiliary_Effects_Curse      );
        _reader.get(_nRow, ref _info.m_iDurability                   );
        _reader.get(_nRow, ref _info.m_fWeight                       );
        _reader.get(_nRow, ref _info.m_iStability                    );

        return true;
    }
}

