using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weaken : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		self.magicResist = (int)(self.magicResist * (1 - this.scaler * skillLevel));
	}

}
