using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAttack : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		bool doubleAttack = isEffective (this.scaler * skillLevel);
		if (doubleAttack) {
			self.attackTime = 2;
		}

	}

}
