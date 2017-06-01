using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleAttack : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		bool doubleAttack = isEffective (this.scaler * skillLevel);

		self.attackTime = doubleAttack ? 2 : 1;

	}

}
