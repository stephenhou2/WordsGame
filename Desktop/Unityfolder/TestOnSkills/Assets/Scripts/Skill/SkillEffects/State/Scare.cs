using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scare : StateSkillEffect {
	
	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		bool isScared = isEffective (this.scaler * skillLevel);
		
		if (isScared){
			self.strength -= Random.Range (3, 5);
			if (self.strength < 0) {
				self.strength = 0;
			}
		}
	}

}
