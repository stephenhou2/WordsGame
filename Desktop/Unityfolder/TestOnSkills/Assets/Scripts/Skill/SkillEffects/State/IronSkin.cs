using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronSkin : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, 
		BattleAgent target,
		int skillLevel,
		bool isMagicTriggered,
		TriggerType triggerType,
		int arg)
	{
		self.amour = (int)(this.scaler * skillLevel * self.amour);
	}


}
