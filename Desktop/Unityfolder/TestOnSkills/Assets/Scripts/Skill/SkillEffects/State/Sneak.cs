using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneak : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		if (triggerType == TriggerType.Dodge) {
			self.attack = (int)((this.scaler * skillLevel + 1) * self.attack);
		}
	}
}
