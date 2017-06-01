using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrestle : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		bool win = isEffective (this.scaler * skillLevel);
		if(win){
			self.strength += 1;
			target.strength -= 1;

			if (self.strength > self.maxStrength) {
				self.strength = self.maxStrength;
			}
			if (target.strength < 0) {
				target.strength = 0;
			}
		}
	}
}
