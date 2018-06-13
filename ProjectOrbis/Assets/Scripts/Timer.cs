using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbis {

    namespace Timing{

        public class Timer
        {

            float StartTime;
            float EndTime;
            float TotalTime;

            public bool isStarted = false;

            /// <summary>
            /// Starts timer.
            /// </summary>
            public void Start()
            {
                if (isStarted == false) {
                    StartTime = Time.time;
                    isStarted = true;
                } else {
                    Debug.LogWarning("Timer is already started");
                }
            }

            /// <summary>
            /// Stops timer and returns total time in seconds;
            /// </summary>
            /// <returns>Time in seconds</returns>
            public float Stop()
            {
                if (isStarted) {
                    EndTime = Time.time;
                    TotalTime = EndTime - StartTime;
                    isStarted = false;
                    return TotalTime;
                } else {
                    Debug.LogError("Timer has not started");
                    return 0f;
                }
                
;
            }

            /// <summary>
            /// Resets timer to initial settings
            /// </summary>
            public void Reset()
            {
                TotalTime = 0;
                StartTime = 0;
                EndTime = 0;
            }

            /// <summary>
            /// Formats time given in MM:SS.XX
            /// </summary>
            /// <param name="time">the time in seconds to format</param>
            /// <returns>formatted time as a string</returns>
            public TimeData FormatTime(float time)
            {
                return new TimeData(time);
            }

            /// <summary>
            /// Gets the timer's last time, formatted by the FormatTime method
            /// </summary>
            /// <returns></returns>
            public TimeData GetFormattedTime()
            {
                return FormatTime(TotalTime);
            }

            public string GetCurrentTimeString()
            {
                if (isStarted) {
                    float currTime = Time.time;
                    float passed = currTime - StartTime;
                    float minutes = Mathf.Floor(passed / 60);
                    float seconds = (passed % 60);

                    return minutes.ToString("00") + ":" + seconds.ToString("00.00");
                } else {
                    return "NULL";
                }

            }
        }


        public struct TimeData {

            public float Minutes { get; private set; }
            public float Seconds { get; private set; }

            public string TimeString;

            public TimeData(float seconds)
            {
                Minutes = Mathf.Floor(seconds / 60);
                Seconds = (seconds % 60);

                TimeString = Minutes.ToString("00") + ":" + Seconds.ToString("00.00");
            }

            public static implicit operator string(TimeData t)
            {
                return t.TimeString;
            }

        }

    }
}

