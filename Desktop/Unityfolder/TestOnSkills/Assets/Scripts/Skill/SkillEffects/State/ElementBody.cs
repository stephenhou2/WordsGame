using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementBody : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{

		if (triggerType == TriggerType.BeMagicalHit) {
			self.magicResist = (int)((1 + this.scaler * skillLevel) * self.magicResist);
		}


	}
}
