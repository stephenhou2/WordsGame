using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seal : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		bool succeedSealed = isEffective (this.scaler * skillLevel);
		if (succeedSealed) {
			self.strength -= (int)(1 + 0.02f * target.power);
		}
	}
}
