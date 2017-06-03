using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Provoke : StateSkillEffect {

	public override void AffectAgent (BattleAgent self, BattleAgent target, int skillLevel, TriggerType triggerType, int attachedInfo)
	{
		self.agility = (int)((1.0f + this.scaler * skillLevel) * self.agility);
		StateSkillEffect fury = GameManager.gameManager.GenerateStates("fury")[0];
		fury.effectTarget = SkillEffectTarget.Enemy;
		BattleAgentStatesManager.AddStateCopyToEffectTarget (self, target, fury, skillLevel);
	}
}
