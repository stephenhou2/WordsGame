using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRecover : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, 
		BattleAgent target,
		int skillLevel,
		bool isMagicTriggered,
		TriggerType triggerType,
		int attachedInfo)
	{
		int healthRecover = (int)(this.scaler * skillLevel * self.maxHealth);
		self.health += healthRecover;
		if (self.health > self.maxHealth) {
			self.health = self.maxHealth;
		}
	}
}
