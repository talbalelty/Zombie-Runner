using System;
using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool fire;
		public bool aimDownSight;
		public bool weaponPickup;
		public bool throwGrenade;
		public int selectedWeaponIndex = -1;
		public int scroll;


		[Header("Movement Settings")]
		public bool analogMovement;

#if !UNITY_IOS || !UNITY_ANDROID
		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
#endif

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			MoveInput(value.Get<Vector2>());
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnFire(InputValue value)
		{
			FireInput(value.isPressed);
		}

		public void OnAimDownSight(InputValue value)
        {
			AimDownSightInput(value.isPressed);
        }

		public void OnWeaponPickup(InputValue value)
        {
			WeaponPickup(value.isPressed);
        }

		public void OnThrowGrenade(InputValue value)
        {
			ThrowGrenade(value.isPressed);
        }

		public void OnPrimaryWeapon(InputValue value)
        {
			PrimaryWeapon(value.isPressed);
        }

		public void OnSecondaryWeapon(InputValue value)
		{
			SecondaryWeapon(value.isPressed);
		}

		public void OnSpecialWeapon(InputValue value)
		{
			SpecialWeapon(value.isPressed);
		}

		public void OnCycleWeapons(InputValue value)
		{
			CycleWeapons(value.Get<Vector2>());
		}


#else
	// old input sys if we do decide to have it (most likely wont)...
#endif


        public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}

		public void FireInput(bool newFireState)
		{
			fire = newFireState;
		}

		public void AimDownSightInput(bool newAimState)
		{
			aimDownSight = newAimState;
		}

		public void WeaponPickup(bool newPickupState)
		{
			weaponPickup = newPickupState;
		}

		public void ThrowGrenade(bool newThrowState)
		{
			throwGrenade = newThrowState;
		}

		public void PrimaryWeapon(bool newSelectedWeaponState)
		{
            if (newSelectedWeaponState)
            {
				selectedWeaponIndex = 0;
            }
		}

		public void SecondaryWeapon(bool newSelectedWeaponState)
		{
			if (newSelectedWeaponState)
			{
				selectedWeaponIndex = 1;
			}
		}

		public void SpecialWeapon(bool newSelectedWeaponState)
		{
			if (newSelectedWeaponState)
			{
				selectedWeaponIndex = 2;
			}
		}

		public void CycleWeapons(Vector2 newCycleState)
		{
			scroll = Mathf.RoundToInt(newCycleState.normalized.y);
		}

#if !UNITY_IOS || !UNITY_ANDROID

		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}

#endif

	}
	
}