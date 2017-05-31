using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overload: StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		self.hurtScaler = 1 + this.scaler * skillLevel;
		target.hurtScaler = self.hurtScaler;
	}
}
