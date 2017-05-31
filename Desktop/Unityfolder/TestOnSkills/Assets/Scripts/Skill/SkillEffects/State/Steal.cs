using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Steal : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		bool succeedSteal = isEffective (this.scaler * skillLevel);

		if (succeedSteal) {
			#warning 从怪物身上偷取一个字母碎片
		}
	}
}
