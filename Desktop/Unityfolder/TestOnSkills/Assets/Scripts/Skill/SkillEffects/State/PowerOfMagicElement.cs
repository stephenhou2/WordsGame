using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerOfMagicElement : StateSkillEffect {


	public override void AffectAgent (BattleAgent self, 
		BattleAgent target,
		int skillLevel,
		bool isMagicTriggered,
		TriggerType triggerType,
		int attachedInfo)
	{
		self.magic = (int)(self.magic * (1 + this.scaler * skillLevel)); 

	}

}
