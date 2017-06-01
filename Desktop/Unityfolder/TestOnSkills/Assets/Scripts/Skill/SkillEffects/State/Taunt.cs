using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taunt : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		target.validActionType = ValidActionType.PhysicalOnly;
		self.attack = (int)(this.scaler * self.attack);
	}
}
