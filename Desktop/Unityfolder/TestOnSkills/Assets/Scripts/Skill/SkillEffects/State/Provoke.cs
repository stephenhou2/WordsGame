using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provoke : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, bool isTriggered, TriggerType triggerType, int attachedInfo)
	{
		target.agility = (int)((1.0f + this.scaler * skillLevel) * target.agility);
		StateSkillEffect fury = GameManager.gameManager.GenerateStates("fury")[0];
		self.AddState (fury,skillLevel);
	}
}
