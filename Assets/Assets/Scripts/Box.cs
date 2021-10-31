using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Interactable))]
public class Box : MonoBehaviour
{
    public BoxType Type;

    private Interactable interactable;
    private new Rigidbody rigidbody;
    private const Hand.AttachmentFlags AttachmentFlags = Hand.defaultAttachmentFlags &
                                                         ~Hand.AttachmentFlags.SnapOnAttach &
                                                         ~Hand.AttachmentFlags.DetachOthers &
                                                         ~Hand.AttachmentFlags.VelocityMovement;

    private void Start() => rigidbody = GetComponent<Rigidbody>();

    private void Awake() => interactable = GetComponent<Interactable>();

    private void HandHoverUpdate(Hand hand)
    {
        var startingGrabType = hand.GetGrabStarting();
        var isGrabEnding = hand.IsGrabEnding(gameObject);

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            hand.HoverLock(interactable);
            hand.AttachObject(gameObject, startingGrabType, AttachmentFlags);
        }
        else if (isGrabEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(interactable);
        }
    }

    private void OnAttachedToHand(Hand hand) => rigidbody.mass = 0;

    private void OnDetachedFromHand(Hand hand) => rigidbody.mass = 0.5f;
}