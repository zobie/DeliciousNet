#region Copyright (c) 2006-2008, Nate Zobrist
/*
 * Copyright (c) 2006-2008, Nate Zobrist
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 *
 * Redistributions of source code must retain the above copyright
 *     notice, this list of conditions and the following disclaimer.
 * Redistributions in binary form must reproduce the above
 *     copyright notice, this list of conditions and the following
 *     disclaimer in the documentation and/or other materials provided with
 *     the distribution.
 * Neither the name of "Nate Zobrist" nor the names of its
 *     contributors may be used to endorse or promote products derived from
 *     this software without specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
 * "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
 * LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
 * A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
 * OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 * SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
 * LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
 * THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
 * OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion Copyright (c) 2006-2008, Nate Zobrist

using System;
using System.Collections.Generic;
using System.Xml;

namespace Delicious
{
	public class Bundle
	{
		private string _Name;
		private string _Tags;


		public Bundle ()
		{
		}


		public Bundle (string name, string tags)
		{
			this.Name = name;
			this.Tags = tags;
		}


		public string Name
		{
			get { return this._Name; }
			set { this._Name = value; }
		}

		public string Tags
		{
			get { return this._Tags; }
			set { this._Tags = value; }
		}


		public string[] GetTagsAsArray ()
		{
			if (this.Tags == null || this.Tags.Length == 0)
				return null;
			return this.Tags.Split (' ');
		}


		public override string ToString ()
		{
			return this.Name + " : " + this.Tags;
		}


		public override bool Equals (object obj)
		{
			if (!(obj is Bundle))
				return false;

			return (this == (Bundle)obj);
		}


		public override int GetHashCode ()
		{
			return this.Name.GetHashCode();
		}


		/// <summary>
		/// Determines whether the specified object instances are the same instance
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public bool ReferenceEquals (object obj)
		{
			return Object.ReferenceEquals (this, obj);
		}



		#region Static Methods


		public static bool operator == (Bundle bundle1, Bundle bundle2)
		{
			if ((object)bundle1 == null)
				return ((object)bundle2 == null);
			else if ((object)bundle2 == null)
				return false;

			return (bundle1.Name == bundle2.Name &&
			        bundle1.Tags == bundle2.Tags);
		}


		public static bool operator != (Bundle bundle1, Bundle bundle2)
		{
			return !(bundle1 == bundle2);
		}



		/// <summary>
		/// Add a new Bundle of Tags.  If either <c>bundleName</c> or <c>tags</c> is null or String.Empty,
		/// we immediately return false.
		/// </summary>
		/// <param name="bundleName">Name of the bundle</param>
		/// <param name="tags">Space-seperated list of tags</param>
		/// <returns><c>true</c> if the Bundle was created, <c>false</c> otherwise</returns>
		public static bool Add (string bundleName, string tags)
		{
			if (bundleName == null || bundleName.Length == 0 ||
				tags == null || tags.Length == 0)
			{
				return false;
			}

			string relativeUrl = Constants.RelativeUrl.BundlesSet;
			relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Bundle, bundleName);
			relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Tags, tags);

			XmlDocument xmlDoc = Connection.Connect (relativeUrl);
			string resultCode = Utilities.ParseForResultCode (xmlDoc.DocumentElement);
			return (resultCode == Constants.ReturnCode.Ok);
		}


		/// <summary>
		/// Delete the specified Bundle.  If the <c>bundleName</c> is null or String.Empty,
		/// we immediately return false.
		/// </summary>
		/// <param name="bundleName">Name of the bundle to delete</param>
		/// <returns><c>true</c> if the Bundle was deleted, <c>false</c> otherwise</returns>
		public static bool Delete (string bundleName)
		{
			if (bundleName == null || bundleName.Length == 0)
				return false;

			string relativeUrl = Constants.RelativeUrl.BundlesDelete;
			relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Bundle, bundleName);
			XmlDocument xmlDoc = Connection.Connect (relativeUrl);
			string resultCode = Utilities.ParseForResultCode (xmlDoc.DocumentElement);
			return (resultCode == Constants.ReturnCode.Ok);
		}


		/// <summary>
		/// Return a list of <c>Bundle</c> objects
		/// </summary>
		/// <returns>List of <c>Bundle</c> objects</returns>
		public static List<Bundle> Get ()
		{
			XmlDocument xmlDoc = Connection.Connect (Constants.RelativeUrl.BundlesAll);
			XmlNodeList nodeList = xmlDoc.DocumentElement.GetElementsByTagName (Constants.XmlTag.Bundle);
			List<Bundle> bundles = new List<Bundle> (nodeList.Count);

			foreach (XmlNode node in nodeList)
			{
				string name = node.Attributes[ Constants.XmlAttribute.Name ].Value;
				string tags = node.Attributes[ Constants.XmlAttribute.Tags ].Value;

				Bundle bundle = new Bundle (name, tags);
				bundles.Add (bundle);
			}

			return bundles;
		}


		#endregion Static Methods
	}
}