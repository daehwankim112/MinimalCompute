using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction.Input;
using UnityEngine;

public class HandsController : MonoBehaviour
{
    [SerializeField] private Hand _leftHand;
    [SerializeField] private Hand _rightHand;
    [SerializeField] private Fluid3D _fluid3D;
    [SerializeField] private Transform[] _spheres;
    
    private static int _handPosesCount = (int) HandJointId.HandMaxSkinnable;
    
    private Pose[] _leftHandPoses = new Pose[_handPosesCount];
    [SerializeField] private Vector3[] _leftHandPosesLocations = new Vector3[_handPosesCount];
    [SerializeField] private bool[] _isLeftHandPosesLocationsInFluid = new bool[_handPosesCount];
    
    private Pose[] _rightHandPoses = new Pose[_handPosesCount];
    [SerializeField] private Vector3[] _rightHandPosesLocations = new Vector3[_handPosesCount];
    [SerializeField] private bool[] _isRightHandPosesLocationsInFluid = new bool[_handPosesCount];

    private float _scale;
    
    void Start()
    {
        _scale = _fluid3D.gameObject.transform.localScale.x;
    }

    void Update()
    {
        UpdateHandsBonesPositionsRelatedToFluid();
        UpdateHandsBonesSpheresInFluid();
    }

    private void UpdateHandsBonesSpheresInFluid()
    {
        for (int i = 0; i < _handPosesCount; i++)
        {
            if (i == (int) HandJointId.HandThumbTip)
            {
                if (_isLeftHandPosesLocationsInFluid[i])
                    _spheres[0].position = _leftHandPosesLocations[i];
                if (_isRightHandPosesLocationsInFluid[i])
                    _spheres[5].position = _rightHandPosesLocations[i];
            }
            if (i == (int) HandJointId.HandIndexTip)
            {
                if (_isLeftHandPosesLocationsInFluid[i])
                    _spheres[1].position = _leftHandPosesLocations[i];
                if (_isRightHandPosesLocationsInFluid[i])
                    _spheres[6].position = _rightHandPosesLocations[i];
            }
            if (i == (int) HandJointId.HandMiddleTip)
            {
                if (_isLeftHandPosesLocationsInFluid[i])
                    _spheres[2].position = _leftHandPosesLocations[i];
                if (_isRightHandPosesLocationsInFluid[i])
                    _spheres[7].position = _rightHandPosesLocations[i];
            }
            if (i == (int) HandJointId.HandRingTip)
            {
                if (_isLeftHandPosesLocationsInFluid[i])
                    _spheres[3].position = _leftHandPosesLocations[i];
                if (_isRightHandPosesLocationsInFluid[i])
                    _spheres[8].position = _rightHandPosesLocations[i];
            }
            if (i == (int) HandJointId.HandPinkyTip)
            {
                if (_isLeftHandPosesLocationsInFluid[i])
                    _spheres[4].position = _leftHandPosesLocations[i];
                if (_isRightHandPosesLocationsInFluid[i])
                    _spheres[9].position = _rightHandPosesLocations[i];
            }
        }
    }
    
    private void UpdateHandsBonesPositionsRelatedToFluid()
    {
        if (_leftHand.IsTrackedDataValid)
        {
            for (int i = 0; i < _handPosesCount; i++)
            {
                _leftHand.GetJointPose((HandJointId) i, out _leftHandPoses[i]);
                _leftHandPosesLocations[i] = _leftHandPoses[i].position;

                if (_leftHandPosesLocations[i].x >= - _scale / 2
                    && _leftHandPosesLocations[i].x <= _scale / 2
                    && _leftHandPosesLocations[i].y >= - _scale / 2
                    && _leftHandPosesLocations[i].y <= _scale / 2
                    && _leftHandPosesLocations[i].z >= - _scale / 2
                    && _leftHandPosesLocations[i].z <= _scale / 2)
                {
                    _isLeftHandPosesLocationsInFluid[i] = true;
                }
                else
                {
                    _isLeftHandPosesLocationsInFluid[i] = false;
                }
            }
        }

        if (_rightHand.IsTrackedDataValid)
        {
            for (int i = 0; i < (int) HandJointId.HandMaxSkinnable; i++)
            {
                _rightHand.GetJointPose((HandJointId) i, out _rightHandPoses[i]);
                _rightHandPosesLocations[i] = _rightHandPoses[i].position;

                if (_rightHandPosesLocations[i].x >= - _scale / 2
                    && _rightHandPosesLocations[i].x <= _scale / 2
                    && _rightHandPosesLocations[i].y >= - _scale / 2
                    && _rightHandPosesLocations[i].y <= _scale / 2
                    && _rightHandPosesLocations[i].z >= - _scale / 2
                    && _rightHandPosesLocations[i].z <= _scale / 2)
                {
                    _isRightHandPosesLocationsInFluid[i] = true;
                }
                else
                {
                    _isRightHandPosesLocationsInFluid[i] = false;
                }
            }
        }
    }
}
