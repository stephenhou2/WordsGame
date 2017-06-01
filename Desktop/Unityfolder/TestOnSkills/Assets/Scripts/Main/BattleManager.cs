using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

	public Player player;

	public Monster monster;

	public static int gameProcess;

	public Button[] skillButtons;

	public Button attackButton;

	public Button defenceButton;

	public Button[] itemButtons;

	public Slider playerHealth;

	public Slider playerStrength;

	public Slider monsterHealth;

	public Slider monsterStrength;

	public Text description;

	public GameObject controlPlane;

//	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
//	static private void CallBackAfterBattleSceneLoaded(){
//		SceneManager.sceneLoaded += OnSceneLoaded;
//	}
//
//	static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
//	{
//		gameProcess = GameManager.gameManager.gameProcess;
//	}



	// 进入战斗场景时初始化玩家和怪物数据
	public void SetupBattleManager(Player player,int gameProcess){
		
//		player.enemy = monster;
//
//		monster.enemy = player;

		monster.SetupMonster (gameProcess);

	}

	public void PlayerAction(int skillId){

		controlPlane.gameObject.SetActive (true);// 遮罩，下回合开始前用户无法再点击按钮

		Skill playerSkill = player.skills [skillId];

		description.text = "使用了" + playerSkill.skillName;

		playerSkill.AffectAgent (player,monster,playerSkill.skillLevel);

		playerSkill.isAvalible = false;// 使用技能之后该技能暂时进入不可用状态

		player.strength -= playerSkill.strengthConsume;//使用魔法后使用者减去对应的气力消耗

		StartCoroutine ("PlayerAnimation");

	}

	public IEnumerator PlayerAnimation(){
		
		#warning player animation
		yield return new WaitForSeconds (1.0f);//动画暂时由延时1s代替

		UpdateStates ();//更新玩家和怪物状态

		MonsterAction ();//怪物行动
	}


	private void MonsterAction(){

		Debug.Log ("monster action");

		if (monster.validActionType == ValidActionType.None) {
			controlPlane.gameObject.SetActive (false);

			EnterNextTurn ();//进入下一回合
			return;
		}

		monster.ManageSkillAvalibility ();

		#warning 这里添加怪物技能逻辑，选择使用的技能
//		Skill monsterSkill = monster.SkillOfMonster ();
		Skill monsterSkill = monster.skills [0];//怪物技能暂时由普通攻击代替

		monsterSkill.AffectAgent (monster, player, monsterSkill.skillLevel);

		monster.strength -= monsterSkill.strengthConsume;

		monsterSkill.isAvalible = false;

		StartCoroutine ("MonsterAnimation");


	}

	public IEnumerator MonsterAnimation(){

		#warning player animation
		yield return new WaitForSeconds (1.0f);

		UpdateStates ();

		controlPlane.gameObject.SetActive (false);

		EnterNextTurn ();//进入下一回合

	}


	public void EnterNextTurn(){
		
		//进入下一回合，用户选技能之前，先根据各自状态对玩家和怪物产生响应的影响
		for(int i = 0;i<player.states.Count;i++){
			StateSkillEffect sse = player.states [i];
			sse.ManageState (player, monster, sse.skillLevel, TriggerType.None, 0);
		}
		for(int i = 0;i<monster.states.Count;i++){
			StateSkillEffect sse = monster.states [i];
			sse.ManageState (monster, player, sse.skillLevel, TriggerType.None, 0);
		}

		// 根据玩家可采取的行动类型设定技能按钮是否可以交互
		ValidActionForPlayer ();
	}

	public void UpdateStates(){
		
		playerHealth.value = player.health;
		playerStrength.value = player.strength;
		monsterHealth.value = monster.health;
		monsterStrength.value = monster.strength;

		#warning picturs of states toAdd



		if (player.health <= 0) {
			Debug.Log ("you lose");
			OnQuitBattle ();
		} else if (monster.health <= 0) {
			Debug.Log ("you win");
			OnQuitBattle ();
		}



	}

	public void ValidActionForPlayer(){

		switch (player.validActionType) {

		case ValidActionType.All:
			EnableOrDisableButtons (true, true, true, true);
			break;
		case ValidActionType.PhysicalExcption:
			EnableOrDisableButtons (false, true, true, true);
			break;
		case ValidActionType.MagicException:
			EnableOrDisableButtons (true, false, true, true);
			break;
		case ValidActionType.None:
			EnableOrDisableButtons (false, false, false, false);
			break;
		case ValidActionType.PhysicalOnly:
			EnableOrDisableButtons (true, false, false, true);
			break;
		case ValidActionType.MagicOnly:
			EnableOrDisableButtons (false, true, false, true);
			break;
		default:
			break;
		}
		// 如果技能还在冷却中或者玩家气力值小于技能消耗的气力值，则相应按钮不可用
		for (int i = 0;i < player.skills.Count;i++) {
			
			Skill s = player.skills [i];

			// 如果是冷却中的技能
			if (s.isAvalible == false) {
				s.actionCount++;
				Debug.Log ("name" + s.skillName + "acitonCount" + s.actionCount);
				if (s.actionCount >= s.actionConsume) {
					skillButtons [i].interactable = true;
					s.isAvalible = true;
					s.actionCount = 0;
					Debug.Log (s.skillName + "-------------------");
				} else {
					skillButtons [i].interactable = false;
					Debug.Log (s.skillName + "++++++++++++++++++++");
				}
			}
			// 如果不是冷却中的技能
			else {
				skillButtons [i].interactable = player.strength >= s.strengthConsume;
			} 
		}

	}

	//根据玩家状态判断按钮是否可以交互
	private void EnableOrDisableButtons(bool isAttackEnable,bool isSkillEnable,bool isItemEnable,bool isDefenceEnable){
		attackButton.interactable = isAttackEnable;
		defenceButton.interactable = isDefenceEnable;
		foreach (Button btn in skillButtons) {
			btn.interactable = isSkillEnable;
		}
		foreach (Button btn in itemButtons) {
			btn.interactable = isItemEnable;
		}
	}



	public void OnAttack(){
		PlayerAction (3);
	}

	public void OnDenfence(){
		PlayerAction (4);
	}

	public void OnSkill(int skillIndex){
		PlayerAction (skillIndex);
	}

	public void OnItem(int itemIndex){

	}

	public void OnQuitBattle(){
		Debug.Log ("quit to main screen");
	}

	public void OnReset(){
		player.ResetBattleAgentProperties (true);
		monster.ResetBattleAgentProperties (true);


		//*************** for test use ************//
		for (int i = 0; i < player.skills.Count; i++) {
			foreach (BaseSkillEffect bse in player.skills[i].skillEffects) {
				bse.actionCount = 0;
			}
		}

		for (int i = 0; i < player.skills.Count; i++) {
			foreach (BaseSkillEffect bse in player.skills[i].skillEffects) {
				bse.actionCount = 0;
			}
		}
		//*************** for test use ************//
	}

}
