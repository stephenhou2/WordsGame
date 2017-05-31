using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treat : BaseSkillEffect {

	public override void AffectAgent (BattleAgent self, 
		BattleAgent target,
		int skillLevel,
		bool isMagicTriggered,
		TriggerType triggerType,
		int arg)
	{
		this.effectType = EffectType.Treat;

		int healthRecover = (int)(self.maxHealth * this.scaler * skillLevel);
		self.health += healthRecover;
		if (self.health > self.maxHealth) {
			self.health = self.maxHealth;
		}
	}

}
