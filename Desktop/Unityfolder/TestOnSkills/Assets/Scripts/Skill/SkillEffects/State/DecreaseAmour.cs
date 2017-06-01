using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseAmour : StateSkillEffect {


	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		self.amour = (int)(self.amour * (1 - this.scaler * skillLevel));
	}
}
