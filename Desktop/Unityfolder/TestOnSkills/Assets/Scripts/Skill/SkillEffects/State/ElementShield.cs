using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementShield : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		self.magicResist = (int)(this.scaler * skillLevel * self.magicResist);
	}
}
