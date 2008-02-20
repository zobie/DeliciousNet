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
	public class Tag
	{
		private string _Name = String.Empty;
		private int _Count = 0;


		/// <summary>
		/// Construct a new <c>Tag</c>
		/// </summary>
		public Tag ()
		{
		}


		/// <summary>
		/// Construct a new <c>Tag</c>
		/// </summary>
		/// <param name="name">Name of the <c>Tag</c></param>
		/// <param name="count">Number of links for the <c>Tag</c></param>
		public Tag (string name, int count)
		{
			this.Name = name;
			this.Count = count;
		}


		/// <summary>
		/// Get/Set the number of links for the <c>Tag</c>
		/// </summary>
		public int Count
		{
			get { return this._Count; }
			set { this._Count = value; }
		}


		/// <summary>
		/// Get/Set the name of the <c>Tag</c>
		/// </summary>
		public string Name
		{
			get { return this._Name; }
			set { this._Name = value; }
		}


		/// <summary>
		/// Returns a string that represents the <c>Tag</c>
		/// </summary>
		public override string ToString ()
		{
			return this.Name + " : " + this.Count;
		}


		public override bool Equals (object obj)
		{
			if (!(obj is Tag))
				return false;

			return this == (Tag)obj;
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


		public static bool operator == (Tag tag1, Tag tag2)
		{
			if ((object)tag1 == null)
				return ((object)tag2 == null);
			else if ((object)tag2 == null)
				return false;

			return (tag1.Name == tag2.Name &&
			        tag1.Count == tag2.Count);
		}


		public static bool operator != (Tag tag1, Tag tag2)
		{
			return !(tag1 == tag2);
		}


		/// <summary>
		/// Gets an ArrayList of <c>Tag</c> objects
		/// </summary>
		/// <returns>ArrayList of <c>Tag</c> objects</returns>
		public static List<Tag> Get ()
		{
			XmlDocument xmlDoc = Connection.Connect (Constants.RelativeUrl.TagsGet);
			XmlNodeList nodeList = xmlDoc.DocumentElement.GetElementsByTagName (Constants.XmlTag.Tag);
			List<Tag> tags = new List<Tag> (nodeList.Count);

			foreach (XmlNode node in nodeList)
			{
				string name = node.Attributes[ Constants.XmlAttribute.Tag ].Value.ToString();
				int count = int.Parse (node.Attributes[ Constants.XmlAttribute.Count ].Value.ToString());
				Tag tag = new Tag (name, count);
				tags.Add (tag);
			}

			return tags;
		}


		/// <summary>
		/// Rename a given tag
		/// </summary>
		/// <param name="oldName">Name of the tag that is to be renamed</param>
		/// <param name="newName">Requested new name for the tag</param>
		/// <returns>true if the tag was renamed, false otherwise</returns>
		public static bool Rename (string oldName, string newName)
		{
			if (oldName == null || oldName.Length == 0 ||
			    newName == null || newName.Length == 0)
			{
				return false;
			}

			string relativeUrl = Constants.RelativeUrl.TagsRename;
			relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.Old, oldName);
			relativeUrl = Utilities.AddParameter (relativeUrl, Constants.UrlParameter.New, newName);
			XmlDocument xmlDoc = Connection.Connect (relativeUrl);
			XmlNodeList nodeList = xmlDoc.DocumentElement.GetElementsByTagName (Constants.XmlTag.Result);
			if (nodeList.Count == 1)
			{
				string done = nodeList[ 0 ].Value;
				return (done == Constants.ReturnCode.Done);
			}

			return false;
		}


		#endregion Static Methods
	}
}