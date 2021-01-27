using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DemoManager : MonoBehaviour
{
    // || Inspector References

    [SerializeField] private PropertiesPanel propertiesPanel;

    [Header("UI Elements")]
    [SerializeField] private Button pictureButton;
    [SerializeField] private Button videoButton;
    [SerializeField] private Button playVideoButton;
    [SerializeField] private Button mediaPropertiesButton;
    [SerializeField] private TextMeshProUGUI messageText;
    [SerializeField] private RawImage photoContentImage;

    // || Config

    private const int PICTURE_MAX_SIZE = 512;

    private void Awake()
    {
        CheckElements();
    }

    private void Start()
    {
        messageText.text = string.Empty;
        mediaPropertiesButton.interactable = false;
        playVideoButton.gameObject.SetActive(false);

        if (CheckIfDeviceHasCamera())
        {
            pictureButton.onClick.AddListener(TakePicture);
            videoButton.onClick.AddListener(RecordVideo);
        }
        else
        {
            pictureButton.interactable = videoButton.interactable = false;
        }
    }

    private void CheckElements()
    {
        string message = string.Empty;

        if (!messageText)
        {
            message = "MessageText is null!";
            throw new Exception(message);
        }

        if (!propertiesPanel)
        {
            message = "Properties Panel is null!";
            messageText.text = message;
            throw new Exception(message);
        }

        if (!pictureButton)
        {
            message = "PictureButton is null!";
            messageText.text = message;
            throw new Exception(message);
        }

        if (!videoButton)
        {
            message = "VideoButton is null!";
            messageText.text = message;
            throw new Exception(message);
        }

        if (!playVideoButton)
        {
            message = "PlayVideoButton is null!";
            messageText.text = message;
            throw new Exception(message);
        }

        if (!mediaPropertiesButton)
        {
            message = "MediaPropertiesButton is null!";
            messageText.text = message;
            throw new Exception(message);
        }

        if (!photoContentImage)
        {
            message = "Content is null!";
            messageText.text = message;
            throw new Exception(message);
        }
    }

    private bool CheckIfDeviceHasCamera()
    {
        bool hasCamera = NativeCamera.DeviceHasCamera();
        if (!hasCamera)
        {
            messageText.text = "Your device doesn't have a camera!";
        }

        return hasCamera;
    }

    private void TakePicture()
    {
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrWhiteSpace(path))
            {
                ApplyMediaTreatment(path, true);
            }

        }, PICTURE_MAX_SIZE);
    }

    private void RecordVideo()
    {
        NativeCamera.Permission permission = NativeCamera.RecordVideo((path) =>
        {
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrWhiteSpace(path))
            {
                // TODO
                ApplyMediaTreatment(path, false);
            }
        });
    }

    private void ApplyMediaTreatment(string path, bool isPicture)
    {
        mediaPropertiesButton.interactable = true;
        mediaPropertiesButton.onClick.RemoveAllListeners();

        if (isPicture)
        {
            Texture2D texture = NativeCamera.LoadImageAtPath(path, PICTURE_MAX_SIZE);
            if (CheckAndApplyTexture(path, texture))
            {
                mediaPropertiesButton.onClick.AddListener(() =>
                {
                    string imageName = StringUtils.GetMediaName(path);
                    propertiesPanel.ShowImageProperties(imageName, NativeCamera.GetImageProperties(path));
                });
            }

            playVideoButton.gameObject.SetActive(false);
        }
        else
        {
            Texture2D texture = NativeCamera.GetVideoThumbnail(path, PICTURE_MAX_SIZE);
            if (CheckAndApplyTexture(path, texture))
            {
                mediaPropertiesButton.onClick.AddListener(() =>
                {
                    string videoName = StringUtils.GetMediaName(path);
                    propertiesPanel.ShowVideoProperties(videoName, NativeCamera.GetVideoProperties(path));
                });
            }

            playVideoButton.gameObject.SetActive(true);
            playVideoButton.onClick.RemoveAllListeners();
            playVideoButton.onClick.AddListener(() =>
            {
                string filePath = string.Format("file://{0}", path);
                Debug.Log("FilePath: " + filePath);
                Handheld.PlayFullScreenMovie(filePath);
            });
        }
    }

    public bool CheckAndApplyTexture(string path, Texture2D texture)
    {
        if (texture == null)
        {
            string message = "Couldn't load texture from {0} !";
            message = string.Format(message, path);

            Debug.LogError(message);

            if (message.Length >= 43)
            {
                message = message.Substring(0, 40);
                message += "...";
            }

            messageText.text = message;
            throw new Exception(message);
        }

        Color color = photoContentImage.color;
        color = Color.white;
        photoContentImage.color = color;
        photoContentImage.texture = texture;

        return true;
    }

    public void ShowMessageAndThrowException(string message)
    {
        messageText.text = message;
        throw new Exception(message);
    }
}
