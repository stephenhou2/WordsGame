using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Skill : MonoBehaviour {

	public string skillName;// 技能名称

	[HideInInspector]public BaseSkillEffect[] skillEffects;//魔法效果数组

	public int strengthConsume;//技能的气力消耗

	public int actionConsume;//技能的行动数

	public int actionCount;//从释放技能开始已经走过的回合数

	public bool isAvalible;// 技能是否可用

	public int skillLevel;// 技能等级


	public void AffectAgent(BattleAgent user, BattleAgent target,int skillLevel,bool isMagicTriggered){
		
		user.strength -= this.strengthConsume;//使用魔法后使用者减去对应的气力消耗

		this.isAvalible = false;//使用魔法后该魔法进入冷却过程，无法使用

		Debug.Log (skillEffects[0]);

		foreach (BaseSkillEffect bse in skillEffects) {
			if (!bse.isStateEffect) {
				bse.AffectAgent (user, target, skillLevel, isMagicTriggered,TriggerType.None,0);
			} else {
				(bse as StateSkillEffect).ManageState (user, target, skillLevel, isMagicTriggered,TriggerType.None,0);
			}
		}

	}

	public override string ToString ()
	{
		return string.Format ("[Skill]" + "\n[SkillName]:" + skillName + "\n[StrengthConsume]:" + strengthConsume + "\n[ActionConsume]:" + actionConsume + "\n[effect1]:" + skillEffects[0].effectName + "\n[effect2]:" + skillEffects[1].effectName);
	}

}

