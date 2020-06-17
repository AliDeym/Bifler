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


using System.IO;
using System.Collections.Generic;


namespace libBifler
{
	/// <summary>
	/// Handles config save and load.
	/// </summary>
	public class ConfigManager
	{
		private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger ();

		private Dictionary<string, string> configs = null;
		private string _file;

		/// <summary>
		/// Config file name. Must be set without format.
		/// </summary>
		public string FileName {
			get {
				return _file;
			}
			set {
				_file = value + ".conf";
			}
		}


		/// <summary>
		/// Creates a config manager.
		/// </summary>
		public ConfigManager ()
		{
			configs = new Dictionary<string, string> ();
		}


		/// <summary>
		/// Creates a config manager.
		/// </summary>
		/// <param name="file">File name. (Must have no format).</param>
		public ConfigManager (string file)
		{
			configs = new Dictionary<string, string> ();

			FileName = file;

			Reload ();
		}


		/// <summary>
		/// Re-loads Config from the file directly.
		/// </summary>
		public void Reload ()
		{
			if (!File.Exists (FileName))
				return;

			using (var sr = new StreamReader (FileName)) {

				var copyLine = sr.ReadLine ();

				if (copyLine != "[BiflerConf by AliDeym] © (2017-2018)") return;

				string readBuffer = null;

				while ((readBuffer = sr.ReadLine()) != null) {

					if (readBuffer.Contains("=")) {

						var splittedLine = readBuffer.Split ('=');

						configs [splittedLine [0].TrimEnd ()] = splittedLine [1].TrimStart ();

					}

				}

			}
		}


		/// <summary>
		/// Saves config into the file.
		/// </summary>
		private void Save ()
		{
			using (var sw = new StreamWriter (FileName)) {

				sw.WriteLine ("[BiflerConf by AliDeym] © (2017-2018)");
				sw.WriteLine ();

				foreach (var keyPair in configs) {

					sw.WriteLine (keyPair.Key + " = " + keyPair.Value);

				}

			}
		}


		/// <summary>
		/// Sets a config key pair.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public void SetConfig (string key, string value)
		{
			configs [key] = value;

			Save ();
		}


		/// <summary>
		/// Gets a config using key.
		/// </summary>
		/// <param name="key">Key</param>
		/// <returns>Value or null.</returns>
		public string GetConfig (string key) => (configs.ContainsKey(key) ? configs [key] : null);
	}
}
