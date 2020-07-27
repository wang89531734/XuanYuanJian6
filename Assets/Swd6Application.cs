using GameFramework;
//using GameScriptPlugin;
//using iGUI;
//using Nini.Config;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Swd6Application : GameApplication
{
    //	[HideInInspector]
    //	public bool GamePause;

    //	[HideInInspector]
    //	public bool m_Is64BitOS = true;

    //	public ENUM_ResourceType m_ResourceType;

    //	public ENUM_LanguageType m_NowLanguageType;

    //	public bool m_IsStoryTest = true;

    //	public bool m_DBFLog;

    //	public bool m_Password;

    //	public int m_ChapID = 100;

    //	private byte[] m_CaptureImageData;

    //	private Texture2D m_CaptureTexture;

    //	private bool m_CaptureScale;

    //	private float m_UpdateResizeTime;

    //	[HideInInspector]
    //	public bool m_InitializeOK;

    //	[HideInInspector]
    //	public Swd6_DebugSetting m_DebugSetting;

    //	public static Swd6Application instance
    //	{
    //		get
    //		{
    //			return GameApplication._instance as Swd6Application;
    //		}
    //	}

    //	public GameObjSystem m_GameObjSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public GameDataSystem m_GameDataSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public ExploreSystem m_ExploreSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public GameMenuSystem m_GameMenuSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public QuestSystem m_QuestSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public ItemSystem m_ItemSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public IdentifySystem m_IdentifySystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public SaveloadSystem m_SaveloadSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public SkillSystem m_SkillSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public MobGuardSystem m_MobGuardSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public AchievementSystem m_AchievementSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public WOPSystem m_WOPSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public InheritSystem m_InheritSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public MapPathSystem m_MapPathSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public StorySystem m_StorySystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public FightSystem m_FightSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public MusicControlSystem m_MusicControlSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public QualitySettingSystem m_QualitySettingSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public MovieSystem m_MovieSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public NormalSettingSystem m_NormalSettingSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public SoundSettingSystem m_SoundSettingSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	public ControlSettingSystem m_ControlSettingSystem
    //	{
    //		get;
    //		private set;
    //	}

    //	private void OnApplicationQuit()
    //	{
    //		Application.CancelQuit();
    //		Process.GetCurrentProcess().Kill();
    //	}

    //	private void OnLevelWasLoaded(int level)
    //	{
    //		this.m_QualitySettingSystem.UpdateSceneQuality();
    //	}

    //	public bool IsDLC()
    //	{
    //		return this.m_ChapID == 101;
    //	}

    //	public override void Update()
    //	{
    //		if (this.GamePause)
    //		{
    //			return;
    //		}
    //		base.Update();
    //		if (this.m_InitializeOK)
    //		{
    //			if (this.m_GameDataSystem != null)
    //			{
    //				this.m_GameDataSystem.Update();
    //			}
    //			if (this.m_QuestSystem != null)
    //			{
    //				this.m_QuestSystem.Update();
    //			}
    //			if (this.m_AchievementSystem != null)
    //			{
    //				this.m_AchievementSystem.Update();
    //			}
    //			if (this.m_GameMenuSystem != null)
    //			{
    //				this.m_GameMenuSystem.Update();
    //			}
    //			if (this.m_QualitySettingSystem != null)
    //			{
    //				this.m_QualitySettingSystem.Update();
    //			}
    //			GameInput.Update();
    //			GameCursor.Update();
    //			this.UpdateInput();
    //			if (this.m_UpdateResizeTime > 0f)
    //			{
    //				this.m_UpdateResizeTime -= Time.deltaTime;
    //				if (this.m_UpdateResizeTime <= 0f)
    //				{
    //					this.m_UpdateResizeTime = 0f;
    //					ExploreMiniMapSystem.Instance.ReSize(this.m_GameDataSystem.m_MapInfo.MapID);
    //				}
    //			}
    //		}
    //	}

    //	public void UpdateInput()
    //	{
    //		if (GameInput.GetKeyDown(KeyCode.P))
    //		{
    //			base.StartCoroutine(this.m_SaveloadSystem.SaveAlbumScreenShot());
    //		}
    //	}

    //	public override void OnGUI()
    //	{
    //		base.OnGUI();
    //	}

    //	protected override void initialize()
    //	{
    //		int systemMemorySize = SystemInfo.systemMemorySize;
    //		string operatingSystem = SystemInfo.operatingSystem;
    //		if (Mathf.RoundToInt((float)systemMemorySize / 1024f) >= 4 && operatingSystem.Contains("64bit"))
    //		{
    //			UnityEngine.Debug.Log("主記憶體大於等於4G，64位元OS");
    //			this.m_Is64BitOS = true;
    //		}
    //		else
    //		{
    //			UnityEngine.Debug.Log("主記憶體小於4G或非64位元OS");
    //			this.m_Is64BitOS = false;
    //		}
    //		if (this.m_ResourceType == ENUM_ResourceType.Runtime)
    //		{
    //			string path = Application.dataPath + "/../SWD6.exe";
    //			FileStream fileStream = File.OpenRead(path);
    //			if (fileStream == null)
    //			{
    //				Application.Quit();
    //				return;
    //			}
    //			if (fileStream.Length < 19000000L)
    //			{
    //				Application.Quit();
    //				return;
    //			}
    //		}
    //		this.InitDebugSetting();
    //		if (this.m_DebugSetting != null)
    //		{
    //			if (this.m_DebugSetting.MapPathTest)
    //			{
    //				UnityEngine.Debug.Log("MapPathTest!!");
    //				base.StartCoroutine(this.InitializeMapPathUT());
    //				return;
    //			}
    //			if (this.m_DebugSetting.FightUnitTest)
    //			{
    //				base.StartCoroutine(this.InitializeFightUT());
    //				return;
    //			}
    //			if (this.m_DebugSetting.SuperSkillUnitTest)
    //			{
    //				base.StartCoroutine(this.InitializeEffectUnitTest("UTSuperSkillState"));
    //				return;
    //			}
    //			if (this.m_DebugSetting.SkillUnitTest)
    //			{
    //				base.StartCoroutine(this.InitializeEffectUnitTest("UTSkillState"));
    //				return;
    //			}
    //			if (this.m_DebugSetting.AttackUnitTest)
    //			{
    //				base.StartCoroutine(this.InitializeEffectUnitTest("UTAttackState"));
    //				return;
    //			}
    //			if (this.m_DebugSetting.CombineSkillUnitTest)
    //			{
    //				base.StartCoroutine(this.InitializeEffectUnitTest("UTCombineSkillState"));
    //				return;
    //			}
    //			if (this.m_DebugSetting.CatchMonsterUnitTest)
    //			{
    //				base.StartCoroutine(this.InitializeEffectUnitTest("UTCatchMonsterState"));
    //				return;
    //			}
    //			if (this.m_DebugSetting.StoryUnitTest)
    //			{
    //				base.StartCoroutine(this.InitializeStoryUT());
    //				return;
    //			}
    //			if (this.m_DebugSetting.StoryUnitTest_Mark)
    //			{
    //				base.StartCoroutine(this.InitializeStoryUT_Mark());
    //				return;
    //			}
    //			if (this.m_DebugSetting.StoryUnitTest_Mark_2)
    //			{
    //				QualitySettings.SetQualityLevel(1, false);
    //				base.StartCoroutine(this.InitializeStoryUT_Mark());
    //				return;
    //			}
    //		}
    //		base.StartCoroutine(this.InitializeNormalGame());
    //	}

    //	private void InitDebugSetting()
    //	{
    //		this.m_DebugSetting = base.GetComponent<Swd6_DebugSetting>();
    //	}

    //	private IEnumerator InitializeEffectUnitTest(string stateName)
    //	{
    //		GameCursor.Init();
    //		yield return base.StartCoroutine(this.ReadGameDB());
    //		this.InitResourceSystem();
    //		this.InitGameSystem();
    //		base.AddGameState(new UTSkillState("UTSkillState", "", this));
    //		base.AddGameState(new UTSuperSkillState("UTSuperSkillState", "", this));
    //		base.AddGameState(new UTAttackState("UTAttackState", "", this));
    //		base.AddGameState(new UTCombineSkillState("UTCombineSkillState", "", this));
    //		base.AddGameState(new UTCatchMonsterState("UTCatchMonsterState", "", this));
    //		this.m_InitializeOK = true;
    //		this.m_GameDataSystem.InitRoleData(this.m_ChapID);
    //		this.SwitchState(stateName);
    //		yield break;
    //	}

    //	private IEnumerator InitializeStoryUT_Mark()
    //	{
    //		GameCursor.Init();
    //		string mapBlockPath = Application.dataPath + "/../DBF/";
    //		string dBF_Path = Application.dataPath + "/../DBF/";
    //		string languagePath = Application.dataPath + "/../DBF/";
    //		GameDataDB.SetConevrt(new SwdJsonCovertor());
    //		GameDataDB.Initialize(mapBlockPath, dBF_Path, languagePath);
    //		GameDataDB.LoadDBFStortTest();
    //		yield return base.StartCoroutine(GameDataDB.LoadLanguage());
    //		this.InitResourceSystem();
    //		UnityEngine.Object.DontDestroyOnLoad(iGUIRoot.instance.gameObject);
    //		base.guiManager.AddGUI(typeof(UI_Menu).Name, UI_Menu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Fight).Name, UI_Fight.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_FinishFight).Name, UI_FinishFight.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Loading).Name, UI_Loading.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Pause).Name, UI_Pause.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Fade).Name, UI_Fade.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Subtitle).Name, UI_Subtitle.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Subtitle_FullScreen).Name, UI_Subtitle_FullScreen.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_StoryMovie).Name, UI_StoryMovie.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameNpcListMenu).Name, UI_GameNpcListMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameGetItemMenu).Name, UI_GameGetItemMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameGMMenu).Name, UI_GameGMMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameReviewStoryMenu).Name, UI_GameReviewStoryMenu.Instance, 0);
    //		yield return 1;
    //		this.InitGameSystem();
    //		yield return 1;
    //		this.InitGameState();
    //		yield return 1;
    //		this.m_InitializeOK = true;
    //		base.guiManager.Initialize();
    //		UI_Menu.Instance.showMovie = false;
    //		AsyncOperation asyncOperation = Application.LoadLevelAsync("Menu");
    //		while (!asyncOperation.isDone)
    //		{
    //			yield return 1;
    //		}
    //		this.SwitchState("MenuState");
    //		yield break;
    //	}

    //	private IEnumerator InitializeStoryUT()
    //	{
    //		GameCursor.Init();
    //		yield return base.StartCoroutine(this.ReadGameDB());
    //		this.InitResourceSystem();
    //		this.InitGUI();
    //		this.InitGameSystem();
    //		base.AddGameState(new UTStoryState("UTStoryState", "", this));
    //		this.m_InitializeOK = true;
    //		base.guiManager.Initialize();
    //		this.m_GameDataSystem.InitRoleData(this.m_ChapID);
    //		this.SwitchState("UTStoryState");
    //		yield break;
    //	}

    //	private IEnumerator InitializeFightUT()
    //	{
    //		GameCursor.Init();
    //		yield return base.StartCoroutine(this.ReadGameDB());
    //		this.InitResourceSystem();
    //		this.InitGUI();
    //		this.InitGameSystem();
    //		this.InitGameState();
    //		this.m_InitializeOK = true;
    //		base.guiManager.Initialize();
    //		this.m_GameDataSystem.InitRoleData(this.m_ChapID);
    //		for (int i = 0; i < 10; i++)
    //		{
    //			this.m_ItemSystem.AddItem(1101 + i, 10, ENUM_ItemState.New, false);
    //		}
    //		for (int j = 0; j < 10; j++)
    //		{
    //			this.m_ItemSystem.AddItem(1301 + j, 10, ENUM_ItemState.New, false);
    //		}
    //		this.m_ItemSystem.AddItem(1051, 10, ENUM_ItemState.New, false);
    //		this.m_ItemSystem.AddItem(1072, 10, ENUM_ItemState.New, false);
    //		this.m_ItemSystem.AddItem(901, 10, ENUM_ItemState.New, false);
    //		this.m_ItemSystem.AddItem(1595, 1, ENUM_ItemState.New, false);
    //		this.m_ItemSystem.AddItem(1081, 10, ENUM_ItemState.New, false);
    //		this.m_ItemSystem.AddItem(1082, 10, ENUM_ItemState.New, false);
    //		this.m_ItemSystem.AddItem(1110, 10, ENUM_ItemState.New, false);
    //		this.m_SkillSystem.LearnSkill(3, 5104);
    //		this.m_SkillSystem.LearnSuperSkill(3, 5601, "全體缸體");
    //		this.m_SkillSystem.LearnSuperSkill(1, 5501, "老漢推車");
    //		this.m_SkillSystem.LearnSuperSkill(1, 5502, "冰火五重天");
    //		this.m_SkillSystem.LearnSuperSkill(2, 5551, "慧能啟迪冥");
    //		this.m_SkillSystem.LearnSuperSkill(2, 5568, "XXXXX");
    //		this.m_SkillSystem.SetActiveSuperSkill(3, 0, 5601);
    //		this.m_SkillSystem.SetActiveSuperSkill(2, 0, 5551);
    //		this.m_SkillSystem.SetActiveSuperSkill(2, 0, 5568);
    //		this.m_SkillSystem.SetActiveSuperSkill(1, 0, 5501);
    //		this.m_SkillSystem.SetActiveSuperSkill(1, 1, 5502);
    //		this.m_SkillSystem.LearnSkill(5, 5203);
    //		this.m_FightSystem.Fight_Test(this.m_DebugSetting.BattleGroupID);
    //		yield break;
    //	}

    //	private IEnumerator InitializeMapPathUT()
    //	{
    //		string mapBlockPath = Application.dataPath + "/../DBF/";
    //		string dBF_Path = Application.dataPath + "/../DBF/";
    //		string languagePath = Application.dataPath + "/../DBF/";
    //		GameDataDB.SetConevrt(new SwdJsonCovertor());
    //		GameDataDB.Initialize(mapBlockPath, dBF_Path, languagePath);
    //		GameDataDB.LoadDBFMapPath();
    //		this.m_MapPathSystem = new MapPathSystem();
    //		this.m_MapPathSystem.Initialize();
    //		GameInput.Initialize();
    //		this.m_InitializeOK = true;
    //		yield return null;
    //		yield break;
    //	}

    //	private IEnumerator InitializeNormalGame()
    //	{
    //		GameCursor.Init();
    //		yield return base.StartCoroutine(this.ReadGameDB());
    //		this.InitResourceSystem();
    //		yield return 1;
    //		this.InitGUI();
    //		yield return 1;
    //		this.InitGameSystem();
    //		yield return 1;
    //		this.InitGameState();
    //		this.m_InitializeOK = true;
    //		base.guiManager.Initialize();
    //		AsyncOperation asyncOperation = Application.LoadLevelAsync("Menu");
    //		while (!asyncOperation.isDone)
    //		{
    //			yield return 1;
    //		}
    //		this.SwitchState("MenuState");
    //		yield break;
    //	}

    //	private IEnumerator ReadConfig()
    //	{
    //		yield return null;
    //		yield break;
    //	}

    //	private IEnumerator ReadGameDB()
    //	{
    //		string mapBlockPath = Application.dataPath + "/../DBF/";
    //		string dBF_Path = Application.dataPath + "/../DBF/";
    //		string languagePath = Application.dataPath + "/../DBF/";
    //		GameDataDB.SetConevrt(new SwdJsonCovertor());
    //		GameDataDB.Initialize(mapBlockPath, dBF_Path, languagePath);
    //		yield return base.StartCoroutine(GameDataDB.LoadDBF());
    //		yield return base.StartCoroutine(GameDataDB.LoadLanguage());
    //		yield break;
    //	}

    //	private void InitGameState()
    //	{
    //		base.AddGameState(new LoadingState("LoadingState", "Loading", this));
    //		base.AddGameState(new MenuState("MenuState", "Main", this));
    //		base.AddGameState(new ExploreState("ExploreState", "", this));
    //		base.AddGameState(new StoryState("StoryState", "", this));
    //		base.AddGameState(new FightState("FightState", "", this));
    //		base.AddGameState(new GameMenuState("GameMenuState", "", this));
    //	}

    //	private void InitResourceSystem()
    //	{
    //		EffectGenerator.Init(this.m_ResourceType);
    //		MonsterGenerator.Init(this.m_ResourceType);
    //		PlayerGenerator.Init(this.m_ResourceType);
    //		MusicGenerator.Init(this.m_ResourceType);
    //		OSGenerator.Init(this.m_ResourceType);
    //		MovieGenerator.Init(this.m_ResourceType);
    //		MusicEffectGenerator.Init(this.m_ResourceType);
    //		ItemGenerator.Init(this.m_ResourceType);
    //		NpcGenerator.Init(this.m_ResourceType);
    //		ImageGenerator.Init(this.m_ResourceType);
    //		OtherElementGenerator.Init(this.m_ResourceType);
    //		MaterialGenerator.Init(this.m_ResourceType);
    //		GameNpcGenerator.Init(this.m_ResourceType);
    //		MapEventGenerator.Init(this.m_ResourceType);
    //		StoryGenerator.Init(this.m_ResourceType);
    //		MapSoundEventGenerator.Init(this.m_ResourceType);
    //		GUIGenerator.Init(this.m_ResourceType);
    //		AnimationGenerator.Init(this.m_ResourceType);
    //	}

    //	public void ClearResourceSystem()
    //	{
    //		EffectGenerator.Clear();
    //		MonsterGenerator.Clear();
    //		PlayerGenerator.Clear();
    //		MusicGenerator.Clear();
    //		OSGenerator.Clear();
    //		MovieGenerator.Clear();
    //		MusicEffectGenerator.Clear();
    //		ItemGenerator.Clear();
    //		NpcGenerator.Clear();
    //		ImageGenerator.Clear();
    //		OtherElementGenerator.Clear();
    //		MaterialGenerator.Clear();
    //		GameNpcGenerator.Clear();
    //		MapEventGenerator.Clear();
    //		StoryGenerator.Clear();
    //		MapSoundEventGenerator.Clear();
    //		AnimationGenerator.Clear();
    //	}

    //	private void InitGameSystem()
    //	{
    //		GameInput.Initialize();
    //		this.m_GameObjSystem = new GameObjSystem();
    //		this.m_GameObjSystem.Initialize();
    //		this.m_GameDataSystem = new GameDataSystem();
    //		this.m_GameDataSystem.Initialize();
    //		this.m_ExploreSystem = new ExploreSystem();
    //		this.m_ExploreSystem.Initialize(this);
    //		this.m_GameMenuSystem = new GameMenuSystem();
    //		this.m_GameMenuSystem.Initialize(this);
    //		this.m_QuestSystem = new QuestSystem();
    //		this.m_QuestSystem.Initialize(this);
    //		this.m_IdentifySystem = new IdentifySystem();
    //		this.m_IdentifySystem.Initialize(this);
    //		this.m_ItemSystem = new ItemSystem();
    //		this.m_ItemSystem.Initialize(this);
    //		this.m_SaveloadSystem = new SaveloadSystem();
    //		this.m_SaveloadSystem.Initialize();
    //		this.m_SkillSystem = new SkillSystem();
    //		this.m_SkillSystem.Initialize();
    //		this.m_MobGuardSystem = new MobGuardSystem();
    //		this.m_MobGuardSystem.Initialize();
    //		this.m_AchievementSystem = new AchievementSystem();
    //		this.m_AchievementSystem.Initialize();
    //		this.m_InheritSystem = new InheritSystem();
    //		this.m_InheritSystem.Initialize(this);
    //		this.m_MapPathSystem = new MapPathSystem();
    //		this.m_MapPathSystem.Initialize();
    //		this.m_WOPSystem = new WOPSystem();
    //		this.m_WOPSystem.Initialize();
    //		if (this.m_WOPSystem.IsDebug())
    //		{
    //			this.m_WOPSystem.InitForNewGame();
    //		}
    //		this.m_StorySystem = new StorySystem();
    //		this.m_StorySystem.Initialize();
    //		this.m_FightSystem = new FightSystem();
    //		this.m_FightSystem.Initialize();
    //		this.m_MusicControlSystem = new MusicControlSystem();
    //		this.m_MusicControlSystem.Initialize();
    //		this.m_QualitySettingSystem = new QualitySettingSystem();
    //		this.m_QualitySettingSystem.Initialize();
    //		this.m_NormalSettingSystem = new NormalSettingSystem();
    //		this.m_NormalSettingSystem.Initialize();
    //		this.m_SoundSettingSystem = new SoundSettingSystem();
    //		this.m_SoundSettingSystem.Initialize();
    //		this.m_ControlSettingSystem = new ControlSettingSystem();
    //		this.m_ControlSettingSystem.Initialize();
    //		this.m_MovieSystem = new MovieSystem();
    //		this.LoadSettings();
    //		this.m_QualitySettingSystem.UpdateGameQuality();
    //		this.m_ControlSettingSystem.UpdateControlSetting();
    //		this.SaveSettings(false);
    //		if (this.m_NormalSettingSystem.GetNormalSetting().m_IsDlC)
    //		{
    //			this.m_ChapID = 101;
    //		}
    //		else
    //		{
    //			this.m_ChapID = 100;
    //		}
    //		this.InitChapterData(this.m_ChapID);
    //	}

    //	private void InitGUI()
    //	{
    //		UnityEngine.Object.DontDestroyOnLoad(iGUIRoot.instance.gameObject);
    //		if (this.m_ResourceType == ENUM_ResourceType.Runtime)
    //		{
    //			GUIGenerator.GetFont();
    //		}
    //		GUIManager expr_23 = base.guiManager;
    //		expr_23.onMouseClickOnUI = (GUIManager.MouseClickOnUI)Delegate.Combine(expr_23.onMouseClickOnUI, new GUIManager.MouseClickOnUI(GUIManagerEvent.MouseClickOnUI));
    //		GUIManager expr_4A = base.guiManager;
    //		expr_4A.onMouseClickNotOnUI = (GUIManager.MouseClickNotOnUI)Delegate.Combine(expr_4A.onMouseClickNotOnUI, new GUIManager.MouseClickNotOnUI(GUIManagerEvent.MouseClickNotOnUI));
    //		base.guiManager.AddGUI(typeof(UI_Menu).Name, UI_Menu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_OutsideStory).Name, UI_OutsideStory.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Explore).Name, UI_Explore.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Fight).Name, UI_Fight.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_FinishFight).Name, UI_FinishFight.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Dragon).Name, UI_Dragon.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Hammer).Name, UI_Hammer.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Loading).Name, UI_Loading.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Pause).Name, UI_Pause.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Fade).Name, UI_Fade.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Subtitle).Name, UI_Subtitle.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Subtitle_FullScreen).Name, UI_Subtitle_FullScreen.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_StoryMovie).Name, UI_StoryMovie.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_ShowImage).Name, UI_ShowImage.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameMainMenu).Name, UI_GameMainMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameRoleMenu).Name, UI_GameRoleMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameItemMenu).Name, UI_GameItemMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameFlagMenu).Name, UI_GameFlagMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameSaveLoadMenu).Name, UI_GameSaveLoadMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameSkillMenu).Name, UI_GameSkillMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameMobBookMenu).Name, UI_GameMobBookMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameMobGuardMenu).Name, UI_GameMobGuardMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameQuestMenu).Name, UI_GameQuestMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameAchievementMenu).Name, UI_GameAchievementMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameAchievementMsg).Name, UI_GameAchievementMsg.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameNpcShopMenu).Name, UI_GameNpcShopMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameWeaponBookMenu).Name, UI_GameWeaponBookMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameMakeNameMenu).Name, UI_GameMakeNameMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameSystemSetting).Name, UI_GameSystemSetting.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameAlbum).Name, UI_GameAlbum.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameInheritMenu).Name, UI_GameInheritMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Inherit).Name, UI_Inherit.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_ZoneMap).Name, UI_ZoneMap.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_SmallMap).Name, UI_SmallMap.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_BigMap).Name, UI_BigMap.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_TalkDialog).Name, UI_TalkDialog.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_BubbleDialog).Name, UI_BubbleDialog.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_PartnerTalkDialog).Name, UI_PartnerTalkDialog.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_OkCancelBox).Name, UI_OkCancelBox.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_WOPMainMenu).Name, UI_WOPMainMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_WOPMonsterCompositeMenu).Name, UI_WOPMonsterCompositeMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_WOPManufactureMenu).Name, UI_WOPManufactureMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_WOPSevenRingMenu).Name, UI_WOPSevenRingMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_WOPRefineMenu).Name, UI_WOPRefineMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_WOPSpellTransferMenu).Name, UI_WOPSpellTransferMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_WOPUpgradeMenu).Name, UI_WOPUpgradeMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_WOPCenterMenu).Name, UI_WOPCenterMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_WOPArenaMenu).Name, UI_WOPArenaMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Unlock).Name, UI_Unlock.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_MemoryPuzzle).Name, UI_MemoryPuzzle.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_UserGuide).Name, UI_UserGuide.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_Help).Name, UI_Help.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_TIP).Name, UI_TIP.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_VERSION).Name, UI_VERSION.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameReviewStory).Name, UI_GameReviewStory.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameChangeZoneMenu).Name, UI_GameChangeZoneMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameNpcListMenu).Name, UI_GameNpcListMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameGetItemMenu).Name, UI_GameGetItemMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameGMMenu).Name, UI_GameGMMenu.Instance, 0);
    //		base.guiManager.AddGUI(typeof(UI_GameReviewStoryMenu).Name, UI_GameReviewStoryMenu.Instance, 0);
    //	}

    //	public void changeStateByLoading(string stateName)
    //	{
    //		SceneState sceneState = base.GetGameStateByName(stateName) as SceneState;
    //		string sceneName = sceneState.getSceneName();
    //		this.ChangeStateByLoading(stateName, sceneName);
    //	}

    //	public void ChangeStateByLoading(string stateName, string sceneName)
    //	{
    //		this.ClearResourceSystem();
    //		LoadingState loadingState = base.GetGameStateByName("LoadingState") as LoadingState;
    //		loadingState.setNextSceneState(stateName, sceneName);
    //		base.StartCoroutine(this.SwitchToLoading());
    //	}

    //	private IEnumerator SwitchToLoading()
    //	{
    //		AsyncOperation asyncOperation = Application.LoadLevelAsync("Loading");
    //		while (!asyncOperation.isDone)
    //		{
    //			yield return 0;
    //		}
    //		this.SwitchState("LoadingState");
    //		yield break;
    //	}

    //	public void ChangeMap(string stateName, int mapid, float x, float y, float z, float dir)
    //	{
    //		S_MapData mapData = this.m_GameDataSystem.GetMapData(mapid);
    //		if (mapData != null)
    //		{
    //			this.m_GameDataSystem.m_MapInfo.MapID = mapid;
    //			this.m_ExploreSystem.PlayerChangePos = new Vector3(x, y, z);
    //			this.m_ExploreSystem.PlayerChangeDir = dir;
    //			this.ChangeStateByLoading(stateName, mapData.Name);
    //			return;
    //		}
    //		UnityEngine.Debug.LogWarning("Map_" + mapid + " Load Error!!");
    //	}

    //	public void ChangeToExploreState(int mapid, float x, float y, float z, float dir)
    //	{
    //		if (mapid == this.m_GameDataSystem.m_MapInfo.MapID)
    //		{
    //			this.m_GameDataSystem.m_MapInfo.MapID = mapid;
    //			this.m_ExploreSystem.PlayerChangePos = new Vector3(x, y, z);
    //			this.m_ExploreSystem.PlayerChangeDir = dir;
    //			this.SwitchState("ExploreState");
    //			CameraControlSystem.FadeTo(0f, 1f);
    //			return;
    //		}
    //		this.ChangeMap("ExploreState", mapid, x, y, z, dir);
    //	}

    //	public void ChangeToStoryState(int mapID, string storyName)
    //	{
    //		string text = "StoryState";
    //		StoryState storyState = base.GetGameStateByName(text) as StoryState;
    //		if (storyState == null)
    //		{
    //			UnityEngine.Debug.LogError("Can not get StoryState");
    //			return;
    //		}
    //		GameState currentGameState = base.GetCurrentGameState();
    //		if (currentGameState == null)
    //		{
    //			UnityEngine.Debug.LogError("Can not get currentGameState");
    //			return;
    //		}
    //		storyState.SetStoryName(storyName);
    //		if (mapID == 100 && currentGameState is MenuState)
    //		{
    //			base.PushState(text);
    //			MenuState menuState = Swd6Application.instance.GetGameStateByName("MenuState") as MenuState;
    //			if (menuState != null)
    //			{
    //				menuState.DestroyBackground();
    //				return;
    //			}
    //		}
    //		else if (mapID == this.m_GameDataSystem.m_MapInfo.MapID)
    //		{
    //			if (currentGameState is StoryState)
    //			{
    //				storyState.CreateNewStory();
    //				return;
    //			}
    //			if (currentGameState is ExploreState)
    //			{
    //				base.PushState(text);
    //				return;
    //			}
    //			if (currentGameState is FightState)
    //			{
    //				this.SwitchState(text);
    //				return;
    //			}
    //			if (currentGameState is MenuState)
    //			{
    //				base.PushState(text);
    //				return;
    //			}
    //		}
    //		else
    //		{
    //			this.ChangeMap(text, mapID, 0f, 0f, 0f, 0f);
    //		}
    //	}

    //	public void ChangeToFightState()
    //	{
    //		base.PushState("FightState");
    //	}

    //	public void SwitchState(string stateName)
    //	{
    //		base.StartCoroutine(base.ChangeState(stateName));
    //	}

    //	public void DelayPopState(float time)
    //	{
    //		base.StartCoroutine(this.DoDelayPopState(time));
    //	}

    //	private IEnumerator DoDelayPopState(float time)
    //	{
    //		CameraControlSystem.FadeTo(1f, time);
    //		yield return new WaitForSeconds(time);
    //		base.PopState();
    //		CameraControlSystem.FadeTo(0f, time);
    //		yield break;
    //	}

    //	public void ChangeToOtherRealm(int mapId, float x, float y, float z)
    //	{
    //		base.StartCoroutine(this.DoChangeToOtherRealm(mapId, x, y, z));
    //	}

    //	private IEnumerator DoChangeToOtherRealm(int mapId, float x, float y, float z)
    //	{
    //		this.m_GameDataSystem.m_MapInfo.OrgMapID = this.m_GameDataSystem.m_MapInfo.MapID;
    //		this.m_GameDataSystem.m_MapInfo.OrgMapPosX = this.m_ExploreSystem.PlayerController.Pos.x;
    //		this.m_GameDataSystem.m_MapInfo.OrgMapPosY = this.m_ExploreSystem.PlayerController.Pos.y;
    //		this.m_GameDataSystem.m_MapInfo.OrgMapPosZ = this.m_ExploreSystem.PlayerController.Pos.z;
    //		this.m_GameDataSystem.m_MapInfo.OrgMapDir = this.m_ExploreSystem.PlayerController.Dir;
    //		CameraControlSystem.FadeTo(1f, 1f);
    //		yield return new WaitForSeconds(1f);
    //		this.ChangeMap("ExploreState", mapId, x, y, z, 0f);
    //		yield break;
    //	}

    //	public void ReloadObjc()
    //	{
    //		GameDataDB.LoadDBF();
    //		GameDataDB.LoadLanguage();
    //		this.m_GameDataSystem.ReLoadObjData();
    //		this.m_GameObjSystem.ReLoadMapObjData();
    //		this.m_GameObjSystem.ReAttackMapObjScript();
    //	}

    //	public void LoadMapMask(string name)
    //	{
    //		base.StartCoroutine(Swd6Application.instance.m_SaveloadSystem.LoadMapMask(name, UI_ZoneMap.Instance.GetMaskTexture()));
    //	}

    //	public void InitNewGame()
    //	{
    //		this.m_GameObjSystem.Clear();
    //		this.m_QuestSystem.Clear();
    //		this.m_SkillSystem.Clear();
    //		this.m_ItemSystem.Clear();
    //		this.m_MobGuardSystem.Clear();
    //		this.m_AchievementSystem.InitForNewGame();
    //		this.m_WOPSystem.InitForNewGame();
    //		this.m_GameObjSystem.Initialize();
    //	}

    //	public void InitChapterData(int chapId)
    //	{
    //		this.m_ChapID = chapId;
    //		switch (chapId)
    //		{
    //		case 100:
    //			this.m_AchievementSystem.m_Enable = true;
    //			UI_PartnerTalkDialog.Instance.Enable(true);
    //			break;
    //		case 101:
    //			this.m_AchievementSystem.m_Enable = false;
    //			UI_PartnerTalkDialog.Instance.Enable(false);
    //			break;
    //		}
    //		this.m_GameDataSystem.InitTeamRoleList(chapId);
    //		this.m_SaveloadSystem.SwitchSaveDirectory(chapId);
    //		GameMenuSystem.Instance.SetBackgroundImage();
    //	}

    //	public void StartNewGame()
    //	{
    //		GameObject gameObject = GameObject.Find("NewGameObject");
    //		this.m_GameDataSystem.InitRoleData(this.m_ChapID);
    //		if (gameObject != null)
    //		{
    //			if (!this.IsDLC())
    //			{
    //				gameObject.SendMessage("DoTalk", SendMessageOptions.DontRequireReceiver);
    //				return;
    //			}
    //			gameObject.SendMessage("DoTalkDLC", SendMessageOptions.DontRequireReceiver);
    //		}
    //	}

    //	public void StartExNewGame()
    //	{
    //		GameObject gameObject = GameObject.Find("NewGameObject");
    //		this.m_InheritSystem.PrepareInheritData();
    //		this.m_GameDataSystem.InitExRoleData(100);
    //		if (gameObject != null)
    //		{
    //			gameObject.SendMessage("DoTalk", SendMessageOptions.DontRequireReceiver);
    //		}
    //	}

    //	public IEnumerator ReturnToStartMenu()
    //	{
    //		this.m_SaveloadSystem.m_AutoSave = false;
    //		this.m_ExploreSystem.m_PlayStory = false;
    //		this.InitNewGame();
    //		this.m_GameDataSystem.m_MapInfo.MapID = 0;
    //		GameObject gameObject = GameObject.Find("ReviewStoryGameObject");
    //		if (gameObject != null)
    //		{
    //			UnityEngine.Object.DestroyObject(gameObject);
    //		}
    //		CameraControlSystem.FadeTo(1f, 1f);
    //		yield return new WaitForSeconds(1f);
    //		this.ClearResourceSystem();
    //		AsyncOperation asyncOperation = Application.LoadLevelAsync("Menu");
    //		while (!asyncOperation.isDone)
    //		{
    //			yield return 1;
    //		}
    //		Swd6Application.instance.SwitchState("MenuState");
    //		yield break;
    //	}

    //	public IEnumerator OpenGameMainMenu()
    //	{
    //		yield return base.StartCoroutine(this.BeginScreenShot(true));
    //		base.PushState("GameMenuState");
    //		yield return null;
    //		yield break;
    //	}

    //	public IEnumerator BeginScreenShot(bool scale)
    //	{
    //		this.m_CaptureScale = scale;
    //		yield return new WaitForEndOfFrame();
    //		try
    //		{
    //			int num = 0;
    //			if (this.m_ResourceType == ENUM_ResourceType.Runtime)
    //			{
    //				QualitySetting qualitySetting = QualitySettingSystem.Instance.GetQualitySetting();
    //				if (qualitySetting.m_IsMaxResolution)
    //				{
    //					num = GameInput.GetHeight();
    //				}
    //			}
    //			if (this.m_CaptureTexture == null)
    //			{
    //				this.m_CaptureTexture = new Texture2D(Screen.width, Screen.height - num, TextureFormat.RGB24, false, true);
    //			}
    //			this.m_CaptureTexture.ReadPixels(new Rect(0f, 0f, (float)Screen.width, (float)(Screen.height - num)), 0, 0);
    //			this.m_CaptureImageData = null;
    //		}
    //		catch (Exception ex)
    //		{
    //			UnityEngine.Debug.LogError("截圖失敗!!");
    //			UnityEngine.Debug.LogError(ex.Message);
    //		}
    //		yield return null;
    //		yield break;
    //	}

    //	public IEnumerator ResizeScreenShot()
    //	{
    //		if (this.m_CaptureTexture != null)
    //		{
    //			this.m_CaptureTexture.Apply();
    //			Texture2D texture2D = new Texture2D(Screen.width, this.m_CaptureTexture.height, TextureFormat.RGB24, false, true);
    //			texture2D.SetPixels32(this.m_CaptureTexture.GetPixels32());
    //			texture2D.Apply();
    //			if (this.m_CaptureScale)
    //			{
    //				TextureScale.Point(texture2D, 342, 256);
    //			}
    //			this.m_CaptureImageData = null;
    //			this.m_CaptureImageData = texture2D.EncodeToPNG();
    //			UnityEngine.Object.Destroy(texture2D);
    //		}
    //		yield return null;
    //		yield break;
    //	}

    //	public void SaveScreenShot(string filename)
    //	{
    //		if (this.m_CaptureImageData == null)
    //		{
    //			this.m_CaptureTexture.Apply();
    //			Texture2D texture2D = new Texture2D(Screen.width, this.m_CaptureTexture.height, TextureFormat.RGB24, false, true);
    //			texture2D.SetPixels32(this.m_CaptureTexture.GetPixels32());
    //			texture2D.Apply();
    //			if (this.m_CaptureScale)
    //			{
    //				TextureScale.Point(texture2D, 342, 256);
    //			}
    //			this.m_CaptureImageData = texture2D.EncodeToPNG();
    //			UnityEngine.Object.Destroy(texture2D);
    //		}
    //		File.WriteAllBytes(filename, this.m_CaptureImageData);
    //	}

    //	public void SaveScreenShot(string filename, string smallFilename)
    //	{
    //		if (this.m_CaptureTexture == null)
    //		{
    //			return;
    //		}
    //		this.m_CaptureTexture.Apply();
    //		this.m_CaptureImageData = this.m_CaptureTexture.EncodeToPNG();
    //		File.WriteAllBytes(filename, this.m_CaptureImageData);
    //		Texture2D texture2D = new Texture2D(Screen.width, this.m_CaptureTexture.height, TextureFormat.RGB24, false, true);
    //		texture2D.SetPixels32(this.m_CaptureTexture.GetPixels32());
    //		texture2D.Apply();
    //		TextureScale.Point(texture2D, 342, 256);
    //		this.m_CaptureImageData = texture2D.EncodeToPNG();
    //		UnityEngine.Object.Destroy(texture2D);
    //		if (this.m_CaptureImageData == null)
    //		{
    //			return;
    //		}
    //		File.WriteAllBytes(smallFilename, this.m_CaptureImageData);
    //	}

    //	public void ClearScreenShot()
    //	{
    //		this.m_CaptureImageData = null;
    //		GC.Collect();
    //	}

    //	public void ReSize()
    //	{
    //		if (this.m_CaptureTexture != null)
    //		{
    //			UnityEngine.Object.Destroy(this.m_CaptureTexture);
    //			this.m_CaptureTexture = null;
    //		}
    //		this.m_UpdateResizeTime = 2f;
    //	}

    //	public void LoadSettings()
    //	{
    //		if (!File.Exists(Application.dataPath + "/../settings.ini"))
    //		{
    //			this.SaveSettings(true);
    //		}
    //		IConfigSource configSource = new IniConfigSource(Application.dataPath + "/../settings.ini");
    //		IConfig config = configSource.Configs["Video"];
    //		QualitySetting qualitySetting = this.m_QualitySettingSystem.GetQualitySetting();
    //		qualitySetting.m_ResolutionWidth = config.GetInt("ResolutionWidth", 1024);
    //		qualitySetting.m_ResolutionHeight = config.GetInt("ResolutionHeight", 768);
    //		qualitySetting.m_EnableFullScreen = config.GetBoolean("FullScreen", false);
    //		qualitySetting.m_QuilitySetting = (Enum_QuilitySettingType)config.GetInt("QuickQuality", 2);
    //		qualitySetting.m_EnableCustomization = config.GetBoolean("Customization", false);
    //		qualitySetting.m_EnableChinesePaint = config.GetBoolean("ChinesePaint", false);
    //		qualitySetting.m_EnableColor = config.GetBoolean("Color", false);
    //		qualitySetting.m_TextureQuality = (Enum_TextureQuality)config.GetInt("TextureQuality", 2);
    //		qualitySetting.m_LayerCull = (Enum_LayerCull)config.GetInt("LayerCulling", 2);
    //		qualitySetting.m_ShadowQuality = (Enum_ShadowQuality)config.GetInt("ShadowQuality", 2);
    //		qualitySetting.m_EnableVsync = config.GetBoolean("Vsync", false);
    //		qualitySetting.m_EnableAA = config.GetBoolean("AntiAliasing", false);
    //		qualitySetting.m_ShadowDistance = config.GetInt("ShadowDistance", 50);
    //		qualitySetting.m_ViewDistance = config.GetInt("ViewDistance", 50);
    //		qualitySetting.m_EnableDOF = config.GetBoolean("DOF", true);
    //		qualitySetting.m_EnableBloom = config.GetBoolean("Bloom", true);
    //		qualitySetting.m_EnableSunShaft = config.GetBoolean("SunShaft", true);
    //		qualitySetting.m_EnableHDR = config.GetBoolean("HDR", true);
    //		qualitySetting.m_EnableSSAO = config.GetBoolean("SSAO", true);
    //		qualitySetting.m_EnableTonemapping = config.GetBoolean("Tonemapping", true);
    //		qualitySetting.m_EnableContrastEnhance = config.GetBoolean("Contrast", true);
    //		qualitySetting.m_RecommendQuality = config.GetInt("RecommendQuality", 5);
    //		IConfig config2 = configSource.Configs["Settings"];
    //		if (config2 == null)
    //		{
    //			config2 = configSource.Configs.Add("Settings");
    //			config2.Set("Version", 1);
    //			configSource.Save();
    //		}
    //		int @int = config2.GetInt("Version", 1);
    //		if (@int < 2)
    //		{
    //			qualitySetting.Reset();
    //			config2.Set("Version", 2);
    //			configSource.Save();
    //		}
    //		IConfig config3 = configSource.Configs["Game"];
    //		NormalSetting normalSetting = this.m_NormalSettingSystem.GetNormalSetting();
    //		normalSetting.m_bEnableTextInCG = config3.GetBoolean("TextInCG", true);
    //		normalSetting.m_bEnableTextInStory = config3.GetBoolean("TextInStory", true);
    //		normalSetting.m_bEnableTextFrameInCG = config3.GetBoolean("TextFrameInCG", true);
    //		normalSetting.m_bEnableTextFrameInStory = config3.GetBoolean("TextFrameInStory", true);
    //		normalSetting.m_TextSpeedWordsPerSec = config3.GetInt("TextSpeedWordsPerSec", 5);
    //		normalSetting.m_SectionWaitingTimeType = config3.GetInt("SectionWaitingTimeType", 3);
    //		normalSetting.m_bEnableAchivementHint = config3.GetBoolean("AchivementHint", true);
    //		normalSetting.m_bEnableMazeClickMove = config3.GetBoolean("MazeClickMove", true);
    //		normalSetting.m_bEnableFightingHPBar = config3.GetBoolean("FightingHPBar", true);
    //		normalSetting.m_bEnableFightStop = config3.GetBoolean("FightStop", true);
    //		normalSetting.m_bEnableFightListAutoPop = config3.GetBoolean("FightListAutoPop", false);
    //		normalSetting.m_FightingSpeed = config3.GetInt("FightingSpeed", 0);
    //		normalSetting.m_FightingSpeed = Mathf.Clamp(normalSetting.m_FightingSpeed, -3, 6);
    //		normalSetting.m_emCharacterStyle = (Enum_CharacterStyle)config3.GetInt("CharacterStyle", 0);
    //		normalSetting.m_bEnableName = config3.GetBoolean("Naming", false);
    //		normalSetting.m_bEnableQuestHint = config3.GetBoolean("QuestHint", true);
    //		normalSetting.m_bEnableTeach = config3.GetBoolean("Teach", true);
    //		normalSetting.m_bEnableJoystick = config3.GetBoolean("Joystick", true);
    //		GameInput.JoyStick = normalSetting.m_bEnableJoystick;
    //		normalSetting.m_IsDlC = config3.GetBoolean("IsDLC", false);
    //		IConfig config4 = configSource.Configs["Sound"];
    //		SoundSetting soundSetting = this.m_SoundSettingSystem.GetSoundSetting();
    //		soundSetting.m_MusicVolumeRate = config4.GetInt("MusicVolumeRate", 70);
    //		soundSetting.m_EffectVolumeRate = config4.GetInt("EffectVolumeRate", 100);
    //		soundSetting.m_SpeechVolumeRate = config4.GetInt("SpeechVolumeRate", 100);
    //		soundSetting.m_EnvironmentVolumeRate = config4.GetInt("EnvironmentVolumeRate", 100);
    //		soundSetting.m_MenuVolumeRate = config4.GetInt("MenuVolumeRate", 70);
    //		IConfig config5 = configSource.Configs["Control"];
    //		Dictionary<KEY_ACTION, KeyCode> keyList = this.m_ControlSettingSystem.GetKeyList();
    //		keyList[KEY_ACTION.UP] = (KeyCode)config5.GetInt("UP", 119);
    //		keyList[KEY_ACTION.DOWN] = (KeyCode)config5.GetInt("DOWN", 115);
    //		keyList[KEY_ACTION.LEFT] = (KeyCode)config5.GetInt("LEFT", 97);
    //		keyList[KEY_ACTION.RIGHT] = (KeyCode)config5.GetInt("RIGHT", 100);
    //		keyList[KEY_ACTION.CAMERA_UP] = (KeyCode)config5.GetInt("CAMERA_UP", 61);
    //		keyList[KEY_ACTION.CAMERA_DOWN] = (KeyCode)config5.GetInt("CAMERA_DOWN", 45);
    //		keyList[KEY_ACTION.CAMERA_LEFT] = (KeyCode)config5.GetInt("CAMERA_LEFT", 113);
    //		keyList[KEY_ACTION.CAMERA_RIGHT] = (KeyCode)config5.GetInt("CAMERA_RIGHT", 101);
    //		keyList[KEY_ACTION.CAMERA_IN] = (KeyCode)config5.GetInt("CAMERA_IN", 93);
    //		keyList[KEY_ACTION.CAMERA_OUT] = (KeyCode)config5.GetInt("CAMERA_OUT", 91);
    //		keyList[KEY_ACTION.CONFIRM] = (KeyCode)config5.GetInt("CONFIRM", 13);
    //		keyList[KEY_ACTION.JUMP] = (KeyCode)config5.GetInt("JUMP", 32);
    //		keyList[KEY_ACTION.TAB] = (KeyCode)config5.GetInt("TAB", 9);
    //		keyList[KEY_ACTION.DASH] = (KeyCode)config5.GetInt("DASH", 120);
    //		keyList[KEY_ACTION.ACTION] = (KeyCode)config5.GetInt("ACTION", 102);
    //		keyList[KEY_ACTION.ROLE_LEFT] = (KeyCode)config5.GetInt("ROLE_LEFT", 44);
    //		keyList[KEY_ACTION.ROLE_RIGHT] = (KeyCode)config5.GetInt("ROLE_RIGHT", 46);
    //		keyList[KEY_ACTION.MAP] = (KeyCode)config5.GetInt("MAP", 109);
    //		keyList[KEY_ACTION.RUN] = (KeyCode)config5.GetInt("RUN", 114);
    //	}

    //	public void SaveSettings(bool isNewSettings)
    //	{
    //		IniConfigSource iniConfigSource = new IniConfigSource();
    //		if (!isNewSettings)
    //		{
    //			iniConfigSource.Load(Application.dataPath + "/../settings.ini");
    //		}
    //		else
    //		{
    //			iniConfigSource.Configs.Add("Settings");
    //			iniConfigSource.Configs.Add("Video");
    //			iniConfigSource.Configs.Add("Game");
    //			iniConfigSource.Configs.Add("Sound");
    //			iniConfigSource.Configs.Add("Control");
    //		}
    //		IConfig config = iniConfigSource.Configs["Video"];
    //		QualitySetting qualitySetting = this.m_QualitySettingSystem.GetQualitySetting();
    //		config.Set("ResolutionWidth", qualitySetting.m_ResolutionWidth);
    //		config.Set("ResolutionHeight", qualitySetting.m_ResolutionHeight);
    //		config.Set("FullScreen", qualitySetting.m_EnableFullScreen);
    //		config.Set("QuickQuality", (int)qualitySetting.m_QuilitySetting);
    //		config.Set("Customization", qualitySetting.m_EnableCustomization);
    //		config.Set("Color", qualitySetting.m_EnableColor);
    //		config.Set("ChinesePaint", qualitySetting.m_EnableChinesePaint);
    //		config.Set("TextureQuality", (int)qualitySetting.m_TextureQuality);
    //		config.Set("LayerCulling", (int)qualitySetting.m_LayerCull);
    //		config.Set("ShadowQuality", (int)qualitySetting.m_ShadowQuality);
    //		config.Set("Vsync", qualitySetting.m_EnableVsync);
    //		config.Set("AntiAliasing", qualitySetting.m_EnableAA);
    //		config.Set("ShadowDistance", qualitySetting.m_ShadowDistance);
    //		config.Set("ViewDistance", qualitySetting.m_ViewDistance);
    //		config.Set("DOF", qualitySetting.m_EnableDOF);
    //		config.Set("Bloom", qualitySetting.m_EnableBloom);
    //		config.Set("SunShaft", qualitySetting.m_EnableSunShaft);
    //		config.Set("HDR", qualitySetting.m_EnableHDR);
    //		config.Set("Tonemapping", qualitySetting.m_EnableTonemapping);
    //		config.Set("SSAO", qualitySetting.m_EnableSSAO);
    //		config.Set("Contrast", qualitySetting.m_EnableContrastEnhance);
    //		if (isNewSettings)
    //		{
    //			config.Set("RecommendQuality", 5);
    //		}
    //		IConfig config2 = iniConfigSource.Configs["Game"];
    //		NormalSetting normalSetting = this.m_NormalSettingSystem.GetNormalSetting();
    //		config2.Set("TextInCG", normalSetting.m_bEnableTextInCG);
    //		config2.Set("TextInStory", normalSetting.m_bEnableTextInStory);
    //		config2.Set("TextFrameInCG", normalSetting.m_bEnableTextFrameInCG);
    //		config2.Set("TextFrameInStory", normalSetting.m_bEnableTextFrameInStory);
    //		config2.Set("TextSpeedWordsPerSec", normalSetting.m_TextSpeedWordsPerSec);
    //		config2.Set("SectionWaitingTimeType", normalSetting.m_SectionWaitingTimeType);
    //		config2.Set("AchivementHint", normalSetting.m_bEnableAchivementHint);
    //		config2.Set("MazeClickMove", normalSetting.m_bEnableMazeClickMove);
    //		config2.Set("FightingHPBar", normalSetting.m_bEnableFightingHPBar);
    //		config2.Set("FightStop", normalSetting.m_bEnableFightStop);
    //		config2.Set("FightListAutoPop", normalSetting.m_bEnableFightListAutoPop);
    //		config2.Set("FightingSpeed", normalSetting.m_FightingSpeed);
    //		config2.Set("CharacterStyle", (int)normalSetting.m_emCharacterStyle);
    //		config2.Set("Naming", normalSetting.m_bEnableName);
    //		config2.Set("QuestHint", normalSetting.m_bEnableQuestHint);
    //		config2.Set("Teach", normalSetting.m_bEnableTeach);
    //		config2.Set("Joystick", normalSetting.m_bEnableJoystick);
    //		config2.Set("IsDLC", normalSetting.m_IsDlC);
    //		IConfig config3 = iniConfigSource.Configs["Sound"];
    //		SoundSetting soundSetting = this.m_SoundSettingSystem.GetSoundSetting();
    //		config3.Set("MusicVolumeRate", soundSetting.m_MusicVolumeRate);
    //		config3.Set("EffectVolumeRate", soundSetting.m_EffectVolumeRate);
    //		config3.Set("SpeechVolumeRate", soundSetting.m_SpeechVolumeRate);
    //		config3.Set("EnvironmentVolumeRate", soundSetting.m_EnvironmentVolumeRate);
    //		config3.Set("MenuVolumeRate", soundSetting.m_MenuVolumeRate);
    //		IConfig config4 = iniConfigSource.Configs["Control"];
    //		Dictionary<KEY_ACTION, KeyCode> keyList = this.m_ControlSettingSystem.GetKeyList();
    //		config4.Set("UP", (int)keyList[KEY_ACTION.UP]);
    //		config4.Set("DOWN", (int)keyList[KEY_ACTION.DOWN]);
    //		config4.Set("LEFT", (int)keyList[KEY_ACTION.LEFT]);
    //		config4.Set("RIGHT", (int)keyList[KEY_ACTION.RIGHT]);
    //		config4.Set("CAMERA_UP", (int)keyList[KEY_ACTION.CAMERA_UP]);
    //		config4.Set("CAMERA_DOWN", (int)keyList[KEY_ACTION.CAMERA_DOWN]);
    //		config4.Set("CAMERA_LEFT", (int)keyList[KEY_ACTION.CAMERA_LEFT]);
    //		config4.Set("CAMERA_RIGHT", (int)keyList[KEY_ACTION.CAMERA_RIGHT]);
    //		config4.Set("CAMERA_IN", (int)keyList[KEY_ACTION.CAMERA_IN]);
    //		config4.Set("CAMERA_OUT", (int)keyList[KEY_ACTION.CAMERA_OUT]);
    //		config4.Set("CONFIRM", (int)keyList[KEY_ACTION.CONFIRM]);
    //		config4.Set("JUMP", (int)keyList[KEY_ACTION.JUMP]);
    //		config4.Set("TAB", (int)keyList[KEY_ACTION.TAB]);
    //		config4.Set("DASH", (int)keyList[KEY_ACTION.DASH]);
    //		config4.Set("ACTION", (int)keyList[KEY_ACTION.ACTION]);
    //		config4.Set("ROLE_LEFT", (int)keyList[KEY_ACTION.ROLE_LEFT]);
    //		config4.Set("ROLE_RIGHT", (int)keyList[KEY_ACTION.ROLE_RIGHT]);
    //		config4.Set("MAP", (int)keyList[KEY_ACTION.MAP]);
    //		config4.Set("RUN", (int)keyList[KEY_ACTION.RUN]);
    //		if (isNewSettings)
    //		{
    //			iniConfigSource.Save(Application.dataPath + "/../settings.ini");
    //			return;
    //		}
    //		iniConfigSource.Save();
    //	}
}
