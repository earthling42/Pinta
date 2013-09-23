// 
// AddinSetupService.cs
//  
// Author:
//       Lluis Sanchez Gual <lluis@novell.com>
// 
// Copyright (c) 2011 Novell, Inc (http://www.novell.com)
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using Mono.Addins;
using Mono.Addins.Setup;
using Pinta.Core;

namespace Pinta
{
	public class AddinSetupService: SetupService
	{
		internal AddinSetupService (AddinRegistry r): base (r)
		{
		}
		
		public bool AreRepositoriesRegistered ()
		{
			string url = GetPlatformRepositoryUrl ();
			return Repositories.ContainsRepository (url);
		}
		
		public void RegisterRepositories (bool enable)
		{
			string url = GetPlatformRepositoryUrl ();
			if (!Repositories.ContainsRepository (url)) {
				var rep = Repositories.RegisterRepository (null, url, false);
				rep.Name = "Pinta Platform Dependent Add-in Repository";
				if (!enable)
					Repositories.SetRepositoryEnabled (url, false);
			}

			url = GetAllRepositoryUrl ();
			if (!Repositories.ContainsRepository (url)) {
				var rep2 = Repositories.RegisterRepository (null, url, false);
				rep2.Name = "Pinta Platform Independent Add-in Repository";
				if (!enable)
					Repositories.SetRepositoryEnabled (url, false);
			}
		}
		
		public string GetPlatformRepositoryUrl ()
		{
			string platform;
			if (SystemManager.GetOperatingSystem () == OS.Windows)
				platform = "Win";
			else if (SystemManager.GetOperatingSystem () == OS.Mac)
				platform = "Mac";
			else
				platform = "Linux";
			
			//TODO: Need to change version number here
			return "http://178.79.177.109:8080/Stable/" + platform + "/" + 1.5 + "/main.mrep";
		}

		public string GetAllRepositoryUrl ()
		{
			//TODO: Need to change version number here
			return "http://178.79.177.109:8080/Stable/All/" + 1.5 + "/main.mrep";
		}
	}
}
