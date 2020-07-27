using GameFramework;
using System;
using UnityEngine;

public class M_PlayerController : M_GameRoleBase
{
	public GameObject targetPointObj;

	public NavMeshAgent m_NavMeshAgent;

	public float m_WalkSpeed = 1f;

	public float m_RunSpeed = 4f;

	public float m_DashSpeed = 6f;

	public float m_WalkMultiplier = 0.5f;

	public float m_DashMultiplier = 1.5f;

	public bool m_IsDash;

	public bool m_bPlayNormalMotion;

	public float m_AutoMoveDist = 0.5f;

	public float m_WaterWaveTime = 0.5f;

	public bool m_IsRun = true;

	public bool m_SlowMove;

	public bool m_Reverse;

	public bool m_bAutoJump;

	public bool m_NoJump;

	public bool m_IsOnOffMeshLink;

	public bool m_DashFOV;

	public bool m_AutoMove;

	public bool m_WalkInWater;

	public bool m_WalkMask;

	public bool m_LMouseDown;

	public float m_LMouseDownTime;

	public Vector3 m_LMouseDownPos = Vector3.zero;

	public float m_BreakTime = 10f;

	public float m_BreakDownTime = 30f;

	public float m_BreakAddDamage = 10f;

	public float m_BreakSpeed = 2f;

	public float m_BreakSlowTime = 0.05f;

	public float m_BreakSlowWaitTime = 2f;

	private GUIManager m_GuiManager;

	public Vector3 m_DirectionVector = Vector3.zero;

	private float oldMaxRotationSpeed;

	private float m_StopDistance = 0.2f;

	public float m_JumpHeight;

	public float m_JumpGravity;

	private float m_OldFOV;

	private string m_PlayMotionName;

	private string m_FootStepWaterName;

	public bool m_EnterTalkEvent;

	public OffMeshLinkData m_LinkData;

	public GameObject m_PickTarget
	{
		get;
		set;
	}

	public GameObject m_MoveTarget
	{
		get;
		set;
	}

	public bool m_LockControl
	{
		get;
		set;
	}

	public M_PlayerMotor m_PlayerMotor
	{
		get;
		set;
	}

	public JumpAndIdle m_JumpMotor
	{
		get;
		set;
	}

	public Vector3 MoveDirection
	{
		get
		{
			return this.m_DirectionVector;
		}
		set
		{
			if (this.oldMaxRotationSpeed == 0f && this.m_PlayerMotor)
			{
				this.oldMaxRotationSpeed = this.m_PlayerMotor.maxRotationSpeed;
				this.m_PlayerMotor.maxRotationSpeed = 500f;
			}
			this.m_DirectionVector = value - base.transform.position;
			this.m_DirectionVector = Util.ProjectOntoPlane(this.m_DirectionVector, base.transform.up);
			if (this.m_DirectionVector.magnitude >= 1f)
			{
				this.m_DirectionVector = this.m_DirectionVector.normalized;
			}
			this.m_DirectionVector = Quaternion.Inverse(base.transform.rotation) * this.m_DirectionVector;
		}
	}

	public GameObject MoveTarget
	{
		get
		{
			return this.m_MoveTarget;
		}
		set
		{
			this.m_MoveTarget = value;
			if (value == null)
			{
				this.MoveDirection = Vector3.zero;
				if (this.m_PlayerMotor)
				{
					this.m_PlayerMotor.desiredMovementDirection = Vector3.zero;
					this.m_PlayerMotor.desiredFacingDirection = Vector3.zero;
				}
			}
			if (this.oldMaxRotationSpeed == 0f && this.m_PlayerMotor)
			{
				this.oldMaxRotationSpeed = this.m_PlayerMotor.maxRotationSpeed;
				this.m_PlayerMotor.maxRotationSpeed = 500f;
			}
		}
	}

	public bool LockControl
	{
		get
		{
			return this.m_LockControl;
		}
		set
		{
			this.m_LockControl = value;
			this.m_EnterTalk = value;
			if (!this.m_LockControl)
			{
				GameCursor.SetCursor(ENUM_CURSOR.NORMAL);
				this.MoveTarget = null;
			}
			else if (this.m_AutoMove)
			{
				this.StopMoveToTarget();
			}
			if (this.m_PlayerMotor)
			{
				this.m_PlayerMotor.desiredFacingDirection = Vector3.zero;
				this.m_PlayerMotor.desiredMovementDirection = Vector3.zero;
			}
		}
	}

	public bool EnterTalk
	{
		get
		{
			return this.m_EnterTalk;
		}
		set
		{
			this.m_EnterTalk = value;
		}
	}

	protected override void initialize()
	{
		this.m_PlayerMotor = (base.GetComponent(typeof(M_PlayerMotor)) as M_PlayerMotor);
		if (this.m_PlayerMotor == null)
		{
			Debug.Log("Motor is null!!");
		}
		this.m_JumpMotor = (base.GetComponent(typeof(JumpAndIdle)) as JumpAndIdle);
		Vector3 position = base.transform.position;
		if (!base.GetComponent<NavMeshAgent>())
		{
			this.m_NavMeshAgent = base.gameObject.AddComponent<NavMeshAgent>();
			this.m_NavMeshAgent.acceleration = 200f;
			this.m_NavMeshAgent.angularSpeed = 500f;
			this.m_NavMeshAgent.baseOffset = -0.01f;
			this.m_NavMeshAgent.walkableMask = 1;
			this.m_NavMeshAgent.enabled = false;
		}
		base.transform.position = position;
		this.m_RoleMotion = base.GetComponent<M_GameRoleMotion>();
		if (this.m_RoleMotion != null)
		{
			this.m_RoleMotion.Init(base.RoleID, 601);
		}
		LegAnimator component = base.gameObject.GetComponent<LegAnimator>();
		if (component)
		{
			int num = 1024;
			num |= 4;
			num |= 16;
			num = ~num;
			component.groundLayers = num;
		}
		LegController component2 = base.gameObject.GetComponent<LegController>();
		if (component2)
		{
			for (int i = 0; i < component2.sourceAnimations.Length; i++)
			{
				if (component2.sourceAnimations[i].name == "walk")
				{
					this.m_WalkSpeed = component2.sourceAnimations[i].nativeSpeed;
				}
				if (component2.sourceAnimations[i].name == "run")
				{
					this.m_RunSpeed = component2.sourceAnimations[i].nativeSpeed;
				}
				if (component2.sourceAnimations[i].name == "sprint")
				{
					this.m_DashSpeed = component2.sourceAnimations[i].nativeSpeed;
				}
			}
		}
		base.gameObject.AddComponent<M_FootStep>();
		if (Swd6Application.instance.m_ExploreSystem.m_MapData.emType == ENUM_MapType.World)
		{
			this.m_RunSpeed = 3.6f;
		}
		this.targetPointObj = Swd6Application.instance.m_ExploreSystem.m_MoveTargetPoint;
		this.m_GuiManager = Swd6Application.instance.guiManager;
		this.m_MoveTarget = null;
		this.m_IsRun = Swd6Application.instance.m_ExploreSystem.Run;
		this.m_PlayerMotor.maxForwardSpeed = this.m_RunSpeed;
		this.m_JumpHeight = this.m_PlayerMotor.jumpHeight;
		this.m_JumpGravity = this.m_PlayerMotor.gravity;
		if (Swd6Application.instance.m_ExploreSystem.m_MapData.emType == ENUM_MapType.Town)
		{
			this.m_JumpHeight = 1.1f;
		}
		this.PlayFaceMotion(2);
		UI_Explore.Instance.SetDashState(false, this.m_IsRun);
		if (Swd6Application.instance.m_ResourceType == ENUM_ResourceType.Develop)
		{
			Debug.Log("Player Init OK!!");
		}
	}

	public override void Update()
	{
		if (!this.LockControl)
		{
			this.UpdateInput();
			this.UpdateMotion();
			this.MousePickFloor();
			this.UpdateMoveTarget();
			this.UpdateMove();
			this.UpdateFOV();
			this.UpdatePlayerTalk();
			this.UpdateWaterWave();
			return;
		}
		if (base.MoveRole)
		{
			this.UpdateMoveDest();
			this.UpdateMove();
			return;
		}
		this.UpdateFaceToTarget();
	}

	private void UpdateWaterWave()
	{
		if (!this.m_WalkInWater)
		{
			return;
		}
		if (this.m_DirectionVector == Vector3.zero)
		{
			return;
		}
		if (this.m_WaterWaveTime > 0f)
		{
			this.m_WaterWaveTime -= Time.deltaTime;
			if (this.m_WaterWaveTime < 0f)
			{
				this.m_WaterWaveTime = 0.5f;
				if (this.m_IsRun)
				{
					this.m_WaterWaveTime = 0.25f;
				}
				if (this.m_IsDash)
				{
					this.m_WaterWaveTime = 0.1f;
				}
				base.SendMessage("OnWaterFootStep", this.m_FootStepWaterName, SendMessageOptions.DontRequireReceiver);
			}
		}
	}

	private void UpdatePlayerTalk()
	{
		if (this.MoveTarget == null)
		{
			return;
		}
		if (this.LockControl)
		{
			return;
		}
		if (this.MoveTarget.tag != "Npc")
		{
			return;
		}
		M_GameNpc component = this.MoveTarget.GetComponent<M_GameNpc>();
		if (component == null)
		{
			return;
		}
		if (component.GetNpcData().MouseTalkRange > 0f)
		{
			float num = Vector3.Distance(this.MoveTarget.transform.position, base.Pos);
			if (num <= component.GetNpcData().MouseTalkRange)
			{
				Swd6Application.instance.m_ExploreSystem.TalkTarget = this.MoveTarget;
				this.StopMoveToTarget();
				component.DoTalkMotion(Swd6Application.instance.m_ExploreSystem.PlayerObj);
				component.RunTalk();
			}
		}
	}

	private void UpdateFOV()
	{
		if (!this.m_DashFOV)
		{
			return;
		}
		GameObject gameObject;
		if (!this.m_IsDash)
		{
			if (this.m_OldFOV > 0f)
			{
				gameObject = GameObject.Find("Main Camera");
				gameObject.camera.fieldOfView = Mathf.Lerp(gameObject.camera.fieldOfView, this.m_OldFOV, Time.deltaTime * 5f);
				if (Mathf.Abs(gameObject.camera.fieldOfView - this.m_OldFOV) < 0.1f)
				{
					gameObject.camera.fieldOfView = this.m_OldFOV;
					this.m_OldFOV = 0f;
				}
			}
			return;
		}
		gameObject = GameObject.Find("Main Camera");
		if (this.m_DirectionVector == Vector3.zero)
		{
			if (this.m_OldFOV > 0f)
			{
				gameObject.camera.fieldOfView = Mathf.Lerp(gameObject.camera.fieldOfView, this.m_OldFOV, Time.deltaTime * 5f);
			}
			return;
		}
		if (gameObject)
		{
			gameObject.camera.fieldOfView = Mathf.Lerp(gameObject.camera.fieldOfView, 70f, Time.deltaTime * 5f);
		}
	}

	private bool UpdateMouseUIEvent()
	{
		return this.m_GuiManager != null && (Input.GetMouseButtonUp(0) || Input.GetMouseButton(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && this.m_GuiManager.DetecteMouseOnUI(true);
	}

	private void UpdateRun()
	{
		if (this.m_WalkMultiplier != 1f)
		{
			if (!this.m_bAutoJump)
			{
				if (this.m_IsRun)
				{
					this.m_PlayerMotor.maxForwardSpeed = this.m_RunSpeed;
				}
				else
				{
					this.m_PlayerMotor.maxForwardSpeed = this.m_WalkSpeed;
				}
			}
			if (!this.m_IsDash)
			{
				Swd6Application.instance.m_ExploreSystem.RestoreStaminaBar();
			}
			else if (this.m_IsDash)
			{
				if (Swd6Application.instance.m_ExploreSystem.ExpendStaminaBar(false))
				{
					if (!this.m_bAutoJump)
					{
						this.m_PlayerMotor.maxForwardSpeed = this.m_DashSpeed;
					}
				}
				else
				{
					this.Dash();
				}
			}
		}
		if (this.m_SlowMove)
		{
			this.m_DirectionVector *= this.m_WalkMultiplier;
		}
		if (this.m_Reverse)
		{
			this.m_DirectionVector.x = -this.m_DirectionVector.x;
			this.m_DirectionVector.z = -this.m_DirectionVector.z;
		}
	}

	private void UpdateMotion()
	{
		if (this.m_bPlayNormalMotion && !base.animation.IsPlaying(this.m_PlayMotionName))
		{
			this.PlayIdleMotion();
		}
	}

	private void UpdateInput()
	{
		if (Camera.main == null)
		{
			return;
		}
		if (Swd6Application.instance.m_ExploreSystem.OpenMainMenu)
		{
			return;
		}
		this.m_DirectionVector = GameInput.GetDirKeyMoveVector();
		if (this.m_DirectionVector == Vector3.zero)
		{
			this.m_DirectionVector = GameInput.GetJoyLAxis();
		}
		if (this.m_DirectionVector.magnitude > 1f)
		{
			this.m_DirectionVector = this.m_DirectionVector.normalized;
		}
		this.m_DirectionVector = this.m_DirectionVector.normalized * Mathf.Pow(this.m_DirectionVector.magnitude, 2f);
		this.m_DirectionVector = Camera.main.transform.rotation * this.m_DirectionVector;
		Quaternion rotation = Quaternion.FromToRotation(Camera.main.transform.forward * -1f, base.transform.up);
		this.m_DirectionVector = rotation * this.m_DirectionVector;
		this.m_DirectionVector = Quaternion.Inverse(base.transform.rotation) * this.m_DirectionVector;
		if (GameInput.GetKeyActionDown(KEY_ACTION.DASH))
		{
			this.Dash();
			if (this.m_DashFOV && this.m_IsDash)
			{
				GameObject gameObject = GameObject.Find("Main Camera");
				if (gameObject)
				{
					this.m_OldFOV = gameObject.camera.fieldOfView;
				}
			}
		}
		if (GameInput.GetKeyActionDown(KEY_ACTION.RUN))
		{
			this.SetRun();
		}
		if (Swd6Application.instance.m_ExploreSystem.IsUseActionSkill)
		{
			this.m_IsDash = false;
		}
		this.UpdateRun();
		if (GameInput.GetKeyActionDown(KEY_ACTION.JUMP))
		{
			this.Jump(false);
		}
		if (this.m_DirectionVector != Vector3.zero)
		{
			if (this.MoveTarget != null)
			{
				this.StopMoveToTarget();
				return;
			}
		}
		else
		{
			this.m_PlayerMotor.desiredMovementDirection = Vector3.zero;
		}
	}

	public void UpdateInputOnMovePlatform()
	{
		LegAnimator component = base.gameObject.GetComponent<LegAnimator>();
		if (component)
		{
			this.m_DirectionVector = GameInput.GetDirKeyMoveVector();
			if (this.m_DirectionVector != Vector3.zero || this.MoveTarget != null)
			{
				this.PlayIdleMotion();
				return;
			}
			component.enabled = false;
			this.PlayMotion(601);
		}
	}

	private void UpdateMoveTarget()
	{
		if (this.m_MoveTarget == null)
		{
			return;
		}
		if (!this.UpdateAutoPathMove())
		{
			return;
		}
		this.m_DirectionVector = Util.ProjectOntoPlane(this.m_DirectionVector, base.transform.up);
		if (this.m_DirectionVector.magnitude >= 1f)
		{
			this.m_DirectionVector = this.m_DirectionVector.normalized;
		}
		this.m_DirectionVector = Quaternion.Inverse(base.transform.rotation) * this.m_DirectionVector;
		this.UpdateRun();
	}

	private void UpdateMoveDest()
	{
		if (!base.MoveRole)
		{
			return;
		}
		this.MoveDirection = this.m_MoveDesPos;
	}

	private void UpdateMove()
	{
		if (this.m_NavMeshAgent != null && Swd6Application.instance.m_ExploreSystem.NavMesh && Swd6Application.instance.m_ExploreSystem.AutoPath && this.m_NavMeshAgent.enabled)
		{
			return;
		}
		if (this.m_DirectionVector == Vector3.zero)
		{
			return;
		}
		if (this.m_IsDash)
		{
			this.PlayFaceMotion(0);
		}
		else
		{
			this.PlayFaceMotion(1);
		}
		this.m_PlayerMotor.desiredMovementDirection = this.m_DirectionVector;
		bool flag = false;
		if (this.IsJump())
		{
			if (this.m_DirectionVector.magnitude - this.m_StopDistance <= 0f)
			{
				flag = true;
			}
		}
		else if (this.m_DirectionVector.magnitude - this.m_StopDistance <= 0f)
		{
			flag = true;
		}
		this.m_JumpMotor.IsMove = true;
		Swd6Application.instance.m_ExploreSystem.AddBattleStep();
		if (flag)
		{
			this.StopMoveToTarget();
		}
	}

	private bool UpdateAutoPathMove()
	{
		this.m_DirectionVector = this.m_MoveTarget.transform.position - base.transform.position;
		if (!Swd6Application.instance.m_ExploreSystem.NavMesh)
		{
			this.m_NavMeshAgent.enabled = false;
			return true;
		}
		if (!Swd6Application.instance.m_ExploreSystem.AutoPath)
		{
			this.m_NavMeshAgent.enabled = false;
			return true;
		}
		if (!this.m_AutoMove)
		{
			return true;
		}
		if (this.MoveTarget == null)
		{
			return true;
		}
		if (this.m_NavMeshAgent != null)
		{
			if (this.m_NavMeshAgent.enabled)
			{
				if (this.IsJump())
				{
					this.m_NavMeshAgent.enabled = false;
					this.m_PlayerMotor.enabled = true;
					return false;
				}
				this.m_NavMeshAgent.enabled = true;
				this.m_PlayerMotor.enabled = false;
				this.m_PlayerMotor.m_NavAgent = true;
				this.m_NavMeshAgent.speed = this.m_PlayerMotor.maxForwardSpeed;
				if (!this.m_NavMeshAgent.SetDestination(this.m_MoveTarget.transform.position))
				{
					this.m_PlayerMotor.enabled = true;
					this.m_PlayerMotor.m_NavAgent = false;
					this.m_NavMeshAgent.enabled = false;
					Debug.Log("SetDestination Error!!");
					return true;
				}
				bool flag = GameMath.IsPosEqualXZ(this.m_MoveTarget.transform.position, base.transform.position, 0.1f);
				if (this.m_NavMeshAgent.pathEndPosition == base.transform.position || flag)
				{
					Debug.Log("NavMoveEnd!!");
					this.StopMoveToTarget();
					return true;
				}
				if (this.m_NavMeshAgent.hasPath)
				{
					if (!this.m_NavMeshAgent.pathPending)
					{
						this.m_NavMeshAgent.enabled = false;
						this.m_PlayerMotor.enabled = true;
						this.m_bAutoJump = false;
						Debug.Log("找尋路徑中!!");
						return false;
					}
				}
				else if (!this.m_NavMeshAgent.isOnOffMeshLink && !this.m_NavMeshAgent.pathPending)
				{
					this.StopMoveToTarget();
					Debug.Log("沒路徑!!");
					return true;
				}
				if (this.m_NavMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
				{
					this.m_PlayerMotor.enabled = true;
					this.m_PlayerMotor.m_NavAgent = false;
					this.m_NavMeshAgent.enabled = false;
					Debug.Log("沒有路徑資料或無效!!");
					return true;
				}
				if (!this.m_NavMeshAgent.isOnOffMeshLink)
				{
					return false;
				}
				this.m_LinkData = this.m_NavMeshAgent.currentOffMeshLinkData;
				Debug.Log("linkData_" + this.m_LinkData.linkType);
				if (this.m_NavMeshAgent.autoTraverseOffMeshLink)
				{
					this.m_NavMeshAgent.autoTraverseOffMeshLink = false;
				}
				this.m_IsOnOffMeshLink = true;
				if (this.m_LinkData.linkType == OffMeshLinkType.LinkTypeJumpAcross)
				{
					this.m_bAutoJump = true;
					this.m_NavMeshAgent.enabled = false;
					this.m_PlayerMotor.enabled = true;
					this.m_PlayerMotor.maxForwardSpeed = this.m_DashSpeed;
					this.m_PlayerMotor.maxBackwardsSpeed = this.m_DashSpeed;
					this.m_PlayerMotor.maxSidewaysSpeed = this.m_DashSpeed;
					this.Jump(false);
					Debug.Log("NavDoJump!!");
					return false;
				}
				if (this.m_LinkData.linkType == OffMeshLinkType.LinkTypeManual)
				{
					Debug.Log("NavDoClimb!!");
					return false;
				}
				if (this.m_LinkData.linkType == OffMeshLinkType.LinkTypeDropDown)
				{
					Debug.Log("DropDown!!");
					this.m_NavMeshAgent.enabled = false;
					this.m_PlayerMotor.enabled = true;
					return false;
				}
			}
			else if (this.m_IsOnOffMeshLink)
			{
				if (this.m_LinkData.linkType == OffMeshLinkType.LinkTypeDropDown)
				{
					this.m_DirectionVector = this.m_LinkData.endPos - this.m_LinkData.startPos;
					if (this.m_bAutoJump && this.IsGround())
					{
						Debug.Log("NavDoDropDown1!!");
						this.m_bAutoJump = false;
						this.m_IsOnOffMeshLink = false;
						this.m_PlayerMotor.enabled = false;
						this.m_NavMeshAgent.enabled = true;
						this.m_NavMeshAgent.SetDestination(this.m_MoveTarget.transform.position);
						return false;
					}
					if (!this.IsJump())
					{
						RaycastHit raycastHit = default(RaycastHit);
						if (Physics.Raycast(new Ray
						{
							origin = base.transform.position + base.transform.forward * 0.1f,
							direction = new Vector3(0f, -1f, 0f)
						}, out raycastHit) && base.transform.position.y - raycastHit.point.y > 0.2f)
						{
							this.m_bAutoJump = true;
							this.Jump(false);
							Debug.Log("NavDoDropDown!!");
						}
					}
				}
				else if (this.m_LinkData.linkType == OffMeshLinkType.LinkTypeJumpAcross)
				{
					if (!this.IsJump())
					{
						Debug.Log("NavJumpOver!!");
						this.m_bAutoJump = false;
						this.m_IsOnOffMeshLink = false;
						this.m_PlayerMotor.enabled = false;
						this.m_NavMeshAgent.enabled = true;
						this.m_NavMeshAgent.SetDestination(this.m_MoveTarget.transform.position);
						return false;
					}
					this.m_DirectionVector = this.m_LinkData.endPos - this.m_LinkData.startPos;
				}
			}
			else if (!this.IsJump())
			{
				this.m_bAutoJump = false;
				this.m_PlayerMotor.enabled = false;
				this.m_PlayerMotor.m_NavAgent = true;
				this.m_NavMeshAgent.enabled = true;
				this.m_NavMeshAgent.SetDestination(this.m_MoveTarget.transform.position);
			}
			else
			{
				this.m_DirectionVector = base.transform.forward;
				if (GameMath.IsPosEqualXZ(this.m_MoveTarget.transform.position, base.transform.position, 0.1f))
				{
					this.m_bAutoJump = false;
					this.StopMoveToTarget();
					Debug.Log("JumpOverDst!!");
				}
			}
		}
		return true;
	}

	private void UpdateFaceToTarget()
	{
		if (this.m_MoveTarget == null)
		{
			return;
		}
		Vector3 vector = this.m_MoveTarget.transform.position - base.transform.position;
		vector = Util.ProjectOntoPlane(vector, base.transform.up);
		if (vector.magnitude >= 1f)
		{
			this.m_DirectionVector = vector.normalized;
		}
		else
		{
			this.m_DirectionVector = vector;
		}
		this.m_PlayerMotor.desiredFacingDirection = this.m_DirectionVector;
	}

	public bool CheckAutoMove(Vector3 moveTarget)
	{
		if (!(this.m_NavMeshAgent != null) || !Swd6Application.instance.m_ExploreSystem.NavMesh)
		{
			return false;
		}
		this.m_NavMeshAgent.enabled = true;
		NavMeshPath path = new NavMeshPath();
		bool flag = this.m_NavMeshAgent.CalculatePath(moveTarget, path);
		if (this.m_NavMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid || !flag)
		{
			Debug.Log("找不到路徑!!");
			this.m_NavMeshAgent.enabled = false;
			this.StopMoveToTarget();
			return false;
		}
		this.m_NavMeshAgent.enabled = false;
		this.StopMoveToTarget();
		return true;
	}

	public void SetAutoMove(Vector3 moveTarget, bool autoMove)
	{
		if (this.IsJump())
		{
			return;
		}
		this.MoveTarget = Swd6Application.instance.m_ExploreSystem.ShowMoveTarget(true, moveTarget);
		if (this.m_NavMeshAgent != null && Swd6Application.instance.m_ExploreSystem.NavMesh && !this.m_WalkMask)
		{
			this.m_NavMeshAgent.enabled = true;
			NavMeshPath path = new NavMeshPath();
			if (!this.m_NavMeshAgent.SetDestination(base.Pos))
			{
				this.m_NavMeshAgent.enabled = false;
				return;
			}
			if (Vector3.Distance(base.Pos, this.m_NavMeshAgent.nextPosition) >= this.m_AutoMoveDist)
			{
				this.m_NavMeshAgent.enabled = false;
				return;
			}
			bool flag = this.m_NavMeshAgent.CalculatePath(moveTarget, path);
			if (this.m_NavMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid || !flag)
			{
				Debug.Log("找不到路徑!!");
				this.m_NavMeshAgent.enabled = false;
				if (autoMove && Swd6Application.instance.m_ExploreSystem.AutoPath)
				{
					this.StopMoveToTarget();
					return;
				}
			}
			else
			{
				if (autoMove && !Swd6Application.instance.m_ExploreSystem.AutoPath)
				{
					autoMove = false;
				}
				if (autoMove)
				{
					this.m_NavMeshAgent.SetDestination(moveTarget);
					this.m_PlayerMotor.m_NavAgent = true;
					this.m_AutoMove = true;
					return;
				}
				this.m_AutoMove = false;
				this.m_NavMeshAgent.enabled = false;
			}
		}
	}

	public void MousePickFloor()
	{
		if (Camera.main == null)
		{
			return;
		}
		if (Swd6Application.instance.gameStateService.getCurrentState() == null)
		{
			Debug.Log("StateNULL");
			return;
		}
		if (Swd6Application.instance.gameStateService.getCurrentState().name != "ExploreState")
		{
			return;
		}
		if (this.UpdateMouseUIEvent())
		{
			return;
		}
		if (Input.GetMouseButtonUp(0))
		{
			if (this.m_LMouseDownTime >= 0.2f)
			{
				this.StopMoveToTarget();
			}
			this.m_LMouseDownPos = Vector3.zero;
			this.m_LMouseDown = false;
			this.m_WalkMask = false;
			this.m_LMouseDownTime = 0f;
			if (this.m_PickTarget)
			{
				return;
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
			if (this.m_PickTarget)
			{
				return;
			}
			this.m_LMouseDownTime = 0f;
			this.m_LMouseDown = true;
			this.m_WalkMask = false;
			this.StopMoveToTarget();
		}
		if (Input.GetMouseButton(0) && !this.m_LMouseDown)
		{
			this.m_LMouseDownTime += Time.deltaTime;
			this.m_LMouseDown = true;
			this.m_WalkMask = false;
		}
		if (this.LockControl)
		{
			return;
		}
		if (this.m_PlayerMotor.gravity == 0f)
		{
			return;
		}
		if (this.m_LMouseDown)
		{
			RaycastHit raycastHit = default(RaycastHit);
			int num = 1024;
			num |= 4;
			num = ~num;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			Physics.Linecast(ray.origin, ray.origin + ray.direction * 1000f, out raycastHit, num);
			if (null != raycastHit.transform)
			{
				if (Swd6Application.instance.m_ResourceType == ENUM_ResourceType.Develop)
				{
					Debug.Log("Hit_" + raycastHit.collider.name);
				}
				if (this.m_LMouseDownTime == 0f)
				{
					if (raycastHit.collider.tag == "Player")
					{
						return;
					}
					if (raycastHit.collider.tag == "No Raycast")
					{
						return;
					}
				}
				if (raycastHit.collider.tag == "Walkable")
				{
					this.m_WalkMask = true;
				}
				new Vector3(raycastHit.point.x, raycastHit.point.y, raycastHit.point.z);
				this.m_LMouseDownPos = raycastHit.point;
				if (this.m_LMouseDownTime >= 0.5f)
				{
					this.StopMoveToTarget();
					this.MoveDirection = raycastHit.point;
				}
				else if (this.m_LMouseDownTime == 0f)
				{
					this.SetAutoMove(this.m_LMouseDownPos, false);
				}
			}
		}
		this.m_LMouseDown = false;
	}

	public bool SetMoveTarget(Ray ray, bool applyNow, bool autoMove)
	{
		RaycastHit raycastHit = default(RaycastHit);
		int num = 1024;
		num |= 4;
		num = ~num;
		Physics.Raycast(ray, out raycastHit, 1000f, num);
		if (!(null != raycastHit.transform))
		{
			return false;
		}
		if (raycastHit.collider.tag == "Player")
		{
			return false;
		}
		if (raycastHit.collider.tag == "No Raycast")
		{
			return false;
		}
		if (autoMove)
		{
			this.SetAutoMove(raycastHit.point, true);
		}
		else
		{
			this.MoveTarget = Swd6Application.instance.m_ExploreSystem.ShowMoveTarget(true, raycastHit.point);
		}
		if (applyNow)
		{
			base.Pos = raycastHit.point;
		}
		return true;
	}

	public bool AdjustPosOnTerrain()
	{
		RaycastHit raycastHit = default(RaycastHit);
		int num = 1024;
		num |= 4;
		num = ~num;
		Ray ray = default(Ray);
		ray.origin = base.Pos;
		ray.origin += new Vector3(0f, 0.5f, 0f);
		ray.direction = new Vector3(0f, -1f, 0f);
		Physics.Raycast(ray, out raycastHit, 1000f, num);
		if (!(null != raycastHit.transform))
		{
			return false;
		}
		if (raycastHit.collider.tag == "Player")
		{
			return false;
		}
		if (raycastHit.collider.tag == "No Raycast")
		{
			return false;
		}
		base.Pos = raycastHit.point;
		return true;
	}

	public override void SetMove(Vector3 destPos)
	{
		this.StopMoveToTarget();
		this.m_MoveDesPos = destPos;
		base.MoveRole = true;
		this.MoveTarget = null;
	}

	public void StopMoveToTarget()
	{
		Swd6Application.instance.m_ExploreSystem.ShowMoveTarget(false, Vector3.zero);
		if (this.oldMaxRotationSpeed > 0f)
		{
			this.m_PlayerMotor.maxRotationSpeed = this.oldMaxRotationSpeed;
		}
		this.oldMaxRotationSpeed = 0f;
		this.MoveTarget = null;
		if (this.m_NavMeshAgent != null)
		{
			if (this.m_NavMeshAgent.enabled)
			{
				base.transform.position = GameMath.RayCastOnTerrain(base.transform.position, 1f);
			}
			this.m_NavMeshAgent.enabled = false;
		}
		if (this.m_PlayerMotor)
		{
			this.m_PlayerMotor.m_NavAgent = false;
			this.m_PlayerMotor.enabled = true;
			this.m_PlayerMotor.desiredMovementDirection = Vector3.zero;
		}
		this.m_bAutoJump = false;
		base.MoveRole = false;
		this.m_AutoMove = false;
		this.m_DirectionVector = Vector3.zero;
		this.PlayFaceMotion(2);
	}

	public override void StopMove()
	{
		Swd6Application.instance.m_ExploreSystem.ShowMoveTarget(false, Vector3.zero);
		if (this.m_NavMeshAgent != null)
		{
			this.m_NavMeshAgent.enabled = false;
		}
		if (this.m_PlayerMotor)
		{
			this.m_PlayerMotor.m_NavAgent = false;
			this.m_PlayerMotor.enabled = true;
			this.m_PlayerMotor.desiredMovementDirection = Vector3.zero;
		}
		this.m_bAutoJump = false;
		base.MoveRole = false;
		this.m_AutoMove = false;
		this.m_DirectionVector = Vector3.zero;
		this.PlayFaceMotion(2);
	}

	public void PlayFaceUpMotion(int id)
	{
		if (this.m_RoleMotion)
		{
			this.m_RoleMotion.PlayExpression_Up(id);
		}
	}

	public void PlayFaceDownMotion(int id)
	{
		if (this.m_RoleMotion)
		{
			this.m_RoleMotion.PlayExpression_Down(id);
		}
	}

	public void PlayMotion(int id)
	{
		LegAnimator component = base.gameObject.GetComponent<LegAnimator>();
		if (component)
		{
			component.enabled = false;
		}
		this.m_RoleMotion.SetCrossMotion(id);
	}

	public void PlayMotionQueued(int id)
	{
		LegAnimator component = base.gameObject.GetComponent<LegAnimator>();
		if (component)
		{
			component.enabled = false;
		}
		this.m_RoleMotion.SetMotionQueued(id);
	}

	public void PlayIdleMotion()
	{
		LegAnimator component = base.gameObject.GetComponent<LegAnimator>();
		if (component)
		{
			component.enabled = true;
		}
		base.animation.CrossFade("locomotion");
		this.m_bPlayNormalMotion = false;
		this.m_RoleMotion.m_Id = 0;
	}

	public void SetMotionSpeed(int id, float speed)
	{
		this.m_RoleMotion.SetMotionSpeed(id, speed);
	}

	public void PlayFaceMotion(int state)
	{
		if (state == 0)
		{
			switch (Swd6Application.instance.m_GameDataSystem.m_PlayerID)
			{
			case 1:
				this.PlayFaceUpMotion(924);
				this.PlayFaceDownMotion(904);
				return;
			case 2:
				this.PlayFaceUpMotion(918);
				this.PlayFaceDownMotion(904);
				return;
			case 3:
				this.PlayFaceUpMotion(918);
				this.PlayFaceDownMotion(904);
				return;
			case 4:
				this.PlayFaceUpMotion(924);
				this.PlayFaceDownMotion(904);
				return;
			case 5:
				this.PlayFaceUpMotion(918);
				this.PlayFaceDownMotion(904);
				return;
			case 6:
				this.PlayFaceUpMotion(918);
				this.PlayFaceDownMotion(904);
				return;
			default:
				return;
			}
		}
		else
		{
			if (state == 1)
			{
				this.PlayFaceUpMotion(901);
				this.PlayFaceDownMotion(904);
				return;
			}
			if (Swd6Application.instance.m_GameDataSystem.m_PlayerID == 1 || Swd6Application.instance.m_GameDataSystem.m_PlayerID == 2 || Swd6Application.instance.m_GameDataSystem.m_PlayerID == 6)
			{
				this.PlayFaceUpMotion(901);
				this.PlayFaceDownMotion(904);
				return;
			}
			if (Swd6Application.instance.m_GameDataSystem.m_PlayerID == 3 || Swd6Application.instance.m_GameDataSystem.m_PlayerID == 5)
			{
				this.PlayFaceUpMotion(901);
				this.PlayFaceDownMotion(913);
				return;
			}
			this.PlayFaceUpMotion(907);
			this.PlayFaceDownMotion(913);
			return;
		}
	}

	public void SetRun()
	{
		this.m_IsRun = !this.m_IsRun;
		if (!this.m_IsDash)
		{
			UI_Explore.Instance.SetRunState(this.m_IsRun);
		}
	}

	public void Dash()
	{
		if (Swd6Application.instance.m_ExploreSystem.IsUseActionSkill)
		{
			return;
		}
		this.m_IsDash = !this.m_IsDash;
		UI_Explore.Instance.SetDashState(this.m_IsDash, this.m_IsRun);
	}

	public void DisableDash()
	{
		this.m_IsDash = false;
		UI_Explore.Instance.SetDashState(this.m_IsDash, this.m_IsRun);
	}

	public void Jump(bool superJump)
	{
		if (this.m_NoJump)
		{
			return;
		}
		bool flag = false;
		if (this.m_NavMeshAgent != null)
		{
			this.m_NavMeshAgent.enabled = false;
			this.m_PlayerMotor.enabled = true;
			this.m_PlayerMotor.m_NavAgent = false;
		}
		if (superJump)
		{
			this.m_PlayerMotor.jumpHeight = 5f;
			this.m_PlayerMotor.gravity = 15f;
			GameObject gameObject = EffectGenerator.CreateEffectGameObject("ep02_jump_01");
			if (gameObject != null)
			{
				Transform transform = TransformTool.SearchHierarchyForBone(base.transform, "Bip001 Footsteps");
				if (transform != null)
				{
					gameObject.transform.position = transform.position;
					gameObject.transform.parent = transform;
				}
				UnityEngine.Object.DestroyObject(gameObject, 3f);
			}
			this.m_JumpMotor.IsSuperJump = true;
			this.m_PlayerMotor.Jump();
			return;
		}
		bool flag2 = false;
		if (!this.m_LockControl && !this.m_EnterTalk && !flag)
		{
			flag2 = true;
		}
		if (GameInput.GetJoyKeyDown(JOYSTICK_KEY.Y))
		{
			flag2 = true;
		}
		if (flag2)
		{
			this.m_PlayerMotor.jumpHeight = this.m_JumpHeight;
			this.m_PlayerMotor.gravity = this.m_JumpGravity;
			this.m_JumpMotor.IsSuperJump = false;
			this.m_PlayerMotor.Jump();
		}
	}

	public bool IsJump()
	{
		return !(this.m_PlayerMotor == null) && this.m_PlayerMotor.IsJump();
	}

	public bool IsJumpOver()
	{
		return !(this.m_PlayerMotor == null) && this.m_PlayerMotor.IsJumpOver();
	}

	public bool IsGround()
	{
		return this.m_PlayerMotor == null || this.m_PlayerMotor.grounded;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Npc")
		{
			this.m_EnterTalk = true;
		}
		if (other.tag == "MusicEvent")
		{
			other.gameObject.SendMessage("OnTalk", SendMessageOptions.DontRequireReceiver);
			if (Swd6Application.instance.m_ResourceType == ENUM_ResourceType.Develop)
			{
				Debug.Log("執行切換音樂事件_" + other.name);
			}
		}
		if (other.tag == "Event")
		{
			this.m_EnterTalkEvent = true;
			if (Swd6Application.instance.gameStateService.getCurrentState().name == "GameMenuState")
			{
				Swd6Application.instance.PopState();
			}
			if (base.MoveRole)
			{
				return;
			}
			other.gameObject.SendMessage("OnTalk", SendMessageOptions.DontRequireReceiver);
			if (Swd6Application.instance.m_ResourceType == ENUM_ResourceType.Develop)
			{
				Debug.Log("執行事件_" + other.name);
			}
		}
		if (other.tag == "BattleEvent")
		{
			Swd6Application.instance.m_ExploreSystem.SetBattleAreaState(other.name);
		}
		if (other.tag == "WaterEvent" || other.tag == "WaterEvent2")
		{
			if (!this.m_WalkInWater)
			{
				string name;
				if (other.tag == "WaterEvent")
				{
					name = "footstep_Water1";
					this.m_FootStepWaterName = "footstep_Water2";
				}
				else
				{
					name = "footstep_Blood1";
					this.m_FootStepWaterName = "footstep_Blood2";
				}
				if (this.IsJumpOver())
				{
					int num = 256;
					num = ~num;
					RaycastHit raycastHit = default(RaycastHit);
					if (Physics.Raycast(new Ray
					{
						origin = base.gameObject.transform.position + new Vector3(0f, 1f, 0f),
						direction = new Vector3(0f, -1f, 0f)
					}, out raycastHit, 1000f, num))
					{
						GameObject gameObject = EffectGenerator.CreateEffectGameObject(name);
						if (gameObject != null)
						{
							gameObject.transform.position = raycastHit.point;
						}
						gameObject = EffectGenerator.CreateEffectGameObject(this.m_FootStepWaterName);
						if (gameObject != null)
						{
							gameObject.transform.position = raycastHit.point;
						}
					}
					MusicControlSystem.PlaySound(3662, 1);
				}
			}
			this.m_WalkInWater = true;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Npc")
		{
			this.m_EnterTalk = true;
		}
		M_GameTrap component = other.gameObject.GetComponent<M_GameTrap>();
		if (component != null && component.GetTrapData() != null)
		{
			this.m_EnterTalk = false;
		}
		M_GameRoleBase component2 = other.gameObject.GetComponent<M_GameRoleBase>();
		if (component2 && component2.Invalid)
		{
			this.m_EnterTalk = false;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		this.m_EnterTalkEvent = false;
		this.m_EnterTalk = false;
		UI_PartnerTalkDialog.Instance.SetTalkEventObj(null);
		if (other.tag == "MusicEvent")
		{
			if (Swd6Application.instance.m_ResourceType == ENUM_ResourceType.Develop)
			{
				Debug.Log("切回原本地圖音樂!!");
			}
			Swd6Application.instance.m_ExploreSystem.PlayMusic(0.5f);
		}
		if (other.tag == "BattleEvent")
		{
			Swd6Application.instance.m_ExploreSystem.ExitBattleArea(other.name);
		}
		if (other.tag == "WaterEvent" || other.tag == "WaterEvent2")
		{
			if (this.m_WalkInWater && this.IsJump())
			{
				MusicControlSystem.PlaySound(3663, 1);
			}
			this.m_WalkInWater = false;
		}
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
	}

	private void OnFootStrike()
	{
		bool flag = false;
		if (this.IsJump())
		{
			if (this.m_WalkInWater)
			{
				return;
			}
			if (!this.IsGround())
			{
				return;
			}
			flag = true;
		}
		else if (this.m_DirectionVector == Vector3.zero)
		{
			return;
		}
		base.SendMessage("OnFootStep", flag, SendMessageOptions.DontRequireReceiver);
	}
}
