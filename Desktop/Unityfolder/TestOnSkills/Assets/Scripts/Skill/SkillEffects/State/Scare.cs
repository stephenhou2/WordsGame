using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scare : StateSkillEffect {
	
	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		bool isScared = isEffective (this.scaler * skillLevel);
		
		if (isScared){
			target.strength -= Random.Range (3, 5);
		}
	}

}
