using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Implementing the FABRIK algorithm
// TODO1: Attach this script to the end bone of the arm you want to apply IK on. 
// TODO2: Change number of bones to how many bones there are in your arm

// Referenced from: https://www.youtube.com/watch?v=qqOAzn05fvk

[ExecuteInEditMode]
public class IK : MonoBehaviour
{
    public int numberOfBones = 2; // How many bones do we want to do IK on
    public Transform target;
    public Transform pole;
    public int numberOfIterations = 10; // How many IK iterations do we want to run every update loop
    public float epsilon = 0.1f; // How close the end bone should be to the target before we stop the algorithm

    Transform[] bones;
    float[] bonesLengths;
    float totalBonesLengths = 0.0f;
    Vector3[] bonePositions; // Array to store bones' positions for IK calculations
    Vector3 baseBonePosition;

    void Awake()
    {
        bones = new Transform[numberOfBones];
        bonesLengths = new float[numberOfBones - 1];
        bonePositions = new Vector3[numberOfBones];

        if (target == null)
        {
            target = new GameObject(gameObject.name + "_Target").transform;
        }

        // Initialize bones array
        Transform currentBone = transform;
        for (int i = numberOfBones - 1; i >= 0; i--)
        {
            bones[i] = currentBone;
            currentBone = currentBone.parent;
        }

        // Initialize bonesLengths array
        for (int i = 0; i <= numberOfBones - 2; i++)
        {
            bonesLengths[i] = Vector3.Distance(bones[i].position, bones[i+1].position);
            totalBonesLengths += bonesLengths[i];
        }
    }

    void LateUpdate()
    {
        FABRIKUpdate();
    }

    // Implement an IK algorithm called FABRIK (Forward And Backward Reaching Inverse Kinematics)
    void FABRIKUpdate()
    {
        if (target == null)
        {
            return;
        }

        // Store each bone's position into an array for IK calculations
        for (int i = 0; i < numberOfBones; i++)
        {
            bonePositions[i] = bones[i].position;
        }

        baseBonePosition = bonePositions[0];

        // If target is outside of the arm's reach, simply stretch the arm fully
        if ((target.position - baseBonePosition).sqrMagnitude > totalBonesLengths * totalBonesLengths)
        {
            Vector3 dir = (target.position - baseBonePosition).normalized;
            for (int i = 1; i < numberOfBones; i++)
            {
                bonePositions[i] = bonePositions[i-1] + (dir * bonesLengths[i-1]);
            }
        }

        // If target is within arm's reach, do the FABRIK algorithm
        else
        {
            for (int iteration = 0; iteration < numberOfIterations; iteration++)
            {
                // If the end bone is close enough to the target, stop iterating
                if ((target.position - bonePositions[numberOfBones-1]).sqrMagnitude < epsilon * epsilon)
                {
                    break;
                }

                // Forward reaching IK
                bonePositions[numberOfBones-1] = target.position; // Set end bone to target
                for (int i = numberOfBones-2; i >= 0; i--)
                {
                    Vector3 dir = (bonePositions[i] - bonePositions[i+1]).normalized;
                    bonePositions[i] = bonePositions[i+1] + (dir * bonesLengths[i]);
                }

                // Backward reaching IK
                bonePositions[0] = baseBonePosition; // Set base bone to base position
                for (int i = 1; i <= numberOfBones-1; i++)
                {
                    Vector3 dir = (bonePositions[i] - bonePositions[i-1]).normalized;
                    bonePositions[i] = bonePositions[i-1] + (dir * bonesLengths[i-1]);
                }
            }
        }

        // Move the whole arm to the pole
        if (pole != null)
        {
            for (int i = 1; i < numberOfBones-1; i++)
            {
                Plane plane = new Plane(bonePositions[i+1] - bonePositions[i-1], bonePositions[i-1]);

                Vector3 boneOnPlane = plane.ClosestPointOnPlane(bonePositions[i]);
                Vector3 poleOnPlane = plane.ClosestPointOnPlane(pole.position);

                float angle = Vector3.SignedAngle(boneOnPlane - bonePositions[i-1], poleOnPlane - bonePositions[i-1], plane.normal);
                bonePositions[i] = Quaternion.AngleAxis(angle, plane.normal) * (bonePositions[i] - bonePositions[i-1]) + bonePositions[i-1];
            }
        }

        // Update the bones' rotations & positions
        for (int i = 0; i < numberOfBones-1; i++)
        {
            // if (i == numberOfBones-1)
            // {
            //     // Debug.Log("Rotate end bone");
            //     // bones[i].rotation = Quaternion.FromToRotation(initTargetForward, target.forward) * bones[i].rotation;
            // }
            // else
            {
                Vector3 initBoneDir = bones[i+1].position - bones[i].position;
                Vector3 currentBoneDir = bonePositions[i+1] - bonePositions[i];
                bones[i].rotation = Quaternion.FromToRotation(initBoneDir, currentBoneDir) * bones[i].rotation;
            }

            // Update positions
            bones[i].position = bonePositions[i];
        } 
    }

    void OnDrawGizmos()
    {
        Transform currentBone = transform;
        for (int i = 0; i < numberOfBones; i++)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(currentBone.position, 0.3f);
            currentBone = currentBone.parent;
        }
    }
}
