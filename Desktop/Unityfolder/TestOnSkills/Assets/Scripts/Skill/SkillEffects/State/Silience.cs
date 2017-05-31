using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Silience : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		bool succeedSilience = isEffective (this.scaler * skillLevel);
		if (succeedSilience) {
			self.validActionType = ValidActionType.MagicException;
		}
	}
}
