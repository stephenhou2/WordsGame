using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {


	public static GameManager gameManager;

	public BattleManager battleManager;

	[HideInInspector]public BattleManager m_battleManager;

	public Player player;

	public int gameProcess;

	public Transform container;

	public SkillGenerator skillGenerator;

	[HideInInspector]public EffectData[] effectsData;

	void Awake(){

		if (gameManager == null) {
			gameManager = this;
		} else if (gameManager != this) {
			Destroy (gameObject);
		}

		player = Player.mainPlayer;

		DontDestroyOnLoad (gameObject);
	}

	//初始化战斗
	public void OnEnterBattle(){

		m_battleManager = Instantiate (battleManager, this.transform);

		m_battleManager.SetupBattleManager (player, gameProcess);

//		battleManager.SetupBattleManager (player, gameProcess);

		SetupBattleScene ();

	}

	// 初始化生成技能的界面
	public void OnGenerateSkill(){
		if (effectsData.Length == 0) {
			effectsData = DataInitializer.LoadEffectsWithPath (CommonData.effectsFilePath, CommonData.effectsFileName);
		}
		skillGenerator.SetupSkillGenerator (effectsData);
	}

	// 使用effectName生成特定效果，如果effectName==null，则生成所有状态类效果
	public StateSkillEffect[] GenerateStates(string effectName){
		if (effectsData.Length == 0) {
			effectsData = DataInitializer.LoadEffectsWithPath (CommonData.effectsFilePath, CommonData.effectsFileName);
		}
		return skillGenerator.GenerateStateSkillEffects (effectsData,effectName);
	}


	//初始化战斗场景
	public void SetupBattleScene(){
		Debug.Log ("进入了战斗场景");
	}

	public void OnSelectEffects(int id_1,int id_2){
		skillGenerator.GenerateSkillWithIds (id_1, id_2,player);
	}

	public void OnConfirm(){
		Debug.Log (player);
	}

	public void OnQuit(){
		skillGenerator.QuitSkillGenerate ();
	}


}
