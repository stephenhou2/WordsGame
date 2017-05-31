using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fury : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		self.attack = (int)((1.0f + this.scaler * skillLevel) * self.attack);
		self.hurtScaler = 1.0f + this.scaler * skillLevel;
	}
}
