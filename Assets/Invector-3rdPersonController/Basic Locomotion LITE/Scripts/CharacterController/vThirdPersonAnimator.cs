using UnityEngine;
using System.Collections;

namespace Invector.CharacterController
{
    public abstract class vThirdPersonAnimator : vThirdPersonMotor
    {
        public virtual void UpdateAnimator()
        {
            if (animator == null || !animator.enabled) return;

            photonView.RPC("RPCIsStrafing", PhotonTargets.All, isStrafing);
            photonView.RPC("RPCIsGrounded", PhotonTargets.All, isGrounded);
            photonView.RPC("RPCGroundDistance", PhotonTargets.All, groundDistance);

            if (!isGrounded)
                photonView.RPC("RPCVerticalVelocity", PhotonTargets.All, verticalVelocity);

            if (isStrafing)
            {
                // strafe movement get the input 1 or -1
                photonView.RPC("RPCInputHorizontal", PhotonTargets.All, direction);
            }

            // free movement get the input 0 to 1
            photonView.RPC("RPCInputVertical", PhotonTargets.All, speed);
        }
        
        [PunRPC]
        protected virtual void RPCIsStrafing(bool rpcIsStrafing)
        {
            if (animator == null)
                animator = GetComponent<Animator> ();  
            animator.SetBool("IsStrafing", rpcIsStrafing);
        }
        
        [PunRPC]
        protected virtual void RPCIsGrounded(bool rpcIsGrounded)
        {
            if (animator == null)
                animator = GetComponent<Animator> ();  
            animator.SetBool("IsGrounded", rpcIsGrounded);
        }
        
        [PunRPC]
        protected virtual void RPCGroundDistance(float rpcGroundDistance)
        {
            if (animator == null)
                animator = GetComponent<Animator> ();  
            animator.SetFloat("GroundDistance", rpcGroundDistance);
        }
        
        [PunRPC]
        protected virtual void RPCVerticalVelocity(float rpcVerticalVelocity)
        {
            if (animator == null)
                animator = GetComponent<Animator> ();  
            animator.SetFloat("VerticalVelocity", rpcVerticalVelocity);
        }
        
        [PunRPC]
        protected virtual void RPCInputHorizontal(float rpcDirection)
        {
            if (animator == null)
                animator = GetComponent<Animator> ();
            animator.SetFloat("InputHorizontal", rpcDirection, 0.1f, Time.deltaTime);
        }
        
        [PunRPC]
        protected virtual void RPCInputVertical(float rpcSpeed)
        {
            if (animator == null)
                animator = GetComponent<Animator> ();  
            animator.SetFloat("InputVertical", rpcSpeed, 0.1f, Time.deltaTime);
        }

        public void OnAnimatorMove()
        {
            // we implement this function to override the default root motion.
            // this allows us to modify the positional speed before it's applied.
            if (isGrounded)
            {
                transform.rotation = animator.rootRotation;

                var speedDir = Mathf.Abs(direction) + Mathf.Abs(speed);
                speedDir = Mathf.Clamp(speedDir, 0, 1);
                var strafeSpeed = (isSprinting ? 1.5f : 1f) * Mathf.Clamp(speedDir, 0f, 1f);
                
                // strafe extra speed
                if (isStrafing)
                {
                    if (strafeSpeed <= 0.5f)
                        ControlSpeed(strafeWalkSpeed);
                    else if (strafeSpeed > 0.5f && strafeSpeed <= 1f)
                        ControlSpeed(strafeRunningSpeed);
                    else
                        ControlSpeed(strafeSprintSpeed);
                }
                else if (!isStrafing)
                {
                    // free extra speed                
                    if (speed <= 0.5f)
                        ControlSpeed(freeWalkSpeed);
                    else if (speed > 0.5 && speed <= 1f)
                        ControlSpeed(freeRunningSpeed);
                    else
                        ControlSpeed(freeSprintSpeed);
                }
            }
        }
    }
}