using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrestle : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		bool win = isEffective (this.scaler * skillLevel);
		if(win){
			self.strength += 1;
			target.strength -= 1;
		}
	}
}
