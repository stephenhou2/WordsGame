using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

	private Player player;

	public Monster monsterModel;

	private Monster monster;

	public int selectedSkillIndex;

	public bool isPlayerTurn;

	public Button[] skillButtons;

	public Button attackButton;

	public Button defenceButton;

	public Button[] itemButtons;


	// 进入战斗场景时初始化玩家和怪物数据
	public void SetupBattleManager(Player player,int gameProcess){
		
		this.player = player;

		Debug.Log (player);

		monster = Instantiate (monsterModel, this.transform);

		monster.SetupMonster (gameProcess);

	}

	public void UseSkill(){

		Debug.Log (player);
		Debug.Log (player.skills);

		Skill playerSkill = player.skills [selectedSkillIndex];

		playerSkill.AffectAgent (player,monster,playerSkill.skillLevel,false);


		MonsterAction ();

		//回合结束判断下回合玩家可以采取的行动类型
		ValidActionForPlayer ();


		//进入下一回合，用户选技能之前，先根据各自状态对玩家和怪物产生响应的影响
		for(int i = 0;i<player.states.Count;i++){
			StateSkillEffect sse = player.states [i];
			sse.ManageState (player, monster, playerSkill.skillLevel, false, TriggerType.None, 0);
		}
		for(int i = 0;i<monster.states.Count;i++){
			StateSkillEffect sse = player.states [i];
			sse.ManageState (monster, player, playerSkill.skillLevel, false, TriggerType.None, 0);
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
			s.actionCount++;
			if (s.actionCount >= s.actionConsume && player.strength >= s.strengthConsume) {
				skillButtons [i].enabled = true;
			} else {
				skillButtons [i].enabled = false;
			}
		}
		
		#warning 武器消耗气力值的判断还没有写 
		//如果玩家气力值小于普通攻击消耗的气力值，攻击按钮不可用

	}
		
	//根据玩家状态判断按钮是否可以交互
	private void EnableOrDisableButtons(bool isAttackEnable,bool isSkillEnable,bool isItemEnable,bool isDefenceEnable){
		attackButton.enabled = isAttackEnable;
		defenceButton.enabled = isDefenceEnable;
		foreach (Button btn in skillButtons) {
			btn.enabled = isSkillEnable;
		}
		foreach (Button btn in itemButtons) {
			btn.enabled = isItemEnable;
		}
	}

	private void MonsterAction(){

//		Skill monsterSkill = monster.SkillOfMonster(monster);
//
//		monsterSkill.AffectAgent (monster, player, monsterSkill.skillLevel, false);
	}


	//按钮点击响应
	public void OnSkillButtonClick(Transform trans,int skillIndex){

		trans.GetComponent<Button> ().enabled = false;
		
		selectedSkillIndex = skillIndex;

		UseSkill ();

	}



}
