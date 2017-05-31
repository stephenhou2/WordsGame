using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infect : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		if (triggerType == TriggerType.BeHit) {
			bool SucceedInfect = isEffective (this.scaler * skillLevel);
			if (SucceedInfect) {
				List<StateSkillEffect> debuffs = new List<StateSkillEffect>();
				foreach (StateSkillEffect sse in self.states) {
					if (sse.effectType == EffectType.DeBuff) {
						debuffs.Add (sse);

					}
				}

				int index = Random.Range (0, debuffs.Count);
				StateSkillEffect debuff = debuffs [index];
				debuff.effectDuration = 1;//传染给攻击者的debuff一共持续1轮
				target.AddState (debuff,skillLevel);

			}
		}
	}

}
