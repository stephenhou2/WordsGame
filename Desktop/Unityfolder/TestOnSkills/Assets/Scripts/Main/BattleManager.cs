using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {

//	private Player player;
//
//	public Monster monsterModel;
//
//	private Monster monster;

	// ********for test use**********//
	public Player player;

	public Monster monster;
	// ********for test use**********//

	public int gameProcess;

	public Button[] skillButtons;

	public Button attackButton;

	public Button defenceButton;

	public Button[] itemButtons;

	public Slider playerHealth;

	public Slider playerStrength;

	public Slider monsterHealth;

	public Slider monsterStrength;

	public Text description;

	public GameObject controlPlane;

	public GameObject battleEndHUD;

	public Text battleEndResult;

	public Button quit;

	public Button reset;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	static private void CallBackAfterBattleSceneLoaded(){
		SceneManager.sceneLoaded += OnSceneLoaded;
	}

	static private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
	{

		BattleManager bm = GameObject.Find ("BattleManager").GetComponent<BattleManager> ();

		int gameProcess = GameManager.gameManager.gameProcess;

		Player player = GameManager.gameManager.player;

		bm.SetupBattleManager (player, gameProcess);
	}

	// **************for test use************//
	// 后面改为scene加载后执行的函数（上面的）
	void Awake(){

		SetupBattleManager (player, gameProcess);

		OnResetGame ();
	}

	// 进入战斗场景时初始化玩家和怪物数据
	public void SetupBattleManager(Player player,int gameProcess){

		// 每一关根据游戏进度创建新的怪物
//		monster = Instantiate (monsterModel);

//		monster.SetupMonster (gameProcess);


		#warning 这里是否会产生循环引用？注意一下
		player.enemy = monster;

		monster.enemy = player;



	}

	public void PlayerAction(int skillId){

		controlPlane.gameObject.SetActive (true);// 遮罩，下回合开始前用户无法再点击按钮

		Skill playerSkill = player.skills [skillId];

		description.text = "player 使用了" + playerSkill.skillName;

		Debug.Log("player 使用了" + playerSkill.skillName);

		playerSkill.AffectAgent (player,monster,playerSkill.skillLevel);

		playerSkill.isAvalible = false;// 使用技能之后该技能暂时进入不可用状态

		player.strength -= playerSkill.strengthConsume;//使用魔法后使用者减去对应的气力消耗

		Debug.Log ("before animation");

		try{
			StartCoroutine ("PlayerAnimation");
		}catch(System.Exception e){
			Debug.Log (e.Message);
		}



	}

	public IEnumerator PlayerAnimation(){
		
		#warning player animation
		yield return new WaitForSeconds (1.0f);//动画暂时由延时1s代替

		UpdatePropertiesByStates ();

		UpdateUIAndBattleResult ();//更新玩家和怪物状态,判断游戏是否结束

		MonsterAction ();//怪物行动
	}


	private void MonsterAction(){

		Debug.Log ("monster action");

		if (monster.validActionType == ValidActionType.None) {
			controlPlane.gameObject.SetActive (false);

			EnterNextTurn ();//进入下一回合
			return;
		}

		monster.ManageSkillAvalibility ();

		#warning 这里添加怪物技能逻辑，选择使用的技能
//		Skill monsterSkill = monster.SkillOfMonster ();
		Skill monsterSkill = monster.skills [0];//怪物技能暂时由普通攻击代替

		monsterSkill.AffectAgent (monster, player, monsterSkill.skillLevel);

		monster.strength -= monsterSkill.strengthConsume;

		monsterSkill.isAvalible = false;

		StartCoroutine ("MonsterAnimation");


	}

	public IEnumerator MonsterAnimation(){

		#warning player animation
		yield return new WaitForSeconds (1.0f);

		UpdatePropertiesByStates ();

		UpdateUIAndBattleResult ();

		controlPlane.gameObject.SetActive (false);

		EnterNextTurn ();//进入下一回合

	}


	public void EnterNextTurn(){

		UpdatePropertiesByStates ();

		BattleAgentStatesManager.CheckStates (player,monster);

		// 根据玩家可采取的行动类型设定技能按钮是否可以交互
		ValidActionForPlayer ();
	}

	private void UpdatePropertiesByStates(){
		for(int i = 0;i<player.states.Count;i++){
			StateSkillEffect sse = player.states [i];
			sse.ExcuteEffect (player, monster, sse.skillLevel, TriggerType.None, 0);
		}
		for(int i = 0;i<monster.states.Count;i++){
			StateSkillEffect sse = monster.states [i];
			sse.ExcuteEffect (monster, player, sse.skillLevel, TriggerType.None, 0);
		}
	}

	public void UpdateUIAndBattleResult(){
		
		UpdateStatesUI ();

		#warning picturs of states toAdd



		if (player.health <= 0) {
			battleEndHUD.gameObject.SetActive (true);
			battleEndResult.text = "You Lose!";

		} else if (monster.health <= 0) {
			battleEndHUD.gameObject.SetActive (true);
			battleEndResult.text = "you win";

		}


	}

	public void UpdateStatesUI(){
		playerHealth.value = player.health;
		playerStrength.value = player.strength;
		monsterHealth.value = monster.health;
		monsterStrength.value = monster.strength;

	}

	public void ValidActionForPlayer(){

		switch (player.validActionType) {

		case ValidActionType.All:
			break;
		case ValidActionType.PhysicalExcption:
			EnableOrDisableButtons (false, true, true, true);
			break;
		case ValidActionType.MagicException:
			EnableOrDisableButtons (true, false, true, true);
			break;
		case ValidActionType.None:
			EnableOrDisableButtons (false, false, false, false);
			break;
		case ValidActionType.PhysicalOnly:
			EnableOrDisableButtons (true, false, false, true);
			break;
		case ValidActionType.MagicOnly:
			EnableOrDisableButtons (false, true, false, true);
			break;
		default:
			break;
		}
		// 如果技能还在冷却中或者玩家气力值小于技能消耗的气力值，则相应按钮不可用
		for (int i = 0;i < player.skills.Count;i++) {
			
			Skill s = player.skills [i];

			// 如果是冷却中的技能
			if (s.isAvalible == false) {
				s.actionCount++;
				Debug.Log (s.skillName + "从使用开始经过了" + s.actionCount + "回合");
				if (s.actionCount >= s.actionConsume && player.strength >= s.strengthConsume) {
					skillButtons [i].interactable = true;
					s.isAvalible = true;
					s.actionCount = 0;
				} else {
					skillButtons [i].interactable = false;
				}
			}
			// 如果不是冷却中的技能
			else {
				skillButtons [i].interactable = player.strength >= s.strengthConsume;
			} 
		}

	}

	//根据玩家状态判断按钮是否可以交互
	private void EnableOrDisableButtons(bool isAttackEnable,bool isSkillEnable,bool isItemEnable,bool isDefenceEnable){
		attackButton.interactable = isAttackEnable && attackButton.interactable;
		defenceButton.interactable = isDefenceEnable && defenceButton.interactable;
		foreach (Button btn in skillButtons) {
			btn.interactable = isSkillEnable && btn.interactable;
		}
		foreach (Button btn in itemButtons) {
			btn.interactable = isItemEnable && btn.interactable;
		}
	}



	public void OnAttack(){
		PlayerAction (3);
	}

	public void OnDenfence(){
		PlayerAction (4);
	}

	public void OnSkill(int skillIndex){
		PlayerAction (skillIndex);
	}

	public void OnItem(int itemIndex){

	}

	//*********** for skill test use **************//
	public void OnResetGame(){
		player.states.Clear ();
		monster.states.Clear ();

		OnReset ();

		battleEndHUD.SetActive (false);
		UpdateStatesUI ();
		foreach (Button btn in skillButtons) {
			btn.interactable = true;
		}
	}

	public void OnQuitBattle(){
		Debug.Log ("quit to main screen");
		battleEndHUD.gameObject.SetActive (false);
	}

	public void OnReset(){
		player.ResetBattleAgentProperties (true);
		monster.ResetBattleAgentProperties (true);


		//*************** for test use ************//
		for (int i = 0; i < player.skills.Count; i++) {
			foreach (BaseSkillEffect bse in player.skills[i].skillEffects) {
				bse.actionCount = 1;
			}
		}

		for (int i = 0; i < player.skills.Count; i++) {
			foreach (BaseSkillEffect bse in player.skills[i].skillEffects) {
				bse.actionCount = 1;
			}
		}



		//*************** for test use ************//
	}

}
