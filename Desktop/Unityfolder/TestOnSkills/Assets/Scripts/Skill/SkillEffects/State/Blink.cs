using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blink : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, 
		BattleAgent target,
		int skillLevel,
		TriggerType triggerType,
		int attachedInfo)
	{
		self.agility = (int)((1 + this.scaler * skillLevel) * self.agility);

	}
}
