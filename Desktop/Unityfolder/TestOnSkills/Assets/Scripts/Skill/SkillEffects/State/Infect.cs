using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infect : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		if (triggerType == TriggerType.BePhysicalHit || triggerType == TriggerType.BeMagicalHit) {
			bool SucceedInfect = isEffective (this.scaler * skillLevel);
			if (SucceedInfect) {
				List<StateSkillEffect> debuffs = new List<StateSkillEffect>();
				foreach (StateSkillEffect sse in target.states) {
					if (sse.effectType == EffectType.DeBuff) {
						StateSkillEffect sse_DeBuff = Instantiate (sse,self.statesContainer.transform);
						debuffs.Add (sse_DeBuff);

					}
				}

				int index = Random.Range (0, debuffs.Count);
				StateSkillEffect debuff = debuffs [index];
				debuff.effectDuration = 1;//传染给攻击者的debuff一共持续1轮
				debuff.actionCount = 0;
				debuff.effectTarget = SkillEffectTarget.Enemy;
				BattleAgentStatesManager.AddStateCopyToEffectTarget (self, target, debuff, skillLevel);

			}
		}
	}

}
