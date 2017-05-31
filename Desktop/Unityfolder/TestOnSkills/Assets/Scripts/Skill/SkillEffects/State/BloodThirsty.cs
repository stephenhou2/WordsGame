using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodThirsty : BaseSkillEffect {


	public override void AffectAgent (BattleAgent self, 
		BattleAgent target,
		int skillLevel,
		bool isMagicTriggered,
		TriggerType triggerType,
		int attachedInfo)
	{
		self.healthAbsorbScalser = this.scaler * skillLevel * self.attack;
	}
}
