/*
Copyright (C) 2017-2018 Ali Deym
This file is part of Bifler <https://github.com/alideym/bifler>.

Bifler is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Bifler is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Bifler.  If not, see <http://www.gnu.org/licenses/>.
*/


using System.Threading.Tasks;

using TriggerServer;


namespace libBifler
{
	public partial class TriggerDevice
	{
		private readonly bool DEBUG = true;

/*#if DEBUG
		private DateTime triggerTime = DateTime.Now;
#endif
*/
		private void HookMethods ()
		{
			networkManager.GetPacket (OpCodes.Trigger).ReadMethod = HandleTrigger;

			networkManager.GetPacket (OpCodes.ProjectRequest).ReadMethod = HandleProjectStatus;

			networkManager.GetPacket (OpCodes.Handshake).ReadMethod = HandleHandshake;

			networkManager.GetPacket (OpCodes.HeartbeatTransmit).ReadMethod = HandleHeartbeat;

			networkManager.GetPacket (OpCodes.CaliberTransmit).ReadMethod = HandleCaliber;

			networkManager.GetPacket (OpCodes.PendingRequest).ReadMethod = HandlePendingReceive;
		}


		/// <summary>
		/// Handles pending received (Start Button press of radio).
		/// </summary>
		/// <param name="magicBoxed">Magic number Byte, Boxed.</param>
		private void HandlePendingReceive (object magicBoxed)
		{
			OnPendingProjectReceived?.Invoke (this, new System.EventArgs ());
		}


		/// <summary>
		/// Handles trigger packets.
		/// </summary>
		/// <param name="trigCountBoxed">Boxed Int(32).</param>
		private void HandleTrigger (object trigCountBoxed)
		{
/*#if DEBUG
			var diffTime = (int)((DateTime.Now - triggerTime).TotalMilliseconds);

			if (diffTime < 20 || diffTime > 42)
				Console.WriteLine ("NET: " + diffTime + "ms");

			triggerTime = DateTime.Now;
#endif*/
			TriggerCount = (int)trigCountBoxed;
			Triggered++;

			Task.Factory.StartNew (() => { OnDeviceTrigger?.Invoke (TriggerCount, Triggered); });

			//log.Info ("Triggered: " + Triggered);
		}


		/// <summary>
		/// Handles project status packets.
		/// </summary>
		/// <param name="boxedBoolean">Boxed Boolean.</param>
		private void HandleProjectStatus (object boxedBoolean)
		{
			
			var projectStatus = (bool)boxedBoolean;

			if (!projectStatus) {
				TriggerCount = -1;
				Triggered = -1;
			} else {
				TriggerCount = 0;
				Triggered = 0;
			}

			log.Info ("Changed project status: " + projectStatus);
		}


		/// <summary>
		/// Handles handshake packets.
		/// </summary>
		/// <param name="boxedVersion">Boxed Version(struct).</param>
		private void HandleHandshake (object boxedVersion)
		{
			var version = (TriggerServer.Version)boxedVersion;

			log.Info ("Handshake with Trigger Device v" + version.X + "." + version.Y + "." + version.Z);
		}


		/// <summary>
		/// Handles caliber packets.
		/// </summary>
		/// <param name="boxedCaliber">Boxed Int(32).</param>
		private void HandleCaliber (object boxedCaliber)
		{
			var caliber = (int)boxedCaliber;

			OnCaliberStatus?.Invoke (caliber);

			log.Info ("Received Calibration: " + caliber + ".");
		}

		
		/// <summary>
		/// Handles heartbeat packets.
		/// </summary>
		/// <param name="boxedMagicNumber">Boxed Byte.</param>
		private void HandleHeartbeat (object boxedMagicNumber)
		{
			var magicNumber = (byte)boxedMagicNumber;
			
			if (TriggerCount <= 0 && Triggered <= 0) {
				TriggerCount = 0;
				Triggered = 0;
			}

			OnHeartbeatReceived?.Invoke ();

			log.Info ("Received heartbeat: " + boxedMagicNumber + ".");
		}
	}
}
