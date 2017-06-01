﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill : MonoBehaviour {

	public string skillName;// 技能名称

	public BaseSkillEffect[] skillEffects;//魔法效果数组

	public int strengthConsume;//技能的气力消耗

	public int actionConsume;//技能的行动数

	public int actionCount;//从释放技能开始已经走过的回合数

	public int skillLevel;// 技能等级

	public bool isAvalible = true;

	public bool isCopiedSkill;

	public int copiedSkillAvalibleTime = 2;

	public void AffectAgent(BattleAgent self, BattleAgent target,int skillLevel){
		
		foreach (BaseSkillEffect bse in skillEffects) {
			if (!bse.isStateEffect) {
				bse.AffectAgent (self, target, skillLevel, TriggerType.None, 0);
			} else {
				(bse as StateSkillEffect).ManageState (self, target, skillLevel, TriggerType.None, 0);
			}
		}
		if (isCopiedSkill) {
			copiedSkillAvalibleTime--;
			if (copiedSkillAvalibleTime <= 0) {
				self.skills.Remove (this);
			}
		}
	}

	public override string ToString ()
	{
		return string.Format ("[Skill]" + "\n[SkillName]:" + skillName + "\n[StrengthConsume]:" + strengthConsume + "\n[ActionConsume]:" + actionConsume + "\n[effect1]:" + skillEffects[0].effectName + "\n[effect2]:" + skillEffects[1].effectName);
	}

}

