using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroController : MonoBehaviour
{
    [Header("Entity")]
    [SerializeField] private HeroEntity _entity;
    private bool _entityWasTouchingGround = false;

    [Header("Coyote Time")]
    [SerializeField] private float _coyoteTimeDuration = 0.2f;
    private float _coyoteTimeCountdown = -1f;


    [Header("Debug")]
    [SerializeField] private bool _guiDebug = false;

    [Header("Jump Buffer")]
    [SerializeField] private float _jumpBufferDuration = 0.2f;
    private float _jumpBufferTimer = 0f;

    private void OnGUI()
    {
        if (!_guiDebug) return;

        GUILayout.BeginVertical(GUI.skin.box);
        GUILayout.Label(gameObject.name);
        GUILayout.Label($"Jump Buffer Timer = {_jumpBufferTimer}");
        GUILayout.Label($"CoyoteTime Countdown = {_coyoteTimeCountdown}");
        GUILayout.EndVertical();
    }

    private void Update()
    {
        UpdateJumpBuffer();

        _entity.SetMoveDirX(GetInputMoveX());

        if (GetInputHeal())
        {
            _entity.Heal(5);
        }

        if (GetInputInv())
        {
            _entity.SetInv();
        }

        if (EntityHasExitGround())
        {
            ResetCoyoteTime();
        }
        else
        {
            UpdateCoyoteTime();
        }

        if (GetInputDownJump())
        {
            if (_entity.IsTouchingGround || !_entity.IsJumping && !_entity.IsJumping)
            {
                _entity.JumpStart();
            }
            else
            {
                ResetJumpBuffer();
            }
        }

        if (IsJumperBufferActive())
        {
            if ((_entity.IsTouchingGround || IsCoyoteTimeActive()) && !_entity.IsJumping)
            {
                _entity.JumpStart();
            }
        }

        _entityWasTouchingGround = _entity.IsTouchingGround;

        if (_entity.IsJumpImpulsing)
        {
            if (!GetInputJump() && _entity.IsJumpMinDurationReached)
            {
                _entity.StopJumpImpulsion();
            }
        }
        GetInputDashX();
    }


    private float GetInputMoveX()
    {
        float inputMoveX = 0f;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Q))
        {
            //Negative means : To the left <=
            inputMoveX = -1f;
        }

        if (Input.GetKey(KeyCode.D))
        {
            //Postive means : To the right =>
            inputMoveX = 1f;
        }

        return inputMoveX;
    }

    private void GetInputDashX()
    {
        if (Input.GetKey(KeyCode.E))
        {
            _entity._StartDash();
        };

    }


    private bool GetInputDownJump()
    {
        return Input.GetKey(KeyCode.Space);
    }

    private bool GetInputJump()
    {
        return Input.GetKey(KeyCode.Space);
    }

    private bool GetInputHeal()
    {
        return Input.GetKey(KeyCode.R);
    }

    private bool GetInputInv()
    {
        return Input.GetKeyDown(KeyCode.V);
    }

    private void ResetJumpBuffer()
    {
        _jumpBufferTimer = 0f;
    }
    private bool IsJumperBufferActive()
    {
        return _jumpBufferTimer < _jumpBufferDuration;
    }

    private void UpdateJumpBuffer()
    {
        if (!IsJumperBufferActive()) return;
        {
            _jumpBufferTimer += Time.deltaTime;
        }
    }

    private void CancelJumpBuffer()
    {
        _jumpBufferTimer = _jumpBufferDuration;
    }

    private void Start()
    {
        CancelJumpBuffer();
    }

    private void UpdateCoyoteTime()
    {
        if (!IsCoyoteTimeActive()) return;
        {
            _coyoteTimeCountdown -= Time.deltaTime;
        }
    }

    private void ResetCoyoteTime()
    {
        _coyoteTimeCountdown = _coyoteTimeDuration;
    }

    private bool IsCoyoteTimeActive()
    {
        return _coyoteTimeCountdown > 0f;
    }

    private bool EntityHasExitGround()
    {
        return _entityWasTouchingGround && !_entity.IsTouchingGround;
    }
}
