﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tearing : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		if (triggerType == TriggerType.Hit) {
			self.amour = (int)((1 - this.scaler * skillLevel) * self.amour);
		}
	}

}