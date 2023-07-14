using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_LevelUp : UI_Popup
{
	enum Texts
	{
		CurrentPlayerLevelText,
		ProjectedPlayerLevelText ,
		CurrentSoulsText			  ,
		SoulsRequiredToLevelUpText,
		CurrentHealthLevelText		  ,
		ProjectedHealthLevelText	  ,
		CurrentStaminaLevelText		  ,
		ProjectedStaminaLevelText	  ,
		CurrentFocusLevelText		  ,
		ProjectedFocusLevelText		  ,
		CurrentHealthPoiseLevelText	  ,
		ProjectedHealthPoiseLevelText ,
		CurrentStrengthLevelText	  ,
		ProjectedStrengthLevelText	  ,
		CurrentDexterityLevelText	  ,
		ProjectedDexterityLevelText	  ,
		CurrentFaithLevelText		  ,
		ProjectedFaithLevelText		  ,
		CurrentIntelligenceLevelText  ,
		ProjectedIntelligenceLevelText,
    }

    enum GameObjects
    { 
		HealthSlider,
		StaminaSlider,
		FocusSlider,
		HealthPoiseSlider,
		StrengthSlider,
		DexteritySlider,
		FaithSlider,
		IntelligenceSlider,
	}


	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		BindText(typeof(Texts));
		BindObject(typeof(GameObjects));



		return true;
	}
}
