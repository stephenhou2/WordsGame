using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roar : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, 
		BattleAgent target,
		int skillLevel,
		bool isMagicTriggered,
		TriggerType triggerType,
		int attachedInfo)
	{
		self.crit = (int)(self.crit * (1 + this.scaler * skillLevel));

		bool isCrit = isEffective (seed * self.crit / (1 + seed * self.crit));

		if (isCrit) {
			self.critScaler = 2.0f;
		}

	}
}
