using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronBody : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		self.amour = (int)((1 + this.scaler * skillLevel) * self.amour);
	}

}
