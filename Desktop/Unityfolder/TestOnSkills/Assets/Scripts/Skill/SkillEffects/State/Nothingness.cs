using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nothingness : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int arg)
	{
		self.hurtScaler = 1 - this.scaler * skillLevel;
	}
}
