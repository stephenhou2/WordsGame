using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisArm : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		bool succeedDisArm = isEffective (this.scaler * skillLevel);
		if (succeedDisArm) {
			self.validActionType = ValidActionType.PhysicalExcption;
		}
	}
}
