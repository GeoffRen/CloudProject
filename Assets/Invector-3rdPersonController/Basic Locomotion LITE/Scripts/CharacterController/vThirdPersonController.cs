using UnityEngine;
using System.Collections;

namespace Invector.CharacterController
{
    public class vThirdPersonController : vThirdPersonAnimator
    {
        public GameObject beam;
        public Transform origin;
        private Coroutine shootCoroutine;
        
        protected virtual void Start()
        {
#if !UNITY_EDITOR
                Cursor.visible = false;
#endif
        }

        public virtual void Sprint(bool value)
        {                                   
            isSprinting = value;            
        }

        public virtual void Strafe()
        {
            if (locomotionType == LocomotionType.OnlyFree) return;
            isStrafing = !isStrafing;
        }

        public virtual void Jump()
        {
            // conditions to do this action
            bool jumpConditions = isGrounded && !isJumping;
            // return if jumpCondigions is false
            if (!jumpConditions) return;
            // trigger jump behaviour
            jumpCounter = jumpTimer;            
            isJumping = true;
            // trigger jump animations            
            if (_rigidbody.velocity.magnitude < 1)
                photonView.RPC("RPCJump", PhotonTargets.All);
            else
                photonView.RPC("RPCJumpMove", PhotonTargets.All);
        }
        
        [PunRPC]
        protected virtual void RPCJump()
        {
            if (animator == null)
                animator = GetComponent<Animator> ();  
            cancelShoot();
            animator.CrossFadeInFixedTime("Jump", 0.1f);
        }
        
        [PunRPC]
        protected virtual void RPCJumpMove()
        {
            if (animator == null)
                animator = GetComponent<Animator> ();  
            cancelShoot();
            animator.CrossFadeInFixedTime("JumpMove", 0.2f);
        }

		public virtual void Attack()
		{
		    if (isJumping || !isGrounded)
            {
                return;
            }
			Debug.Log ("Attack");
			photonView.RPC ("RPCAttack", PhotonTargets.All);
		}

		[PunRPC]
		protected virtual void RPCAttack() 
        {
            if (animator == null)
                animator = GetComponent<Animator> ();    
            animator.CrossFadeInFixedTime("spell", 0.2f);
            
            cancelShoot();
            shootCoroutine = StartCoroutine(ShootBeam());
        }
        
        IEnumerator ShootBeam()
        {
            yield return new WaitForSeconds(1.5f);
            beam.SetActive(true);
            yield return new WaitForSeconds(.75f);
            beam.SetActive(false);
        }
        
        private void cancelShoot()
        {
            if (shootCoroutine != null)
            {
                StopCoroutine(shootCoroutine);
            }
            beam.SetActive(false);
        }
        
        [PunRPC]
        

        public virtual void RotateWithAnotherTransform(Transform referenceTransform)
        {
            var newRotation = new Vector3(transform.eulerAngles.x, referenceTransform.eulerAngles.y, transform.eulerAngles.z);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(newRotation), strafeRotationSpeed * Time.fixedDeltaTime);
            targetRotation = transform.rotation;
        }
    }
}