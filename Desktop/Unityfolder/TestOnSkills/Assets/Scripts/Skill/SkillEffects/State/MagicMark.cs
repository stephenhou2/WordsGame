using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMark : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		if (triggerType == TriggerType.BePhysicalHit) {

			self.magicResist = (int)((1 - this.scaler * skillLevel) * self.magicResist);
		}

	}
}
