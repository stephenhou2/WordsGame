using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBody : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		if (triggerType == TriggerType.BePhysicalHit) {
			self.amour = (int)((1 + this.scaler * skillLevel) * self.amour);
		}
	}

}
