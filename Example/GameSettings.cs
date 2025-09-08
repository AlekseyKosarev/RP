using RP.Core;
using UnityEngine;

namespace RP.Example
{
    public static class GameSettings
    {
        public static readonly RP<float> SoundMultiplier = new(1f);
        public static readonly RP<float> MusicMultiplier = new(0.8f);
        public static readonly RP<int> FrameRate = new(60);
        public static readonly RP<bool> VibrationEnabled = new(true);

        // Удобные методы для изменения с валидацией
        public static void SetSoundMultiplier(float value)
        {
            value = Mathf.Clamp01(value);
            SoundMultiplier.Value = value;
        }

        public static void SetMusicMultiplier(float value)
        {
            value = Mathf.Clamp01(value);
            MusicMultiplier.Value = value;
        }

        public static void SetFrameRate(int value)
        {
            value = Mathf.Clamp(value, 30, 120);
            FrameRate.Value = value;
            Application.targetFrameRate = value;
        }

        public static void SetVibration(bool enabled)
        {
            VibrationEnabled.Value = enabled;
        }

        // Инициализация (если нужно загрузить из сохранения)
        public static void InitializeDefaults()
        {
            // Можно вызвать при старте игры
            // Значения уже установлены в конструкторах
        }
    }
}