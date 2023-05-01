using System;
using QFramework;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class UIGameStart : MonoBehaviour, ICanSendCommand
    {
        private readonly Lazy<GUIStyle> mLabelStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
        {
            fontSize = 60,
            alignment = TextAnchor.MiddleCenter
        });

        private readonly Lazy<GUIStyle> mButtonStyle = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button)
        {
            fontSize = 40,
            alignment = TextAnchor.MiddleCenter
        });

        public IArchitecture GetArchitecture()
        {
            return Game.Interface;
        }

        private void OnGUI()
        {
            var labelRect = RectHelper.RectForAnchorCenter(Screen.width * 0.5f, Screen.height * 0.5f, 600, 100);

            GUI.Label(labelRect, "土豆兄弟", mLabelStyle.Value);
            
            var buttonRect = RectHelper.RectForAnchorCenter(Screen.width * 0.5f, Screen.height * 0.5f + 150, 300, 100);

            if (GUI.Button(buttonRect, "开始游戏", mButtonStyle.Value))
            {
                AudioKit.PlayMusic("resources://Audio/BackGroundSound");
                AudioKit.Settings.MusicVolume.Value = 0.2f;
                AudioKit.Settings.VoiceVolume.Value = 0.2f;
                AudioKit.Settings.SoundVolume.Value = 0.2f;
                SceneManager.LoadScene("Game");
            }
        }
    }
}