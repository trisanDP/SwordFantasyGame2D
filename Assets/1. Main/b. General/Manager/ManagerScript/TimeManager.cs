using UnityEngine;
using System;

namespace BrokenLands {
    public class TimeManager : MonoBehaviour {
        #region Fields and Properties
        [Header("Time Settings")]
        [Tooltip("Time ratio multiplier. Adjust this to change the in-game time progression.")]
        public float timeRatio = 1f; // Allows only values between 1 and 2
        public int dayStartHour = 6;
        public int nightStartHour = 18;

        public event Action OnDayStarted;
        public event Action OnNightStarted;

        private float realTimeElapsed = 0f;
        private int inGameMinutesPassed = 0;
        private int currentInGameHour = 0;
        private int daysPassed = 0;
        private float currentTime = 0f;

        private bool isPaused = false;

        private GameManager gameManager;
        #endregion

        #region Unity Lifecycle
        private void Start() {
            gameManager = GameManager.Instance;
        }

        private void Update() {
            // Only update time if the game is in Play state and not paused
            if(GameManager.Instance != null /* && GameManager.Instance.GetCurrentState() == GameState.Play */ && !isPaused) {
                UpdateInGameTime();
                CheckTimeEvents();
                UpdateClock();
            }
        }
        #endregion

        #region Time Management
        private void UpdateInGameTime() {
            realTimeElapsed += Time.deltaTime;
            int newInGameMinutes = Mathf.FloorToInt(realTimeElapsed * timeRatio);

            if(newInGameMinutes != inGameMinutesPassed) {
                inGameMinutesPassed = newInGameMinutes;
                currentInGameHour = (inGameMinutesPassed / 60) % 24;
                currentTime = currentInGameHour + (inGameMinutesPassed % 60) / 60f;

                if(inGameMinutesPassed >= 1440) {
                    daysPassed++;
                    inGameMinutesPassed -= 1440;
                }
            }
        }

        private void CheckTimeEvents() {
            if(currentInGameHour == dayStartHour) {
                TriggerDayStart();
            } else if(currentInGameHour == nightStartHour) {
                TriggerNightStart();
            }
        }

        private void TriggerDayStart() {
            OnDayStarted?.Invoke();
            Debug.Log("Day has started!");
        }

        private void TriggerNightStart() {
            OnNightStarted?.Invoke();
            Debug.Log("Night has started!");
        }
        #endregion

        #region Pause and Time Controls
        public void ToggleTimePause() {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0f : 1f;
            Debug.Log(isPaused ? "Time Paused" : "Time Resumed");
        }
        public void PauseTime() {
            Time.timeScale = 0f;
            Debug.Log("Time Paused");
            isPaused = true;
        }

        public void ResumeTime() {
            Time.timeScale = 1f;
            Debug.Log("Time Resumed");
            isPaused = false;
        }

        public void SlowDownTime() {
            timeRatio = Mathf.Max(1f, timeRatio / 10f);
            Debug.Log($"Time ratio set to {timeRatio}");
        }

        public void SpeedUpTime() {
            timeRatio = Mathf.Min(1000f, timeRatio * 10);
            Debug.Log($"Time ratio set to {timeRatio}");
        }
        #endregion

        #region Clock and UI Updates
        private void UpdateClock() {
            int hours = (inGameMinutesPassed / 60) % 24;
            int minutes = inGameMinutesPassed % 60;
            UiManager.Instance?.UpdateClockUI(hours, minutes);
        }

        public string GetInGameTime() {
            int hours = (inGameMinutesPassed / 60) % 24;
            int minutes = inGameMinutesPassed % 60;
            return string.Format("{0:D2}:{1:D2}", hours, minutes);
        }

        public int GetDaysPassed() {
            return daysPassed;
        }

        public float GetCurrentTime() {
            return currentTime;
        }
        #endregion
    }
}
