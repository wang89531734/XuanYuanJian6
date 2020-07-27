using System;
using System.Collections;
using UnityEngine;

namespace GameFramework
{
    public abstract class GameApplication : MonoBehaviour
    {
        //		protected static GameApplication _instance;

        //		public GameStateService gameStateService
        //		{
        //			get;
        //			private set;
        //		}

        //		public IMessageService messageService
        //		{
        //			get;
        //			private set;
        //		}

        //		public TimeScheduleSystem timeScheduleSystem
        //		{
        //			get;
        //			private set;
        //		}

        //		public GUIManager guiManager
        //		{
        //			get;
        //			private set;
        //		}

        //		public bool isPause
        //		{
        //			get;
        //			protected set;
        //		}

        //		protected virtual void Awake()
        //		{
        //			GameApplication._instance = this;
        //			this.isPause = false;
        //			UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
        //		}

        //		public virtual void Start()
        //		{
        //			this.gameStateService = new GameStateService();
        //			this.guiManager = GUIManager.instance;
        //			GUIManager.instance.Initialize();
        //			this.initialize();
        //		}

        //		public virtual void Update()
        //		{
        //			this.gameStateService.update();
        //			this.guiManager.ForceUpdate();
        //			this.guiManager.Update();
        //		}

        //		public virtual void OnGUI()
        //		{
        //		}

        //		protected abstract void initialize();

        //		public void Pause()
        //		{
        //			Time.timeScale = 0f;
        //			this.isPause = true;
        //		}

        //		public void ReStart()
        //		{
        //			Time.timeScale = 1f;
        //			this.isPause = false;
        //		}

        //		public void AddGameState(GameState gameState)
        //		{
        //			this.gameStateService.addState(gameState);
        //		}

        //		public IEnumerator ChangeState(string nextStateName)
        //		{
        //			yield return 0;
        //			this.gameStateService.changeState(nextStateName);
        //			yield break;
        //		}

        //		public void PushState(string pushStateName)
        //		{
        //			this.gameStateService.pushState(pushStateName);
        //		}

        //		public void PopState()
        //		{
        //			this.gameStateService.popState();
        //		}

        //		public GameState GetGameStateByName(string stateName)
        //		{
        //			return this.gameStateService.getState(stateName);
        //		}

        //		public GameState GetCurrentGameState()
        //		{
        //			return this.gameStateService.getCurrentState();
        //		}
    }
}
