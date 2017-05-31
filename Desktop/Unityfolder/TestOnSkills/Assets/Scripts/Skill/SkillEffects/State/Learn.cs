using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Learn: StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		bool succeedSteal = isEffective (this.scaler * skillLevel);
		if (succeedSteal) {
			List<StateSkillEffect> buffs = new List<StateSkillEffect>();
			foreach (StateSkillEffect sse in target.states) {
				if (sse.effectType == EffectType.Buff) {
					StateSkillEffect sse_Buff = Instantiate (sse,self.statesContainer.transform);
					buffs.Add (sse_Buff);
				}
			}
			int index = Random.Range (0, buffs.Count);
			StateSkillEffect buff = buffs [index];
			buff.effectDuration = 1;//传染给攻击者的debuff一共持续1轮
			self.AddState (buff,skillLevel);

		}
	}
}
