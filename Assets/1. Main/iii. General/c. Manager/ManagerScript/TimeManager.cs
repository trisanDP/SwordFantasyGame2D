using UnityEngine;
using System;

namespace BrokenLands
{
    public class TimeManager : MonoBehaviour {
        [Header("Time Settings")]
        public float timeRatio = 1f;
        public int dayStartHour = 6;
        public int nightStartHour = 18;

        public event Action OnDayStarted;
        public event Action OnNightStarted;

        private float realTimeElapsed = 0f;
        private int inGameMinutesPassed = 0;
        private int currentInGameHour = 0;
        private int daysPassed = 0;
        private float currentTime = 0f;

        GameManager gameManager;

        private void Start() {
            gameManager = GameManager.Instance;
        }
        private void Update() {
            // Only update time if game is in Play state
            if(GameManager.Instance != null && GameManager.Instance.GetCurrentState() == GameManager.GameState.Play) {
                UpdateInGameTime();
                CheckTimeEvents();
                UpdateClock();
            }
        }

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

        public void PauseTime() {
            Time.timeScale = 0f;
            Debug.Log("Time Paused");
        }

        public void ResumeTime() {
            Time.timeScale = 1f;
            Debug.Log("Time Resumed");
        }

        private void UpdateClock() {
            int hours = (inGameMinutesPassed / 60) % 24;
            int minutes = inGameMinutesPassed % 60;
/*            gameManager.uiManager?.UpdateClockUI(hours, minutes);*/
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
    }

}
