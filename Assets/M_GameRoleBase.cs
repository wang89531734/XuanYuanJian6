using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(M_GameRoleMotion))]
public abstract class M_GameRoleBase : MonoBehaviour
{
	public delegate void EventHandler(GameObject e);

	public delegate IEnumerator EventHandlerTalk();

	public delegate bool EventHandlerMoveOver(Vector3 e);

	protected int m_RoleId;

	protected GameObjState m_RoleState = new GameObjState();

	protected S_GameObjData m_GameObjData;

	protected GameObject m_PrefObj;

	protected Vector3 m_HidePos = Vector3.zero;

	protected Vector3 m_MoveDirection = Vector3.zero;

	protected Vector3 m_RotateDirection = Vector3.zero;

	protected Vector3 m_MoveDesPos = Vector3.zero;

	protected float m_DesiredDistance = 0.1f;

	public float m_MoveSpeed = 1f;

	protected float m_TurnAngle;

	protected float m_TurnSpeed;

	public int m_MotionID;

	public int m_TalkMotionID;

	public int m_TurnMotion;

	public int m_WaitMotion;

	protected float m_Alpha = 1f;

	protected bool m_FadeIn;

	public bool m_EnterTalk;

	public bool m_DontRemoveScript;

	protected GameObject m_QuestHint;

	protected Component m_QuestTalk;

	protected S_NpcData m_NpcData;

	public M_GameRoleMotion m_RoleMotion;

	public M_GameRoleBase.EventHandler OnTalkDelegate;

	public M_GameRoleBase.EventHandlerTalk OnTalkBeforEventDelegate;

	public M_GameRoleBase.EventHandlerTalk OnTalkAfterEventDelegate;

	public M_GameRoleBase.EventHandlerMoveOver OnFollowBoundDelegate;

	public M_GameRoleBase.EventHandlerTalk OnTalkAfterTrigger;

	public int RoleID
	{
		get
		{
			return this.m_RoleId;
		}
		set
		{
			this.m_RoleId = value;
		}
	}

	public GameObjState RoleState
	{
		get
		{
			return this.m_RoleState;
		}
	}

	public bool MoveRole
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.Move);
		}
		set
		{
			if (value)
			{
				this.m_RoleState.Set(ENUM_GameObjFlag.Move);
				return;
			}
			this.m_RoleState.Reset(ENUM_GameObjFlag.Move);
		}
	}

	public bool PauseMove
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.PauseMove);
		}
		set
		{
			if (value)
			{
				this.m_RoleState.Set(ENUM_GameObjFlag.PauseMove);
				return;
			}
			this.m_RoleState.Reset(ENUM_GameObjFlag.PauseMove);
		}
	}

	public bool DisableRole
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.Disable);
		}
		set
		{
			if (value)
			{
				this.OnExitFocus();
				this.CheckQuestState();
				ExploreMiniMapSystem.Instance.UpdateQuestIcon(this.RoleID);
				this.m_RoleState.Set(ENUM_GameObjFlag.Disable);
				UnityEngine.Object.DestroyObject(base.gameObject);
				this.m_GameObjData.GameObj = null;
				return;
			}
			this.m_RoleState.Reset(ENUM_GameObjFlag.Disable);
			if (this.m_GameObjData.GameObj == null)
			{
				Swd6Application.instance.m_GameObjSystem.LoadObj(this.m_GameObjData);
			}
		}
	}

	public virtual bool HideRole
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.Hide);
		}
		set
		{
			if (value)
			{
				this.m_RoleState.Set(ENUM_GameObjFlag.Hide);
			}
			else
			{
				this.m_RoleState.Reset(ENUM_GameObjFlag.Hide);
				if (this.m_NpcData != null && this.m_NpcData.emType == ENUM_NpcType.Treasure)
				{
					ExploreMiniMapSystem.Instance.UpdateQuestIcon(this.RoleID);
				}
			}
			this.HideGameObj(value);
			if (this.m_GameObjData == null)
			{
				return;
			}
			this.CheckQuestState();
		}
	}

	public bool NoCollider
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.NoCollider);
		}
		set
		{
			if (value)
			{
				this.m_EnterTalk = false;
				this.m_RoleState.Set(ENUM_GameObjFlag.NoCollider);
			}
			else
			{
				this.m_RoleState.Reset(ENUM_GameObjFlag.NoCollider);
			}
			Collider[] componentsInChildren = base.gameObject.GetComponentsInChildren<Collider>();
			if (componentsInChildren != null)
			{
				Collider[] array = componentsInChildren;
				for (int i = 0; i < array.Length; i++)
				{
					Collider collider = array[i];
					collider.enabled = !value;
				}
			}
			MeshCollider[] componentsInChildren2 = base.gameObject.GetComponentsInChildren<MeshCollider>();
			if (componentsInChildren2 != null)
			{
				MeshCollider[] array2 = componentsInChildren2;
				for (int j = 0; j < array2.Length; j++)
				{
					MeshCollider meshCollider = array2[j];
					meshCollider.enabled = !value;
					meshCollider.isTrigger = value;
				}
			}
			CharacterController component = base.gameObject.GetComponent<CharacterController>();
			if (component != null)
			{
				component.enabled = !value;
			}
		}
	}

	public virtual bool FadeOut
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.FadeOut);
		}
		set
		{
			if (value)
			{
				this.m_FadeIn = false;
				this.m_Alpha = 1f;
				this.m_RoleState.Set(ENUM_GameObjFlag.FadeOut);
				return;
			}
			this.m_FadeIn = true;
			this.m_Alpha = 0f;
			this.m_RoleState.Reset(ENUM_GameObjFlag.FadeOut);
		}
	}

	public bool Run
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.Run);
		}
		set
		{
			if (value)
			{
				this.m_RoleState.Set(ENUM_GameObjFlag.Run);
				return;
			}
			this.m_RoleState.Reset(ENUM_GameObjFlag.Run);
		}
	}

	public bool TurnRole
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.Turn);
		}
		set
		{
			if (value)
			{
				this.m_RoleState.Set(ENUM_GameObjFlag.Turn);
				return;
			}
			this.m_RoleState.Reset(ENUM_GameObjFlag.Turn);
		}
	}

	public bool Open
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.Open);
		}
		set
		{
			if (value)
			{
				this.m_RoleState.Set(ENUM_GameObjFlag.Open);
				return;
			}
			this.m_RoleState.Reset(ENUM_GameObjFlag.Open);
		}
	}

	public bool TalkTurn
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.TalkTurn);
		}
		set
		{
			if (value)
			{
				this.m_RoleState.Set(ENUM_GameObjFlag.TalkTurn);
				return;
			}
			this.m_RoleState.Reset(ENUM_GameObjFlag.TalkTurn);
		}
	}

	public bool Talk
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.Talk);
		}
		set
		{
			if (value)
			{
				this.m_RoleState.Set(ENUM_GameObjFlag.Talk);
				return;
			}
			this.m_RoleState.Reset(ENUM_GameObjFlag.Talk);
		}
	}

	public bool Pick
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.Pick);
		}
		set
		{
			if (value)
			{
				this.m_RoleState.Set(ENUM_GameObjFlag.Pick);
				return;
			}
			this.m_RoleState.Reset(ENUM_GameObjFlag.Pick);
		}
	}

	public bool Grouund
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.Ground);
		}
		set
		{
			if (value)
			{
				this.m_RoleState.Set(ENUM_GameObjFlag.Ground);
				return;
			}
			this.m_RoleState.Reset(ENUM_GameObjFlag.Ground);
		}
	}

	public int Motion
	{
		get
		{
			return this.m_RoleMotion.GetMotion();
		}
		set
		{
			this.SetMotion(value);
		}
	}

	public bool Invalid
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.Invalid);
		}
		set
		{
			if (value)
			{
				this.m_RoleState.Set(ENUM_GameObjFlag.Invalid);
				return;
			}
			this.m_RoleState.Reset(ENUM_GameObjFlag.Invalid);
		}
	}

	public bool View
	{
		get
		{
			return this.m_RoleState.Test(ENUM_GameObjFlag.View);
		}
		set
		{
			if (value)
			{
				this.m_RoleState.Set(ENUM_GameObjFlag.View);
				return;
			}
			this.m_RoleState.Reset(ENUM_GameObjFlag.View);
		}
	}

	public float Dir
	{
		get
		{
			return base.transform.eulerAngles.y;
		}
		set
		{
			this.SetRotate(0f, value, 0f);
		}
	}

	public Vector3 Pos
	{
		get
		{
			return this.GetPos();
		}
		set
		{
			this.SetPos(value);
		}
	}

	public float MoveSpeed
	{
		get
		{
			return this.m_MoveSpeed;
		}
		set
		{
			this.m_MoveSpeed = value;
		}
	}

	private void Start()
	{
		if (Swd6Application.instance)
		{
			Swd6Application instance = Swd6Application.instance;
			int mapID = instance.m_GameDataSystem.m_MapInfo.MapID;
			string text = base.gameObject.name;
			if (base.gameObject.transform.parent != null)
			{
				text = base.gameObject.transform.parent.name;
			}
			if (text.Contains("NPC_"))
			{
				text = text.Replace("NPC_", "");
				this.RoleID = int.Parse(text);
				this.m_NpcData = GameDataDB.NpcDB.GetData(this.RoleID);
				if (this.m_NpcData == null)
				{
					Debug.Log("找不到NPC資料_" + this.RoleID);
				}
				if (!instance.m_GameObjSystem.CheckGameObjData(this.RoleID))
				{
					int num = (int)this.m_RoleState.Get();
					string.Format(string.Concat(new object[]
					{
						base.gameObject.name,
						"+",
						this.RoleID,
						"+",
						mapID,
						"+",
						num.ToString("X")
					}), new object[0]);
					this.LoadPrefabModel();
					this.m_GameObjData = new S_GameObjData(this.RoleID, mapID, this.GetPos(), this.GetDir(), this.m_NpcData.Motion, this.m_RoleState, base.gameObject);
					instance.m_GameObjSystem.AddGameObjData(this.m_GameObjData);
				}
				else
				{
					this.m_GameObjData = instance.m_GameObjSystem.GetObjData(this.RoleID);
					if (this.m_GameObjData != null)
					{
						this.LoadPrefabModel();
						this.m_GameObjData.GameObj = base.gameObject;
						int num2 = (int)this.m_GameObjData.State.Get();
						string.Format(string.Concat(new object[]
						{
							base.gameObject.name,
							"+",
							this.m_GameObjData.MapId,
							"+",
							num2.ToString("X")
						}), new object[0]);
						this.m_RoleState = this.m_GameObjData.State;
						if (mapID != this.m_GameObjData.MapId)
						{
							UnityEngine.Object.Destroy(base.gameObject);
							return;
						}
						if (this.m_GameObjData.Pos == Vector3.zero)
						{
							this.m_GameObjData.Pos = this.GetPos();
						}
						this.SetPos(this.m_GameObjData.Pos);
						if (this.m_GameObjData.Dir != 1000f)
						{
							this.Dir = this.m_GameObjData.Dir;
						}
						if (this.HideRole)
						{
							this.HideRole = true;
						}
						if (this.NoCollider)
						{
							this.NoCollider = true;
						}
						if (this.DisableRole && this.m_NpcData.emType != ENUM_NpcType.Egg)
						{
							if (this.Open)
							{
								ExploreMiniMapSystem.Instance.ChangeToOpenIcon(this.RoleID);
							}
							UnityEngine.Object.Destroy(base.gameObject);
						}
					}
				}
			}
		}
		if (this.m_NpcData != null)
		{
			this.m_RoleMotion = base.GetComponent<M_GameRoleMotion>();
			if (this.m_RoleMotion != null)
			{
				this.m_RoleMotion.Init(this.RoleID, this.m_GameObjData.Motion);
			}
			if (this.m_NpcData.Talk != null)
			{
				base.gameObject.AddComponent(this.m_NpcData.Talk);
			}
		}
		ExploreMiniMapSystem.Instance.CreateNpcIcon(this.RoleID);
		this.initialize();
	}

	private void OnDestroy()
	{
		if (this.m_QuestHint)
		{
			UnityEngine.Object.DestroyObject(this.m_QuestHint);
		}
		this.m_QuestHint = null;
	}

	protected abstract void initialize();

	public void SetMotion(int id)
	{
		this.m_MotionID = id;
		if (this.m_RoleMotion != null)
		{
			if (this.m_NpcData != null)
			{
				if (this.m_NpcData.emType == ENUM_NpcType.Role)
				{
					this.m_RoleMotion.SetCrossMotion(this.m_MotionID);
				}
				else
				{
					this.m_RoleMotion.SetMotion(this.m_MotionID);
				}
			}
			else
			{
				this.m_RoleMotion.SetMotion(this.m_MotionID);
			}
			this.m_GameObjData.Motion = id;
		}
	}

	public void SetMotionQueued(int id)
	{
		this.m_MotionID = id;
		this.m_RoleMotion.SetMotionQueued(id);
		this.m_GameObjData.Motion = id;
	}

	public bool IsMotionFinished(int id)
	{
		return this.m_RoleMotion.IsMotionFinished(id);
	}

	public void SetPos(Vector3 pos)
	{
		base.transform.position = pos;
	}

	public void SetPos(float x, float y, float z)
	{
		this.SetPos(new Vector3(x, y, z));
	}

	public Vector3 GetPos()
	{
		return base.transform.position;
	}

	public void Translate(float x, float y, float z)
	{
		if (base.transform.parent != null)
		{
			base.transform.parent.Translate(x, y, z);
			return;
		}
		base.transform.Translate(x, y, z);
	}

	public void SetRotate(float xAngle, float yAngle, float zAngle)
	{
		base.transform.eulerAngles = new Vector3(xAngle, yAngle, zAngle);
	}

	public void SetDir(float dir)
	{
		this.SetRotate(0f, dir, 0f);
	}

	public float GetDir()
	{
		return base.transform.eulerAngles.y;
	}

	public virtual void Update()
	{
		if (this.FadeOut)
		{
			if (!this.HideRole)
			{
				this.m_Alpha -= Time.deltaTime * 0.5f;
				if (this.m_Alpha < 0f)
				{
					this.m_Alpha = 0f;
				}
				this.AlphaObj(this.m_Alpha);
				if (this.m_Alpha <= 0f)
				{
					this.HideRole = true;
					return;
				}
			}
		}
		else if (!this.HideRole && this.m_FadeIn)
		{
			this.m_Alpha += Time.deltaTime * 0.5f;
			if (this.m_Alpha > 1f)
			{
				this.m_Alpha = 1f;
			}
			this.AlphaObj(this.m_Alpha);
			if (this.m_Alpha >= 1f)
			{
				this.m_FadeIn = false;
				this.RestoreOrgShader("Softstar/Bump Specular");
			}
		}
	}

	public void UpdateTurn()
	{
		if (this.TurnRole && this.Turn(this.m_TurnAngle, this.m_TurnSpeed))
		{
			this.TurnRole = false;
			if (this.m_RoleMotion == null)
			{
				return;
			}
			if (this.m_WaitMotion > 0)
			{
				this.m_RoleMotion.SetMotion(this.m_WaitMotion);
				this.m_WaitMotion = 0;
				return;
			}
			if (this.TalkTurn)
			{
				this.TalkTurn = false;
				if (this.m_TalkMotionID > 0)
				{
					this.m_RoleMotion.SetMotion(this.m_TalkMotionID);
					this.m_RoleMotion.SetMotionQueued(this.m_NpcData.Motion);
					return;
				}
				this.m_RoleMotion.SetMotion(this.m_NpcData.Motion);
			}
		}
	}

	public void SetTurn(float flAngle, float flSpeed)
	{
		this.TurnRole = false;
		this.m_TurnAngle = GameMath.ClampAngle(flAngle, 0f, 360f);
		this.m_TurnSpeed = flSpeed;
		if (0f == this.m_TurnSpeed)
		{
			this.SetDir(this.m_TurnAngle);
		}
		else
		{
			this.TurnRole = true;
		}
		if (this.m_TurnMotion > 0)
		{
			if (this.m_RoleMotion != null)
			{
				this.m_RoleMotion.SetMotion(this.m_TurnMotion);
			}
			this.m_TurnMotion = 0;
		}
	}

	public void SetTurn(Vector3 pos, float flSpeed)
	{
		this.TurnRole = false;
		float angle = GameMath.GetAngle(this.GetPos(), pos);
		this.m_TurnAngle = GameMath.ClampAngle(angle, 0f, 360f);
		this.m_TurnSpeed = flSpeed;
		if (0f == this.m_TurnSpeed)
		{
			this.SetDir(this.m_TurnAngle);
		}
		else
		{
			this.TurnRole = true;
		}
		if (this.m_TurnMotion > 0)
		{
			if (this.m_RoleMotion != null)
			{
				this.m_RoleMotion.SetMotion(this.m_TurnMotion);
			}
			this.m_TurnMotion = 0;
		}
	}

	public bool SmoothTurn(float flAngle, float flSpeed)
	{
		flAngle = GameMath.ClampAngle(flAngle, 0f, 360f);
		float dir = this.GetDir();
		float angle = GameMath.LerpAngle(dir, flAngle, Time.deltaTime * flSpeed);
		this.SetDir(GameMath.ClampAngle(angle, 0f, 360f));
		return Mathf.Abs(dir - flAngle) < 1f;
	}

	public bool Turn(float flAngle, float flSpeed)
	{
		flAngle = GameMath.ClampAngle(flAngle, 0f, 360f);
		float dir = this.GetDir();
		if (Mathf.Abs(dir - flAngle) < 1f)
		{
			return true;
		}
		float num = Time.deltaTime * flSpeed;
		float num2 = flAngle - dir;
		if (Mathf.Abs(num2) > 180f)
		{
			if (dir < flAngle)
			{
				num2 = -360f - dir + flAngle;
			}
			else
			{
				num2 = 360f - dir + flAngle;
			}
		}
		if (num > Mathf.Abs(num2))
		{
			num = Mathf.Abs(num2);
		}
		if (num2 < 0f)
		{
			num = -num;
		}
		this.SetDir(GameMath.ClampAngle(360f + dir + num, 0f, 360f));
		return false;
	}

	public void LoadPrefabModel()
	{
	}

	public void AttachDefaultComponent()
	{
		if (!base.gameObject.GetComponent<CapsuleCollider>())
		{
			CapsuleCollider capsuleCollider = base.gameObject.AddComponent<CapsuleCollider>();
			capsuleCollider.center = new Vector3(0f, 1f, 0f);
			capsuleCollider.radius = 1f;
			capsuleCollider.height = 2.2f;
			capsuleCollider.isTrigger = true;
			if (this.NoCollider)
			{
				capsuleCollider.enabled = false;
			}
		}
		if (!base.gameObject.GetComponent<CharacterController>())
		{
			CharacterController characterController = base.gameObject.AddComponent<CharacterController>();
			characterController.center = new Vector3(0f, 1.155f, 0f);
			characterController.radius = 0.4f;
			characterController.height = 2.4f;
			if (this.NoCollider)
			{
				characterController.enabled = false;
			}
		}
	}

	public void SetNpcData(S_NpcData npcData)
	{
		this.m_NpcData = npcData;
		if (this.m_NpcData.Pick == 1)
		{
			this.Pick = true;
			return;
		}
		this.Pick = false;
	}

	public S_NpcData GetNpcData()
	{
		return this.m_NpcData;
	}

	public virtual void SetMove(Vector3 moveDest)
	{
		this.MoveRole = true;
		this.m_MoveDesPos = moveDest;
		this.m_MoveDirection = this.m_MoveDesPos - this.GetPos();
		this.m_MoveDirection = this.m_MoveDirection.normalized;
		this.m_RotateDirection = this.m_MoveDirection;
	}

	public void SetMoveDir(Vector3 moveDir)
	{
		this.m_MoveDirection = moveDir;
		this.m_MoveDirection.y = 0f;
		this.m_MoveDirection = this.m_MoveDirection.normalized;
		float d = Mathf.Clamp(this.m_MoveSpeed * Time.deltaTime, this.m_DesiredDistance + 0.01f, 2f);
		Vector3 move = this.GetPos() + this.m_MoveDirection * d;
		this.SetMove(move);
	}

	public virtual void StopMove()
	{
		this.m_MoveDirection = Vector3.zero;
		this.MoveRole = false;
	}

	public bool IsMoveOver()
	{
		return !this.MoveRole;
	}

	public void HideGameObj(bool bHide)
	{
		SkinnedMeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
		if (componentsInChildren != null)
		{
			SkinnedMeshRenderer[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = array[i];
				if (skinnedMeshRenderer)
				{
					skinnedMeshRenderer.enabled = !bHide;
				}
			}
		}
		MeshRenderer[] componentsInChildren2 = base.gameObject.GetComponentsInChildren<MeshRenderer>();
		if (componentsInChildren2 != null)
		{
			MeshRenderer[] array2 = componentsInChildren2;
			for (int j = 0; j < array2.Length; j++)
			{
				MeshRenderer meshRenderer = array2[j];
				if (meshRenderer)
				{
					meshRenderer.enabled = !bHide;
				}
			}
		}
		Light[] componentsInChildren3 = base.gameObject.GetComponentsInChildren<Light>();
		if (componentsInChildren != null)
		{
			Light[] array3 = componentsInChildren3;
			for (int k = 0; k < array3.Length; k++)
			{
				Light light = array3[k];
				if (light)
				{
					light.enabled = !bHide;
				}
			}
		}
		ParticleSystem[] componentsInChildren4 = base.gameObject.GetComponentsInChildren<ParticleSystem>();
		if (componentsInChildren4 != null)
		{
			ParticleSystem[] array4 = componentsInChildren4;
			for (int l = 0; l < array4.Length; l++)
			{
				ParticleSystem particleSystem = array4[l];
				if (particleSystem)
				{
					if (bHide)
					{
						particleSystem.Stop();
					}
					else
					{
						particleSystem.Play();
					}
				}
			}
		}
		if (base.gameObject.renderer != null)
		{
			base.gameObject.renderer.enabled = !bHide;
		}
	}

	public void AlphaObj(float alpha)
	{
		SkinnedMeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
		if (componentsInChildren != null)
		{
			SkinnedMeshRenderer[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = array[i];
				if (skinnedMeshRenderer && skinnedMeshRenderer.renderer)
				{
					skinnedMeshRenderer.renderer.material.color = new Color(1f, 1f, 1f, alpha);
				}
			}
		}
		MeshRenderer[] componentsInChildren2 = base.gameObject.GetComponentsInChildren<MeshRenderer>();
		if (componentsInChildren2 != null)
		{
			MeshRenderer[] array2 = componentsInChildren2;
			for (int j = 0; j < array2.Length; j++)
			{
				MeshRenderer meshRenderer = array2[j];
				if (meshRenderer && meshRenderer.renderer)
				{
					meshRenderer.renderer.material.color = new Color(1f, 1f, 1f, alpha);
				}
			}
		}
		if (base.gameObject.renderer != null)
		{
			base.gameObject.renderer.material.color = new Color(1f, 1f, 1f, alpha);
		}
	}

	public void SetRenderColor(Color color)
	{
		SkinnedMeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
		if (componentsInChildren != null)
		{
			SkinnedMeshRenderer[] array = componentsInChildren;
			for (int i = 0; i < array.Length; i++)
			{
				SkinnedMeshRenderer skinnedMeshRenderer = array[i];
				if (skinnedMeshRenderer)
				{
					skinnedMeshRenderer.renderer.material.color = color;
				}
			}
		}
	}

	public void SetShader(string shader)
	{
		if (this.m_NpcData == null)
		{
			Debug.Log("SetShader m_NpcData == null");
			return;
		}
		Transform child = base.gameObject.transform.GetChild(0);
		if (child != null)
		{
			Renderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<Renderer>();
			if (componentsInChildren != null)
			{
				Renderer[] array = componentsInChildren;
				for (int i = 0; i < array.Length; i++)
				{
					Renderer renderer = array[i];
					if (renderer)
					{
						renderer.material.shader = Shader.Find(shader);
					}
				}
			}
		}
	}

	public void RestoreOrgShader(string shader)
	{
		if (this.m_NpcData == null)
		{
			Debug.Log("SetShader m_NpcData == null");
			return;
		}
		Transform child = base.gameObject.transform.GetChild(0);
		if (child != null)
		{
			Renderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<Renderer>();
			if (componentsInChildren != null)
			{
				Renderer[] array = componentsInChildren;
				for (int i = 0; i < array.Length; i++)
				{
					Renderer renderer = array[i];
					if (renderer && renderer.material.shader.name == "Softstar/Transparent/Bumped Diffuse_zFight")
					{
						renderer.material.shader = Shader.Find(shader);
					}
				}
			}
		}
	}

	private bool UpdateMouseUIEvent()
	{
		return Swd6Application.instance.guiManager != null && Swd6Application.instance.guiManager.DetecteMouseOnUI(true);
	}

	public void OnMouseDown()
	{
		if (this.UpdateMouseUIEvent())
		{
			return;
		}
		if (Swd6Application.instance.m_ExploreSystem.LockPlayer)
		{
			return;
		}
		if (Input.GetMouseButton(0))
		{
			GameObject ridePetObj = Swd6Application.instance.m_ExploreSystem.RidePetObj;
			if (ridePetObj != null)
			{
				M_RidePetController component = ridePetObj.GetComponent<M_RidePetController>();
				if (component)
				{
					if (this.Pick)
					{
						component.MoveTarget = base.gameObject;
					}
					return;
				}
			}
			GameObject gameObject = GameObject.Find("Player");
			if (gameObject)
			{
				M_PlayerController component2 = gameObject.GetComponent<M_PlayerController>();
				if (component2)
				{
					if (base.gameObject == gameObject)
					{
						Debug.Log("OnMouseDown_PlayObj");
						return;
					}
					if (this.Pick)
					{
						component2.MoveTarget = base.gameObject;
					}
				}
			}
		}
	}

	public void OnMouseEnter()
	{
		if (this.UpdateMouseUIEvent())
		{
			return;
		}
		GameObject gameObject = GameObject.Find("Player");
		if (gameObject)
		{
			M_PlayerController component = gameObject.GetComponent<M_PlayerController>();
			if (component && this.Pick)
			{
				component.m_PickTarget = base.gameObject;
				this.OnHeadLookAt(gameObject, base.gameObject);
			}
		}
		GameObject ridePetObj = Swd6Application.instance.m_ExploreSystem.RidePetObj;
		if (ridePetObj != null)
		{
			M_RidePetController component2 = ridePetObj.GetComponent<M_RidePetController>();
			if (component2)
			{
				component2.m_PickTarget = base.gameObject;
			}
		}
	}

	public void OnMouseOver()
	{
		if (this.UpdateMouseUIEvent())
		{
			GameCursor.SetCursor(ENUM_CURSOR.NORMAL);
			return;
		}
		if (base.gameObject.name == "Player")
		{
			return;
		}
		if (this.Invalid)
		{
			return;
		}
		if (Swd6Application.instance.m_ExploreSystem.LockPlayer)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("Player");
		if (gameObject)
		{
			M_PlayerController component = gameObject.GetComponent<M_PlayerController>();
			if (component && this.Pick)
			{
				component.m_PickTarget = base.gameObject;
				this.OnHeadLookAt(gameObject, base.gameObject);
				if (this.m_NpcData.emType == ENUM_NpcType.Treasure && this.HideRole && this.m_NpcData.Show == 0 && this.m_NpcData.emActionHint == ENUM_ActionHint.Null)
				{
					return;
				}
				if (this.m_NpcData.emType == ENUM_NpcType.Mine)
				{
					GameCursor.SetCursor(ENUM_CURSOR.GATHER);
				}
				else if (this.m_NpcData.emType == ENUM_NpcType.Treasure || this.m_NpcData.emType == ENUM_NpcType.Egg)
				{
					GameCursor.SetCursor(ENUM_CURSOR.PICK);
				}
				else if (this.m_NpcData.emType == ENUM_NpcType.Trap)
				{
					GameCursor.SetCursor(ENUM_CURSOR.SEE);
				}
				else
				{
					GameCursor.SetCursor(ENUM_CURSOR.TALK);
				}
			}
		}
		GameObject ridePetObj = Swd6Application.instance.m_ExploreSystem.RidePetObj;
		if (ridePetObj != null)
		{
			M_RidePetController component2 = ridePetObj.GetComponent<M_RidePetController>();
			if (component2)
			{
				component2.m_PickTarget = base.gameObject;
				if (this.m_NpcData.emType == ENUM_NpcType.Mine)
				{
					GameCursor.SetCursor(ENUM_CURSOR.GATHER);
					return;
				}
				if (this.m_NpcData.emType == ENUM_NpcType.Treasure || this.m_NpcData.emType == ENUM_NpcType.Egg)
				{
					GameCursor.SetCursor(ENUM_CURSOR.PICK);
					return;
				}
				if (this.m_NpcData.emType == ENUM_NpcType.Trap)
				{
					GameCursor.SetCursor(ENUM_CURSOR.SEE);
					return;
				}
				GameCursor.SetCursor(ENUM_CURSOR.TALK);
			}
		}
	}

	public void OnMouseExit()
	{
		this.OnExitFocus();
	}

	public void OnExitFocus()
	{
		GameCursor.SetCursor(ENUM_CURSOR.NORMAL);
		if (base.gameObject.name == "Player")
		{
			return;
		}
		GameObject gameObject = GameObject.Find("Player");
		if (gameObject)
		{
			M_PlayerController component = gameObject.GetComponent<M_PlayerController>();
			if (component)
			{
				component.m_PickTarget = null;
				this.OnHeadLookAt(gameObject, null);
			}
		}
		GameObject ridePetObj = Swd6Application.instance.m_ExploreSystem.RidePetObj;
		if (ridePetObj != null)
		{
			M_RidePetController component2 = ridePetObj.GetComponent<M_RidePetController>();
			if (component2)
			{
				component2.m_PickTarget = null;
			}
		}
	}

	public void OnHeadLookAt(GameObject sourceObj, GameObject targetObj)
	{
		if (sourceObj == null)
		{
			return;
		}
		M_HeadLookController component = sourceObj.GetComponent<M_HeadLookController>();
		if (component == null)
		{
			return;
		}
		if (targetObj == null)
		{
			component.Return(0.5f);
			return;
		}
		if (Vector3.Distance(targetObj.transform.position, sourceObj.transform.position) >= 4f)
		{
			component.Return(0.5f);
			return;
		}
		Transform transform = null;
		Transform[] componentsInChildren = targetObj.transform.GetComponentsInChildren<Transform>();
		Transform[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			Transform transform2 = array[i];
			if (transform2.name == "Bip001 Head")
			{
				transform = transform2;
			}
		}
		if (transform != null)
		{
			component.SetLookTarget(transform);
			return;
		}
		Vector3 position = targetObj.transform.position;
		component.target = position;
	}

	private GameObject CreateQuestHintObj(int id)
	{
		if (this.m_QuestHint)
		{
			UnityEngine.Object.Destroy(this.m_QuestHint);
		}
		string text = "QuestHint" + id;
		GameObject gameObject = OtherElementGenerator.CreateOtherGameObject(text);
		if (gameObject == null)
		{
			Debug.Log("CreateQuestHintObj error__" + text);
		}
		return gameObject;
	}

	public void CheckQuestState()
	{
		this.SetQuestState(ENUM_QuestHint.Null);
		if (this.HideRole)
		{
			this.m_GameObjData.QuestState = ENUM_QuestState.Null;
			ExploreMiniMapSystem.Instance.UpdateQuestIcon(this.RoleID);
			return;
		}
		bool flag = false;
		bool flag2 = false;
		int num = Swd6Application.instance.m_QuestSystem.CheckNpctFinishState(this.RoleID, ref flag2);
		if (num > 0)
		{
			S_Quest data = GameDataDB.QuestDB.GetData(num);
			if (data != null)
			{
				if (this.m_QuestTalk && this.m_QuestTalk.name != data.Talk)
				{
					UnityEngine.Object.DestroyObject(this.m_QuestTalk);
					this.m_QuestTalk = null;
				}
				if (this.m_QuestTalk == null)
				{
					this.m_QuestTalk = base.gameObject.AddComponent(data.Talk);
					if (this.m_QuestTalk)
					{
						this.m_QuestTalk.name = data.Talk;
					}
				}
			}
			if (flag2)
			{
				this.m_GameObjData.QuestState = ENUM_QuestState.Report;
				this.SetQuestState(ENUM_QuestHint.Report);
				Swd6Application.instance.m_QuestSystem.ReportQuestRecord(num);
				if (Swd6Application.instance.m_ResourceType == ENUM_ResourceType.Develop)
				{
					Debug.Log(this.RoleID + "角色可回報任務_" + num);
				}
				if (!this.HideRole)
				{
					ExploreMiniMapSystem.Instance.UpdateQuestIcon(this.RoleID);
				}
				return;
			}
		}
		num = Swd6Application.instance.m_QuestSystem.CheckNpctRunState(this.RoleID);
		if (num > 0)
		{
			S_Quest data2 = GameDataDB.QuestDB.GetData(num);
			if (data2 != null)
			{
				if (this.m_QuestTalk && this.m_QuestTalk.name != data2.Talk)
				{
					UnityEngine.Object.DestroyObject(this.m_QuestTalk);
					this.m_QuestTalk = null;
				}
				if (this.m_QuestTalk == null)
				{
					this.m_QuestTalk = base.gameObject.AddComponent(data2.Talk);
					if (this.m_QuestTalk)
					{
						this.m_QuestTalk.name = data2.Talk;
					}
				}
				this.m_GameObjData.QuestState = ENUM_QuestState.Normal;
				this.SetQuestState(ENUM_QuestHint.NoReport);
				if (Swd6Application.instance.m_ResourceType == ENUM_ResourceType.Develop)
				{
					Debug.Log(this.RoleID + "角色正在進行任務_" + num);
				}
				if (!this.HideRole)
				{
					ExploreMiniMapSystem.Instance.UpdateQuestIcon(this.RoleID);
				}
				return;
			}
		}
		num = Swd6Application.instance.m_QuestSystem.CheckNpctGiveState(this.RoleID, ref flag);
		if (num > 0 && flag)
		{
			Swd6Application.instance.m_QuestSystem.ActiveQuestRecord(num);
			this.m_GameObjData.QuestState = ENUM_QuestState.Active;
			S_Quest data3 = GameDataDB.QuestDB.GetData(num);
			if (data3 != null)
			{
				if (this.m_QuestTalk && this.m_QuestTalk.name != data3.Talk)
				{
					UnityEngine.Object.DestroyObject(this.m_QuestTalk);
					this.m_QuestTalk = null;
				}
				if (this.m_QuestTalk == null)
				{
					this.m_QuestTalk = base.gameObject.AddComponent(data3.Talk);
					if (this.m_QuestTalk)
					{
						this.m_QuestTalk.name = data3.Talk;
					}
				}
				this.SetQuestState(ENUM_QuestHint.Give);
			}
			if (Swd6Application.instance.m_ResourceType == ENUM_ResourceType.Develop)
			{
				Debug.Log(this.RoleID + "角色可接取任務_" + num);
			}
			if (!this.HideRole)
			{
				ExploreMiniMapSystem.Instance.UpdateQuestIcon(this.RoleID);
			}
			return;
		}
		if (this.m_QuestTalk && !this.m_DontRemoveScript)
		{
			UnityEngine.Object.DestroyObject(this.m_QuestTalk);
			this.m_QuestTalk = null;
		}
		this.m_GameObjData.QuestState = ENUM_QuestState.Null;
		ExploreMiniMapSystem.Instance.UpdateQuestIcon(this.RoleID);
	}

	public void SetQuestState(ENUM_QuestHint state)
	{
		int id = 0;
		switch (state)
		{
		case ENUM_QuestHint.Null:
			if (this.m_QuestHint)
			{
				UnityEngine.Object.Destroy(this.m_QuestHint);
			}
			return;
		case ENUM_QuestHint.Give:
			id = 1;
			break;
		case ENUM_QuestHint.NoGive:
			id = 3;
			break;
		case ENUM_QuestHint.Report:
			id = 2;
			break;
		case ENUM_QuestHint.NoReport:
			id = 4;
			break;
		}
		if (this.HideRole)
		{
			return;
		}
		this.m_QuestHint = this.CreateQuestHintObj(id);
	}

	public void ShowQuestHint()
	{
		if (this.m_QuestHint == null)
		{
			return;
		}
		Vector3 position = base.gameObject.transform.position;
		position.y += 2f;
		this.m_QuestHint.transform.position = position;
	}

	public void RunTalk()
	{
		this.Talk = true;
		if (this.m_GameObjData.QuestState != ENUM_QuestState.Null)
		{
			if (Swd6Application.instance.m_ResourceType == ENUM_ResourceType.Develop)
			{
				Debug.Log("DoQuestTalk_" + this.m_GameObjData.QuestState);
			}
			if (this.m_GameObjData.QuestState == ENUM_QuestState.Active)
			{
				base.SendMessage("GiveQuest", SendMessageOptions.DontRequireReceiver);
			}
			if (this.m_GameObjData.QuestState == ENUM_QuestState.Normal)
			{
				base.SendMessage("RunQuest", SendMessageOptions.DontRequireReceiver);
			}
			if (this.m_GameObjData.QuestState == ENUM_QuestState.Report)
			{
				base.SendMessage("ReportQuest", SendMessageOptions.DontRequireReceiver);
				return;
			}
		}
		else
		{
			if (Swd6Application.instance.m_ResourceType == ENUM_ResourceType.Develop)
			{
				Debug.Log("DoTalk_" + this.RoleID);
			}
			base.SendMessage("DoTalk", SendMessageOptions.DontRequireReceiver);
		}
	}

	public void SetAttachItem(int itemID, int position)
	{
		string text = "1006";
		if (position != 0)
		{
			text = "1007";
		}
		this.m_GameObjData.AttachID[position] = itemID;
		S_Item data = GameDataDB.ItemDB.GetData(itemID);
		if (data == null)
		{
			Debug.LogWarning("itemData not be found : " + itemID);
			return;
		}
		GameObject gameObject = ItemGenerator.CreateItemGameObject(data.StoryPrefabName);
		if (gameObject == null)
		{
			Debug.LogWarning("item not be found : " + data.StoryPrefabName);
			return;
		}
		gameObject.name = data.StoryPrefabName;
		Transform transform = TransformTool.SearchHierarchyForBone(base.gameObject.transform, text);
		if (transform == null)
		{
			Debug.LogWarning("node not be found : " + text);
			return;
		}
		gameObject.transform.position = transform.position;
		gameObject.transform.rotation = transform.rotation;
		gameObject.transform.parent = transform;
	}

	public void DeleteAttachItem(int itemID, int position)
	{
		string text = "1006";
		if (position != 0)
		{
			text = "1007";
		}
		this.m_GameObjData.AttachID[position] = 0;
		S_Item data = GameDataDB.ItemDB.GetData(itemID);
		if (data == null)
		{
			Debug.LogWarning("itemData not be found : " + itemID);
			return;
		}
		Transform transform = TransformTool.SearchHierarchyForBone(base.gameObject.transform, text);
		if (transform == null)
		{
			Debug.LogWarning("node not be found : " + text);
			return;
		}
		Transform transform2 = transform.FindChild(data.StoryPrefabName);
		if (transform2 == null)
		{
			Debug.LogWarning("item not be found : " + data.StoryPrefabName);
			return;
		}
		UnityEngine.Object.Destroy(transform2.gameObject);
	}

	public void LoadAttachItem()
	{
		if (this.m_GameObjData.AttachID[0] > 0)
		{
			this.SetAttachItem(this.m_GameObjData.AttachID[0], 0);
		}
		if (this.m_GameObjData.AttachID[1] > 0)
		{
			this.SetAttachItem(this.m_GameObjData.AttachID[1], 1);
		}
	}

	public void SetHairLight(bool light)
	{
		SkinnedMeshRenderer[] componentsInChildren = base.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
		SkinnedMeshRenderer[] array = componentsInChildren;
		for (int i = 0; i < array.Length; i++)
		{
			SkinnedMeshRenderer skinnedMeshRenderer = array[i];
			if (skinnedMeshRenderer.name.Contains("hair"))
			{
				this.m_GameObjData.HairNoLight = !light;
				if (light)
				{
					skinnedMeshRenderer.gameObject.layer = 0;
				}
				else
				{
					skinnedMeshRenderer.gameObject.layer = 20;
				}
			}
		}
	}

	public void LoadHairLightState()
	{
		this.SetHairLight(!this.m_GameObjData.HairNoLight);
	}
}
