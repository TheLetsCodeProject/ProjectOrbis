using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Orbis {

    namespace Timer{

        public class Timer
        {

            float StartTime;
            float EndTime;
            float TotalTime;

            /// <summary>
            /// Starts timer.
            /// </summary>
            public void Start()
            {
                StartTime = Time.time;
            }

            /// <summary>
            /// Stops timer and returns total time in seconds;
            /// </summary>
            /// <returns>Time in seconds</returns>
            public float Stop()
            {
                EndTime = Time.time;
                TotalTime = EndTime - StartTime;
                return TotalTime;
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
            public string FormatTime(float time)
            {
                string minutes = Mathf.Floor(time / 60).ToString("00");
                string seconds = (time % 60).ToString("00.00");
                return minutes + ":" + seconds;
            }

            /// <summary>
            /// Gets the timer's last time, formatted by the FormatTime method
            /// </summary>
            /// <returns></returns>
            public string GetFormattedTime()
            {
                return FormatTime(TotalTime);
            }
        }

    }
}

