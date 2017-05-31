using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongWill : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		if (self.health / self.maxHealth <= 0.2f) {
			float gainScaler = (skillLevel / 100 * self.maxHealth);
			self.health *= (int)((1 + gainScaler) * self.maxHealth);
			self.strength *= (int)((1 + gainScaler) * self.maxStrength);
			self.attack *= (int)((1 + gainScaler) * self.attack);
			self.magic *= (int)((1 + gainScaler) * self.magic);
			self.agility *= (int)((1 + gainScaler) * self.agility);
			self.crit *= (int)((1 + gainScaler) * self.crit);
			self.amour *= (int)((1 + gainScaler) * self.amour);
			self.magicResist *= (int)((1 + gainScaler) * self.magicResist);
		}
	}
}
