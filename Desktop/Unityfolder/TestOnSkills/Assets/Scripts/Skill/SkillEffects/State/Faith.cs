using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Faith : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		float healthPercentage = self.health / self.maxHealth;
		if (healthPercentage <= this.scaler * (10 + skillLevel)) {
			self.attack = 4 * self.attack;
		} else {
			self.attack = 2 *  self.attack;
		}
	}
}
