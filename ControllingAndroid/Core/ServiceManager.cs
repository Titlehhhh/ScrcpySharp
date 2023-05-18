using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControllingAndroid.Core
{
	public class ServiceManager
	{
		private static InputManagerWrapper inputManager = new InputManagerWrapper();
		public static InputManagerWrapper getInputManager()
		{

			return inputManager;

		}

	}
}

