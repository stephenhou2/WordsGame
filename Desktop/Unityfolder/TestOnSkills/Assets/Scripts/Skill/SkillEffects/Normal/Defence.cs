using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defence : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		
		self.hurtScaler = this.scaler;
		self.strength += 3;
	}
}
