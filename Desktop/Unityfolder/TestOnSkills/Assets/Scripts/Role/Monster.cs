using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : BattleAgent {

	public int monsterId;



	public void SetupMonster(int gameProcess){
//		Skill skill_1 = GameManager.gameManager.skillGenerator.GenerateSkillWithIds (2, 20),skillsContainer.transform);
	}
		
	//怪物的技能选择
	public Skill SkillOfMonster(Monster monster){
		Skill monsterSkill = null;
		switch (monster.validActionType) {
		case ValidActionType.All:
			
			break;
		case ValidActionType.PhysicalExcption:
			
			break;
		case ValidActionType.MagicException:
			
			break;
		case ValidActionType.None:
			
			break;
		case ValidActionType.PhysicalOnly:
			
			break;
		case ValidActionType.MagicOnly:
			
			break;
		default:
			break;
		}

		return monsterSkill;

	}
}
