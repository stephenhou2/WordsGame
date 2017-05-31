using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sorcery : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		self.validActionType = ValidActionType.None;
		self.agility = (int)((1.0f - this.scaler * skillLevel) * self.agility);
	}
}
