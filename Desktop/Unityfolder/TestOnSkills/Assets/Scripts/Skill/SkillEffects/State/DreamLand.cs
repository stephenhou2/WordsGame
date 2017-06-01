using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamLand : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		if (triggerType == TriggerType.Dodge) {
			self.attack -= (int)((1 - this.scaler * skillLevel) * self.attack);
		}
	}
}
