using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yield : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		if (self.health / self.maxHealth <= 0.2f) {
			bool strongWill = false;
			foreach (StateSkillEffect sse in self.states) {
				if (sse.GetType () == typeof(StrongWill)) {
					strongWill = true;
				}
			}
			bool isYield = isEffective (skillLevel);
			if (isYield && !strongWill) {
				#warning直接投降
			}
		}
	}
}
