using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAgentStatesManager:MonoBehaviour {

	// 状态管理方法，提供状态类效果的等级信息和作用的单位信息
	public static void AddStateCopyToEffectTarget (BattleAgent self, BattleAgent enemy, StateSkillEffect sse,int skillLevel)
	{
		sse.skillLevel = skillLevel;
		// 根据技能指向将状态加到指定的对象身上
		switch (sse.effectTarget) {
		case SkillEffectTarget.Self:
			AddStateTo (self,sse);
			break;
		case SkillEffectTarget.Enemy:
			AddStateTo (enemy,sse);
			break;
		case SkillEffectTarget.Both:
			AddStateTo (self,sse);
			AddStateTo (enemy,sse);
			break;
		default:
			break;
		}

	}

	// 将状态添加到对象身上
	public static void AddStateTo(BattleAgent ba,StateSkillEffect sse){

		StateSkillEffect state = null;

		for (int i = 0; i < ba.states.Count; i++) {
			state = ba.states [i];
			if (state.id == sse.id) {
				state.actionCount = 1;
				return;
			}
		}
		state = Instantiate (sse);
		ba.AddState (state);
		Debug.Log (ba.agentName + " add state: " + state.effectName);

	}


	public static void CheckStates(BattleAgent player,BattleAgent monster){

		CheckStatesOf (player);
		CheckStatesOf (monster);
	}


	private static void CheckStatesOf(BattleAgent ba){
		for(int i = 0;i<ba.states.Count;i++){
			StateSkillEffect sse = ba.states [i];
			sse.actionCount++;
			Debug.Log ("---------" + sse.effectName + sse.actionCount);
			if (sse.actionCount > sse.effectDuration) {
				ba.RemoveState (sse);
				Debug.Log (ba.agentName + "remove state:" + sse.effectName);
				Destroy (sse);
				i--;
			}
		}
	}
}
