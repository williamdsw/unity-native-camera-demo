using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PropertiesPanel : MonoBehaviour
{
    // || Inspector References
    [SerializeField] private DemoManager demoManager;

    [Header("UI Elements")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI mediaNameText;
    [SerializeField] private TextMeshProUGUI heightText;
    [SerializeField] private TextMeshProUGUI widthText;
    [SerializeField] private TextMeshProUGUI mimeTypeText;
    [SerializeField] private TextMeshProUGUI orientationText;
    [SerializeField] private TextMeshProUGUI durationText;
    [SerializeField] private TextMeshProUGUI rotationText;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        CheckElements();
    }

    private void Start()
    {
        closeButton.onClick.AddListener(() => panel.SetActive(false));
    }

    private void CheckElements()
    {
        if (!demoManager)
        {
            throw new Exception("DemoManager is null!");
        }

        if (!panel)
        {
            demoManager.ShowMessageAndThrowException("Panel is null!");
        }

        if (!mediaNameText)
        {
            demoManager.ShowMessageAndThrowException("MediaNameText is null!");
        }

        if (!heightText)
        {
            demoManager.ShowMessageAndThrowException("HeightText is null!");
        }

        if (!widthText)
        {
            demoManager.ShowMessageAndThrowException("WidthText is null!");
        }

        if (!mimeTypeText)
        {
            demoManager.ShowMessageAndThrowException("MimeTypeText is null!");
        }

        if (!orientationText)
        {
            demoManager.ShowMessageAndThrowException("OrientationText is null!");
        }

        if (!durationText)
        {
            demoManager.ShowMessageAndThrowException("DurationText is null!");
        }

        if (!rotationText)
        {
            demoManager.ShowMessageAndThrowException("RotationText is null!");
        }

        if (!closeButton)
        {
            demoManager.ShowMessageAndThrowException("CloseButton is null!");
        }
    }

    public void ShowImageProperties(string imageName, NativeCamera.ImageProperties properties)
    {
        mimeTypeText.gameObject.SetActive(true);
        orientationText.gameObject.SetActive(true);
        durationText.gameObject.SetActive(false);
        rotationText.gameObject.SetActive(false);

        mediaNameText.text = imageName;
        heightText.text = string.Format("Height: {0}", properties.height);
        widthText.text = string.Format("Width: {0}", properties.width);
        mimeTypeText.text = string.Format("MimeType: {0}", properties.mimeType);
        orientationText.text = string.Format("Orientation: {0}", properties.orientation);

        panel.SetActive(true);
    }

    public void ShowVideoProperties(string videoName, NativeCamera.VideoProperties properties)
    {
        durationText.gameObject.SetActive(true);
        rotationText.gameObject.SetActive(true);
        mimeTypeText.gameObject.SetActive(false);
        orientationText.gameObject.SetActive(false);

        mediaNameText.text = videoName;
        heightText.text = string.Format("Height: {0}", properties.height);
        widthText.text = string.Format("Width: {0}", properties.width);
        durationText.text = string.Format("Duration: {0}", properties.duration);
        rotationText.text = string.Format("Rotation: {0}", properties.rotation);

        panel.SetActive(true);
    }
}
