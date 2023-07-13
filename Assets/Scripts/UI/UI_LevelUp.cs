using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_LevelUp : UI_Popup
{
	public PlayerStatsManager playerStatsManager;

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

	Slider HealthSlider		 ;
	Slider StaminaSlider	 ;
	Slider FocusSlider		 ;
	Slider HealthPoiseSlider ;
	Slider StrengthSlider	 ;
	Slider DexteritySlider	 ;
	Slider FaithSlider		 ;
	Slider IntelligenceSlider;

	public override bool Init()
	{
		if (base.Init() == false)
			return false;

		BindText(typeof(Texts));
		BindObject(typeof(GameObjects));



		return true;
	}

	// 플레이어의 현재 상태를 업데이트
    private void OnEnable()
    {
		GetText((int)Texts.CurrentPlayerLevelText).text = playerStatsManager.  playerLvel.		 ToString();
		GetText((int)Texts.CurrentHealthLevelText      ).text = playerStatsManager.healthLevel      .ToString();
		GetText((int)Texts.CurrentStaminaLevelText     ).text = playerStatsManager.staminaLevel     .ToString();
		GetText((int)Texts.CurrentFocusLevelText       ).text = playerStatsManager.focusLevel       .ToString();
		GetText((int)Texts.CurrentHealthPoiseLevelText       ).text = playerStatsManager.poiseLevel       .ToString();
		GetText((int)Texts.CurrentStrengthLevelText    ).text = playerStatsManager.strengthLevel    .ToString();
		GetText((int)Texts.CurrentDexterityLevelText   ).text = playerStatsManager.dexterityLevel   .ToString();
		GetText((int)Texts.CurrentIntelligenceLevelText).text = playerStatsManager.intelligenceLevel.ToString();
		GetText((int)Texts.CurrentFaithLevelText      ).text = playerStatsManager.faithLevel.       ToString();

		HealthSlider       = GetObject((int) GameObjects.HealthSlider).GetComponent<Slider>();
		StaminaSlider	  = GetObject((int) GameObjects.StaminaSlider	 ).GetComponent<Slider>();
		FocusSlider		  = GetObject((int) GameObjects.FocusSlider		 ).GetComponent<Slider>();
		HealthPoiseSlider  = GetObject((int) GameObjects.HealthPoiseSlider ).GetComponent<Slider>();
		StrengthSlider	  = GetObject((int) GameObjects.StrengthSlider	 ).GetComponent<Slider>();
		DexteritySlider    = GetObject((int) GameObjects.DexteritySlider   ).GetComponent<Slider>();
		FaithSlider		  = GetObject((int) GameObjects.FaithSlider		 ).GetComponent<Slider>();
		IntelligenceSlider = GetObject((int) GameObjects.IntelligenceSlider).GetComponent<Slider>();

		HealthSlider	   .value       = playerStatsManager.	healthLevel;
		StaminaSlider	   .value = playerStatsManager.			staminaLevel     ;
		FocusSlider		   .value = playerStatsManager.			focusLevel       ;
		HealthPoiseSlider  .value = playerStatsManager.			poiseLevel       ;
		StrengthSlider	   .value = playerStatsManager.			strengthLevel    ;
		DexteritySlider	   .value = playerStatsManager .		dexterityLevel   ;
		FaithSlider		   .value = playerStatsManager.			intelligenceLevel;
		IntelligenceSlider.value = playerStatsManager.			faithLevel;

		HealthSlider	   .maxValue =99;
		StaminaSlider	   .maxValue =99;
		FocusSlider		   .maxValue =99;
		HealthPoiseSlider  .maxValue =99;
		StrengthSlider	   .maxValue =99;
		DexteritySlider	   .maxValue =99;
		FaithSlider		   .maxValue =99;
		IntelligenceSlider.maxValue = 99;

		HealthSlider	   .minValue       = playerStatsManager.healthLevel;
		StaminaSlider	   .minValue = playerStatsManager.staminaLevel     ;
		FocusSlider		   .minValue = playerStatsManager.focusLevel       ;
		HealthPoiseSlider  .minValue = playerStatsManager.poiseLevel       ;
		StrengthSlider	   .minValue = playerStatsManager.strengthLevel    ;
		DexteritySlider	   .minValue = playerStatsManager .dexterityLevel   ;
		FaithSlider		   .minValue = playerStatsManager.intelligenceLevel;
		IntelligenceSlider.minValue = playerStatsManager.faithLevel;


		GetText((int)Texts.ProjectedPlayerLevelText).text = playerStatsManager.playerLvel.ToString();
		GetText((int)Texts.ProjectedHealthLevelText).text = playerStatsManager.healthLevel.ToString();
		GetText((int)Texts.ProjectedStaminaLevelText).text = playerStatsManager.staminaLevel.ToString();
		GetText((int)Texts.ProjectedFocusLevelText).text = playerStatsManager.focusLevel.ToString();
		GetText((int)Texts.ProjectedHealthPoiseLevelText).text = playerStatsManager.poiseLevel.ToString();
		GetText((int)Texts.ProjectedStrengthLevelText).text = playerStatsManager.strengthLevel.ToString();
		GetText((int)Texts.ProjectedDexterityLevelText).text = playerStatsManager.dexterityLevel.ToString();
		GetText((int)Texts.ProjectedIntelligenceLevelText).text = playerStatsManager.intelligenceLevel.ToString();
		GetText((int)Texts.ProjectedFaithLevelText).text = playerStatsManager.faithLevel.ToString();
	}

	// 플레이어의 다음 스탯 총합을 업데이트, 모든 수치를 더 함으로써
	private void UpdateProjectedPlayerLevel()
    {
		TextMeshProUGUI ProjectedPlayerLevelText = GetText((int) Texts.ProjectedPlayerLevelText);

		ProjectedPlayerLevelText = GetText((int)Texts.CurrentPlayerLevelText);
		ProjectedPlayerLevelText.text += Mathf.RoundToInt(HealthSlider.value - playerStatsManager.healthLevel);
		ProjectedPlayerLevelText.text += Mathf.RoundToInt(StaminaSlider	  .value - playerStatsManager.staminaLevel     );
		ProjectedPlayerLevelText.text += Mathf.RoundToInt(FocusSlider		  .value - playerStatsManager.focusLevel       );
		ProjectedPlayerLevelText.text += Mathf.RoundToInt(HealthPoiseSlider .value - playerStatsManager.poiseLevel       );
		ProjectedPlayerLevelText.text += Mathf.RoundToInt(StrengthSlider	  .value - playerStatsManager.strengthLevel    );
		ProjectedPlayerLevelText.text += Mathf.RoundToInt(DexteritySlider   .value - playerStatsManager.dexterityLevel   );
		ProjectedPlayerLevelText.text += Mathf.RoundToInt(FaithSlider		  .value - playerStatsManager.intelligenceLevel);
		ProjectedPlayerLevelText.text += Mathf.RoundToInt(IntelligenceSlider.value - playerStatsManager.faithLevel);

		GetText((int)Texts.ProjectedPlayerLevelText).text = ProjectedPlayerLevelText.text;
	}

	public void UpdateHealthLevelSlider()
    {
		GetText((int)Texts.ProjectedHealthLevelText).text = HealthSlider.value.ToString();
		UpdateProjectedPlayerLevel();
	}
}
