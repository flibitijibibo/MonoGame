#region License
/* FNA - XNA4 Reimplementation for Desktop Platforms
 * Copyright 2009-2014 Ethan Lee and the MonoGame Team
 *
 * Released under the Microsoft Public License.
 * See LICENSE for details.
 */
#endregion

#region Using clause
using System;
#endregion Using clause

namespace Microsoft.Xna.Framework.Input.Touch
{
    /// <summary>
    /// Allows retrieval of capabilities information from touch panel device.
    /// </summary>
    public struct TouchPanelCapabilities
    {
		private bool hasPressure;
		private bool isConnected;
		private int maximumTouchCount;
        private bool initialized;

        internal void Initialize()
        {
            if (!initialized)
            {
                initialized = true;

                // There does not appear to be a way of finding out if a touch device supports pressure.
                // XNA does not expose a pressure value, so let's assume it doesn't support it.
                hasPressure = false;

#if WINDOWS_STOREAPP
                // Is a touch device present?
                var caps = new Windows.Devices.Input.TouchCapabilities();
                isConnected = caps.TouchPresent != 0;

                // Iterate through all pointer devices and find the maximum number of concurrent touches possible
                maximumTouchCount = 0;
                var pointerDevices = Windows.Devices.Input.PointerDevice.GetPointerDevices();
                foreach (var pointerDevice in pointerDevices)
                    maximumTouchCount = Math.Max(maximumTouchCount, (int)pointerDevice.MaxContacts);
#else
		        isConnected = true;
		        maximumTouchCount = 8;
#endif
            }
		}

        public bool HasPressure
        {
            get
            {
                return hasPressure;
            }
        }

        /// <summary>
        /// Returns true if a device is available for use.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                return isConnected;
            }
        }

        /// <summary>
        /// Returns the maximum number of touch locations tracked by the touch panel device.
        /// </summary>
        public int MaximumTouchCount
        {
            get
            {
                return maximumTouchCount;
            }
        }
    }
}